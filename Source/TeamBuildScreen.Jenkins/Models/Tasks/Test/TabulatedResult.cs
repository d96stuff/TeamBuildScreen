// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TabulatedResult.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models.Tasks.Test
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [XmlInclude(typeof(MetaTabulatedResult))]
    [XmlInclude(typeof(JUnit.TestResult))]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.tasks.test.TabulatedResult")]
    public class TabulatedResult : TestResult
    {
    }
}