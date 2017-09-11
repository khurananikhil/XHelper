//------------------------------------------------------------------------------
// <copyright file="XMLXPath.cs" company="Company">
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
    [Guid("f94b2b5d-b405-47b8-956b-00146a99473f")]
    public class XMLXPath : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XMLXPath"/> class.
        /// </summary>
        public XMLXPath() : base(null)
        {
            this.Caption = "XPath Explorer";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new XMLXPathControl();
            this.ToolBar = new CommandID(XPathExportCommand.CommandSet, XPathExportCommand.TWToolbar);
        }
    }
}
