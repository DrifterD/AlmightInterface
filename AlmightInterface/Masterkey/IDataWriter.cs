using System;
using System.Collections.Generic;
using System.Text;

namespace WebMasterkey
{
    interface IDataWriter
    {

        /// <summary>
        /// 保存读取数据(默认地址：日期+当前程序/data)
        /// </summary>
        void Save();

        /// <summary>
        /// 保存读取数据
        /// </summary>
        /// <param name="filepath">文件全名</param>
        void Save(String filepath);


    }
}
