using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Contracts
{
    public interface IDirectoryChanger
    {
        void ChangeCurrentDirectoryAbsolute(string absolutePath);
        void ChangeCurrentDirectoryRelative(string relativePath);
    }
}
