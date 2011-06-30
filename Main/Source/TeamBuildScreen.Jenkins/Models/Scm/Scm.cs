// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scm.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models.Scm
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.scm.SCM")]
    public class Scm
    {
        private RepositoryBrowser browserField;

        private string typeField;

        [XmlElement("browser", Form = XmlSchemaForm.Unqualified)]
        public RepositoryBrowser Browser
        {
            get
            {
                return this.browserField;
            }

            set
            {
                this.browserField = value;
            }
        }

        [XmlElement("type", Form = XmlSchemaForm.Unqualified)]
        public string Type
        {
            get
            {
                return this.typeField;
            }

            set
            {
                this.typeField = value;
            }
        }
    }
}