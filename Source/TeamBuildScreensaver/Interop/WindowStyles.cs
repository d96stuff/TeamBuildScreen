//-----------------------------------------------------------------------
// <copyright file="WindowStyles.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Interop
{
    /// <summary>
    /// Provides windows style constants required by some Win32 API calls.
    /// </summary>
    internal static class WindowStyles
    {
        #region Constants

        public const uint WindowStyleChild = 0x40000000;
        public const uint WindowStyleVisible = 0x10000000;
        public const uint WindowStyleClipChildren = 0x02000000;

        #endregion
    }
}