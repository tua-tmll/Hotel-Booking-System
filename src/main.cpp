#include "Models/Person.h"
#include "Models/Client.h"
#include "Models/Administrator.h"
#include "Models/BookingRequest.h"
#include "Models/Room.h"
#include "GUI/MainForm.h"
#include <iostream>
#include <vector>
#include <memory>

void demonstrateInheritance();
void demonstrateRoomHierarchy();
void demonstrateBookingSystem();

// Windows entry point
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow) {
    try {
        // Create and initialize the main form
        MainForm mainForm;
        if (!mainForm.Initialize(hInstance, nCmdShow)) {
            MessageBox(nullptr, L"Failed to create window", L"Error", MB_OK | MB_ICONERROR);
            return 1;
        }

        // Demonstrate system functionality
        demonstrateInheritance();
        demonstrateRoomHierarchy();
        demonstrateBookingSystem();

        // Save data to file
        BookingRequest::saveToFile("bookings.csv");

        // Run the message loop
        mainForm.Run();

        return 0;
    }
    catch (const std::exception& e) {
        MessageBoxA(nullptr, e.what(), "Error", MB_OK | MB_ICONERROR);
        return 1;
    }
}

void demonstrateInheritance() {
    // Create a client
    Client client("John Doe", "john@example.com", "1234567890", 
                 "123 Main St", 30, "password123");
    
    // Create an administrator
    Administrator admin("Admin User", "admin@hotel.com", "0987654321",
                      "Hotel Office", 35, "admin123");
    
    // Demonstrate polymorphic behavior
    std::vector<Person*> people;
    people.push_back(&client);
    people.push_back(&admin);
    
    for (const auto& person : people) {
        OutputDebugStringA(("\nPerson Role: " + person->getRole() + "\n").c_str());
        person->displayInfo();
    }
}

void demonstrateRoomHierarchy() {
    // Create different types of rooms
    StandardRoom standard(101);
    DeluxeRoom deluxe(201);
    Suite suite(301);
    
    // Demonstrate polymorphic behavior
    std::vector<Room*> rooms;
    rooms.push_back(&standard);
    rooms.push_back(&deluxe);
    rooms.push_back(&suite);
    
    for (const auto& room : rooms) {
        OutputDebugStringA("\nRoom Information:\n");
        room->displayInfo();
    }
}

void demonstrateBookingSystem() {
    // Create a client
    Client client("Jane Smith", "jane@example.com", "5555555555",
                 "456 Oak St", 25, "password456");
    
    // Create an administrator
    Administrator admin("Admin User", "admin@hotel.com", "0987654321",
                      "Hotel Office", 35, "admin123");
    
    // Create a booking request
    BookingRequest* request = client.createBookingRequest(2, "Standard", 3);
    
    // Administrator processes the request
    admin.addPendingRequest(request);
    admin.viewPendingRequests();
    
    // Approve the request
    admin.approveRequest(request);
    
    // Client makes payment
    client.setBalance(1000.0); // Set initial balance
    if (client.makePayment(request->getTotalCost())) {
        OutputDebugStringA("Payment successful!\n");
    } else {
        OutputDebugStringA("Payment failed!\n");
    }
    
    // View booking history
    client.viewBookingHistory();
    
    // Generate admin report
    admin.generateReport();
} 