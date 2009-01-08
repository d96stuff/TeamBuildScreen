//-----------------------------------------------------------------------
// <copyright file="BuildSetting.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.DataModels
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    public class BuildSetting
    {
        #region Properties

        public string Platform { get; set; }
        public string Configuration { get; set; }
        public string TeamProject { get; set; }
        public string DefinitionName { get; set; }
        public bool IsEnabled { get; set; }

        #endregion
    }
}