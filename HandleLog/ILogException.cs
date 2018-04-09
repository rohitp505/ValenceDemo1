using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleLog
{
   public interface ILogException
    {
        void LogException(string errormessage, string StackTrace, string Source);
    }
}
