using HW2.DistinctIntegers;
using NUnit.Framework;

namespace HW2.DistinctIntegers.Tests { 
    public class DistincIntegersTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestHashSetMethodNormal()
        {
            int[] testArr = { 1, 2, 3, 3 };
            Assert.AreEqual(
                3, 
                DistinctIntergers.HashSetMethod(testArr)
                );

        }
        [Test]
        public void TestHashSetMethodEdge()
        {
            int[] testArr = { 1 };
            Assert.AreEqual(
                1,
                DistinctIntergers.HashSetMethod(testArr)
                );

        }
        [Test]
        public void TestStorageComplexityMethodNormal()
        {
            int[] testArr = { 1, 2, 3, 3 };
            Assert.AreEqual(
                3,
                DistinctIntergers.StorageComplexityMethod(testArr)
                );

        }
        [Test]
        public void TestStorageComplexityMethodEdge()
        {
            int[] testArr = { 1 };
            Assert.AreEqual(
                1,
                DistinctIntergers.StorageComplexityMethod(testArr)
                );

        }
        [Test]
        public void TestSortedMethodNormal()
        {
            int[] testArr = { 1, 2, 3, 3 };
            Assert.AreEqual(
                3,
                DistinctIntergers.SortedMethod(testArr)
                );

        }
        [Test]
        public void TestSortedMethodEdge()
        {
            int[] testArr = { 1 };
            Assert.AreEqual(
                1,
                DistinctIntergers.SortedMethod(testArr)
                );

        }
    }
}
