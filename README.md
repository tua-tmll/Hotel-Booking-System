# Library Management System

A C# Windows Forms application demonstrating inheritance and polymorphism using Material Design.

## Requirements

- Visual Studio 2022
- .NET 6.0 SDK
- Windows OS

## Setup Instructions

1. Clone or download this repository
2. Open `LibrarySystem.sln` in Visual Studio 2022
3. Restore NuGet packages (Right-click on solution → Restore NuGet Packages)
4. Build the solution (Build → Build Solution)
5. Run the application (Debug → Start Debugging or press F5)

## Features

- Add and manage books and magazines
- Material Design UI
- File I/O operations for data persistence
- Polymorphic behavior through inheritance
- Static linked list implementation

## Project Structure

- `LibraryItem.cs` - Abstract base class for library items
- `Book.cs` - Book class implementation
- `Magazine.cs` - Magazine class implementation
- `LibraryManager.cs` - Static manager class for library operations
- `MainForm.cs` - Main application window
- `AddBookForm.cs` - Form for adding new books
- `AddMagazineForm.cs` - Form for adding new magazines

## Dependencies

- MaterialSkin.2 (v2.0.0) - For Material Design UI components 