using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using WebMasterkey;
namespace TestTechnic
{

    class Program
    {
        #region API
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd,
    uint Msg, int wParam, string lParam);

        [DllImport("User32.DLL")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent,
            IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("User32.DLL")]
        public static extern IntPtr WindowFromPoint(Point rpoint);

        [DllImport("User32.DLL")]
        public static extern int GetWindowText(
            IntPtr hWnd,
            StringBuilder ipString,
            int nMaxCount);

        [DllImport("User32.DLL")]
        private static extern int GetClassName(
            IntPtr hWnd,
            StringBuilder ipString,
            int nMaxCount
            );

        #endregion


        public const uint WM_SETTEXT = 0x000C;

        [STAThread]
        static void Main(string[] args)
        {      
                try
                {
                 MKTask   task = new MKTask(Application.StartupPath + "\\" + "zckp.xml");
                    task.StartTask();
                    //while (true)
                    //{
                    //    Command();
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

            #region API test
            //while (true)
            //{
            //    int x = Cursor.Position.X;
            //    int y = Cursor.Position.Y;

            //    IntPtr formHandel = WindowFromPoint(new Point(x, y));

            //    StringBuilder title = new StringBuilder(256);
            //    StringBuilder classStr = new StringBuilder(256);

            //    GetWindowText(formHandel, title, title.Capacity);
            //    GetClassName(formHandel, classStr, classStr.Capacity);

            //    Console.WriteLine("The title is :" + title.ToString());
            //    Console.WriteLine("The class name is " + classStr.ToString());
            //    Console.WriteLine("The handle is " + formHandel.ToString());
            //    Console.WriteLine("The cursor point : x=" + x + " y=" + y);

            //    //Console.WriteLine("please input ")
            //}

            #endregion
            Console.ReadKey();
        }

        public static void Command()
        {
            MKTask task = null;
            string tmpComand = String.Empty;

            tmpComand = Console.ReadLine();

            if (tmpComand.Trim() == "start")
            {
                Console.WriteLine("please input xml name:");

                tmpComand = Console.ReadLine();

                if (!string.IsNullOrEmpty(tmpComand))
                {
                    task = new MKTask(Application.StartupPath + "\\" + tmpComand);
                    task.StartTask();
                }

            }

            if (tmpComand.Trim() == "stop")
            {
                task.Save();
            }

        }
    }
}
