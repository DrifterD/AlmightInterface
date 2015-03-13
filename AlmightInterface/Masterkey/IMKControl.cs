using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WebMasterkey
{
    interface IMKControl
    {
        void MKEvent(mshtml.IHTMLDocument2 htmlDoc);

        void MKEvent(mshtml.IHTMLDocument2 htmlDoc,XmlNode parameterNode);
    }
}
