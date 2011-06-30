// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestObject.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models.Tasks.JUnit
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using TeamBuildScreen.Hudson.Models.Tasks.Test;

    [XmlInclude(typeof(Test.TestObject))]
    [XmlInclude(typeof(Test.TestResult))]
    [XmlInclude(typeof(TabulatedResult))]
    [XmlInclude(typeof(MetaTabulatedResult))]
    [XmlInclude(typeof(TestResult))]
    [XmlInclude(typeof(CaseResult))]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.tasks.junit.TestObject")]
    public class TestObject
    {
    }
}