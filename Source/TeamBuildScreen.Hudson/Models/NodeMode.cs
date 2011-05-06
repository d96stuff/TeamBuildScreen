// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeMode.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(TypeName = "hudson.model.Node-Mode")]
    public enum NodeMode
    {
        NORMAL, 
        EXCLUSIVE, 
    }
}