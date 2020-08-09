using System;
using System.Diagnostics;

namespace ObjectSimSystem
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                var objectSimSystem = new ObjectSimSystem();

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
