// <copyright file="ConstantNode.cs" company="Flavio Alvarez Penate">
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
    /// Inherits from Node class. One of the possible 3 types of nodes for the tree.
    /// </summary>
    internal class ConstantNode : Node
    {
        /// <summary>
        /// attribute for keeping track of a constant node's value.
        /// </summary>
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value"> double type. </param>
        public ConstantNode(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets or sets the value attribute.
        /// </summary>
        public double Value { get => this.value; set => this.value = value; }

        /// <summary>
        /// Evaluates a constant Node.
        /// </summary>
        /// <returns> double. </returns>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
