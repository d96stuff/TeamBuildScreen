// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CaseResultStatus.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models.Tasks.JUnit
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(TypeName = "hudson.tasks.junit.CaseResult-Status")]
    public enum CaseResultStatus
    {
        PASSED, 
        SKIPPED, 
        FAILED, 
        FIXED, 
        REGRESSION, 
    }
}