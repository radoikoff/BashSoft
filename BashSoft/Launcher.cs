using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public class Launcher
    {
        public static void Main()
        {
            IOManager.TraverseDirectory(@"C:\Users\vradoyko\Projects");
        }
    }
}
