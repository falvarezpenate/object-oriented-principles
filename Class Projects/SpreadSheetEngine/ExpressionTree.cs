// <copyright file="ExpressionTree.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetEngine
{
    /// <summary>
    /// Class that will handle formulas for SpreadSheet.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Root of the expression tree.
        /// </summary>
        private Node root;

        /// <summary>
        /// Dictionary keeping track of all variables and their values.
        /// </summary>
        private Dictionary<string, double> variables;

        /// <summary>
        /// Stack of nodes for compilation of tree.
        /// </summary>
        private Stack<Node> treeStack;

        /// <summary>
        /// Operator factory for creating operators.
        /// </summary>
        private OperatorFactory opFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> string type. </param>
        public ExpressionTree(string expression)
        {
            this.root = null;
            this.variables = new Dictionary<string, double>();
            this.treeStack = new Stack<Node>();
            this.opFactory = new OperatorFactory();
            this.Compile(expression);
        }

        /// <summary>
        /// Sets specified variable within ExpressionTree variables dictionary.
        /// </summary>
        /// <param name="variableName"> string type. </param>
        /// <param name="variableValue"> double type. </param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variables[variableName] = variableValue;
        }

        /// <summary>
        /// Get keys from variables dictionary.
        /// </summary>
        /// <returns> array of variables names. </returns>
        public string[] GetVariableNames()
        {
            return this.variables.Keys.ToArray();
        }

        /// <summary>
        /// Encapsulates private Evaluate() method that requires parameters.
        /// </summary>
        /// <returns> double type. </returns>
        public double Evaluate()
        {
            return this.Evaluate(this.root);
        }

        /// <summary>
        /// Compiles a tree of expressions.
        /// </summary>
        /// <param name="expression"> string type. </param>
        private void Compile(string expression)
        {
            ShuntingYard sy = new ShuntingYard(expression);
            string postfixExp = sy.Postfix;

            // tokenize
            string[] tokens = postfixExp.Split(' ');

            foreach (string symbol in tokens)
            {
                // symbol is operand
                if (!this.opFactory.IsOperator(symbol))
                {
                    // a constant
                    if (char.IsDigit(symbol[0]))
                    {
                        ConstantNode cNode = new ConstantNode(Convert.ToDouble(symbol));
                        this.treeStack.Push(cNode);
                    }
                    else
                    {
                        VariableNode vNode = new VariableNode(symbol);

                        // we add to dictionary as default 0 in order to add key to dictionary
                        // however, there wont be any default values set to 0 due to try catch exception
                        // inside of spreadsheet. This is just so that GetVariableNames() doesnt return empty.
                        this.variables[symbol] = 0;
                        this.treeStack.Push(vNode);
                    }
                }

                // symbol is operator
                else
                {
                    OperatorNode opNode = this.opFactory.CreateOperatorNode(Convert.ToChar(symbol));
                    opNode.Right = this.treeStack.Pop();
                    opNode.Left = this.treeStack.Pop();
                    this.treeStack.Push(opNode);
                }
            }

            this.root = this.treeStack.Pop(); // set the root to the tree.
        }

        /// <summary>
        /// Private evaluate() method that returns a double through recursion of tree operations.
        /// </summary>
        /// <param name="node"> Node type. Any of the 3 types of nodes supported. </param>
        /// <returns> a double. </returns>
        /// <exception cref="NotSupportedException"> unsupported operator. </exception>
        private double Evaluate(Node node)
        {
            OperatorNode opNode = this.root as OperatorNode;
            if (opNode != null)
            {
                this.UpdateVariableNodes(opNode);
            }

            // try to evaluate the node as a constant
            // the "as" operator is evaluated to null
            // as opposed to throwing an exception
            ConstantNode constantNode = node as ConstantNode;
            if (constantNode != null)
            {
                return constantNode.Evaluate();
            }

            // as a variable
            VariableNode variableNode = node as VariableNode;
            if (variableNode != null)
            {
                if (this.variables.ContainsKey(variableNode.Name))
                {
                    return this.variables[variableNode.Name];
                }
            }

            // it is an operator node if we came here
            OperatorNode operatorNode = node as OperatorNode;
            if (operatorNode != null)
            {
                return operatorNode.EvaluateOperator();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Traverses tree and updates any variable nodes using dictionary.
        /// </summary>
        /// <param name="node"> Node type. </param>
        private void UpdateVariableNodes(Node node)
        {
            if (node == null)
            {
                return;
            }

            OperatorNode opNode = node as OperatorNode;

            // if it can be passed an operator node, then it contains a left and a right node.
            if (opNode != null)
            {
                this.UpdateVariableNodes(opNode.Left);
            }

            // if its a variable node we have to update its value using the dictionary.
            VariableNode variableNode = node as VariableNode;
            if (variableNode != null)
            {
                variableNode.Value = this.variables[variableNode.Name];
            }

            // perform same traversal.
            opNode = node as OperatorNode;
            if (opNode != null)
            {
                this.UpdateVariableNodes(opNode.Right);
            }
        }
    }
}
