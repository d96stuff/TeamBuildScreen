// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OverallLoadStatistics.cs" company="Jim Liddell">
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
    [XmlType(TypeName = "hudson.model.OverallLoadStatistics")]
    public class OverallLoadStatistics : LoadStatistics
    {
        private MultiStageTimeSeries totalQueueLengthField;

        [XmlElement("totalQueueLength", Form = XmlSchemaForm.Unqualified)]
        public MultiStageTimeSeries TotalQueueLength
        {
            get
            {
                return this.totalQueueLengthField;
            }

            set
            {
                this.totalQueueLengthField = value;
            }
        }
    }
}