using System;
using System.Diagnostics;

namespace MappingSystem
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                var mappingSystem = new MappingSystem();

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }
        }
    }
}
