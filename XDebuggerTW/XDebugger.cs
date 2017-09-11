//------------------------------------------------------------------------------
// <copyright file="XDebugger.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace XMLHelper
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;
    using System.ComponentModel.Design;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("4f2ca848-0b56-4dd6-ab6f-c1c97c684325")]
    public class XDebugger : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XDebugger"/> class.
        /// </summary>
        public XDebugger() : base(null)
        {
            this.Caption = "XDebugger";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new XDebuggerControl();            
        }
    }    
}
