using System;

namespace LibrarySystem
{
    public class Book : LibraryItem
    {
        // Additional properties specific to books
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PageCount { get; set; }
        public string Publisher { get; set; }
        public int Edition { get; set; }

        // Constructors
        public Book() : base()
        {
            Author = string.Empty;
            ISBN = string.Empty;
            Publisher = string.Empty;
            PageCount = 0;
            Edition = 1;
        }

        public Book(string title, string itemId, decimal price, string author, string isbn, int pageCount, string publisher, int edition)
            : base(title, itemId, price)
        {
            Author = author;
            ISBN = isbn;
            PageCount = pageCount;
            Publisher = publisher;
            Edition = edition;
        }

        // Override methods
        public override string GetItemType() => "Book";

        public override decimal CalculateLateFee(int daysLate)
        {
            // $1 per day late fee for books
            return daysLate * 1.0m;
        }

        public override string GetItemInfo()
        {
            return $"{base.GetItemInfo()}\nAuthor: {Author}\nISBN: {ISBN}\nPages: {PageCount}\nPublisher: {Publisher}\nEdition: {Edition}";
        }

        public override string Serialize()
        {
            return $"{GetItemType()}|{Title}|{ItemId}|{Price}|{IsAvailable}|{Author}|{ISBN}|{PageCount}|{Publisher}|{Edition}";
        }

        public override void Deserialize(string data)
        {
            string[] parts = data.Split('|');
            if (parts.Length >= 10)
            {
                Title = parts[1];
                ItemId = parts[2];
                Price = decimal.Parse(parts[3]);
                IsAvailable = bool.Parse(parts[4]);
                Author = parts[5];
                ISBN = parts[6];
                PageCount = int.Parse(parts[7]);
                Publisher = parts[8];
                Edition = int.Parse(parts[9]);
            }
        }

        // New methods specific to books
        public string GetCitation()
        {
            return $"{Author} ({DateAdded.Year}). {Title}. {Publisher}.";
        }

        public bool IsReferenceBook()
        {
            return Title.ToLower().Contains("reference") || 
                   Title.ToLower().Contains("dictionary") || 
                   Title.ToLower().Contains("encyclopedia");
        }
    }
} 