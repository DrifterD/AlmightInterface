using System;
using System.Collections.Generic;
using System.Text;

namespace AlmightInterface
{
    public class CheckObject
    {

        public SHDocVw.InternetExplorer CheckControls(string itemUrl, string itemTitle = "")
        {
            SHDocVw.ShellWindows sh = new SHDocVw.ShellWindows();
            string tmpName;
            SHDocVw.InternetExplorer tmpIE=new SHDocVw.InternetExplorer();

            if (string.IsNullOrEmpty(itemUrl)) return tmpIE;

            foreach (SHDocVw.InternetExplorer item in sh)
            {
                tmpName = item.Name;

                if (tmpName == "Internet Explorer")
                {
                    if (!string.IsNullOrEmpty(itemTitle))
                    {
                        if (item.LocationName == itemTitle && item.LocationURL == itemUrl)
                        {
                            tmpIE = item;
                        }
                    }
                    else
                    {
                        if (item.LocationURL == itemUrl)
                        {
                            tmpIE = item;
                        }
                    }
                }
            }
            return tmpIE;
        }
    }
}
