// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Jim Liddell">
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
    [XmlType(TypeName = "hudson.model.User")]
    public class User
    {
        private string absoluteUrlField;

        private string descriptionField;

        private string fullNameField;

        private string idField;

        private UserProperty[] propertyField;

        [XmlElement("absoluteUrl", Form = XmlSchemaForm.Unqualified)]
        public string AbsoluteUrl
        {
            get
            {
                return this.absoluteUrlField;
            }

            set
            {
                this.absoluteUrlField = value;
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

        [XmlElement("fullName", Form = XmlSchemaForm.Unqualified)]
        public string FullName
        {
            get
            {
                return this.fullNameField;
            }

            set
            {
                this.fullNameField = value;
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

        [XmlElement("property", Form = XmlSchemaForm.Unqualified)]
        public UserProperty[] Property
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
    }
}