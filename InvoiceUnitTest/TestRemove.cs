using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvoiceLibrary;

namespace InvoiceUnitTest
{
    [TestClass]
    public class TestRemove
    {
        ItemRepository itemRepo;

        [TestInitialize]
        public void TestInitalize()
        {
            itemRepo = new ItemRepository();
            itemRepo.SetItem("00000001", "Apple", 6.99m);
            itemRepo.SetItem("00000002", "Banana", 10.21m);
            itemRepo.SetItem("00000003", "Orange", 5.21m);
            itemRepo.SetItem("00000004", "Pineapple", 5.21m);
            itemRepo.SetItem("00000005", "Blueberries", 6.27m);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            itemRepo.Empty();
        }

        [TestMethod]
        public void TestRemoveLine()
        {
            Invoice invoice = new Invoice();

            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Apple"), 2);
            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Orange"), 3);
            Assert.IsTrue(invoice.LineItems.Count==2, "Did not remove a line");
            invoice.RemoveInvoiceLine(1);
            Assert.IsTrue(invoice.LineItems.Count == 1&& invoice.LineItems[0].Description=="Orange", 
                "Did not remove the right line");
            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Pineapple"), 3);
            invoice.RemoveInvoiceLine("00000004");
            Assert.IsTrue(invoice.LineItems.Count == 1 && invoice.LineItems[0].Description == "Orange",
                "Did not remove the right line");

        }

    }
}
