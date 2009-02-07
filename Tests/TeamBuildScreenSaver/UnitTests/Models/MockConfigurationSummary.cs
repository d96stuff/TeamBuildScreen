﻿//-----------------------------------------------------------------------
// <copyright file="MockConfigurationSummary.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.UnitTests.Models
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using Microsoft.TeamFoundation.Build.Client;

    #endregion

    public class MockConfigurationSummary : IConfigurationSummary
    {
        #region Fields

        private List<ITestSummary> summaries;

        #endregion

        #region Constructors

        public MockConfigurationSummary()
        {
            this.summaries = new List<ITestSummary>();
        }

        #endregion

        #region IConfigurationSummary Members

        public ICodeCoverageSummary AddCodeCoverageSummary()
        {
            throw new NotImplementedException();
        }

        public ICompilationSummary AddCompilationSummary()
        {
            throw new NotImplementedException();
        }

        public ITestSummary AddTestSummary()
        {
            throw new NotImplementedException();
        }

        public void AddTestSummary(ITestSummary summary)
        {
            this.summaries.Add(summary);
        }

        public List<ICodeCoverageSummary> CodeCoverageSummaries
        {
            get { throw new NotImplementedException(); }
        }

        public List<ICompilationSummary> CompilationSummaries
        {
            get { throw new NotImplementedException(); }
        }

        public string Flavor
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

        public string FullName
        {
            get { throw new NotImplementedException(); }
        }

        public int Id
        {
            get { throw new NotImplementedException(); }
        }

        public string LogFile
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

        public string Platform
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

        public List<ITestSummary> TestSummaries
        {
            get
            {
                return summaries;
            }
        }

        public int TotalCompilationErrors
        {
            get { throw new NotImplementedException(); }
        }

        public int TotalCompilationWarnings
        {
            get { throw new NotImplementedException(); }
        }

        public int TotalStaticAnalysisErrors
        {
            get { throw new NotImplementedException(); }
        }

        public int TotalStaticAnalysisWarnings
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
