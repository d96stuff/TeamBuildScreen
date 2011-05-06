//-----------------------------------------------------------------------
// <copyright file="BuildInfoViewModelTests.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.UnitTests
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TeamBuildScreen.Core.Models;
    using TeamBuildScreen.Demo;
    using TeamBuildScreen.Core.ViewModels;
    using TeamBuildScreen.Core.Views;

    #endregion

    [TestClass]
    public class BuildInfoViewModelTests
    {
        #region Fields

        private BuildInfoModel model;
        private MockBuildServerService mockService;
        private MockBuildInfo mockBuildInfo;
        private string key;
        private BuildInfoViewModel viewModel;

        #endregion

        #region Methods

        [TestInitialize]
        public void TestInitialize()
        {
            string teamProject = "Team Project";
            string definitionName = "Definition Name";
            string configuration = "Configuration";
            string platform = "Platform";
            this.key = string.Format("{0};{1}", teamProject, definitionName);
            this.mockService = new MockBuildServerService();
            this.mockBuildInfo = new MockBuildInfo(BuildStatus.Stopped, "Requested For", DateTime.MinValue, true, DateTime.MinValue);
            this.model = new BuildInfoModel(teamProject, definitionName, configuration, platform, this.mockService);

            this.viewModel = new BuildInfoViewModel(this.model);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockService.Dispose();
            this.mockService = null;
            this.mockBuildInfo.TestsPassed = null;
            this.mockBuildInfo.TestsFailed = null;
        }

        [TestMethod]
        public void StatusReflectsBuildDetail()
        {
            this.mockService.SetBuildInfo(this.key, this.mockBuildInfo);

            this.mockService.RaiseQueryCompleted();

            Assert.AreEqual<BuildStatus?>(this.mockBuildInfo.Status, this.viewModel.Status);
        }

        //[TestMethod]
        //public void SummaryWithTestResultsCorrectlyPopulated()
        //{
        //    // arrange
        //    this.mockBuildInfo.TestsPassed = 100;
        //    this.mockBuildInfo.TestsFailed = 50;
        //    StringBuilder summary = this.GetSummaryPrefix();
        //    summary.AppendLine(string.Format("Test results: {0} passed, {1} failed, {2} total.", this.mockBuildInfo.TestsPassed, this.mockBuildInfo.TestsFailed, this.mockBuildInfo.TestsTotal));
        //    this.mockService.SetBuildInfo(this.key, this.mockBuildInfo);

        //    // act
        //    this.mockService.RaiseQueryCompleted();

        //    // assert
        //    Assert.AreEqual<string>(summary.ToString().Trim(), this.viewModel.Summary.Trim());
        //}

        //[TestMethod]
        //public void SummaryWithoutTestResultsCorrectlyPopulated()
        //{
        //    StringBuilder summary = this.GetSummaryPrefix();
        //    summary.AppendLine("No test result.");

        //    this.mockService.SetBuildInfo(this.key, this.mockBuildInfo);

        //    this.mockService.RaiseQueryCompleted();

        //    Assert.AreEqual<string>(summary.ToString().Trim(), this.viewModel.Summary.Trim());
        //}

        [TestMethod]
        public void IsQueuedPropertyChangedWhenServiceQueryCompleted()
        {
            this.AssertPropertyChangedWhenServiceQueryCompleted("IsQueued");
        }

        [TestMethod]
        public void StatusPropertyChangedWhenServiceQueryCompleted()
        {
            this.AssertPropertyChangedWhenServiceQueryCompleted("Status");
        }

        private StringBuilder GetSummaryPrefix()
        {
            StringBuilder summary = new StringBuilder();

            summary.AppendLine(string.Format(
                "{0}: {1}",
                "Team Project",
                "Definition Name"));
            summary.AppendLine(new BuildStatusStringConverter().Convert(this.mockBuildInfo.Status, typeof(string), null, null).ToString());
            summary.AppendLine("Requested by " + this.mockBuildInfo.RequestedFor);
            summary.AppendLine("Started on " + this.mockBuildInfo.StartTime);
            summary.AppendLine("Completed on " + this.mockBuildInfo.FinishTime);

            return summary;
        }

        private void AssertPropertyChangedWhenServiceQueryCompleted(string propertyName)
        {
            string expectedPropertyName = propertyName;

            this.mockService.SetBuildInfo(this.key, this.mockBuildInfo);

            bool propertyChangedRaised = false;

            this.viewModel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == expectedPropertyName)
                {
                    propertyChangedRaised = true;
                }
            };

            this.mockService.RaiseQueryCompleted();

            Assert.IsTrue(propertyChangedRaised);
        }

        #endregion
    }
}