// <copyright file="DivOperator.cs" company="Flavio Alvarez Penate">
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
    /// Division Operator subclass.
    /// </summary>
    internal class DivOperator : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivOperator"/> class.
        /// </summary>
        /// <param name="op"> char type. </param>
        public DivOperator()
            : base('/')
        {
            this.Left = null;
            this.Right = null;
            this.Precedence = 2;
        }

        /// <summary>
        /// Evaluates Division.
        /// </summary>
        /// <returns> double type. </returns>
        public override double EvaluateOperator()
        {
            return this.Left.Evaluate() / this.Right.Evaluate();
        }
    }
}
