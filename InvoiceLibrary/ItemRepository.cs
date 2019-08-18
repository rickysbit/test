using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoiceLibrary
{

    public class ItemRepository
    {
        public List<Item> ItemList { get; protected set; }

        public ItemRepository()
        {
            // ItemList = new Dictionary<string, Item>(); 
            ItemList = new List<Item>();
        }

        public IList<Item> Empty()
        {
            ItemList.Clear();
            return ItemList;
        }

        public Item SetItem(string itemId, string description, decimal cost)
        {
            if (!IsUniqueDescription(itemId, description))
                throw new Exception("Duplicate description.");

            var searchItem = QueryByItemId(itemId);

            // If no item record then create one, otherwise modify the record.
            if (searchItem == null)
            {
                var newItem = new Item(itemId, description, cost);
                ItemList.Add(newItem);
                return newItem;
            }
            else
            {
                searchItem.Cost = cost;
                searchItem.Description = description;
                return searchItem;
            }
        }

        public bool IsUniqueDescription(string itemId, string description)
        {
            Item ret = ItemList.Where(u => u.ItemId != itemId && u.Description == description).FirstOrDefault();
            return ret == null;
        }

        public int RemoveItem(string itemId)
        {
            return ItemList.RemoveAll(a => a.ItemId == itemId);
        }


        public Item QueryByItemId(string itemId)
        {
            return ItemList.Where(u => u.ItemId == itemId).SingleOrDefault();
        }
        public Item QueryByDescription(string description)
        {
            return ItemList.Where(u => u.Description == description).SingleOrDefault();
        }

    }

}
