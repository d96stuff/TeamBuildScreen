//-----------------------------------------------------------------------
// <copyright file="BuildServerServiceMessageViewModelTests.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaverTests
{
    #region Usings

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TeamBuildScreenSaver.ViewModels;
    using TeamBuildScreenSaverTests.Models;

    #endregion

    [TestClass]
    public class BuildServerServiceMessageViewModelTests
    {
        #region Fields

        private BuildServerServiceMessageViewModel viewModel;
        MockBuildServerService mockService;

        #endregion

        #region Methods

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockService = new MockBuildServerService();
            this.viewModel = new BuildServerServiceMessageViewModel(this.mockService);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockService.Dispose();
            this.mockService = null;
        }

        [TestMethod]
        public void IsVisibleIsFalseByDefault()
        {
            Assert.IsFalse(this.viewModel.IsVisible);
        }

        [TestMethod]
        public void IsVisibleIsTrueAfterError()
        {
            this.mockService.RaiseError();

            Assert.IsTrue(this.viewModel.IsVisible);
        }

        [TestMethod]
        public void IsVisibleIsResetAfterQueryCompleted()
        {
            this.mockService.RaiseError();

            Assert.IsTrue(this.viewModel.IsVisible);

            this.mockService.RaiseQueryCompleted();

            Assert.IsFalse(this.viewModel.IsVisible);
        }

        #endregion
    }
}