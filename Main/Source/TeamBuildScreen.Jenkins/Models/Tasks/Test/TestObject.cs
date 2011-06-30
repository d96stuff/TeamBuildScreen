// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestObject.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models.Tasks.Test
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using TeamBuildScreen.Hudson.Models.Tasks.JUnit;

    [XmlInclude(typeof(TestResult))]
    [XmlInclude(typeof(TabulatedResult))]
    [XmlInclude(typeof(MetaTabulatedResult))]
    [XmlInclude(typeof(JUnit.TestResult))]
    [XmlInclude(typeof(CaseResult))]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.tasks.test.TestObject")]
    public class TestObject : JUnit.TestObject
    {
    }
}