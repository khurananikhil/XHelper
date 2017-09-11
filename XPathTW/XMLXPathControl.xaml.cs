//------------------------------------------------------------------------------
// <copyright file="XMLXPathControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace XMLHelper
{
    using Helper;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Excel = Microsoft.Office.Interop.Excel;

    /// <summary>
    /// Interaction logic for XMLXPathControl.
    /// </summary>
    public partial class XMLXPathControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XMLXPathControl"/> class.
        /// </summary>
        public XMLXPathControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "XMLXPath");
        }
    }


    #region Export to excel

    public class XPathList : List<XPaths> { }

    public class XPaths
    {
        public string XPath { get; set; }
    };

    #endregion
}