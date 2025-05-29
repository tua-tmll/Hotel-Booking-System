using System;

namespace LibrarySystem
{
    public sealed class Magazine : LibraryItem
    {
        // Additional properties specific to magazines
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISSN { get; set; }
        public int IssueNumber { get; set; }
        public string Category { get; set; }

        // Constructors
        public Magazine() : base()
        {
        }

        public Magazine(string title, string itemId, decimal price, string publisher, DateTime publicationDate, string issn, int issueNumber, string category)
            : base(title, itemId, price)
        {
            Publisher = publisher;
            PublicationDate = publicationDate;
            ISSN = issn;
            IssueNumber = issueNumber;
            Category = category;
        }

        // Override methods
        public override string GetItemType()
        {
            return "Magazine";
        }

        public override decimal CalculateLateFee(int daysLate)
        {
            // $2 per day late fee for magazines (higher than books)
            return daysLate * 2.0m;
        }

        public override string GetItemInfo()
        {
            return base.GetItemInfo() + 
                   $"\nPublisher: {Publisher}" +
                   $"\nPublication Date: {PublicationDate.ToShortDateString()}" +
                   $"\nISSN: {ISSN}" +
                   $"\nIssue Number: {IssueNumber}" +
                   $"\nCategory: {Category}";
        }

        // New methods specific to magazines
        public bool IsCurrentIssue()
        {
            return (DateTime.Now - PublicationDate).TotalDays <= 30;
        }

        public string GetCitation()
        {
            return $"{Title} ({PublicationDate.Year}). {Publisher}, Issue {IssueNumber}.";
        }
    }
} 