using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ReflectionTest
{
    public class FunctionDisplayer
    {
        public static void DisplayFunctions(string path)
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
