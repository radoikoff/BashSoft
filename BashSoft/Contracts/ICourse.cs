using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Contracts
{
    public interface ICourse
    {
        string Name { get; }

        IReadOnlyDictionary<string, IStudent> StudentsByName { get;}

        void EnrollStudent(IStudent student);
    }
}
