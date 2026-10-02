// <copyright file="AddOperator.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetEngine
{
    /// <summary>
    /// Operator class for addition.
    /// </summary>
    internal class AddOperator : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddOperator"/> class.
        /// </summary>
        /// <param name="op"> char type. </param>
        public AddOperator()
            : base('+')
        {
            this.Left = null;
            this.Right = null;
            this.Precedence = 1;
        }

        /// <summary>
        /// Evaluates addition.
        /// </summary>
        /// <returns> double type. </returns>
        public override double EvaluateOperator()
        {
            return this.Left.Evaluate() + this.Right.Evaluate();
        }
    }
}
