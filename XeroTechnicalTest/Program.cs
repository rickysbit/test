/*
    Welcome to the Xero technical excercise!
    ---------------------------------------------------------------------------------
    The test consists of a small invoice application that has a number of issues.

    Your job is to fix them and make sure you can perform the functions in each method below.

    Note your first job is to get the solution compiling! 
	
    Rules
    ---------------------------------------------------------------------------------
    * The entire solution must be written in C# (any version)
    * You can modify any of the code in this solution, split out classes, add projects etc
    * You can modify Invoice and InvoiceLine, rename and add methods, change property types (hint) 
    * Feel free to use any libraries or frameworks you like as long as they are .net based
    * Feel free to write tests (hint) 
    * Show off your skills! 

    Good luck :) 

    When you have finished the solution please zip it up and email it back to the recruiter or developer who sent it to you
*/

using System;
using System.Collections.Generic;
using InvoiceLibrary;

namespace XeroTechnicalTest
{
    public class Program
    {
        static ItemRepository itemRepo;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Xero Tech Test!");

            itemRepo = new ItemRepository();
            itemRepo.SetItem("00000001", "Apple", 6.99m);
            itemRepo.SetItem("00000002", "Banana", 10.21m);
            itemRepo.SetItem("00000003", "Orange", 5.21m);
            itemRepo.SetItem("00000004", "Pineapple", 5.21m);
            itemRepo.SetItem("00000005", "Blueberries", 6.27m);

            CreateInvoiceWithOneItem();
            CreateInvoiceWithMultipleItemsAndQuantities();
            RemoveItem();
            MergeInvoices();
            CloneInvoice();
            InvoiceToString();
        }

        private static void CreateInvoiceWithOneItem()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Apple"),1);

            Console.WriteLine("Single Item Invoice, Total Cost: {0}", invoice.GetTotal());
        }

        private static void CreateInvoiceWithMultipleItemsAndQuantities()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Banana"), 4);
            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Orange"), 1);
            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Pineapple"), 5);

            Console.WriteLine("Multiple Items Invoice, Total Cost: {0}", invoice.GetTotal());
        }

        private static void RemoveItem()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Orange"), 1);

            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Banana"), 4);

            invoice.RemoveInvoiceLine(1);
            Console.WriteLine("Remove Invoice line, Total Cost: {0}", invoice.GetTotal());
        }

        private static void MergeInvoices()
        {
            var invoice1 = new Invoice();

            invoice1.AddInvoiceLine(itemRepo.QueryByDescription("Banana"), 4);

            var invoice2 = new Invoice();

            invoice2.AddInvoiceLine(itemRepo.QueryByDescription("Orange"), 1);
            invoice2.AddInvoiceLine(itemRepo.QueryByDescription("Blueberries"), 3);

            invoice1.MergeInvoices(invoice2);
            Console.WriteLine("Merge Invoices, Total Cost: {0}", invoice1.GetTotal());
        }

        private static void CloneInvoice()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Apple"), 1);
            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Blueberries"), 3);

            var clonedInvoice = invoice.Clone();
            Console.WriteLine("Clone Invoice, Total Cost: {0}",clonedInvoice.GetTotal());
        }

        private static void InvoiceToString()
        {
            var invoice = new Invoice(1000);
            invoice.AddInvoiceLine(itemRepo.QueryByDescription("Apple"), 1);

            Console.WriteLine(invoice.ToString());
        }
    }
}
