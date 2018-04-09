using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleLog
{
    public sealed class logGenerator : ILogException
    {
        /// <summary>
        /// lazy call with singleton pattern
        /// </summary>
        private logGenerator()
        {
        }
        private static readonly Lazy<logGenerator> instanceloggenerator = new Lazy<logGenerator>(() => new logGenerator());

        public static logGenerator CreateInstance
        {
            get
            {
                return instanceloggenerator.Value;
            }
        }
        /// <summary>
        /// Log file creation 
        /// </summary>
        /// <param name="errormessage"></param>
        public void LogException(string errormessage,string StackTrace,string Source)
        {
            string fileName = string.Format("{0}_{1}.log", "Exception" , DateTime.Now.ToString("dd-MMM-yyyy"));
            string logFilePath = string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
            StringBuilder stringbuilder = new StringBuilder();
            stringbuilder.AppendLine("--------------------------------------------------");
            stringbuilder.AppendLine(DateTime.Now.ToString());
            stringbuilder.AppendLine(errormessage);
            stringbuilder.AppendLine(StackTrace);
            stringbuilder.AppendLine(Source);
            using (StreamWriter streamwriter = new StreamWriter(logFilePath, true))
            {
                streamwriter.Write(stringbuilder.ToString());
                streamwriter.Flush();
            }

        }
    }
}
