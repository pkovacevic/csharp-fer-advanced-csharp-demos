using System;
using System.Globalization;

namespace ExtensionMethodsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "3.41";


            //double d = StringUtilities.ToDouble(s);
            double d = s.ToDouble();

        }
    }

    // Option #1: Create your own string
    // Does not work! String is marked as "sealed", and we cannot inherit from it.
    //sealed class MyString : string
    //{

    //}

    // Option #2: Add utilities class that will encapsualte this functionality.
    // Works but not as elegant syntax as someString.ToDouble();
    //public class StringUtilities
    //{
    //    public static double ToDouble(string s)
    //    {
    //        var d = double.Parse(s);
    //        return d;
    //    }
    //}

    // Option #3: Extension methods
    // Perfect!
    public static class StringUtilities
    {
        public static double ToDouble(this string s)
        {
            var d = double.Parse(s);
            return d;
        }
    }

}
