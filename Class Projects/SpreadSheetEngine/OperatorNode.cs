// <copyright file="OperatorNode.cs" company="Flavio Alvarez Penate">
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
    /// One of the three types of Nodes that inherit from that abstract class.
    /// </summary>
    internal abstract class OperatorNode : Node
    {
        /// <summary>
        /// Character that keeps track of what type of operator it is.
        /// </summary>
        private char op;

        /// <summary>
        /// Int that keeps track of precedence.
        /// </summary>
        private int precedence;

        /// <summary>
        /// Keeps track of the node left of the operator.
        /// </summary>
        private Node left;

        /// <summary>
        /// Keeps track of the node right of the operator.
        /// </summary>
        private Node right;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="newOperator"> char type. </param>
        public OperatorNode(char newOperator)
        {
            this.op = newOperator;
            this.left = null;
            this.right = null;
            this.precedence = -1;
        }

        /// <summary>
        /// Gets or sets the operator char.
        /// </summary>
        public char Operator { get => this.op; set => this.op = value; }

        /// <summary>
        /// Sets or gets precedence.
        /// </summary>
        public int Precedence { get => this.precedence; set => this.precedence = value; }

        /// <summary>
        /// Gets or sets the left node.
        /// </summary>
        public Node Left { get => this.left; set => this.left = value; }

        /// <summary>
        /// Gets or sets the right node.
        /// </summary>
        public Node Right { get => this.right; set => this.right = value; }

        /// <summary>
        /// Overrides abstract method.
        /// </summary>
        /// <returns> Evaluate operator method. </returns>
        public override double Evaluate()
        {
            return this.EvaluateOperator();
        }

        /// <summary>
        /// Abstract method that needs to be implemented by each subclass.
        /// </summary>
        /// <returns> double type. </returns>
        public abstract double EvaluateOperator();
    }
}
