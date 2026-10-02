// <copyright file="Program.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using SpreadSheetEngine;

namespace HW5
{
    /// <summary>
    /// Program that runs console app.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        internal static void Main()
        {
            RunApp();
        }

        /// <summary>
        /// Prints a menu.
        /// </summary>
        /// <param name="currentExpression"> prints current expression. </param>
        internal static void PrintMenu(string currentExpression)
        {
            Console.WriteLine("Menu: (Current Expression = {0} )", currentExpression);
            Console.WriteLine("1. Enter new expression.");
            Console.WriteLine("2. Set a variable value.");
            Console.WriteLine("3. Evaluate tree.");
            Console.WriteLine("4. Quit.");
        }

        /// <summary>
        /// Runs the console application.
        /// </summary>
        internal static void RunApp()
        {
            string expression = "1+2+3";
            ExpressionTree expTree = new ExpressionTree(expression);
            int option = 0;
            string name = string.Empty;
            double value = 0;
            do
            {
                PrintMenu(expression);
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter a new expression: ");
                        expression = Console.ReadLine();
                        expTree = new ExpressionTree(expression);
                        break;
                    case 2:
                        Console.WriteLine("Enter variable name: ");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter variable value: ");
                        value = Convert.ToDouble(Console.ReadLine());
                        expTree.SetVariable(name, value);
                        break;
                    case 3:
                        Console.WriteLine(expTree.Evaluate());
                        break;
                    case 4:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Select a valid option.");
                        break;
                }
            }
            while (option < 4 && option > 0);
        }
    }
}