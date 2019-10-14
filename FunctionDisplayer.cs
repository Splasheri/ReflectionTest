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
                        if (methods.Count > 0)
                        {
                            Console.WriteLine(type.Name);
                            foreach (string methodName in methods)
                            {
                                Console.WriteLine("\t" + methodName);
                            }
                        }
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
