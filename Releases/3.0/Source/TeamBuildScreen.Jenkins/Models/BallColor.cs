// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BallColor.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(TypeName = "hudson.model.BallColor")]
    public enum BallColor
    {
        red, 
        red_anime, 
        yellow, 
        yellow_anime, 
        blue, 
        blue_anime, 
        grey, 
        grey_anime, 
        disabled, 
        disabled_anime, 
        aborted, 
        aborted_anime, 
    }
}