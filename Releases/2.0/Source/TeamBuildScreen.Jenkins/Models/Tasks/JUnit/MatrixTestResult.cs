// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MatrixTestResult.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models.Tasks.JUnit
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;


    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.tasks.junit.MatrixTestResult")]
    [XmlRoot("matrixTestResult", Namespace = "", IsNullable = false)]
    public class MatrixTestResult : TestResult
    {
    }
}