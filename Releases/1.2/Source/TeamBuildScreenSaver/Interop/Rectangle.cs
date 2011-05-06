//-----------------------------------------------------------------------
// <copyright file="Rectangle.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Interop
{
    /// <summary>
    /// A managed representation of a rectange returned from a Win32 API call.
    /// </summary>
    public struct Rectangle
    {
        /// <summary>
        /// The left component of the rectange.
        /// </summary>
        public int Left;

        /// <summary>
        /// The top component of the rectange.
        /// </summary>
        public int Top;

        /// <summary>
        /// The right component of the rectange.
        /// </summary>
        public int Right;

        /// <summary>
        /// The bottom component of the rectange.
        /// </summary>
        public int Bottom;
    }
}