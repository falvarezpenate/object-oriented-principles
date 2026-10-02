// <copyright file="ExpressionTreeTests.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

namespace ExpressionTreeTests
{
    /// <summary>
    /// Class for testing the ExpressionTree.
    /// </summary>
    public class ExpressionTreeTests
    {
        /// <summary>
        /// Built in Setup() method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test that checks the evaluation method of a single symbol input.
        /// </summary>
        [Test]
        public void TreeEvaluationEdgeTest()
        {
            ExpressionTree expTree = new ExpressionTree("2");
            Assert.That(expTree.Evaluate(), Is.EqualTo(2));
        }

        /// <summary>
        /// Test that checks the evaluation method of a normal input.
        /// </summary>
        [Test]
        public void TreeEvaluationNormalAddTest()
        {
            ExpressionTree expTree = new ExpressionTree("1+2+3");
            Assert.That(expTree.Evaluate(), Is.EqualTo(6));
        }

        /// <summary>
        /// Test that checks the evaluation method of a normal input.
        /// </summary>
        [Test]
        public void TreeEvaluationNormalSubTest()
        {
            ExpressionTree expTree = new ExpressionTree("10-5-2");
            Assert.That(expTree.Evaluate(), Is.EqualTo(3));
        }

        /// <summary>
        /// Test that checks the evaluation method of a normal input.
        /// </summary>
        [Test]
        public void TreeEvaluationNormalMulTest()
        {
            ExpressionTree expTree = new ExpressionTree("2*2*2");
            Assert.That(expTree.Evaluate(), Is.EqualTo(8));
        }

        /// <summary>
        /// Test that checks the evaluation method of a normal input.
        /// </summary>
        [Test]
        public void TreeEvaluationNormalDivTest()
        {
            ExpressionTree expTree = new ExpressionTree("4/2/2");
            Assert.That(expTree.Evaluate(), Is.EqualTo(1));
        }

        /// <summary>
        /// Test that checks the set variable method of a normal input.
        /// </summary>
        [Test]
        public void TreeSetVariableNormalTest()
        {
            ExpressionTree expTree = new ExpressionTree("hello+2+3");
            expTree.SetVariable("hello", 1);
            Assert.That(expTree.Evaluate(), Is.EqualTo(6));
        }

        /// <summary>
        /// Test that checks turning a infix notation containing only digits into postfix.
        /// </summary>
        [Test]
        public void ShuntingYardPostfixNormalTest()
        {
            ShuntingYard test = new ShuntingYard("1+2+3");
            Assert.That(test.Postfix, Is.EqualTo("1 2 + 3 +"));
        }

        /// <summary>
        /// Test that checks turning a infix notation containing a variable into postfix.
        /// </summary>
        [Test]
        public void ShuntingYardPostfixSpecialTest()
        {
            ShuntingYard test = new ShuntingYard("hello+world+3");
            Assert.That(test.Postfix, Is.EqualTo("hello world + 3 +"));
        }

        /// <summary>
        /// Test that checks that the turning of infix to postfix returns no parenthesis.
        /// </summary>
        [Test]
        public void ShuntingYardPostfixParenthesisTest()
        {
            ShuntingYard test = new ShuntingYard("(1+2)*3");
            Assert.That(test.Postfix, Is.EqualTo("1 2 + 3 *"));
        }

        /// <summary>
        /// Test that evaluates multiple operations.
        /// </summary>
        [Test]
        public void TreeEvaluationMultipleOpTest()
        {
            ExpressionTree expTree = new ExpressionTree("1+2-3");
            Assert.That(expTree.Evaluate(), Is.EqualTo(0));
        }

        /// <summary>
        /// Test that checks that a tree follows correct order of operation with parenthesis.
        /// </summary>
        [Test]
        public void TreeEvaluationParenthesisTest()
        {
            ExpressionTree expTree = new ExpressionTree("(1+2)*3");
            Assert.That(expTree.Evaluate(), Is.EqualTo(9));
        }

        /// <summary>
        /// Test that checks that a tree follows correct precedence without need of parenthesis.
        /// </summary>
        [Test]
        public void TreeEvaluationPrecedenceTest()
        {
            ExpressionTree expTree = new ExpressionTree("1+2*3");
            Assert.That(expTree.Evaluate(), Is.EqualTo(7));
        }

        /// <summary>
        /// Test that checks making use of all of the previous criteria put together.
        /// </summary>
        [Test]
        public void TreeEvaluationComplexFormulaTest()
        {
            ExpressionTree expTree = new ExpressionTree("(1+world)*3+hello/2"); // world will be 0 since its not assigned a val.
            expTree.SetVariable("hello", 2);
            Assert.That(expTree.Evaluate(), Is.EqualTo(4));
        }
    }
}