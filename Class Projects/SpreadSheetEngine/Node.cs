// <copyright file="Node.cs" company="Flavio Alvarez Penate">
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
    /// Node class for ExpressionTree. Serves as an abstract type that unifies all different types of nodes to a single parent.
    /// </summary>
    public abstract class Node
    {
        public abstract double Evaluate();
    }
}
