// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MatrixBuild.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.model.MatrixBuild")]
    [XmlRoot("matrixBuild", Namespace = "", IsNullable = false)]
    public class MatrixBuild : Build
    {
    }
}