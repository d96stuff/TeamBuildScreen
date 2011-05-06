// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Actionable.cs" company="Jim Liddell">
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

    using TeamBuildScreen.Hudson.Models.Labels;

    [XmlInclude(typeof(QueueItem))]
    [XmlInclude(typeof(Run))]
    [XmlInclude(typeof(AbstractItem))]
    [XmlInclude(typeof(Job))]
    [XmlInclude(typeof(AbstractProject))]
    [XmlInclude(typeof(Label))]
    [XmlInclude(typeof(LabelAtom))]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.model.Actionable")]
    public class Actionable
    {
        private object[] actionField;

        [XmlElement("action", Form = XmlSchemaForm.Unqualified)]
        public object[] Action
        {
            get
            {
                return this.actionField;
            }

            set
            {
                this.actionField = value;
            }
        }
    }
}