//-----------------------------------------------------------------------
// <copyright file="Win32Api.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Interop
{
    #region Usings

    using System;
    using System.Runtime.InteropServices;

    #endregion

    /// <summary>
    /// Provides access to Win32 APIs.
    /// </summary>
    internal class Win32Api
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(IntPtr hWnd, ref Rectangle lpRect);
    }
}