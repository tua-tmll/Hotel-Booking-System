using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibrarySystem
{
    // 1. Generic repository interface with constraints
    public interface IRepository<T> where T : class, new()
    {
        void Add(T item);
        bool Remove(string itemId);
        T? Find(string itemId);
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(Func<T, bool> predicate);
    }

    // 2. Generic repository implementation
    public class Repository<T> : IRepository<T> where T : LibraryItem, new()
    {
        private readonly LinkedList<T> _items = new LinkedList<T>();

        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _items.AddLast(item);
        }

        public bool Remove(string itemId)
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

        public T? Find(string itemId)
        {
            return _items.FirstOrDefault(item => item.ItemId == itemId);
        }

        public IEnumerable<T> GetAll()
        {
            foreach (var item in _items)
                yield return item;
        }

        public IEnumerable<T> Search(Func<T, bool> predicate)
        {
            foreach (var item in _items)
                if (predicate(item))
                    yield return item;
        }
    }

    // 3. Generic delegate for item events
    public delegate void ItemEventHandler<T>(T item) where T : LibraryItem;

    public class LibraryManager
    {
        // Use the generic repository for LibraryItem
        private static readonly Repository<LibraryItem> _items = new Repository<LibraryItem>();

        public static IReadOnlyList<LibraryItem> Items => _items.GetAll().ToList();

        public LibraryItem? this[int index]
        {
            get => _items.GetAll().ElementAtOrDefault(index);
        }

        // 4. Generic event using the delegate
        public static event ItemEventHandler<LibraryItem>? OnItemAdded;
        public static event ItemEventHandler<LibraryItem>? OnItemRemoved;

        static LibraryManager()
        {
            LibraryLogger.OnLog += delegate(string message)
            {
                Console.WriteLine(message);
            };
        }

        public static void AddItem(LibraryItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _items.Add(item);
            LibraryLogger.Log($"Added new item: {item.Title} (ID: {item.ItemId})");
            OnItemAdded?.Invoke(item);
        }

        public static bool RemoveItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentException("Item ID cannot be null or empty", nameof(itemId));

            var item = _items.Find(itemId);
            var removed = _items.Remove(itemId);
            if (removed && item != null)
            {
                LibraryLogger.Log($"Removed item with ID: {itemId}");
                OnItemRemoved?.Invoke(item);
            }
            return removed;
        }

        public static LibraryItem? FindItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentException("Item ID cannot be null or empty", nameof(itemId));

            return _items.Find(itemId);
        }

        // 5. Iterator using yield return
        public static IEnumerable<LibraryItem> GetAvailableItems()
        {
            foreach (var item in _items.GetAll())
                if (item.IsAvailable)
                    yield return item;
        }

        public static void SaveToFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var item in _items.GetAll())
                    {
                        writer.WriteLine(item.Serialize());
                    }
                }
                LibraryLogger.Log($"Saved {Items.Count} items to file: {filePath}");
            }
            catch (Exception ex)
            {
                LibraryLogger.LogError($"Error saving to file: {ex.Message}");
                throw;
            }
        }

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
                // Clear repository
                // (No direct clear method, so remove all by id)
                var allIds = _items.GetAll().Select(i => i.ItemId).ToList();
                foreach (var id in allIds)
                    _items.Remove(id);

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
                            _items.Add(item);
                        }
                    }
                }
                LibraryLogger.Log($"Loaded {Items.Count} items from file: {filePath}");
            }
            catch (Exception ex)
            {
                LibraryLogger.LogError($"Error loading from file: {ex.Message}");
                throw;
            }
        }

        public static string DisplayAllItems()
        {
            return string.Join("\n\n", _items.GetAll().Select(item => item.GetItemInfo()));
        }

        public static IEnumerable<LibraryItem> SearchItems(Func<LibraryItem, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return _items.Search(predicate);
        }

        // 6. Generic method with constraint
        public static T GetMaxPricedItem<T>(IEnumerable<T> items) where T : LibraryItem, IComparable<T>
        {
            if (items == null || !items.Any())
                throw new ArgumentException("No items provided");
            T max = items.First();
            foreach (var item in items)
                if (item.CompareTo(max) > 0) max = item;
            return max;
        }
    }
} 