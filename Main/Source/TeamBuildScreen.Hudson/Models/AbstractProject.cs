// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractProject.cs" company="Jim Liddell">
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
    [XmlType(TypeName = "hudson.model.AbstractProject")]
    public class AbstractProject : Job
    {
        private bool concurrentBuildField;

        private AbstractProject[] downstreamProjectField;

        private Scm.Scm scmField;

        private AbstractProject[] upstreamProjectField;

        [XmlElement("concurrentBuild", Form = XmlSchemaForm.Unqualified)]
        public bool ConcurrentBuild
        {
            get
            {
                return this.concurrentBuildField;
            }

            set
            {
                this.concurrentBuildField = value;
            }
        }

        [XmlElement("downstreamProject", Form = XmlSchemaForm.Unqualified)]
        public AbstractProject[] DownstreamProject
        {
            get
            {
                return this.downstreamProjectField;
            }

            set
            {
                this.downstreamProjectField = value;
            }
        }

        [XmlElement("scm", Form = XmlSchemaForm.Unqualified)]
        public Scm.Scm Scm
        {
            get
            {
                return this.scmField;
            }

            set
            {
                this.scmField = value;
            }
        }

        [XmlElement("upstreamProject", Form = XmlSchemaForm.Unqualified)]
        public AbstractProject[] UpstreamProject
        {
            get
            {
                return this.upstreamProjectField;
            }

            set
            {
                this.upstreamProjectField = value;
            }
        }
    }
}