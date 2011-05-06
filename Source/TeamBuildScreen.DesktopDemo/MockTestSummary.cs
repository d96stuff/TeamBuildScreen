//-----------------------------------------------------------------------
// <copyright file="MockTestSummary.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaverDemo
{
    #region Usings

    using System;
    using Microsoft.TeamFoundation.Build.Client;

    #endregion

    public class MockTestSummary : ITestSummary
    {
        #region Fields

        private int passed;
        private int failed;

        #endregion

        #region Constructors

        public MockTestSummary(int passed, int failed)
        {
            this.passed = passed;
            this.failed = failed;
        }

        #endregion

        #region ITestSummary Members

        public int Id
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string RunId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool RunPassed
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string RunUser
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public int TestsFailed
        {
            get
            {
                return this.failed;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int TestsPassed
        {
            get
            {
                return this.passed;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int TestsTotal
        {
            get
            {
                return this.passed + this.failed;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}