// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Jim Liddell" file="Hudson.cs">
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
    [XmlType(TypeName = "hudson.model.Hudson")]
    [XmlRoot("hudson", Namespace = "", IsNullable = false)]
    public class Hudson : Node
    {
        private string descriptionField;

        private Job[] jobField;

        private OverallLoadStatistics overallLoadField;

        private View primaryViewField;

        private int slaveAgentPortField;

        private bool useCrumbsField;

        private bool useSecurityField;

        private View[] viewField;

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

        [XmlElement("job", Form = XmlSchemaForm.Unqualified)]
        public Job[] Job
        {
            get
            {
                return this.jobField;
            }

            set
            {
                this.jobField = value;
            }
        }

        [XmlElement("overallLoad", Form = XmlSchemaForm.Unqualified)]
        public OverallLoadStatistics OverallLoad
        {
            get
            {
                return this.overallLoadField;
            }

            set
            {
                this.overallLoadField = value;
            }
        }

        [XmlElement("primaryView", Form = XmlSchemaForm.Unqualified)]
        public View PrimaryView
        {
            get
            {
                return this.primaryViewField;
            }

            set
            {
                this.primaryViewField = value;
            }
        }

        [XmlElement("slaveAgentPort", Form = XmlSchemaForm.Unqualified)]
        public int SlaveAgentPort
        {
            get
            {
                return this.slaveAgentPortField;
            }

            set
            {
                this.slaveAgentPortField = value;
            }
        }

        [XmlElement("useCrumbs", Form = XmlSchemaForm.Unqualified)]
        public bool UseCrumbs
        {
            get
            {
                return this.useCrumbsField;
            }

            set
            {
                this.useCrumbsField = value;
            }
        }

        [XmlElement("useSecurity", Form = XmlSchemaForm.Unqualified)]
        public bool UseSecurity
        {
            get
            {
                return this.useSecurityField;
            }

            set
            {
                this.useSecurityField = value;
            }
        }

        [XmlElement("view", Form = XmlSchemaForm.Unqualified)]
        public View[] View
        {
            get
            {
                return this.viewField;
            }

            set
            {
                this.viewField = value;
            }
        }
    }
}