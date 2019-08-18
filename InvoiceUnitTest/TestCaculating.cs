using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvoiceLibrary;

namespace InvoiceUnitTest
{
    [TestClass]
    public class TestCaculating
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
        public void TestToString()
        {
            Invoice invoice = new Invoice();

            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Apple"), 2);
            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Orange"), 3);
            Assert.IsTrue(invoice.GetTotal()== 6.99m*2+5.21m*3,
                "Fail to caculate total cost");
        }

    }
}
