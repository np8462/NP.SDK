using System.Collections.Generic;
using System.Text;

namespace NP.SDK.Core.IO.FileTools
{
    public class TextFileJoinOptions
    {
        public IList<string> InputFiles { get; private set; }

        public string OutputFile { get; set; }

        public bool AddFileHeaders { get; set; }

        public Encoding Encoding { get; set; }

        public TextFileJoinOptions()
        {
            InputFiles = new List<string>();

            Encoding = Encoding.UTF8;

            AddFileHeaders = true;
        }
    }
}