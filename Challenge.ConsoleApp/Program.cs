using Challenge.Core;
using System;
using System.IO;
using static Challenge.Core.CertificateManager;

namespace Challenge.ConsoleApp
{
    public class Program
    {


        public static void Main(string[] args)
        {
            if(args.Length < 2)
            {
                Console.WriteLine("Error: Wrong arguments");
                return;
            }

            string command = args[0];

            if (command == "add")
            {
                string path = args[1];

                Add(path);
            }
            else if (command == "remove")
            {
                string path = args[1];

                Remove(path);
            }
            else
            {
                Console.WriteLine("Error: Command doesn't exist");
            }
        }

        public static void Add(string path)
        {
            try
            {
                byte[] data = File.ReadAllBytes(path);

                CertificateManager manager = new CertificateManager();
                ResponseType response = manager.AddCertificate(data);

                if(response == ResponseType.Success)
                {
                    Console.WriteLine("Added Certificate: "+Path.GetFullPath(path));
                }
                else
                {
                    Console.WriteLine("Error: " + response.ToString());
                }
            }
            catch
            {
                Console.WriteLine("Error: File doesn't exist at: "+Path.GetFullPath(path));
            }
        }

        public static void Remove(string path)
        {
            try
            {
                byte[] data = File.ReadAllBytes(path);

                CertificateManager manager = new CertificateManager();
                ResponseType response = manager.RemoveCertificate(data);

                if (response == ResponseType.Success)
                {
                    Console.WriteLine("Removed Certificate: " + Path.GetFullPath(path));
                }
                else
                {
                    Console.WriteLine("Error: " + response.ToString());
                }
            }
            catch
            {
                Console.WriteLine("Error: File doesn't exist at: " + Path.GetFullPath(path));
            }
        }
    }
}
