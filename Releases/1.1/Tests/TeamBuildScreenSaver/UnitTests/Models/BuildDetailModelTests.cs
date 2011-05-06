//-----------------------------------------------------------------------
// <copyright file="BuildDetailModelTests.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.UnitTests
{
    #region Usings

    using System;
    using System.ComponentModel;
    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TeamBuildScreenSaver.Models;
    using TeamBuildScreenSaver.UnitTests.Models;

    #endregion

    [TestClass]
    public class BuildDetailModelTests
    {
        #region Fields

        private BuildDetailModel model;
        private MockBuildServerService mockService;
        private IBuildDetail mockBuildDetail;
        private IBuildDefinition mockBuildDefinition;
        private string key;

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
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockService.Dispose();
            this.mockService = null;
        }

        [TestMethod]
        public void ModelPropertyChangedWhenServiceQueryCompleted()
        {
            this.AssertPropertyChangedWhenServiceQueryCompleted("Model");
        }

        [TestMethod]
        public void IsQueuedPropertyChangedWhenServiceQueryCompleted()
        {
            this.AssertPropertyChangedWhenServiceQueryCompleted("IsQueued");
        }

        private void AssertPropertyChangedWhenServiceQueryCompleted(string propertyName)
        {
            string expectedPropertyName = propertyName;

            this.mockService.SetBuildDetail(this.key, this.mockBuildDetail);

            bool propertyChangedRaised = false;

            this.model.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
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