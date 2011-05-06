//-----------------------------------------------------------------------
// <copyright file="MockCompilationSummary.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.UnitTests.Models
{
    using System.Collections.Generic;

    using Microsoft.TeamFoundation.Build.Client;

    public class MockCompilationSummary : ICompilationSummary
    {
        #region ICompilationSummary Members

        public ICompilationSummary AddCompilationSummary()
        {
            return new MockCompilationSummary();
        }

        public List<ICompilationSummary> Children
        {
            get
            {
                return new List<ICompilationSummary>();
            }
        }

        public int CompilationErrors
        {
            get; set;
        }

        public int CompilationWarnings
        {
            get; set;
        }

        public int Id
        {
            get; set;
        }

        public ICompilationSummary Parent
        {
            get
            {
                return new MockCompilationSummary();
            }
        }

        public string ProjectFile
        {
            get; set;
        }

        public void Save()
        {
            
        }

        public int StaticAnalysisErrors
        {
            get; set;
        }

        public int StaticAnalysisWarnings
        {
            get; set;
        }

        #endregion
    }
}
