using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows;
using System.CodeDom;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.Xml;

namespace ConsoleTester
{
    internal static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;


            using (XmlReader reader = XmlReader.Create("D:\\package.xml", settings))
            {
                while (reader.Read())
                {
                    Console.Write(reader.Value);
                }
            }
            PressAnyKey();
            return 0;
        }

        private static void PressAnyKey()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }



    }
}
