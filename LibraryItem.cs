using System;

namespace LibrarySystem
{
    public abstract class LibraryItem
    {
        // Properties
        public string Title { get; set; }
        public string ItemId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DateAdded { get; protected set; }
        public decimal Price { get; set; }

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
            }
        }

        public virtual void Return()
        {
            IsAvailable = true;
        }

        public virtual string GetItemInfo()
        {
            return $"Title: {Title}\nID: {ItemId}\nAvailable: {IsAvailable}\nDate Added: {DateAdded.ToShortDateString()}";
        }

        // Abstract methods that must be implemented by derived classes
        public abstract string GetItemType();
        public abstract decimal CalculateLateFee(int daysLate);
    }
} 