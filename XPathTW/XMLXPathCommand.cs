//------------------------------------------------------------------------------
// <copyright file="XMLXPathCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;
using EnvDTE80;
using System.Xml;
using System.Windows;
using System.Collections.Generic;
using XMLHelper.Helper;

namespace XMLHelper
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class XMLXPathCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("d664261c-af9f-44ee-a150-d62db6e3acd4");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLXPathCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private XMLXPathCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.ShowToolWindow, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static XMLXPathCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new XMLXPathCommand(package);
        }

        /// <summary>
        /// Shows the tool window when the menu item is clicked.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void ShowToolWindow(object sender, EventArgs e)
        {
            List<string> xPathList = new List<string>();
            // Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            ToolWindowPane window = this.package.FindToolWindow(typeof(XMLXPath), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;

            // Get current window  xml content
            DTE2 dteService = (DTE)this.ServiceProvider.GetService(typeof(DTE)) as DTE2;

            try
            {
                if (dteService.ActiveDocument != null)
                {


                    //if (string.Equals(dteService.ActiveDocument.Type, "Text", StringComparison.CurrentCultureIgnoreCase))
                    //{
                    TextDocument docText = dteService.ActiveDocument.Object("TextDocument") as TextDocument;

                    EditPoint objEditPoint = docText.StartPoint.CreateEditPoint();
                    string xmlDocText = objEditPoint.GetText(docText.EndPoint);

                    if (!string.IsNullOrEmpty(xmlDocText))
                    {

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(xmlDocText);
                        foreach (XmlNode node in doc.ChildNodes)
                        {
                            List<string> objXPathList = new List<string>();
                            XPathHelper.ExtractXPath(node, "", objXPathList);
                            xPathList.AddRange(objXPathList);
                        }

                        if (xPathList.Count > 0)
                        {
                            //XMLXPathControl
                            XMLXPathControl xPathControl = window.Content as XMLXPathControl;
                            if (xPathControl != null)
                            {
                                XPathList xPathsList = new XPathList();
                                foreach (string xPath in xPathList)
                                {
                                    xPathsList.Add(new XPaths { XPath = xPath });
                                }
                                xPathControl.dgXPath.ItemsSource = xPathsList;
                            }
                        }
                    }
                    //dteService.ItemOperations.NewFile("General\\XML File", "Xpath");            
                    //TextDocument objXMLDoc = dteService.ActiveDocument.Object("TextDocument") as TextDocument;
                    //EditPoint objEditPoint = objXMLDoc.StartPoint.CreateEditPoint();
                    //objEditPoint.Delete(objEditPoint);
                    //objEditPoint.Insert("sample details");
                    // Load it into XMLDocument
                    // Get Xpath list
                    // Bind it with datagrid
                    //MessageBox.Show(xmlDocText);
                    //}
                }
                else
                {
                    MessageBox.Show("Please select document");
                }
            }
            catch (Exception exp)
            {
                // create debugging report
                MessageBox.Show(exp.Message);
            }

            // Shown window
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
    }
}
