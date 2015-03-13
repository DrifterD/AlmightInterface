using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WebMasterkey
{
   public  class MKText:MKBaseControl
    {
       public MKText(XmlNode htmlDoc)
           : base(htmlDoc)
       {
           ControlValue = string.IsNullOrEmpty(htmlDoc.Attributes["value"].Value) ? string.Empty : htmlDoc.Attributes["value"].Value;
           Method = Convert.ToInt32((htmlDoc.Attributes["method"].Value));
       }

       public override void MKEvent(mshtml.IHTMLDocument2 htmlDoc)
       {         
           mshtml.HTMLInputElement inputEle = null;

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
               ShiftMode(inputEle);
           }

       }

       private void ShiftMode(mshtml.HTMLInputElement  ele) 
       { 
           //read
            if( Method==1)
            {
                ControlValue = ele.value;
            }

           //write
            if (Method == 2)
            {
                ele.value = ControlValue;
            }
       }
    }
}
