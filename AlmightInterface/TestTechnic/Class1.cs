using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTechnic
{
    class Class1:Interface1
    {

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
               _name=value;
            }
        }

        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age=value;
            }
        }

        public void ShowInfo()
        {
           Console.WriteLine("My name is "+_name+ ",  I am "+_age.ToString());
        }
    }
}
