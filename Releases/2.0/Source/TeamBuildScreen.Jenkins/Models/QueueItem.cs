// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueueItem.cs" company="Jim Liddell">
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
    [XmlType(TypeName = "hudson.model.Queue-Item")]
    public class QueueItem : Actionable
    {
        private bool blockedField;

        private bool buildableField;

        private string paramsField;

        private bool stuckField;

        private object taskField;

        private string whyField;

        [XmlElement("blocked", Form = XmlSchemaForm.Unqualified)]
        public bool Blocked
        {
            get
            {
                return this.blockedField;
            }

            set
            {
                this.blockedField = value;
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

        [XmlElement("params", Form = XmlSchemaForm.Unqualified)]
        public string Params
        {
            get
            {
                return this.paramsField;
            }

            set
            {
                this.paramsField = value;
            }
        }

        [XmlElement("stuck", Form = XmlSchemaForm.Unqualified)]
        public bool Stuck
        {
            get
            {
                return this.stuckField;
            }

            set
            {
                this.stuckField = value;
            }
        }

        [XmlElement("task", Form = XmlSchemaForm.Unqualified)]
        public object Task
        {
            get
            {
                return this.taskField;
            }

            set
            {
                this.taskField = value;
            }
        }

        [XmlElement("why", Form = XmlSchemaForm.Unqualified)]
        public string Why
        {
            get
            {
                return this.whyField;
            }

            set
            {
                this.whyField = value;
            }
        }
    }
}