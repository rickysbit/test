using System;

namespace InvoiceLibrary
{
    [Serializable]
    public class InvoiceLine
    {
        public string ItemId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }

        public InvoiceLine() { }
        public InvoiceLine(Item item, decimal quantity)
        {
            ItemId = item.ItemId;
            Description = item.Description;
            Cost = item.Cost;
            Quantity = quantity;
        }
    }
}