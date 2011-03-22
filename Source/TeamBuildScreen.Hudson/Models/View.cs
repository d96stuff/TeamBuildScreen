// --------------------------------------------------------------------------------------------------------------------
// <copyright file="View.cs" company="Jim Liddell">
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
    [XmlType(TypeName = "hudson.model.View")]
    public class View
    {
        private string descriptionField;

        private object[] jobField;

        private string nameField;

        private string urlField;

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
        public object[] Job
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