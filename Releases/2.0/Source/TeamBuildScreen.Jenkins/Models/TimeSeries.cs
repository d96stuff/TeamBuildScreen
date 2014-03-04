// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeSeries.cs" company="Jim Liddell">
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
    [XmlType(TypeName = "hudson.model.TimeSeries")]
    public class TimeSeries
    {
        private object[] historyField;

        private object latestField;

        [XmlElement("history", Form = XmlSchemaForm.Unqualified)]
        public object[] History
        {
            get
            {
                return this.historyField;
            }

            set
            {
                this.historyField = value;
            }
        }

        [XmlElement("latest", Form = XmlSchemaForm.Unqualified)]
        public object Latest
        {
            get
            {
                return this.latestField;
            }

            set
            {
                this.latestField = value;
            }
        }
    }
}