// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Job.cs" company="Jim Liddell">
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

    [XmlInclude(typeof(AbstractProject))]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.model.Job")]
    public class Job : AbstractItem
    {
        private Run[] buildField;
        private bool buildableField;

        private BallColor colorField;

        private bool colorFieldSpecified;

        private Run firstBuildField;

        private HealthReport[] healthReportField;

        private bool inQueueField;

        private bool keepDependenciesField;

        private Run lastBuildField;

        private Run lastCompletedBuildField;

        private Run lastFailedBuildField;

        private Run lastStableBuildField;

        private Run lastSuccessfulBuildField;

        private Run lastUnstableBuildField;

        private Run lastUnsuccessfulBuildField;

        private int nextBuildNumberField;

        private JobProperty[] propertyField;

        private QueueItem queueItemField;

        [XmlElement("build", Form = XmlSchemaForm.Unqualified)]
        public Run[] Build
        {
            get
            {
                return this.buildField;
            }

            set
            {
                this.buildField = value;
            }
        }

        [XmlElement("buildable", Form = XmlSchemaForm.Unqualified)]
        public bool Buildable
        {
            get
            {
                return this.buildableField;
            }

            set
            {
                this.buildableField = value;
            }
        }

        [XmlElement("color", Form = XmlSchemaForm.Unqualified)]
        public BallColor Color
        {
            get
            {
                return this.colorField;
            }

            set
            {
                this.colorField = value;
            }
        }

        [XmlIgnore]
        public bool ColorSpecified
        {
            get
            {
                return this.colorFieldSpecified;
            }

            set
            {
                this.colorFieldSpecified = value;
            }
        }

        [XmlElement("firstBuild", Form = XmlSchemaForm.Unqualified)]
        public Run FirstBuild
        {
            get
            {
                return this.firstBuildField;
            }

            set
            {
                this.firstBuildField = value;
            }
        }

        [XmlElement("healthReport", Form = XmlSchemaForm.Unqualified)]
        public HealthReport[] HealthReport
        {
            get
            {
                return this.healthReportField;
            }

            set
            {
                this.healthReportField = value;
            }
        }

        [XmlElement("inQueue", Form = XmlSchemaForm.Unqualified)]
        public bool InQueue
        {
            get
            {
                return this.inQueueField;
            }

            set
            {
                this.inQueueField = value;
            }
        }

        [XmlElement("keepDependencies", Form = XmlSchemaForm.Unqualified)]
        public bool KeepDependencies
        {
            get
            {
                return this.keepDependenciesField;
            }

            set
            {
                this.keepDependenciesField = value;
            }
        }

        [XmlElement("lastBuild", Form = XmlSchemaForm.Unqualified)]
        public Run LastBuild
        {
            get
            {
                return this.lastBuildField;
            }

            set
            {
                this.lastBuildField = value;
            }
        }

        [XmlElement("lastCompletedBuild", Form = XmlSchemaForm.Unqualified)]
        public Run LastCompletedBuild
        {
            get
            {
                return this.lastCompletedBuildField;
            }

            set
            {
                this.lastCompletedBuildField = value;
            }
        }

        [XmlElement("lastFailedBuild", Form = XmlSchemaForm.Unqualified)]
        public Run LastFailedBuild
        {
            get
            {
                return this.lastFailedBuildField;
            }

            set
            {
                this.lastFailedBuildField = value;
            }
        }

        [XmlElement("lastStableBuild", Form = XmlSchemaForm.Unqualified)]
        public Run LastStableBuild
        {
            get
            {
                return this.lastStableBuildField;
            }

            set
            {
                this.lastStableBuildField = value;
            }
        }

        [XmlElement("lastSuccessfulBuild", Form = XmlSchemaForm.Unqualified)]
        public Run LastSuccessfulBuild
        {
            get
            {
                return this.lastSuccessfulBuildField;
            }

            set
            {
                this.lastSuccessfulBuildField = value;
            }
        }

        [XmlElement("lastUnstableBuild", Form = XmlSchemaForm.Unqualified)]
        public Run LastUnstableBuild
        {
            get
            {
                return this.lastUnstableBuildField;
            }

            set
            {
                this.lastUnstableBuildField = value;
            }
        }

        [XmlElement("lastUnsuccessfulBuild", Form = XmlSchemaForm.Unqualified)]
        public Run LastUnsuccessfulBuild
        {
            get
            {
                return this.lastUnsuccessfulBuildField;
            }

            set
            {
                this.lastUnsuccessfulBuildField = value;
            }
        }

        [XmlElement("nextBuildNumber", Form = XmlSchemaForm.Unqualified)]
        public int NextBuildNumber
        {
            get
            {
                return this.nextBuildNumberField;
            }

            set
            {
                this.nextBuildNumberField = value;
            }
        }

        [XmlElement("property", Form = XmlSchemaForm.Unqualified)]
        public JobProperty[] Property
        {
            get
            {
                return this.propertyField;
            }

            set
            {
                this.propertyField = value;
            }
        }

        [XmlElement("queueItem", Form = XmlSchemaForm.Unqualified)]
        public QueueItem QueueItem
        {
            get
            {
                return this.queueItemField;
            }

            set
            {
                this.queueItemField = value;
            }
        }
    }
}