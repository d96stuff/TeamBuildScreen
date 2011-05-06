//-----------------------------------------------------------------------
// <copyright file="IMessageViewModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.ViewModels
{
    public interface IMessageViewModel
    {
        #region Properties

        string Message { get; }
        bool IsVisible { get; }

        #endregion
    }
}