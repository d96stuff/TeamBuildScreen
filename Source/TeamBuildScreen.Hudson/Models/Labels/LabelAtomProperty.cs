// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelAtomProperty.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models.Labels
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    /// <summary>
    /// The label atom property.
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "hudson.model.labels.LabelAtomProperty")]
    public class LabelAtomProperty
    {
    }
}