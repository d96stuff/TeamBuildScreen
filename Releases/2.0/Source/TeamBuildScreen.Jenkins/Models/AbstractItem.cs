﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractItem.cs" company="Jim Liddell">
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

    
    [XmlInclude(typeof(Job))]
    [XmlInclude(typeof(AbstractProject))]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.model.AbstractItem")]
    public class AbstractItem : Actionable
    {
        private string descriptionField;

        private string displayNameField;

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

        
        [XmlElement("displayName", Form = XmlSchemaForm.Unqualified)]
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }

            set
            {
                this.displayNameField = value;
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