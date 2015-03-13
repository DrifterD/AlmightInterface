using System;
using System.Collections.Generic;
using System.Text;

namespace WebMasterkey
{
   public interface IMKTask
    {
       int Period { get; set; }

       string XmlPath { get; set; }

        void StopTask();

        void StartTask();
    }
}
