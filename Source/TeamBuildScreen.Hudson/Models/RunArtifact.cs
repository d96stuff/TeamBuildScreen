// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RunArtifact.cs" company="Jim Liddell">
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
    [XmlType(TypeName = "hudson.model.Run-Artifact")]
    public class RunArtifact
    {
        private string displayPathField;

        private string fileNameField;

        private string relativePathField;

        [XmlElement("displayPath", Form = XmlSchemaForm.Unqualified)]
        public string DisplayPath
        {
            get
            {
                return this.displayPathField;
            }

            set
            {
                this.displayPathField = value;
            }
        }

        [XmlElement("fileName", Form = XmlSchemaForm.Unqualified)]
        public string FileName
        {
            get
            {
                return this.fileNameField;
            }

            set
            {
                this.fileNameField = value;
            }
        }

        [XmlElement("relativePath", Form = XmlSchemaForm.Unqualified)]
        public string RelativePath
        {
            get
            {
                return this.relativePathField;
            }

            set
            {
                this.relativePathField = value;
            }
        }
    }
}