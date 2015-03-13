using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace WebMasterkey
{
    public class MKLink:MKBaseControl
    {

        public MKLink(XmlNode node)
            : base(node)
        {
            ControlValue = string.IsNullOrEmpty(node.Attributes["value"].Value) ? string.Empty : node.Attributes["value"].Value;
        }


        public override void MKEvent(mshtml.IHTMLDocument2 htmlDoc)
        {
            if (string.IsNullOrEmpty(ControlId) && string.IsNullOrEmpty(ControlName))
            {
                EventwithoutID(htmlDoc);
            }
            else
            {
                EventWithID(htmlDoc);
            }
        }

        /// <summary>
        /// 执行没有ID元素的控件
        /// </summary>
        /// <param name="htmlDoc"></param>
        private void EventwithoutID(mshtml.IHTMLDocument2 htmlDoc)
        {
            mshtml.IHTMLElementCollection htmlLinks = htmlDoc.links;
            foreach (mshtml.IHTMLElement item in htmlLinks)
            {
                if (item.innerText == ControlValue)
                {
                    item.click();                 
                }
            }
        }

        /// <summary>
        /// 执行存在ID,或者name元素的控件
        /// </summary>
        /// <param name="htmlDoc"></param>
        private void EventWithID(mshtml.IHTMLDocument2 htmlDoc)
        {
            mshtml.HTMLAnchorElement inputEle = (mshtml.HTMLAnchorElement)htmlDoc.all.item(ControlId, 0);

            if (inputEle != null)
            {
                inputEle.click();
            }

        }
    }
}
