using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceLibrary
{
    public class Item
    {
        public string ItemId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public Item(string id, string description, decimal cost)
        {
            ItemId = id; Description = description; Cost = cost;
        }

    }

}
