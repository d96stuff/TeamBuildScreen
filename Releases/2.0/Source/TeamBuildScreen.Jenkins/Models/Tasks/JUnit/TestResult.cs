// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestResult.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models.Tasks.JUnit
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    using TeamBuildScreen.Hudson.Models.Tasks.Test;

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.tasks.junit.TestResult")]
    [XmlRoot("testResult", Namespace = "", IsNullable = false)]
    public class TestResult : MetaTabulatedResult
    {
        private object durationField;

        private int failCountField;

        private int passCountField;

        private int skipCountField;

        private SuiteResult[] suiteField;

        [XmlElement("duration", Form = XmlSchemaForm.Unqualified)]
        public object Duration
        {
            get
            {
                return this.durationField;
            }

            set
            {
                this.durationField = value;
            }
        }

        [XmlElement("failCount", Form = XmlSchemaForm.Unqualified)]
        public int FailCount
        {
            get
            {
                return this.failCountField;
            }

            set
            {
                this.failCountField = value;
            }
        }

        [XmlElement("passCount", Form = XmlSchemaForm.Unqualified)]
        public int PassCount
        {
            get
            {
                return this.passCountField;
            }

            set
            {
                this.passCountField = value;
            }
        }

        [XmlElement("skipCount", Form = XmlSchemaForm.Unqualified)]
        public int SkipCount
        {
            get
            {
                return this.skipCountField;
            }

            set
            {
                this.skipCountField = value;
            }
        }

        [XmlElement("suite", Form = XmlSchemaForm.Unqualified)]
        public SuiteResult[] Suite
        {
            get
            {
                return this.suiteField;
            }

            set
            {
                this.suiteField = value;
            }
        }
    }
}