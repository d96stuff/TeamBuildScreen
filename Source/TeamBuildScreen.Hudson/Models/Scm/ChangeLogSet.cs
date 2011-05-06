// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangeLogSet.cs" company="Jim Liddell">
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
    [XmlType(TypeName = "hudson.scm.ChangeLogSet")]
    public class ChangeLogSet
    {
        private object[] itemField;

        private string kindField;

        [XmlElement("item", Form = XmlSchemaForm.Unqualified)]
        public object[] Item
        {
            get
            {
                return this.itemField;
            }

            set
            {
                this.itemField = value;
            }
        }

        [XmlElement("kind", Form = XmlSchemaForm.Unqualified)]
        public string Kind
        {
            get
            {
                return this.kindField;
            }

            set
            {
                this.kindField = value;
            }
        }
    }
}