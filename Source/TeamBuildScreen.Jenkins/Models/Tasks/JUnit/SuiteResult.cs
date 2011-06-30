// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SuiteResult.cs" company="Jim Liddell">
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
    [XmlType(TypeName = "hudson.tasks.junit.SuiteResult")]
    public class SuiteResult
    {
        private CaseResult[] caseField;

        private object durationField;

        private string nameField;

        private string stderrField;

        private string stdoutField;

        private string timestampField;

        [XmlElement("case", Form = XmlSchemaForm.Unqualified)]
        public CaseResult[] Case
        {
            get
            {
                return this.caseField;
            }

            set
            {
                this.caseField = value;
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

        [XmlElement("timestamp", Form = XmlSchemaForm.Unqualified)]
        public string Timestamp
        {
            get
            {
                return this.timestampField;
            }

            set
            {
                this.timestampField = value;
            }
        }
    }
}