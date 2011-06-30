// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HealthReport.cs" company="Jim Liddell">
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
    [XmlType(TypeName = "hudson.model.HealthReport")]
    public class HealthReport
    {
        private string descriptionField;

        private string iconUrlField;

        private int scoreField;

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

        [XmlElement("iconUrl", Form = XmlSchemaForm.Unqualified)]
        public string IconUrl
        {
            get
            {
                return this.iconUrlField;
            }

            set
            {
                this.iconUrlField = value;
            }
        }

        [XmlElement("score", Form = XmlSchemaForm.Unqualified)]
        public int Score
        {
            get
            {
                return this.scoreField;
            }

            set
            {
                this.scoreField = value;
            }
        }
    }
}