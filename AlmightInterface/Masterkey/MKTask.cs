using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using System.Timers;
using System.IO;

namespace WebMasterkey
{
    public class MKTask : IMKTask,IDataWriter
    {
        private int _period;

        public int Period
        {
            get { return _period; }
            set { _period = value; }
        }
        private string _xmlPath;

        public string XmlPath
        {
            get { return _xmlPath; }
            set { _xmlPath = value; }
        }

        private Timer _taskTimer;

        private List<MKPage> _pages;


        public MKTask(string filepath)
        {
            _xmlPath = filepath;
            _pages = new List<MKPage>();
            _taskTimer = new Timer();
            AnalyzeXmlScript();           
            
        }


        public void StopTask()
        {
            _taskTimer.Stop();
        }

        public void StartTask()
        {        
            if (_period == -1)
            {
                TaskEvent();
            }
            else
            {
                _taskTimer.Elapsed += ElapsedEventHandler;
                _taskTimer.Interval = _period;
                _taskTimer.Start();
            }
        }

        public void Save()
        {
            string filePath = DateTime.Now.ToString("yyyymmddHHMMSS") + ".xml";
            Save(filePath);
        }

        public void Save(string filepath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("values");

            foreach (MKPage item in _pages)
            {
                foreach (MKBaseControl itemMK in item.MkControls)
                {
                    MKBaseControl tmp = itemMK as MKText;
                    if (tmp != null)
                    {
                        XmlElement tmpEle = xmlDoc.CreateElement(tmp.ControlId == String.Empty ? tmp.ControlName : tmp.ControlId);
                        tmpEle.Value = tmp.ControlValue;
                        root.AppendChild(tmpEle);
                    }
                }
            }

            xmlDoc.AppendChild(root);
            xmlDoc.Save(filepath);
        }


        #region private method

        private void AnalyzeXmlScript()
        {
            XmlDocument xmd = new XmlDocument();

            xmd.Load(_xmlPath);
            XmlNode root=xmd.DocumentElement;
            XmlNode pageNode = root.ChildNodes[0];

            CheckPeriod(root.Attributes[0].Value);

            foreach (XmlNode item in pageNode.ChildNodes)
            {
                MKPage tmpPage = new MKPage(item);
                _pages.Add(tmpPage);
            }

        }

        /// <summary>
        /// 检查period参数
        /// </summary>
        /// <param name="attrVaue">period参数</param>
        /// <returns></returns>
        private void CheckPeriod(string attrVaue)
        {
            if (!Int32.TryParse(attrVaue, out _period))
            {
                _period = -1;
            }
            else
            { 
                if (_period==0)
                {
                    _period=3;
                }
            }
        }

        private void ElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            TaskEvent();
        }

        private void TaskEvent()
        {
            foreach (MKPage item in _pages)
            {
                item.execOptions();
            }
        }

        #endregion


      
    }
}
