using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ReflectionTest
{
    internal class Program
    {
        private const string WRONG_PATH = "Wrong path";
        private const string NO_DLL = "There's no dll files";

        private static void Main(string[] args)
        {
            string path = args.Length == 1 ?
                args[0] : InputArguments();
            if (Directory.Exists(path))
            {
                string[] dlls = Directory.GetFiles(path, "*.dll");
                if (dlls.Length==0)
                {
                    Console.WriteLine(NO_DLL);
                }
                Assembly assembly;
                foreach (string file in dlls)
                {
                    assembly = Assembly.LoadFile(Path.GetFullPath(file));
                    foreach (Type type in assembly.GetTypes())
                    {
                        PrintTypeMethods(type);
                    }
                }
            }
            else
            {
                Console.WriteLine(WRONG_PATH);
            }
        }
        private static string InputArguments()
        {
            Console.WriteLine("Write path to directory with dll's");
            return Console.ReadLine();
        }
        private static void PrintTypeMethods(Type type)
        {
            List<string> methods = new List<string>();
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                methods.Add(method.Name);
            }
            foreach (MethodInfo method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (!method.IsPrivate)
                {
                    methods.Add(method.Name);
                }
            }
            if (methods.Count>0)
            {
                Console.WriteLine(type.Name);
                foreach (var methodName in methods)
                {
                    Console.WriteLine("\t" + methodName);
                }
            }
        }
    }
}
