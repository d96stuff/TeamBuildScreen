// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Run.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.model.Run")]
    public class Run : Actionable
    {
        private RunArtifact[] artifactField;

        private bool buildingField;

        private string descriptionField;

        private long durationField;

        private string fullDisplayNameField;

        private string idField;

        private bool keepLogField;

        private int numberField;

        private object resultField;

        private long timestampField;

        private bool timestampFieldSpecified;

        private string urlField;

        [XmlElement("artifact", Form = XmlSchemaForm.Unqualified)]
        public RunArtifact[] Artifact
        {
            get
            {
                return this.artifactField;
            }

            set
            {
                this.artifactField = value;
            }
        }

        [XmlElement("building", Form = XmlSchemaForm.Unqualified)]
        public bool Building
        {
            get
            {
                return this.buildingField;
            }

            set
            {
                this.buildingField = value;
            }
        }

        [XmlElement("description", Form = XmlSchemaForm.Unqualified)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }

            set
            {
                this.descriptionField = value;
            }
        }

        [XmlElement("duration", Form = XmlSchemaForm.Unqualified)]
        public long Duration
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

        [XmlElement("fullDisplayName", Form = XmlSchemaForm.Unqualified)]
        public string FullDisplayName
        {
            get
            {
                return this.fullDisplayNameField;
            }

            set
            {
                this.fullDisplayNameField = value;
            }
        }

        [XmlElement("id", Form = XmlSchemaForm.Unqualified)]
        public string Id
        {
            get
            {
                return this.idField;
            }

            set
            {
                this.idField = value;
            }
        }

        [XmlElement("keepLog", Form = XmlSchemaForm.Unqualified)]
        public bool KeepLog
        {
            get
            {
                return this.keepLogField;
            }

            set
            {
                this.keepLogField = value;
            }
        }

        [XmlElement("number", Form = XmlSchemaForm.Unqualified)]
        public int Number
        {
            get
            {
                return this.numberField;
            }

            set
            {
                this.numberField = value;
            }
        }

        [XmlElement("result", Form = XmlSchemaForm.Unqualified)]
        public object Result
        {
            get
            {
                return this.resultField;
            }

            set
            {
                this.resultField = value;
            }
        }

        [XmlElement("timestamp", Form = XmlSchemaForm.Unqualified)]
        public long Timestamp
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

        [XmlIgnore]
        public bool TimestampSpecified
        {
            get
            {
                return this.timestampFieldSpecified;
            }

            set
            {
                this.timestampFieldSpecified = value;
            }
        }

        [XmlElement("url", Form = XmlSchemaForm.Unqualified)]
        public string Url
        {
            get
            {
                return this.urlField;
            }

            set
            {
                this.urlField = value;
            }
        }
    }
}