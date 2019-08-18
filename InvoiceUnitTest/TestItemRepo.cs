using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvoiceLibrary;
using System.Linq;

namespace InvoiceUnitTest
{
    [TestClass]
    public class TestItemRepo
    {
        ItemRepository itemRepo;

        public TestItemRepo()
        {
        }

        [TestInitialize]
        public void TestInitalize()
        {
            itemRepo = new ItemRepository();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            itemRepo.Empty();
        }

        [DataTestMethod]
        [DataRow("00000001", "Apple" , "6.99")]
        [DataRow("00000002", "Banana", "10.21")]
        [DataRow("00000003", "Orange", "5.21")]
        [DataRow("00000004", "Pineapple", "5.21")]
        [DataRow("00000005", "Blueberries", "6.27")]
        [DataRow("00000003", "Orange", "5.31")]
        public void CanSetItem(string itemId, string description, string cost)
        {
            itemRepo.SetItem(itemId, description, decimal.Parse(cost));

            Assert.AreEqual(itemRepo.ItemList.FindAll(a=>a.ItemId==itemId
                && a.Description==description && a.Cost== decimal.Parse(cost)).Count,1,
                "Fail to add an item");
        }

        [TestMethod]
        public void CanQueryItem()
        {
            itemRepo.SetItem("00000001", "Apple", 6.99m);
            itemRepo.SetItem("00000002", "Banana", 10.21m);
            itemRepo.SetItem("00000003", "Orange", 5.21m);
            itemRepo.SetItem("00000004", "Pineapple", 5.21m);
            itemRepo.SetItem("00000005", "Blueberries", 6.27m);

            Item item = itemRepo.QueryByItemId("00000001");
            Assert.IsTrue(item.Cost == 6.99m, "Fail to query item");

            item = itemRepo.QueryByItemId("00000002");
            Assert.IsTrue(item.Cost == 10.21m, "Fail to query item");

        }

    }
}
