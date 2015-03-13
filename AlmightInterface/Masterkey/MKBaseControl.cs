using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WebMasterkey
{
    public class MKBaseControl : IMKControl
    {
        private string _controlId;

        private int _method;

        public int Method
        {
            get { return _method; }
            set { _method = value; }
        }

        public string ControlId
        {
            get { return _controlId; }
            set { _controlId = value; }
        }
        private string _controlName;

        public string ControlName
        {
            get { return _controlName; }
            set { _controlName = value; }
        }
        private string _controlType;

        public string ControlType
        {
            get { return _controlType; }
            set { _controlType = value; }
        }

        private string _controlText;
        /// <summary>
        /// 控件显示内容
        /// </summary>
        public string ControlText
        {
            get { return _controlText; }
            set { _controlText = value; }
        }

        private string _controlValue;
        /// <summary>
        /// 控件值
        /// </summary>
        public string ControlValue
        {
            get { return _controlValue; }
            set { _controlValue = value; }
        }

        private Boolean _inFrmae;

        public Boolean InFrmae
        {
            get { return _inFrmae; }
            set { _inFrmae = value; }
        }


        public MKBaseControl(XmlNode controlNode)
        {
            _controlId = string.IsNullOrEmpty(controlNode.Attributes["id"].Value) ? string.Empty : controlNode.Attributes["id"].Value;
            _controlName = string.IsNullOrEmpty(controlNode.Attributes["name"].Value) ? string.Empty : controlNode.Attributes["name"].Value;

            if (controlNode.Attributes["inframe"] == null)
            {
                _inFrmae = false;
            }
            else
            {
                string tmpFlag = controlNode.Attributes["inframe"].Value;
                _inFrmae = string.IsNullOrEmpty(tmpFlag) ? false : (tmpFlag.Trim() == "1" ? true : false);
            }

        }

        public void MKOption(mshtml.IHTMLDocument2 htmlDoc)
        {
            if (!_inFrmae)
            {
                //不存在ifrmae中
                MKEvent(htmlDoc);
            }
            else
            {
                //存在ifrmae中         
                mshtml.IHTMLFramesCollection2 frams = htmlDoc.frames;
                object tmpJ = new object();
                if (frams.length > 0)
                {
                    for (int i = 0; i < frams.length; i++)
                    {
                        tmpJ = i;
                        mshtml.IHTMLWindow2 framWin = frams.item(ref tmpJ) as mshtml.IHTMLWindow2;
                        mshtml.IHTMLDocument2 framDoc = framWin.document;
                        MKEvent(framDoc);
                    }
                }
            }
        }


        /// <summary>
        /// 根据提供的html对象操作对应的控件行为
        /// </summary>
        /// <param name="htmlDoc"></param>
        public virtual void MKEvent(mshtml.IHTMLDocument2 htmlDoc)
        {

        }

        public virtual void MKEvent(mshtml.IHTMLDocument2 htmlDoc, XmlNode parameterNode)
        {

        }
    }
}
