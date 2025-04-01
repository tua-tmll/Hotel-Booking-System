#include "Models/BookingRequest.h"
#include "Models/Client.h"
#include <Windows.h>
#include <fstream>
#include <sstream>

// Initialize static member
BookingRequest* BookingRequest::requestList = nullptr;

BookingRequest::BookingRequest() 
    : client(nullptr), occupants(0), roomType(""), duration(0), 
      status("Pending"), totalCost(0.0), next(nullptr) {}

BookingRequest::BookingRequest(Client* client, int occupants, 
                             const std::string& roomType, int duration)
    : client(client), occupants(occupants), roomType(roomType), 
      duration(duration), status("Pending"), totalCost(0.0), next(nullptr) {
    calculateTotalCost();
    addToList(this);
}

BookingRequest::~BookingRequest() {
    // Remove from list if needed
    if (this == requestList) {
        requestList = next;
    } else {
        BookingRequest* current = requestList;
        while (current && current->next != this) {
            current = current->next;
        }
        if (current) {
            current->next = next;
        }
    }
}

void BookingRequest::displayInfo() const {
    std::string info = "Booking Request Details:\n";
    info += "Client: " + (client ? client->getName() : "Unknown") + "\n";
    info += "Occupants: " + std::to_string(occupants) + "\n";
    info += "Room Type: " + roomType + "\n";
    info += "Duration (days): " + std::to_string(duration) + "\n";
    info += "Status: " + status + "\n";
    info += "Total Cost: $" + std::to_string(totalCost) + "\n";
    OutputDebugStringA(info.c_str());
}

void BookingRequest::calculateTotalCost() {
    double basePrice = 0.0;
    if (roomType == "Standard") {
        basePrice = 100.0;
    } else if (roomType == "Deluxe") {
        basePrice = 200.0;
    } else if (roomType == "Suite") {
        basePrice = 300.0;
    }
    
    totalCost = basePrice * duration;
}

bool BookingRequest::validateRequest() const {
    return client != nullptr && 
           occupants > 0 && 
           !roomType.empty() && 
           duration > 0;
}

void BookingRequest::addToList(BookingRequest* request) {
    if (!requestList) {
        requestList = request;
    } else {
        request->next = requestList;
        requestList = request;
    }
}

void BookingRequest::displayList() {
    BookingRequest* current = requestList;
    while (current) {
        current->displayInfo();
        OutputDebugStringA("-------------------\n");
        current = current->next;
    }
}

void BookingRequest::saveToFile(const std::string& filename) {
    std::ofstream file(filename);
    if (file.is_open()) {
        BookingRequest* current = requestList;
        while (current) {
            file << current->client->getName() << ","
                 << current->occupants << ","
                 << current->roomType << ","
                 << current->duration << ","
                 << current->status << ","
                 << current->totalCost << std::endl;
            current = current->next;
        }
        file.close();
    }
}

void BookingRequest::loadFromFile(const std::string& filename) {
    std::ifstream file(filename);
    if (file.is_open()) {
        std::string line;
        while (std::getline(file, line)) {
            std::stringstream ss(line);
            std::string clientName, roomType, status;
            int occupants, duration;
            double totalCost;
            
            std::getline(ss, clientName, ',');
            ss >> occupants;
            ss.ignore();
            std::getline(ss, roomType, ',');
            ss >> duration;
            ss.ignore();
            std::getline(ss, status, ',');
            ss >> totalCost;
            
            // Create new booking request
            BookingRequest* request = new BookingRequest();
            request->setOccupants(occupants);
            request->setRoomType(roomType);
            request->setDuration(duration);
            request->setStatus(status);
            request->setTotalCost(totalCost);
            addToList(request);
        }
        file.close();
    }
} 