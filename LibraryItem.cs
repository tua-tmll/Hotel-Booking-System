using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public abstract class LibraryItem : ISerializable
    {
        // Properties
        public string Title { get; set; }
        public string ItemId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DateAdded { get; protected set; }
        public decimal Price { get; set; }

        // Dictionary for additional properties
        protected Dictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

        // Indexer for accessing additional properties
        public object this[string key]
        {
            get => AdditionalProperties.ContainsKey(key) ? AdditionalProperties[key] : null;
            set => AdditionalProperties[key] = value;
        }

        // Constructors
        protected LibraryItem()
        {
            DateAdded = DateTime.Now;
            IsAvailable = true;
        }

        protected LibraryItem(string title, string itemId, decimal price)
        {
            Title = title;
            ItemId = itemId;
            Price = price;
            DateAdded = DateTime.Now;
            IsAvailable = true;
        }

        // Virtual methods
        public virtual void CheckOut()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                LibraryLogger.Log($"Item {Title} (ID: {ItemId}) has been checked out.");
            }
        }

        public virtual void Return()
        {
            IsAvailable = true;
            LibraryLogger.Log($"Item {Title} (ID: {ItemId}) has been returned.");
        }

        public virtual string GetItemInfo()
        {
            return $"Title: {Title}\nID: {ItemId}\nAvailable: {IsAvailable}\nDate Added: {DateAdded.ToShortDateString()}";
        }

        // Abstract methods that must be implemented by derived classes
        public abstract string GetItemType();
        public abstract decimal CalculateLateFee(int daysLate);

        // ISerializable implementation
        public virtual string Serialize()
        {
            return $"{GetItemType()}|{Title}|{ItemId}|{Price}|{IsAvailable}";
        }

        public virtual void Deserialize(string data)
        {
            string[] parts = data.Split('|');
            if (parts.Length >= 5)
            {
                Title = parts[1];
                ItemId = parts[2];
                Price = decimal.Parse(parts[3]);
                IsAvailable = bool.Parse(parts[4]);
            }
        }
    }
} 