using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibrarySystem
{
    public class LibraryManager
    {
        // Static linked list to track all items
        private static LinkedList<LibraryItem> _items = new LinkedList<LibraryItem>();
        
        // Static property to access the items
        public static LinkedList<LibraryItem> Items => _items;

        // Add a new item to the library
        public static void AddItem(LibraryItem item)
        {
            _items.AddLast(item);
        }

        // Remove an item from the library
        public static bool RemoveItem(string itemId)
        {
            var node = _items.First;
            while (node != null)
            {
                if (node.Value.ItemId == itemId)
                {
                    _items.Remove(node);
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        // Find an item by ID
        public static LibraryItem FindItem(string itemId)
        {
            return _items.FirstOrDefault(item => item.ItemId == itemId);
        }

        // Get all available items
        public static IEnumerable<LibraryItem> GetAvailableItems()
        {
            return _items.Where(item => item.IsAvailable);
        }

        // Save items to file
        public static void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in _items)
                {
                    writer.WriteLine($"{item.GetItemType()}|{item.Title}|{item.ItemId}|{item.Price}|{item.IsAvailable}");
                }
            }
        }

        // Load items from file
        public static void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return;

            _items.Clear();
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length >= 5)
                {
                    LibraryItem? item = null;
                    switch (parts[0])
                    {
                        case "Book":
                            item = new Book
                            {
                                Title = parts[1],
                                ItemId = parts[2],
                                Price = decimal.Parse(parts[3])
                            };
                            break;
                        case "Magazine":
                            item = new Magazine
                            {
                                Title = parts[1],
                                ItemId = parts[2],
                                Price = decimal.Parse(parts[3])
                            };
                            break;
                    }

                    if (item != null)
                    {
                        item.IsAvailable = bool.Parse(parts[4]);
                        _items.AddLast(item);
                    }
                }
            }
        }

        // Static method to display all items
        public static string DisplayAllItems()
        {
            return string.Join("\n\n", _items.Select(item => item.GetItemInfo()));
        }
    }
} 