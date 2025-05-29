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
        public override string GetItemType()
        {
            return "Book";
        }

        public override decimal CalculateLateFee(int daysLate)
        {
            // $1 per day late fee for books
            return daysLate * 1.0m;
        }

        public override string GetItemInfo()
        {
            return base.GetItemInfo() + 
                   $"\nAuthor: {Author}" +
                   $"\nISBN: {ISBN}" +
                   $"\nPages: {PageCount}" +
                   $"\nPublisher: {Publisher}" +
                   $"\nEdition: {Edition}";
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