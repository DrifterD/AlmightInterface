
using System;
using System.Collections.Generic;
using System.Text;

namespace AlmightInterface
{
    class Class3:Class2
    {

        public override int Add(int a, int b)
        {
            return base.Add(a+10, b);
        }

    }
}
