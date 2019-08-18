using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvoiceLibrary;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceUnitTest
{
    [TestClass]
    public class TestSampleCases
    {
        ItemRepository itemRepo;

        public TestSampleCases()
        {
        }

        [TestInitialize]
        public void TestInitalize()
        {
            itemRepo = new ItemRepository();
            itemRepo.SetItem("00000001", "Apple", 6.99m);
            itemRepo.SetItem("00000002", "Banana", 10.21m);
            itemRepo.SetItem("00000003", "Orange", 5.21m);
            itemRepo.SetItem("00000004", "Pineapple", 5.21m);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            itemRepo.Empty();
        }

        [DataTestMethod]
        [DataRow(new string[] { "Apple" }, new int[] { 1 }, "6.99")]
        [DataRow(new string[] { "Apple", "Banana" }, new int[] { 1, 1 }, "17.2")]
        [DataRow(new string[] { "Banana", "Apple"}, new int[] { 1, 1 }, "17.2")]
        public void TestGivenCases(string[] codes, int[] quantity, string expectedResult)
        {
            InvoiceLibrary.Invoice invoice = new InvoiceLibrary.Invoice();
            for (int i = 0; i < codes.Length; i++)
            {
                invoice.AddInvoiceLine(itemRepo.QueryByDescription(codes[i]), quantity[i]);
            }
            decimal result = invoice.GetTotal();

            Assert.IsTrue(result == decimal.Parse(expectedResult), $"Should be {expectedResult} while got {result}");
        }

    }
}
