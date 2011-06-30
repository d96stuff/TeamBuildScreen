// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Jim Liddell" file="Build.cs">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using TeamBuildScreen.Hudson.Models;

    [XmlInclude(typeof(FreeStyleBuild))]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.model.Build")]
    public class Build : AbstractBuild
    {
    }
}