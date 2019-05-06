using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberToCurrency;


namespace NumberToCurrency
{
    class Program
    {


        static void Main(string[] args)
        {
            //The contents of main provide a simple interface for users to manually enter numbers to convert values
            //The actual method can simply be called by creating an instance of the CoverterClass (see line 20) and running
            //ConvertNumbersToWords(input) making sure to replace input with a string representation of the desired number (see line 40)
            ConverterClass cc = new ConverterClass();
            string input = "";
            bool inputCorrect = false;
            bool rerun = false;
            do
            {
                while (!inputCorrect)
                {
                    try
                    {
                        Console.WriteLine("Please provide a dollar value between -2147483647.99 and 2147483647.99 inclusive e.g. $12.30 is entered as 12.30");
                        input = Console.ReadLine();
                        double test = double.Parse(input);
                        inputCorrect = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("It looks like there's a mistake in the format of your number, please try re-entering it.");
                    }
                }
                Console.WriteLine(cc.ConvertNumbersToWords(input));

                string yn = "";
                while (yn != "y" && yn != "n")
                {
                    Console.WriteLine("Would you like to try another value? (y/n)");
                    yn = Console.ReadLine();
                    if (yn == "y")
                    {
                        rerun = true;
                        inputCorrect = false;
                    }
                    else if (yn == "n")
                    {
                        rerun = false;
                    }
                }
            } while (rerun);
        }
    }
}
