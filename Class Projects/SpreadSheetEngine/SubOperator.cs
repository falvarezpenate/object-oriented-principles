// <copyright file="SubOperator.cs" company="Flavio Alvarez Penate">
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
    /// Subtraction operator subclass.
    /// </summary>
    internal class SubOperator : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubOperator"/> class.
        /// </summary>
        /// <param name="op"> char type. </param>
        public SubOperator()
            : base('-')
        {
            this.Left = null;
            this.Right = null;
            this.Precedence = 1;
        }

        /// <summary>
        /// Evaluates subtraction.
        /// </summary>
        /// <returns> double. </returns>
        public override double EvaluateOperator()
        {
            return this.Left.Evaluate() - this.Right.Evaluate();
        }
    }
}
