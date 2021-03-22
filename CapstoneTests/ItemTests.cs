using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void TestDecreaseStock()
        {
            Item item = new Chip("testItem", 1.00M);
            item.DecreaseStock();

            int expectedStock = 4;
            int actualStock = item.StockCount;

            Assert.AreEqual(expectedStock, actualStock);
        }

        [TestMethod]
        public void TestSoldOut()
        {
            Item item = new Chip("testItem", 1.00M);
            item.DecreaseStock();
            item.DecreaseStock();
            item.DecreaseStock();
            item.DecreaseStock();
            item.DecreaseStock();

            bool expectedBool = true;
            bool actualBool = item.SoldOut;

            Assert.AreEqual(expectedBool, actualBool);
        }

        [TestMethod]
        public void TestDisplayItem()
        {
            Item item = new Candy("testItem", 2.50M);

            string expectedString = "testItem 2.50 5";
            string actualString = item.DisplayItem();

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void TestDisplayItemSoldOut()
        {
            Item item = new Candy("testItem", 2.50M);
            item.DecreaseStock();
            item.DecreaseStock();
            item.DecreaseStock();
            item.DecreaseStock();
            item.DecreaseStock();
            item.DecreaseStock();

            string expectedString = "testItem 2.50 SOLD OUT";
            string actualString = item.DisplayItem();

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void TestCandyItemMessage()
        {
            Item item = new Candy("testItem", 2.50M);

            string expectedString = "Munch Munch, Yum!";
            string actualString = item.ItemMessage();

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void TestChipItemMessage()
        {
            Item item = new Chip("testItem", 2.50M);

            string expectedString = "Crunch Crunch, Yum!";
            string actualString = item.ItemMessage();

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void TestDrinkItemMessage()
        {
            Item item = new Drink("testItem", 2.50M);

            string expectedString = "Glug Glug, Yum!";
            string actualString = item.ItemMessage();

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void TestGumItemMessage()
        {
            Item item = new Gum("testItem", 2.50M);

            string expectedString = "Chew Chew, Yum!";
            string actualString = item.ItemMessage();

            Assert.AreEqual(expectedString, actualString);
        }
    }
}
