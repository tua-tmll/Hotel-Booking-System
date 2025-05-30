using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibrarySystem
{
    public class LibraryManager
    {
        // Static linked list to track all items
        private static readonly LinkedList<LibraryItem> _items = new LinkedList<LibraryItem>();
        
        // Static property to access the items
        public static IReadOnlyList<LibraryItem> Items => _items.ToList();

        // Indexer for accessing items by index
        public LibraryItem? this[int index]
        {
            get => _items.ElementAtOrDefault(index);
        }

        // Constructor
        static LibraryManager()
        {
            // Anonymous method for logging
            LibraryLogger.OnLog += delegate(string message)
            {
                Console.WriteLine(message);
            };
        }

        // Add a new item to the library
        public static void AddItem(LibraryItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _items.AddLast(item);
            LibraryLogger.Log($"Added new item: {item.Title} (ID: {item.ItemId})");
        }

        // Remove an item from the library
        public static bool RemoveItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentException("Item ID cannot be null or empty", nameof(itemId));

            var node = _items.First;
            while (node != null)
            {
                if (node.Value.ItemId == itemId)
                {
                    _items.Remove(node);
                    LibraryLogger.Log($"Removed item with ID: {itemId}");
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        // Find an item by ID using lambda expression
        public static LibraryItem? FindItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentException("Item ID cannot be null or empty", nameof(itemId));

            return _items.FirstOrDefault(item => item.ItemId == itemId);
        }

        // Get all available items using lambda expression
        public static IEnumerable<LibraryItem> GetAvailableItems()
        {
            return _items.Where(item => item.IsAvailable);
        }

        // Save items to file
        public static void SaveToFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var item in _items)
                    {
                        writer.WriteLine(item.Serialize());
                    }
                }
                LibraryLogger.Log($"Saved {_items.Count} items to file: {filePath}");
            }
            catch (Exception ex)
            {
                LibraryLogger.LogError($"Error saving to file: {ex.Message}");
                throw;
            }
        }

        // Load items from file
        public static void LoadFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

            if (!File.Exists(filePath))
            {
                LibraryLogger.LogError($"File not found: {filePath}");
                return;
            }

            try
            {
                _items.Clear();
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line)) continue;

                    string[] parts = line.Split('|');
                    if (parts.Length >= 5)
                    {
                        LibraryItem? item = null;
                        switch (parts[0])
                        {
                            case "Book":
                                item = new Book();
                                break;
                            case "Magazine":
                                item = new Magazine();
                                break;
                        }

                        if (item != null)
                        {
                            item.Deserialize(line);
                            _items.AddLast(item);
                        }
                    }
                }
                LibraryLogger.Log($"Loaded {_items.Count} items from file: {filePath}");
            }
            catch (Exception ex)
            {
                LibraryLogger.LogError($"Error loading from file: {ex.Message}");
                throw;
            }
        }

        // Static method to display all items using lambda expression
        public static string DisplayAllItems()
        {
            return string.Join("\n\n", _items.Select(item => item.GetItemInfo()));
        }

        // Search items using lambda expression
        public static IEnumerable<LibraryItem> SearchItems(Func<LibraryItem, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return _items.Where(predicate);
        }
    }
} 