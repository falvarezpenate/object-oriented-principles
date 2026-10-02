// <copyright file="Tests.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

namespace HW3.Tests
{
    /// <summary>
    /// Class that runs the tests for HW3 project.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Set up method for Unit Tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Tests the special exception for the first value in
        /// FibonacciTextReader.ReadLine() overloaded method from HW3.
        /// </summary>
        [Test]
        public void TestFibonacciTextReaderReadLineException1()
        {
            FibonacciTextReader ftr = new FibonacciTextReader(5);
            string s = ftr.ReadLine();
            Assert.That(s, Is.EqualTo("0"));
        }

        /// <summary>
        /// Tests the special exception for the second value in
        /// FibonacciTextReader.ReadLine() overloaded method from HW3.
        /// </summary>
        [Test]
        public void TestFibonacciTextReaderReadLineException2()
        {
            FibonacciTextReader ftr = new FibonacciTextReader(10);
            string s = ftr.ReadLine();
            s = ftr.ReadLine();
            Assert.That(s, Is.EqualTo("1"));
        }

        /// <summary>
        /// Tests an average case for the
        /// FibonacciTextReader.ReadLine() overloaded method from HW3.
        /// </summary>
        [Test]
        public void TestFibonacciTextReaderReadLineNormal()
        {
            FibonacciTextReader ftr = new FibonacciTextReader(6);
            string s = ftr.ReadLine();
            s = ftr.ReadLine();
            s = ftr.ReadLine();
            s = ftr.ReadLine();
            s = ftr.ReadLine();
            s = ftr.ReadLine();
            Assert.That(s, Is.EqualTo("5"));
        }

        /// <summary>
        /// Tests the case for which a user tries to read a line from the
        /// sequence that surpasses the maximum number of lines indicated in the
        /// FibonacciTextReader.ReadLine() overloaded method from HW3.
        /// </summary>
        [Test]
        public void TestFibonacciTextReaderReadLineEdge()
        {
            FibonacciTextReader ftr = new FibonacciTextReader(6);
            string s = ftr.ReadLine();
            s = ftr.ReadLine();
            s = ftr.ReadLine();
            s = ftr.ReadLine();
            s = ftr.ReadLine();
            s = ftr.ReadLine();
            s = ftr.ReadLine();
            Assert.That(s, Is.EqualTo(null));
        }
    }
}