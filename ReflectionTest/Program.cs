using System;
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
            DisplayMethods(path);
        }
        private static string InputArguments()
        {
            Console.WriteLine("Write path to directory with dll's");
            return Console.ReadLine();
        }
        public static void DisplayMethods(string path)
        {
            const string WRONG_PATH = "Wrong path";
            const string NO_DLL = "There's no dll files";

            if (Directory.Exists(path))
            {
                string[] dlls = Directory.GetFiles(path, "*.dll");
                if (dlls.Length == 0)
                {
                    Console.WriteLine(NO_DLL);
                }
                Assembly assembly;
                foreach (string file in dlls)
                {
                    assembly = Assembly.LoadFile(Path.GetFullPath(file));
                    foreach (Type type in assembly.GetTypes())
                    {
                        #region Output
                        Console.WriteLine(type.Name);
                        foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                        {
                            Console.WriteLine("\t" + method.Name);
                        }
                        foreach (MethodInfo method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
                        {
                            if (!method.IsPrivate)
                            {
                                Console.WriteLine("\t" + method.Name);
                            }
                        }
                        #endregion
                    }
                }
            }
            else
            {
                Console.WriteLine(WRONG_PATH);
            }
        }
    }
}
