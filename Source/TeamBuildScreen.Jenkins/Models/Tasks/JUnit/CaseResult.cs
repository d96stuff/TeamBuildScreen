// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CaseResult.cs" company="Jim Liddell">
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

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.tasks.junit.CaseResult")]
    public class CaseResult : Test.TestResult
    {
        private int ageField;

        private string classNameField;

        private object durationField;

        private string errorDetailsField;

        private string errorStackTraceField;

        private int failedSinceField;

        private string nameField;

        private bool skippedField;

        private CaseResultStatus statusField;

        private bool statusFieldSpecified;

        private string stderrField;

        private string stdoutField;

        [XmlElement("age", Form = XmlSchemaForm.Unqualified)]
        public int Age
        {
            get
            {
                return this.ageField;
            }

            set
            {
                this.ageField = value;
            }
        }

        [XmlElement("className", Form = XmlSchemaForm.Unqualified)]
        public string ClassName
        {
            get
            {
                return this.classNameField;
            }

            set
            {
                this.classNameField = value;
            }
        }

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

        [XmlElement("errorDetails", Form = XmlSchemaForm.Unqualified)]
        public string ErrorDetails
        {
            get
            {
                return this.errorDetailsField;
            }

            set
            {
                this.errorDetailsField = value;
            }
        }

        [XmlElement("errorStackTrace", Form = XmlSchemaForm.Unqualified)]
        public string ErrorStackTrace
        {
            get
            {
                return this.errorStackTraceField;
            }

            set
            {
                this.errorStackTraceField = value;
            }
        }

        [XmlElement("failedSince", Form = XmlSchemaForm.Unqualified)]
        public int FailedSince
        {
            get
            {
                return this.failedSinceField;
            }

            set
            {
                this.failedSinceField = value;
            }
        }

        [XmlElement("name", Form = XmlSchemaForm.Unqualified)]
        public string Name
        {
            get
            {
                return this.nameField;
            }

            set
            {
                this.nameField = value;
            }
        }

        [XmlElement("skipped", Form = XmlSchemaForm.Unqualified)]
        public bool Skipped
        {
            get
            {
                return this.skippedField;
            }

            set
            {
                this.skippedField = value;
            }
        }

        [XmlElement("status", Form = XmlSchemaForm.Unqualified)]
        public CaseResultStatus Status
        {
            get
            {
                return this.statusField;
            }

            set
            {
                this.statusField = value;
            }
        }

        [XmlIgnore]
        public bool StatusSpecified
        {
            get
            {
                return this.statusFieldSpecified;
            }

            set
            {
                this.statusFieldSpecified = value;
            }
        }

        [XmlElement("stderr", Form = XmlSchemaForm.Unqualified)]
        public string Stderr
        {
            get
            {
                return this.stderrField;
            }

            set
            {
                this.stderrField = value;
            }
        }

        [XmlElement("stdout", Form = XmlSchemaForm.Unqualified)]
        public string Stdout
        {
            get
            {
                return this.stdoutField;
            }

            set
            {
                this.stdoutField = value;
            }
        }
    }
}