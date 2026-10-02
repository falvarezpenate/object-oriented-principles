// <copyright file="FibonacciTextReader.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    /// <summary>
    /// Loads a textbox with the determined number of Fibonacci Sequence values.
    /// </summary>
    public class FibonacciTextReader : TextReader
    {
        /// <summary>
        /// keeps track of max number of lines/fibonacci values to be loaded into textbox.
        /// </summary>
        private int maxLines;

        /// <summary>
        /// Contains the nextLine item to return with ReadLine() method. Gets updated every iteration.
        /// </summary>
        private BigInteger nextLine;

        /// <summary>
        /// Equates to n-1th index of current Fibonacci location.
        /// </summary>
        private BigInteger previousFirst;

        /// <summary>
        /// Equates to n-2th index of current Fibonacci location.
        /// </summary>
        private BigInteger previousSecond;

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// </summary>
        /// <param name="newMaxLines"> Maximum number of lines of fibonacci values to be loaded into textbox. </param>
        public FibonacciTextReader(int newMaxLines)
        {
            this.maxLines = newMaxLines;
            this.nextLine = 0;
            this.previousFirst = 1;
            this.previousSecond = 1;
        }

        /// <summary>
        /// Override the ReadLine method which delivers the next number (as a string) in the
        /// Fibonaci sequence.
        /// </summary>
        /// <returns> the next int in the fibonacci sequence as a string. </returns>
        public override string? ReadLine()
        {
            if (this.maxLines > 0)
            {
                string temp = Convert.ToString(this.nextLine);
                this.CalculateNextLine();
                this.maxLines--; // decrease the number of lines left to write.
                return temp;
            }

            // maxLines number of iterations have passed. Time to pass empty string since there's nothing to write.
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Function that calculates next Fibonacci Sequence number.
        /// </summary>
        public void CalculateNextLine()
        {
            this.previousFirst = this.previousSecond;
            this.previousSecond = this.nextLine;
            this.nextLine = this.previousFirst + this.previousSecond;
        }
    }
}
