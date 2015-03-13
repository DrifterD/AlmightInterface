using System;
using System.Collections.Generic;
using System.Text;

namespace AlmightInterface
{
    class Class2:Interface1
    {
        public string Say()
        {
            return "good";
        }

        public virtual int Add(int a, int b)
        {
            return a + b;
        }
    }
}
