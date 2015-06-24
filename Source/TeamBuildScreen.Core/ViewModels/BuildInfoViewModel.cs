//-----------------------------------------------------------------------
// <copyright file="BuildDetailViewModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.ViewModels
{
	#region Usings

	using System;
	using System.ComponentModel;
	using TeamBuildScreen.Core.Models;

	#endregion

	public class BuildInfoViewModel : INotifyPropertyChanged
	{
		#region Properties

		/// <summary>
		/// The model underlying the <see cref="BuildInfoViewModel"/>.
		/// </summary>
		public BuildInfoModel DataModel { get; set; }

		/// <summary>
		/// Gets the status for the <see cref="BuildInfoViewModel"/>.
		/// </summary>
		public BuildStatus Status { get; private set; }

		/// <summary>
		/// Gets the description for the <see cref="BuildInfoViewModel"/>.
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// Gets the name of the person who requested the <see cref="BuildInfoViewModel"/>.
		/// </summary>
		public string RequestedBy { get; private set; }

		/// <summary>
		/// Gets the time when the build was started.
		/// </summary>
		public DateTime? StartedOn { get; private set; }

		/// <summary>
		/// Gets the formatted string for status.
		/// </summary>
		public string LatestStatus { get; private set; }

		/// <summary>
		/// Gets the time the build was completed.
		/// </summary>
		public DateTime? CompletedOn { get; private set; }

		/// <summary>
		/// Gets the test results for the <see cref="BuildInfoViewModel"/>.
		/// </summary>
		public string TestResults { get; private set; }

		/// <summary>
		/// Gets the bottom line for the <see cref="BuildInfoViewModel"/>.
		/// </summary>
		public string BottomLine { get; private set; }

		/// <summary>
		/// Gets the test results for the <see cref="BuildInfoViewModel"/>.
		/// </summary>
		public string FailedTests { get; private set; }

		/// <summary>
		/// Gets a value that indicates whether the build has any builds queued.
		/// </summary>
		public bool IsQueued
		{
			get
			{
				return this.DataModel.IsQueued;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the current build was completed more than a week ago.
		/// </summary>
		public bool IsStale
		{
			get
			{
				return this.DataModel.IsStale;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the build completed with warnings.
		/// </summary>
		public bool HasWarnings
		{
			get; set;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="BuildInfoViewModel"/> class.
		/// </summary>
		/// <param name="buildInfoModel">The model underlying the <see cref="BuildInfoViewModel"/>.</param>
		public BuildInfoViewModel(BuildInfoModel buildInfoModel)
		{
			this.Init(buildInfoModel);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Initializes the <see cref="BuildInfoViewModel"/> with the specified <see cref="BuildInfoModel"/>.
		/// </summary>
		/// <param name="buildInfoModel">The model that is encapsulated by the <see cref="BuildInfoViewModel"/>.</param>
		private void Init(BuildInfoModel buildInfoModel)
		{
			this.DataModel = buildInfoModel;
			this.DataModel.PropertyChanged += (sender, e) => this.UpdateFromModel();

			this.Description = this.DataModel.Description;
			this.Status = BuildStatus.Loading;
			this.LatestStatus = BuildStatus.Loading.ToFriendlyString();

			this.HasWarnings = false;
		}

		/// <summary>
		/// Updates the view model from the model.
		/// </summary>
		private void UpdateFromModel()
		{
			bool hasWarnings = false;

			this.Description = this.DataModel.Description;
			this.Status = BuildStatus.NoneFound;
			this.RequestedBy = null;
			this.StartedOn = null;
			this.CompletedOn = null;
			this.TestResults = null;
			this.FailedTests = null;
			this.LatestStatus = BuildStatus.NoneFound.ToFriendlyString();

			if (this.DataModel.Model != null)
			{
				this.Status = this.DataModel.Model.Status;

				this.RequestedBy = this.DataModel.Model.RequestedFor;

				// remove domain prefix from requestedBy, if found
				if (!string.IsNullOrEmpty(this.RequestedBy))
				{
					this.RequestedBy = this.RequestedBy.Contains("\\")
										   ? this.RequestedBy.Split('\\')[1]
										   : this.RequestedBy;
				}
				this.StartedOn = this.DataModel.Model.StartTime;
				if (DataModel.Model.Status == BuildStatus.InProgress)
				{
					UpdateInProgress();
				}
				else
				{
					this.LatestStatus = this.DataModel.Model.Status.ToFriendlyString();
				}

				if (this.DataModel.Model.BuildFinished)
				{
					UpdateFinished();

					hasWarnings = this.DataModel.Model.HasWarnings;
				}
			}

			this.HasWarnings = hasWarnings;

			this.OnPropertyChanged("IsQueued");
			this.OnPropertyChanged("IsStale");
			this.OnPropertyChanged("Status");
			this.OnPropertyChanged("Description");
			this.OnPropertyChanged("RequestedBy");
			this.OnPropertyChanged("StartedOn");
			this.OnPropertyChanged("CompletedOn");
			this.OnPropertyChanged("TestResults");
			this.OnPropertyChanged("FailedTests");
			this.OnPropertyChanged("HasWarnings");
			this.OnPropertyChanged("LatestStatus");
			this.OnPropertyChanged("BottomLine");
		}

		private void UpdateFinished()
		{
			TimeSpan buildTime = (TimeSpan) (this.DataModel.Model.FinishTime - this.DataModel.Model.StartTime);
			this.LatestStatus = String.Format("{0} {1:d} {1:t} ({2} min)", this.DataModel.Model.Status.ToFriendlyString(),
				this.DataModel.Model.FinishTime, Math.Round(buildTime.TotalMinutes));
			this.CompletedOn = this.DataModel.Model.FinishTime;

			if (this.DataModel.Model.TestsTotal.HasValue && this.DataModel.Model.TestsTotal > 0)
			{
				this.TestResults = string.Format("Test results: {0} passed, {1} failed, {2} total.",
					this.DataModel.Model.TestsPassed, this.DataModel.Model.TestsFailed,
					this.DataModel.Model.TestsTotal);
				if (DataModel.Model.FailedTests.Count > 0)
				{
					this.BottomLine = "Failed tests: ";
					foreach (string failedTestName in this.DataModel.Model.FailedTests)
					{
						this.BottomLine += failedTestName + " ";
					}
				}
				else
				{
					this.BottomLine = string.Empty;
				}
				if (this.DataModel.Model.TestsFailed > 0)
				{
					this.FailedTests = this.DataModel.Model.TestsFailed.ToString();
				}
			}
			else
			{
				this.TestResults = string.Empty;
				if (this.Status == BuildStatus.PartiallySucceeded)
				{
					this.FailedTests = "?";
				}
			}

			if (this.DataModel.Model.CodeCoverage.HasValue)
			{
				this.TestResults += string.Format(" Code Coverage: {0}%", this.DataModel.Model.CodeCoverage);
			}
		}

		private void UpdateInProgress()
		{
			TimeSpan buildTime = (TimeSpan) (DateTime.Now - this.DataModel.Model.StartTime);
			this.LatestStatus = String.Format("{0} {1:d} {1:t} ({2} min)", this.DataModel.Model.Status.ToFriendlyString(),
				this.DataModel.Model.StartTime, Math.Round(buildTime.TotalMinutes));
			switch (DataModel.Model.CompilationStatus)
			{
				case BuildPhaseStatus.Failed:
					this.TestResults = String.Format("Compilation Failed");
					break;
				case BuildPhaseStatus.Succeeded:
					this.TestResults = String.Format("Compilation Succeeded");
					break;
				case BuildPhaseStatus.Unknown:
					break;
			}
			switch (DataModel.Model.TestStatus)
			{
				case BuildPhaseStatus.Failed:
					this.TestResults += String.Format(" Test Failed");
					break;
				case BuildPhaseStatus.Succeeded:
					this.TestResults += String.Format(" Test Succeeded");
					break;
				case BuildPhaseStatus.Unknown:
					break;
			}
			this.BottomLine = string.Empty;
		}

		/// <summary>
		/// Raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
		/// </summary>
		/// <param name="propertyName">The name of the property that changed.</param>
		private void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public static BuildInfoViewModel FromString(string s, IBuildServerService service)
		{
			return new BuildInfoViewModel(BuildInfoModel.FromString(s, service));
		}

		#endregion

		#region INotifyPropertyChanged Members

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}