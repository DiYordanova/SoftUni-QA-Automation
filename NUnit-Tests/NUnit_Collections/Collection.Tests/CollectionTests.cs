using Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace Collection.Tests
{
    public class CollectionTests
    {       
        [Test]
        public void EmptyConstructor()
        {
            var nums = new Collection<int>();
            Assert.AreEqual(0, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
            Assert.That(nums.ToString(), Is.EqualTo("[]"));

        }

        [Test]
        public void ConstructorSingleItem()
        {
            var nums = new Collection<int>(10);
            Assert.That(nums.ToString(), Is.EqualTo("[10]"));
        }

        [Test]
        public void ConstructorMultipleItem()
        {
            var nums = new Collection<int>(1, 2, 3);
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 3]"));
        }

        [Test]
        public void Add()
        {
            var nums = new Collection<int>(10);
            nums.Add(11);
            Assert.That(nums.ToString(), Is.EqualTo("[10, 11]"));
        }

        [Test]
        public void AddWithGrow()
        {
            var nums = new Collection<int>(555);
            int oldCapacity = nums.Capacity;
            nums.Add(556);
            Assert.That(nums.ToString(), Is.EqualTo("[555, 556]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void AddRange()
        {
            var nums = new Collection<int>(10);
            nums.AddRange(11, 12, 13);
            Assert.That(nums.ToString(), Is.EqualTo("[10, 11, 12, 13]"));
        }


        [Test]
        public void GetByIndex()
        {
            Collection<string> products = new Collection<string>("bread", "butter");
            string firstProduct = products[0];
            string secondProduct = products[1];
            Assert.AreEqual("bread", firstProduct);
            Assert.AreEqual("butter", secondProduct);
        }


        [Test]
        public void GetByInvalidIndex()
        {
            Collection<string> products = new Collection<string>("bread", "butter", "milk");
            Assert.That(() => { var product = products[-3]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var product = products[3]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var product = products[100]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.AreEqual("[bread, butter, milk]", products.ToString());
        }
        [Test]
        public void AddRangeWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            nums.AddRange(555, 556);
            Assert.That(nums.ToString(), Is.EqualTo("[555, 556]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }
    }
}