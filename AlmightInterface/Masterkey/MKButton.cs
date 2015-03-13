using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace WebMasterkey
{
   public class MKButton:MKBaseControl
    {

       public MKButton(XmlNode htmlDoc)
           : base(htmlDoc)
       { 
        
       }

       public override void MKEvent(mshtml.IHTMLDocument2 htmlDoc)
       {
             mshtml.HTMLInputElement inputEle =null;

           if (String.IsNullOrEmpty(ControlId))
           {
               inputEle = (mshtml.HTMLInputElement)htmlDoc.all.item(ControlName, 0);
           }
           else
           {
              inputEle = (mshtml.HTMLInputElement)htmlDoc.all.item(ControlId, 0);
           }

           if (inputEle != null)
           {
               inputEle.click();
           }

       }

       public override void MKEvent(mshtml.IHTMLDocument2 htmlDoc, XmlNode parameterNode)
       {
          
       }
    }
}
