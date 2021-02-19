using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_Of_Hanoi_DMProject
{
    public class MoveInfo
    {
        public string src;
        public string dest;
        public int number;

        public MoveInfo(string s, string d, int n)
        {
            src = s;
            dest = d;
            number = n;
        }
    }
}
