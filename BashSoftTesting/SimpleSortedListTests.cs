using BashSoft.Contracts;
using BashSoft.DataStructures;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BashSoftTesting
{
    [TestFixture]
    public class SimpleSortedListTests
    {
        private ISimpleOrderedBag<string> names;

        [Test]
        public void TestEmptyCtor()
        {
            this.names = new SimpleSortedList<string>();
            Assert.That(this.names.Capacity, Is.EqualTo(16));
            Assert.That(this.names.Size, Is.EqualTo(0));
        }

        [Test]
        public void TestCtorWithInitialCapacity()
        {
            this.names = new SimpleSortedList<string>(20);
            Assert.That(this.names.Capacity, Is.EqualTo(20));
            Assert.That(this.names.Size, Is.EqualTo(0));
        }

        [Test]
        public void TestCtorWithAllParameters()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase, 30);
            Assert.That(this.names.Capacity, Is.EqualTo(30));
            Assert.That(this.names.Size, Is.EqualTo(0));
        }

        [Test]
        public void TestCtorWithInitialComparer()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);
            Assert.That(this.names.Capacity, Is.EqualTo(16));
            Assert.That(this.names.Size, Is.EqualTo(0));
        }

        [Test]
        public void TestAdd_IncreasesSize()
        {
            this.names = new SimpleSortedList<string>();
            names.Add("Pesho");
            Assert.That(this.names.Size, Is.EqualTo(1));
        }

        [Test]
        public void TestAddNull_ThrowsException()
        {
            this.names = new SimpleSortedList<string>();
            Assert.That(() => this.names.Add(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestAddUnsortedData_IsHeldSorted()
        {
            this.names = new SimpleSortedList<string>();
            names.Add("Rosen");
            names.Add("Georgi");
            names.Add("Bobi");
            FieldInfo namesType = names.GetType().GetField("innerCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            string[] resultCollection = ((string[])namesType.GetValue(names)).Take(4).ToArray();
            string[] expectedCollection = new string[] { "Bobi", "Georgi", "Rosen", null };
            Assert.That(resultCollection, Is.EquivalentTo(expectedCollection));
        }

        [Test]
        public void TestAddingMoreThanInitialCapacity()
        {
            var collection = new SimpleSortedList<int>();
            for (int i = 1; i < 18; i++)
            {
                collection.Add(i);
            }
            Assert.That(collection.Capacity, Is.Not.EqualTo(16));
            Assert.That(collection.Size, Is.EqualTo(17));
        }

        [Test]
        public void TestAddingAllFromCollection_IncreasesSize()
        {
            this.names = new SimpleSortedList<string>();
            string[] input = new string[] { "asdf", "qwerty", "uhaaa", "yup" };
            names.AddAll(input);
            Assert.That(this.names.Size, Is.EqualTo(input.Length));
        }

        [Test]
        public void TestAddingAllFromNull_ThrowsException()
        {
            this.names = new SimpleSortedList<string>();
            Assert.That(() => this.names.AddAll(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestAddAllKeepsSorted()
        {
            this.names = new SimpleSortedList<string>();
            string[] input = new string[] { "qwerty", "yup", "Uhaaa", "Baba", "asdf" };
            names.AddAll(input);
            FieldInfo namesType = names.GetType().GetField("innerCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            string[] resultCollection = ((string[])namesType.GetValue(names)).Take(input.Length).ToArray();
            string[] expectedCollection = new string[] { "asdf", "Baba", "qwerty", "Uhaaa", "yup" };
            Assert.That(resultCollection, Is.EquivalentTo(expectedCollection));
        }

        [Test]

        public void TestRemoveValidElement_DecreasesSize()
        {
            this.names = new SimpleSortedList<string>();
            names.Add("Opa");
            bool result = names.Remove("Opa");
            Assert.That(result == true);
            Assert.That(this.names.Size, Is.EqualTo(0));
        }

        [Test]
        [TestCase("yup")]
        [TestCase("Uhaaa")]
        [TestCase("Baba")]
        public void TestRemoveValidElement_RemovesSelectedOne(string itemToRemove)
        {
            this.names = new SimpleSortedList<string>();
            string[] input = new string[] { "qwerty", "yup", "Uhaaa", "Baba", "asdf" };

            names.AddAll(input);
            bool result = names.Remove(itemToRemove);

            FieldInfo namesType = names.GetType().GetField("innerCollection", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.That(namesType.GetValue(names), Has.No.Member(itemToRemove));
            Assert.That(this.names.Size, Is.EqualTo(input.Length - 1));
            Assert.That(result == true);
        }

        [Test]
        public void TestRemovingNull_ThrowsException()
        {

            this.names = new SimpleSortedList<string>();
            int sizeBeforeRemoval = this.names.Size;

            Assert.That(() => this.names.Remove(null), Throws.ArgumentNullException);
            Assert.That(this.names.Size, Is.EqualTo(sizeBeforeRemoval));
        }

        [Test]
        public void TestJoinWithNull()
        {
            this.names = new SimpleSortedList<string>();
            string[] input = new string[] { "aaaaa", "cccc", "BBBB", "dd", "eeee" };

            names.AddAll(input);

            Assert.That(() => names.JoinWith(null), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase(",")]
        [TestCase(", ")]
        [TestCase("-----")]
        public void TestJoinWorksFine(string separator)
        {
            this.names = new SimpleSortedList<string>();
            string[] input = new string[] { "aaaaa", "cccc", "BBBB", "dd", "eeee" };

            names.AddAll(input);
            string result = names.JoinWith(separator);

            Assert.That(result, Is.EqualTo(string.Join(separator, input.OrderBy(n => n))));
        }
    }
}
