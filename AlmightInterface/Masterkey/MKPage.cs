using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Reflection;
using SHDocVw;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace WebMasterkey
{
    public class MKPage : IMKPage
    {
        #region private fields
        private string _errorMsg;
        private int _errorNo;
        private string _pageUrl;
        private string _pageTitle;
        private XmlNode _pageNode;
        private SHDocVw.InternetExplorer _speicfiedyIPage;
        private List<MKBaseControl> _mkControls;
        private string _pageSource;

        public List<MKBaseControl> MkControls
        {
            get { return _mkControls; }
            set { _mkControls = value; }
        }
        //等待页面加载超时时间（单位：秒）
        private const int PAGETIMEOUT = 30;
        private Stopwatch _sw;
        private AutoResetEvent _evnet = new AutoResetEvent(true);
        #endregion

        #region public properties
        public string PageUrl
        {
            get { return _pageUrl; }
            set { _pageUrl = value; }
        }
        public string PageTitle
        {
            get { return _pageTitle; }
            set { _pageTitle = value; }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { _errorMsg = value; }
        }
        #endregion

        #region public method
        public MKPage(XmlNode xmlNode)
        {            
            _mkControls = new List<MKBaseControl>();
            _sw = new Stopwatch();
            _pageNode = xmlNode;
            _pageUrl = _pageNode .Attributes[0].Value;
            _pageTitle = _pageNode.Attributes[1].Value;
            _pageSource=  _pageNode.Attributes[2].Value;
            AnalyzeXmlScript();
        }

        public void execOptions()
        {
            _speicfiedyIPage = GetPage(_pageUrl, _pageTitle);
            //判断浏览器是否加载完成

            if (_speicfiedyIPage == null)
            {
                CreatePage();
                _speicfiedyIPage = GetPage(_pageUrl, _pageTitle);   
            }

            //todo: 检查加载网页；
            CheckPageComplited();


            mshtml.IHTMLDocument2 htmldoc2 = _speicfiedyIPage.Document as mshtml.IHTMLDocument2;
            foreach (MKBaseControl item in _mkControls)
            {
                //todo: 处理加载超时问题              

                item.MKOption(htmldoc2);
              //  item.MKEvent(htmldoc2);

                //执行提交或者超链接必须确保加载完成
                if (item is MKButton || item is MKLink)
                {
                    CheckPageComplited();
                }
            }
        }

        /// <summary>
        /// 检查是否存在页面
        /// </summary>
        /// <returns>存在true,否则false</returns>
        public bool IsExctingPage()
        {
            return GetPage(_pageUrl, _pageTitle) == null ? false : true;
        }

        #endregion

        #region private method

        /// <summary>
        ///解析xml，获取当前page页面以及相关控件操作。
        /// </summary>
        private void AnalyzeXmlScript()
        {
               XmlNode root=null;
          if (!string.IsNullOrEmpty(_pageSource))
            {
                XmlDocument xmld = new XmlDocument();
                xmld.Load(_pageSource);
                root = xmld.DocumentElement;
            }

            foreach (XmlNode item in _pageNode.FirstChild.ChildNodes)
            {
                //todo:根据type类型来实例化控件，通过工厂模式创建相关控件信息。
                _mkControls.Add(GenerageControlFactory(item,root));
            }
            
        }

        /// <summary>
        /// 根据URL,或者标题获取指定页面
        /// </summary>
        /// <param name="itemUrl">url</param>
        /// <param name="itemTitle">标题</param>
        /// <returns>成功则返回对象，null则返回失败</returns>
        private SHDocVw.InternetExplorer GetPage(string itemUrl, string itemTitle)
        {
            SHDocVw.ShellWindows sh = new SHDocVw.ShellWindows();
            SHDocVw.InternetExplorer tmpIE = null;

            string tmpName;

            if (string.IsNullOrEmpty(itemUrl))
            {
                return null;
            }

            foreach (SHDocVw.InternetExplorer item in sh)
            {
                tmpName = item.Name;

                if (tmpName == Constants.IE)
                {
                    if (!string.IsNullOrEmpty(itemTitle))
                    {
                        if (item.LocationName == itemTitle && item.LocationURL == itemUrl)
                        {
                            tmpIE = item;
                            break;
                        }
                    }
                    else
                    {
                        if (item.LocationURL == itemUrl)
                        {
                            tmpIE = item;
                            break;
                        }
                    }
                }
            }
      
            return tmpIE;
        }

        /// <summary>
        /// 根据相关节点创建对应控件对象
        /// </summary>
        /// <param name="tmpNode"></param>
        /// <returns></returns>
        private MKBaseControl GenerageControlFactory(XmlNode tmpNode,XmlNode sourceDoc)
        {
            MKBaseControl mkControl = null;
            string tmpType = tmpNode.Attributes["type"].Value;
            Type t = Type.GetType(Constants.MASTERKEY + tmpType);
            Object obj = Activator.CreateInstance(t, new object[] { tmpNode });
            mkControl = obj as MKBaseControl;

            if (obj == null) {
                throw new Exception(Constants.UNKNOWTYPE);
            }

            if (!String.IsNullOrEmpty(_pageSource))
            {
               ReadValue(sourceDoc, mkControl);
            }

            return mkControl;
        }

        /// <summary>
        /// 检查浏览器是否完全加载成功，循环超过timeout则退出。
        /// </summary>
        private void CheckPageComplited()
        {
            _sw.Reset();
            _sw.Start();

            while (true)
            {
                if (_sw.ElapsedMilliseconds <= 30000)
                {
                    Thread.Sleep(500);

                    if ((_speicfiedyIPage.Busy) || _speicfiedyIPage.ReadyState != tagREADYSTATE.READYSTATE_COMPLETE)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    //加载时间超时；
                    //todo
                    throw new Exception(Constants.LOADFAILED);           
                }
            }
        }

        private void CreatePage()
        {            
           Process.Start("IEXPLORE",_pageUrl);
           Thread.Sleep(2000);
        }

        #endregion

        /// <summary>
        /// 读取xml值
        /// </summary>
        /// <param name="root">xml根文件</param>
        /// <param name="control">控件</param>
        public void  ReadValue(XmlNode root,MKBaseControl control)
        {
           String tmpIDName = control.ControlId == string.Empty ? control.ControlName : control.ControlId;

            XmlNode tmpNode = root.SelectSingleNode(tmpIDName);

            //存在该节点，并且该节点是只读节点
            if (tmpNode != null && control.Method==2)
            {
                control.ControlValue = tmpNode.InnerText;
            }
        }
    }
}
