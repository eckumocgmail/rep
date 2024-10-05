using System.Reflection;
using System.Security.Cryptography.Xml;

namespace CustomAttributes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Assembly.GetCallingAssembly().GetName().Name);
            CustomAttributesUnit.Test();
        }
    }
}
