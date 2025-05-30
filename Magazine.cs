using System;

namespace LibrarySystem
{
    public class Magazine : LibraryItem
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
            Publisher = string.Empty;
            ISSN = string.Empty;
            Category = string.Empty;
            PublicationDate = DateTime.Now;
            IssueNumber = 0;
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
        public override string GetItemType() => "Magazine";

        public override decimal CalculateLateFee(int daysLate)
        {
            // $2 per day late fee for magazines (higher than books)
            return daysLate * 2.0m;
        }

        public override string GetItemInfo()
        {
            return $"{base.GetItemInfo()}\nPublisher: {Publisher}\nPublication Date: {PublicationDate.ToShortDateString()}\nISSN: {ISSN}\nIssue Number: {IssueNumber}\nCategory: {Category}";
        }

        public override string Serialize()
        {
            return $"{GetItemType()}|{Title}|{ItemId}|{Price}|{IsAvailable}|{Publisher}|{PublicationDate:yyyy-MM-dd}|{ISSN}|{IssueNumber}|{Category}";
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
                Publisher = parts[5];
                PublicationDate = DateTime.Parse(parts[6]);
                ISSN = parts[7];
                IssueNumber = int.Parse(parts[8]);
                Category = parts[9];
            }
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