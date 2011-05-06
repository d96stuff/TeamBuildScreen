// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Node.cs" company="Jim Liddell">
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

    [XmlInclude(typeof(Hudson))]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.model.Node")]
    public class Node
    {
        private LabelAtom[] assignedLabelField;

        private NodeMode modeField;

        private bool modeFieldSpecified;

        private string nodeDescriptionField;

        private string nodeNameField;

        private int numExecutorsField;

        [XmlElement("assignedLabel", Form = XmlSchemaForm.Unqualified)]
        public LabelAtom[] AssignedLabel
        {
            get
            {
                return this.assignedLabelField;
            }

            set
            {
                this.assignedLabelField = value;
            }
        }

        [XmlElement("mode", Form = XmlSchemaForm.Unqualified)]
        public NodeMode Mode
        {
            get
            {
                return this.modeField;
            }

            set
            {
                this.modeField = value;
            }
        }

        [XmlIgnore]
        public bool ModeSpecified
        {
            get
            {
                return this.modeFieldSpecified;
            }

            set
            {
                this.modeFieldSpecified = value;
            }
        }

        [XmlElement("nodeDescription", Form = XmlSchemaForm.Unqualified)]
        public string NodeDescription
        {
            get
            {
                return this.nodeDescriptionField;
            }

            set
            {
                this.nodeDescriptionField = value;
            }
        }

        [XmlElement("nodeName", Form = XmlSchemaForm.Unqualified)]
        public string NodeName
        {
            get
            {
                return this.nodeNameField;
            }

            set
            {
                this.nodeNameField = value;
            }
        }

        [XmlElement("numExecutors", Form = XmlSchemaForm.Unqualified)]
        public int NumExecutors
        {
            get
            {
                return this.numExecutorsField;
            }

            set
            {
                this.numExecutorsField = value;
            }
        }
    }
}