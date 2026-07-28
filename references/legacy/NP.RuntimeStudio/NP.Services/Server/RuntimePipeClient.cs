using System.IO;
using System.IO.Pipes;
using System.Text;

namespace NP.Services.Server
{
    public class RuntimePipeClient
    {
        private const string PipeName =
            "NP.Runtime";

        public bool Send(string json)
        {
            try
            {
                using (NamedPipeClientStream pipe =
                    new NamedPipeClientStream(
                        ".",
                        PipeName,
                        PipeDirection.Out))
                {
                    pipe.Connect(300);

                    using (StreamWriter writer =
                        new StreamWriter(pipe))
                    {
                        writer.AutoFlush = true;

                        writer.WriteLine(json);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}