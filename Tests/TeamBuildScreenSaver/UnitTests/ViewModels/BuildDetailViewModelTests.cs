//-----------------------------------------------------------------------
// <copyright file="BuildDetailViewModelTests.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.UnitTests
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Text;
    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TeamBuildScreenSaver.Models;
    using TeamBuildScreenSaver.UnitTests.Models;
    using TeamBuildScreenSaver.ViewModels;
    using TeamBuildScreenSaver.Views;

    #endregion

    [TestClass]
    public class BuildDetailViewModelTests
    {
        #region Fields

        private BuildDetailModel model;
        private MockBuildServerService mockService;
        private IBuildDetail mockBuildDetail;
        private IBuildDefinition mockBuildDefinition;
        private string key;
        private BuildDetailViewModel viewModel;
        private int? testsPassed = null;
        private int? testsFailed = null;

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
            this.mockBuildDefinition = new MockBuildDefinition(teamProject, definitionName);
            this.mockBuildDetail = new MockBuildDetail(BuildStatus.Stopped, "Requested For", DateTime.MinValue, true, DateTime.MinValue, mockBuildDefinition);
            this.model = new BuildDetailModel(teamProject, definitionName, configuration, platform, this.mockService);

            ConfigurationSummaryHandler handler = new ConfigurationSummaryHandler(this.MockConfigurationSummaryHandler);

            this.viewModel = new BuildDetailViewModel(this.model, handler);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockService.Dispose();
            this.mockService = null;
            this.testsPassed = null;
            this.testsFailed = null;
        }

        [TestMethod]
        public void StatusReflectsBuildDetail()
        {
            this.mockService.SetBuildDetail(this.key, this.mockBuildDetail);

            this.mockService.RaiseQueryCompleted();

            Assert.AreEqual<BuildStatus?>(this.mockBuildDetail.Status, this.viewModel.Status);
        }

        [TestMethod]
        public void SummaryWithTestResultsCorrectlyPopulated()
        {
            this.testsPassed = 100;
            this.testsFailed = 50;

            StringBuilder summary = this.GetSummaryPrefix();
            summary.AppendLine(string.Format("Test results: {0} passed, {1} failed, {2} total.", this.testsPassed, this.testsFailed, this.testsPassed + this.testsFailed));

            this.mockService.SetBuildDetail(this.key, this.mockBuildDetail);

            this.mockService.RaiseQueryCompleted();

            Assert.AreEqual<string>(summary.ToString().Trim(), this.viewModel.Summary.Trim());
        }

        [TestMethod]
        public void SummaryWithoutTestResultsCorrectlyPopulated()
        {
            StringBuilder summary = this.GetSummaryPrefix();
            summary.AppendLine("No test result.");

            this.mockService.SetBuildDetail(this.key, this.mockBuildDetail);

            this.mockService.RaiseQueryCompleted();

            Assert.AreEqual<string>(summary.ToString().Trim(), this.viewModel.Summary.Trim());
        }

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

        [TestMethod]
        public void SummaryPropertyChangedWhenServiceQueryCompleted()
        {
            this.AssertPropertyChangedWhenServiceQueryCompleted("Summary");
        }

        private StringBuilder GetSummaryPrefix()
        {
            StringBuilder summary = new StringBuilder();

            summary.AppendLine(string.Format(
                "{0}: {1}",
                this.mockBuildDetail.BuildDefinition.TeamProject,
                this.mockBuildDetail.BuildDefinition.Name));
            summary.AppendLine(new BuildStatusStringConverter().Convert(this.mockBuildDetail.Status, typeof(string), null, null).ToString());
            summary.AppendLine("Requested by " + this.mockBuildDetail.RequestedFor);
            summary.AppendLine("Started on " + this.mockBuildDetail.StartTime);
            summary.AppendLine("Completed on " + this.mockBuildDetail.FinishTime);

            return summary;
        }

        private void AssertPropertyChangedWhenServiceQueryCompleted(string propertyName)
        {
            string expectedPropertyName = propertyName;

            this.mockService.SetBuildDetail(this.key, this.mockBuildDetail);

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

        private IConfigurationSummary MockConfigurationSummaryHandler(IBuildDetail build, string flavour, string platform)
        {
            MockConfigurationSummary mockConfigurationSummary = new MockConfigurationSummary();

            if (this.testsPassed.HasValue)
            {
                ITestSummary testSummary =
                    new MockTestSummary(this.testsPassed.Value, this.testsFailed.Value);

                mockConfigurationSummary.AddTestSummary(testSummary);
            }

            return mockConfigurationSummary;
        }

        #endregion
    }
}