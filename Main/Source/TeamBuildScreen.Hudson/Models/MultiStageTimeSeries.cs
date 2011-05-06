// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiStageTimeSeries.cs" company="Jim Liddell">
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
    [XmlType(TypeName = "hudson.model.MultiStageTimeSeries")]
    public class MultiStageTimeSeries
    {
        private TimeSeries hourField;

        private TimeSeries minField;

        private TimeSeries sec10Field;

        [XmlElement("hour", Form = XmlSchemaForm.Unqualified)]
        public TimeSeries Hour
        {
            get
            {
                return this.hourField;
            }

            set
            {
                this.hourField = value;
            }
        }

        [XmlElement("min", Form = XmlSchemaForm.Unqualified)]
        public TimeSeries Min
        {
            get
            {
                return this.minField;
            }

            set
            {
                this.minField = value;
            }
        }

        [XmlElement("sec10", Form = XmlSchemaForm.Unqualified)]
        public TimeSeries Sec10
        {
            get
            {
                return this.sec10Field;
            }

            set
            {
                this.sec10Field = value;
            }
        }
    }
}