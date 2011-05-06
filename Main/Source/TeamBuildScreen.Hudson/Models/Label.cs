// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Label.cs" company="Jim Liddell">
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

    using TeamBuildScreen.Hudson.Models.Labels;

    [XmlInclude(typeof(LabelAtom))]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.model.Label")]
    public class Label : Actionable
    {
        private int busyExecutorsField;

        private object[] cloudField;

        private string descriptionField;

        private int idleExecutorsField;

        private LoadStatistics loadStatisticsField;

        private string nameField;

        private Node[] nodeField;

        private bool offlineField;

        private AbstractProject[] tiedJobField;

        private int totalExecutorsField;

        [XmlElement("busyExecutors", Form = XmlSchemaForm.Unqualified)]
        public int BusyExecutors
        {
            get
            {
                return this.busyExecutorsField;
            }

            set
            {
                this.busyExecutorsField = value;
            }
        }

        [XmlElement("cloud", Form = XmlSchemaForm.Unqualified)]
        public object[] Cloud
        {
            get
            {
                return this.cloudField;
            }

            set
            {
                this.cloudField = value;
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

        [XmlElement("idleExecutors", Form = XmlSchemaForm.Unqualified)]
        public int IdleExecutors
        {
            get
            {
                return this.idleExecutorsField;
            }

            set
            {
                this.idleExecutorsField = value;
            }
        }

        [XmlElement("loadStatistics", Form = XmlSchemaForm.Unqualified)]
        public LoadStatistics LoadStatistics
        {
            get
            {
                return this.loadStatisticsField;
            }

            set
            {
                this.loadStatisticsField = value;
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

        [XmlElement("node", Form = XmlSchemaForm.Unqualified)]
        public Node[] Node
        {
            get
            {
                return this.nodeField;
            }

            set
            {
                this.nodeField = value;
            }
        }

        [XmlElement("offline", Form = XmlSchemaForm.Unqualified)]
        public bool Offline
        {
            get
            {
                return this.offlineField;
            }

            set
            {
                this.offlineField = value;
            }
        }

        [XmlElement("tiedJob", Form = XmlSchemaForm.Unqualified)]
        public AbstractProject[] TiedJob
        {
            get
            {
                return this.tiedJobField;
            }

            set
            {
                this.tiedJobField = value;
            }
        }

        [XmlElement("totalExecutors", Form = XmlSchemaForm.Unqualified)]
        public int TotalExecutors
        {
            get
            {
                return this.totalExecutorsField;
            }

            set
            {
                this.totalExecutorsField = value;
            }
        }
    }
}