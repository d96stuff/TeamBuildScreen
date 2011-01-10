using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamBuildScreen.Core.Models
{
    public abstract class BuildInfoBase
    {
        private int? testsFailed;
        private int? testsPassed;
        private int? testsTotal;

        public int? TestsFailed
        {
            get
            {
                return this.testsFailed;
            }
            protected set
            {
                this.testsFailed = value;
            }
        }

        public int? TestsPassed
        {
            get
            {
                return this.testsPassed;
            }
            protected set
            {
                this.testsPassed = value;
            }
        }

        public int? TestsTotal
        {
            get
            {
                return this.testsTotal;
            }
            protected set
            {
                this.testsTotal = value;
            }
        }

        public bool HasWarnings
        {
            get;
            protected set;
        }
    }
}
