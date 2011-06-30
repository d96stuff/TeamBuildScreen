// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadStatistics.cs" company="Jim Liddell">
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

    [XmlInclude(typeof(OverallLoadStatistics))]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.model.LoadStatistics")]
    public class LoadStatistics
    {
        private MultiStageTimeSeries busyExecutorsField;

        private MultiStageTimeSeries queueLengthField;

        private MultiStageTimeSeries totalExecutorsField;

        [XmlElement("busyExecutors", Form = XmlSchemaForm.Unqualified)]
        public MultiStageTimeSeries BusyExecutors
        {
            get
            {
                return this.busyExecutorsField;
            }

            set
            {
                this.busyExecutorsField = value;
            }
        }

        [XmlElement("queueLength", Form = XmlSchemaForm.Unqualified)]
        public MultiStageTimeSeries QueueLength
        {
            get
            {
                return this.queueLengthField;
            }

            set
            {
                this.queueLengthField = value;
            }
        }

        [XmlElement("totalExecutors", Form = XmlSchemaForm.Unqualified)]
        public MultiStageTimeSeries TotalExecutors
        {
            get
            {
                return this.totalExecutorsField;
            }

            set
            {
                this.totalExecutorsField = value;
            }
        }
    }
}