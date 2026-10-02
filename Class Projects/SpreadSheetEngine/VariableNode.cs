// <copyright file="VariableNode.cs" company="Flavio Alvarez Penate">
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
    internal class VariableNode : Node
    {
        /// <summary>
        /// attribute for keeping track of a constant node's value.
        /// </summary>
        private string name;

        /// <summary>
        /// value of variable.
        /// </summary>
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name"> string type. </param>
        public VariableNode(string name)
        {
            this.name = name;
            this.value = 0.0;
        }

        /// <summary>
        /// Gets or sets the name attribute.
        /// </summary>
        public string Name { get => this.name; set => this.name = value; }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public double Value { get => this.value; set => this.value = value; }

        /// <summary>
        /// Evaluates variable node.
        /// </summary>
        /// <returns> double. </returns>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
