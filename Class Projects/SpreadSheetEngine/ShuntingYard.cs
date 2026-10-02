// <copyright file="ShuntingYard.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetEngine
{
    /// <summary>
    /// This class takes a infix notation calculation and turns it into a postfix notation formula.
    /// </summary>
    public class ShuntingYard
    {
        /// <summary>
        /// infix notation string.
        /// </summary>
        private string infix;

        /// <summary>
        /// postfix notation result.
        /// </summary>
        private string postfix;

        /// <summary>
        /// stack to hold operators for postfix string creation.
        /// </summary>
        private Stack<char> operatorStack;

        /// <summary>
        /// Operator factory that handles precedence.
        /// </summary>
        private OperatorFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShuntingYard"/> class.
        /// </summary>
        /// <param name="infix"> string type. </param>
        public ShuntingYard(string infix)
        {
            this.infix = infix;
            this.postfix = string.Empty;
            this.operatorStack = new Stack<char>();
            this.factory = new OperatorFactory();
        }

        /// <summary>
        /// Gets or sets infix attribute.
        /// </summary>
        public string Infix { get => this.infix; set => this.infix = value; }

        /// <summary>
        /// Gets postfix result.
        /// </summary>
        public string Postfix { get => this.InfixToPostfix(); }

        /// <summary>
        /// Takes the infix notation formula entered and outputs it in postfix notation.
        /// </summary>
        /// <returns> a string in postfix notation. </returns>
        /// <exception> Doesn't handle parenthesis yet. </exception>
        /// <exception> Doesn't handle order of operations since we're assuming only 1 operator present at a time. </exception>
        private string InfixToPostfix()
        {
            for (int i = 0; i < this.infix.Length; i++)
            {
                char c = this.infix[i];

                // Open Parenthesis encountered.
                if (c == '(')
                {
                    this.operatorStack.Push(c);
                }

                // Closed Parenthesis encountered.
                else if (c == ')')
                {
                    // until the matching parenthesis is found, pop all operators
                    while (this.operatorStack.Peek() != '(' && this.operatorStack.Count > 0)
                    {
                        this.postfix += this.operatorStack.Pop();
                        this.postfix += ' '; // add a space between operators.
                    }

                    if (this.operatorStack.Peek() == '(')
                    {
                        this.operatorStack.Pop(); // pop the '(' from stack
                    }
                }

                // Constant encountered.
                else if (char.IsDigit(c))
                {
                    this.postfix += c;

                    // need to get the entire number not jsut the first digit.
                    if (i < this.infix.Length - 1)
                    {
                        i++;
                        c = this.infix[i];

                        // until an operator is found.
                        while (char.IsDigit(c) && i < this.infix.Length)
                        {
                            this.postfix += c;
                            i++;

                            // if this isn't added, program tries to get an index out of bounds when at the end of the expresssion
                            if (i < this.infix.Length)
                            {
                                c = this.infix[i];
                            }
                        }

                        i--; // go back an index since i gets increased one additional time after the final loop runs.
                    }

                    this.postfix += ' '; // separate each new operand/operator by space.
                }

                 // Variable encountered.
                else if (char.IsLetter(c))
                {
                    this.postfix += c;

                    // need to get the entire variable not jsut the first char.
                    if (i < this.infix.Length - 1)
                    {
                        i++;
                        c = this.infix[i];

                        // until an operator is found.
                        // same logic as getting a multiple digit number.
                        while (char.IsLetterOrDigit(c) && i < this.infix.Length)
                        {
                            this.postfix += c;
                            i++;
                            if (i < this.infix.Length)
                            {
                                c = this.infix[i];
                            }
                        }

                        i--;
                    }

                    this.postfix += ' '; // separate each new operand/operator by space.
                }

                // Operator encountered.
                else
                {
                    // account for precedence if the operator stack is not empty.
                    while (this.operatorStack.Count > 0 &&
                        this.factory.GetPrecedence(c) <= this.factory.GetPrecedence(this.operatorStack.Peek()))
                    {
                        this.postfix += this.operatorStack.Pop();
                        this.postfix += ' '; // separate each new operand/operator by space.
                    }

                    this.operatorStack.Push(c); // operator ready to be pushed to stack and follow correct precedence.
                }
            }

            // Pop all the operators from stack.
            while (this.operatorStack.Count > 0)
            {
                this.postfix += this.operatorStack.Pop();
                this.postfix += ' '; // separate each new operand/operator by space.
            }

            this.postfix = this.postfix.TrimEnd(); // get rid of trailing whitespace

            return this.postfix;
        }
    }
}
