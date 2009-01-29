//-----------------------------------------------------------------------
// <copyright file="IMessageViewModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.ViewModels
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    public interface IMessageViewModel
    {
        #region Properties

        string Message { get; }
        bool IsVisible { get; }

        #endregion
    }
}