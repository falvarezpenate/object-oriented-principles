// <copyright file="OperatorFactory.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetEngine
{
    /// <summary>
    /// Class that acts as a Factory of Operators and manages information regarding supported operators.
    /// </summary>
    internal class OperatorFactory
    {
        /// <summary>
        /// List of operator characters.
        /// </summary>
        private List<string> operatorList = new List<string>();

        /// <summary>
        /// Dictionary linking operators to their precedence.
        /// </summary>
        private Dictionary<char, int> precedence = new Dictionary<char, int>();

        /// <summary>
        /// dictionary of operator subclasses.
        /// </summary>
        private Dictionary<char, Type> operators = new Dictionary<char, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorFactory"/> class.
        /// </summary>
        public OperatorFactory()
        {
            this.TraverseAvailableOperators((op, type) => this.operators.Add(op, type));
        }

        /// <summary>
        /// Delegate for operator subclasses.
        /// </summary>
        /// <param name="op"> char type. </param>
        /// <param name="type"> Type of operator node. </param>
        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Gets the precedence of a operator from the dictionary.
        /// </summary>
        /// <param name="op"> char type. </param>
        /// <returns> an integer. The larger the more priority the op has. Ret -1 is not supported. </returns>
        public int GetPrecedence(char op)
        {
            // check if operator is supported.
            if (this.precedence.Keys.Contains(op))
            {
                return this.precedence[op];
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Determines if an operator is supported.
        /// </summary>
        /// <param name="op"> string type op. </param>
        /// <returns> bool type. </returns>
        public bool IsOperator(string op)
        {
            return this.operatorList.Contains(op);
        }

        /// <summary>
        /// Creates an operator node.
        /// </summary>
        /// <param name="op"> char symbol of op. </param>
        /// <returns> a operator node subclass. </returns>
        /// <exception cref="Exception"> operator doesn't exist. </exception>
        public OperatorNode CreateOperatorNode(char op)
        {
            if (this.operators.ContainsKey(op))
            {
                object operatorNodeObject = System.Activator.CreateInstance(this.operators[op]);
                if (operatorNodeObject is OperatorNode)
                {
                    return (OperatorNode)operatorNodeObject;
                }
            }

            throw new Exception("Unhandled operator");
        }

        /// <summary>
        /// Traverses all subclasses of operator node and adds them to the program.
        /// </summary>
        /// <param name="onOperator"> delegate. </param>
        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            // get the type declaration of OperatorNode
            Type operatorNodeType = typeof(OperatorNode);

            // Iterate over all loaded assemblies:
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Get all types that inherit from our OperatorNode class using LINQ
                IEnumerable<Type> operatorTypes =
                assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                // Iterate over those subclasses of OperatorNode
                foreach (var type in operatorTypes)
                {
                    // for each subclass, retrieve the Operator property
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    PropertyInfo precedenceField = type.GetProperty("Precedence");
                    if (operatorField != null && precedenceField != null)
                    {
                        // Get the character of the Operator
                        // object value = operatorField.GetValue(type);
                        // If your “Operator” property is not static, use the following code instead:
                        object value = operatorField.GetValue(Activator.CreateInstance(type));
                        object prec = precedenceField.GetValue(Activator.CreateInstance(type));
                        if (value is char && prec is int)
                        {
                            char operatorSymbol = (char)value;

                            // And invoke the function passed as parameter
                            // with the operator symbol and the operator class
                            onOperator(operatorSymbol, type);
                            this.operatorList.Add(operatorSymbol.ToString());

                            int precedenceVal = (int)prec;

                            this.precedence[operatorSymbol] = precedenceVal;
                        }
                    }
                }
            }
        }
    }
}
