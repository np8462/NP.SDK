using System;
using System.IO;

namespace NP.SDK.Core.IO.FileTools
{
    public static class TextFileJoiner
    {
        public static void Join(TextFileJoinOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            if (options.InputFiles == null || options.InputFiles.Count == 0)
                throw new InvalidOperationException("No input files selected.");

            if (string.IsNullOrWhiteSpace(options.OutputFile))
                throw new InvalidOperationException("Output file is not specified.");

            using (StreamWriter writer =
                new StreamWriter(options.OutputFile, false, options.Encoding))
            {
                foreach (string file in options.InputFiles)
                {
                    if (!File.Exists(file))
                        continue;

                    if (options.AddFileHeaders)
                    {
                        writer.WriteLine("//============================================================");
                        writer.WriteLine("// File : " + Path.GetFileName(file));
                        writer.WriteLine("// Path : " + file);
                        writer.WriteLine("//============================================================");
                        writer.WriteLine();
                    }

                    using (StreamReader reader =
                        new StreamReader(file, options.Encoding))
                    {
                        while (!reader.EndOfStream)
                        {
                            writer.WriteLine(reader.ReadLine());
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine();
                }
            }
        }
    }
}