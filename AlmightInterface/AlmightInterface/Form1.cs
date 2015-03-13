using System;
using System.Windows.Forms;
using System.Threading;

namespace AlmightInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SHDocVw.ShellWindows sh = new SHDocVw.ShellWindows();

            string tmpName;

            foreach (SHDocVw.InternetExplorer item in sh)
            {
                tmpName = item.Name;

                if (tmpName == "Internet Explorer")
                {
                    if (item.LocationName == "百度一下，你就知道") {
                        mshtml.IHTMLDocument2 htmlContent = item.Document as mshtml.IHTMLDocument2;

                        

                        textBox1.Text = htmlContent.body.outerHTML;
                        htmlContent.parentWindow.execScript("function confirm(\"are you ok?\"){return true;}", "javascript");

                        mshtml.IHTMLElementCollection inputs = (mshtml.IHTMLElementCollection)htmlContent.all.tags("INPUT");
                        mshtml.IHTMLElementCollection inputsbaidu = (mshtml.IHTMLElementCollection)htmlContent.all.item("百度一下", 0);
                        
                       mshtml.HTMLInputElement tmpinput= (mshtml.HTMLInputElement)inputs.item("kw", 0);
                  
   
                       mshtml.HTMLInputElement submit =(mshtml.HTMLInputElement) inputs.item("su", 0);
                       submit.click();

                        

                    }                    
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Interface1 t1 = new Class2();

            MessageBox.Show(t1.Say()+"add method"+t1.Add(1,1));


           



        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Url =new Uri("http://baidu.com/");            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
