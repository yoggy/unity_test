using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiveNamePipe
{
    class ReceiveNamedPipe
    {
        static void Usage()
        {
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetEntryAssembly();
            string exe = Path.GetFileName(myAssembly.Location);

            Console.WriteLine($"");
            Console.WriteLine($"usage: {exe} named_pipe_name");
            Console.WriteLine($"");
            Console.WriteLine($"example: ");
            Console.WriteLine($"    {exe} test1234");
            Console.WriteLine($"");

            Environment.Exit(0);
        }


        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length != 2)
            {
                Usage();
            }

            string named_pipe_name = args[1];

            var pipe = new NamedPipeServerStream(named_pipe_name, PipeDirection.In);
            Console.WriteLine($"info: named_pipe_name={named_pipe_name}");
            pipe.WaitForConnection();

            if (pipe.IsConnected == false)
            {
                Console.WriteLine($"error: pipe.IsConnected == false");
                Environment.Exit(0);
            }
            Console.WriteLine($"info: pipe.IsConnected == true");

            StreamReader sr = new StreamReader(pipe);

            while (true)
            {
                string message = sr.ReadLine();
                if (!String.IsNullOrEmpty(message))
                {
                    Console.WriteLine($"info : sr.ReadLine() message={message}");
                }

                if (pipe.IsConnected == false)
                {
                    Console.WriteLine($"read : {message}");
                    break;
                }
            }

            sr.Close();
            pipe.Close();
        }
    }
}
