using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLHelper.Helper
{
    class XPathHelper
    {


        public static void ExtractXPath(XmlNode node, String parentPath, List<string> xPathList)
        {
            string nodePath = parentPath + '/' + node.Name;

            if (!(node is XmlText))
            {
                xPathList.Add(nodePath);

                foreach (XmlNode childNode in node.ChildNodes)
                    ExtractXPath(childNode, nodePath, xPathList);
            }

            //return xPathList;
        }
    }
}
