// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractBuild.cs" company="Jim Liddell">
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

    using global::TeamBuildScreen.Hudson.Models;
    using global::TeamBuildScreen.Hudson.Models.Scm;

    [XmlInclude(typeof(Build))]
    [XmlIncludeAttribute(typeof(FreeStyleBuild))]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlTypeAttribute(TypeName = "hudson.model.AbstractBuild")]
    public class AbstractBuild : Run
    {
        private string builtOnField;

        private ChangeLogSet changeSetField;

        private User[] culpritField;

        [XmlElement("builtOn", Form = XmlSchemaForm.Unqualified)]
        public string BuiltOn
        {
            get
            {
                return this.builtOnField;
            }

            set
            {
                this.builtOnField = value;
            }
        }

        [XmlElement("changeSet", Form = XmlSchemaForm.Unqualified)]
        public ChangeLogSet ChangeSet
        {
            get
            {
                return this.changeSetField;
            }

            set
            {
                this.changeSetField = value;
            }
        }

        [XmlElement("culprit", Form = XmlSchemaForm.Unqualified)]
        public User[] Culprit
        {
            get
            {
                return this.culpritField;
            }

            set
            {
                this.culpritField = value;
            }
        }
    }
}