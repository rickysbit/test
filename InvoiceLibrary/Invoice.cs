using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace InvoiceLibrary
{
    /// <summary>
    /// Creates a deep clone of the current invoice (all fields and properties)
    /// </summary>
    public static class CloneExtensions
    {
        public static T Clone<T>(this T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            T result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }

    }

    [Serializable]
    public class Invoice
    {
        static private int nextNumber = 1;

        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceLine> LineItems { get; set; }

        public Invoice(int serialNumber=0)
        {
            InvoiceNumber = serialNumber==0? Invoice.nextNumber++.ToString("D8"):serialNumber.ToString("D8");
            InvoiceDate = DateTime.Now;
            LineItems = new List<InvoiceLine>();
        }

        public void AddInvoiceLine(Item item, decimal quantity)
        {
            var line = LineItems.Where(a => a.ItemId == item.ItemId).SingleOrDefault();
            if (line == null)
                LineItems.Add(new InvoiceLine(item, quantity));
            else
                line.Quantity += quantity;
        }


        public void RemoveInvoiceLine(string itemID)
        {
            LineItems.RemoveAll(x => x.ItemId == itemID);
        }
        public void RemoveInvoiceLine(int lineNum)
        {
            LineItems.RemoveAt(lineNum-1);
        }

        /// <summary>
        /// GetTotal should return the sum of (Cost * Quantity) for each line item
        /// </summary>
        public decimal GetTotal()
        {
            decimal total = 0;
            foreach (var invoiceLine in LineItems)
                total+=invoiceLine.Quantity*invoiceLine.Cost;
            return total;
        }

        /// <summary>
        /// MergeInvoices appends the items from the sourceInvoice to the current invoice
        /// </summary>
        /// <param name="sourceInvoice">Invoice to merge from</param>
        public void MergeInvoices(Invoice sourceInvoice)
        {
            foreach(var line in sourceInvoice.LineItems)
                AddInvoiceLine(new Item(line.ItemId,line.Description,line.Cost), line.Quantity);
        }

 
        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Invoice Number: [InvoiceNumber], InvoiceDate: [DD/MM/YYYY], LineItemCount: [Number of items in LineItems] 
        /// </summary>
        public override string ToString()
        {
            return string.Format("Invoice Number: {0}, InvoiceDate: {1:dd/MM/yyyy}, LineItemCount: {2}", InvoiceNumber, InvoiceDate, LineItems.Count);
        }
    }
}