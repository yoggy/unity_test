using System;
using System.IO;
using System.IO.Pipes;

namespace SendNamedPipe
{
    class SendNamedPipe
    {
        static void Usage()
        {
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetEntryAssembly();
            string exe = Path.GetFileName(myAssembly.Location);

            Console.WriteLine($"");
            Console.WriteLine($"usage: {exe} named_pipe_name message");
            Console.WriteLine($"");
            Console.WriteLine($"example: ");
            Console.WriteLine($"    {exe} test1234 test_message...");
            Console.WriteLine($"");

            Environment.Exit(0);
        }


        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length != 3)
            {
                Usage();
            }

            string named_pipe_name = args[1];
            string message = args[2];

            var pipe = new NamedPipeClientStream(".", named_pipe_name, PipeDirection.Out);

            try
            {
                pipe.Connect(7000); // timeout
            }
            catch (Exception e)
            {
                Console.WriteLine($"error: {e}");
                Environment.Exit(0);
            }

            var sw = new StreamWriter(pipe);

            sw.WriteLine(message);
            Console.WriteLine($"info: sw.WriteLine() message={message}");

            sw.Close();
            pipe.Close();
        }
    }
}
