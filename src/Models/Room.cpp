#include "Models/Room.h"
#include <Windows.h>
#include <string>

// Room class implementation
Room::Room() : roomNumber(0), type(""), isOccupied(false), 
               pricePerDay(0.0), maxOccupants(0) {}

Room::Room(int roomNumber, const std::string& type, double pricePerDay, int maxOccupants)
    : roomNumber(roomNumber), type(type), isOccupied(false), 
      pricePerDay(pricePerDay), maxOccupants(maxOccupants) {}

void Room::displayInfo() const {
    std::string info = "Room Number: " + std::to_string(roomNumber) + "\n" +
                      "Type: " + type + "\n" +
                      "Status: " + (isOccupied ? "Occupied" : "Available") + "\n" +
                      "Price per day: $" + std::to_string(pricePerDay) + "\n" +
                      "Max Occupants: " + std::to_string(maxOccupants) + "\n";
    OutputDebugStringA(info.c_str());
}

bool Room::isAvailable() const {
    return !isOccupied;
}

double Room::calculatePrice(int days) const {
    return pricePerDay * days;
}

bool Room::canAccommodate(int occupants) const {
    return occupants <= maxOccupants;
}

// RoomType class implementation
RoomType::RoomType() : Room(), features("") {}

RoomType::RoomType(int roomNumber, const std::string& type, 
                   double pricePerDay, int maxOccupants)
    : Room(roomNumber, type, pricePerDay, maxOccupants), features("") {}

void RoomType::displayInfo() const {
    Room::displayInfo();
    OutputDebugStringA(("Features: " + features + "\n").c_str());
}

bool RoomType::isAvailable() const {
    return Room::isAvailable();
}

double RoomType::calculatePrice(int days) const {
    return Room::calculatePrice(days);
}

bool RoomType::canAccommodate(int occupants) const {
    return Room::canAccommodate(occupants);
}

void RoomType::setRoomFeatures(const std::string& features) {
    this->features = features;
}

std::string RoomType::getRoomFeatures() const {
    return features;
}

// StandardRoom implementation
StandardRoom::StandardRoom(int roomNumber)
    : RoomType(roomNumber, "Standard", 100.0, 2) {
    setRoomFeatures("Basic amenities, TV, WiFi, Air conditioning");
}

void StandardRoom::displayInfo() const {
    OutputDebugStringA("=== Standard Room ===\n");
    RoomType::displayInfo();
}

// DeluxeRoom implementation
DeluxeRoom::DeluxeRoom(int roomNumber)
    : RoomType(roomNumber, "Deluxe", 200.0, 4) {
    setRoomFeatures("Premium amenities, Smart TV, High-speed WiFi, Mini bar, Ocean view");
}

void DeluxeRoom::displayInfo() const {
    OutputDebugStringA("=== Deluxe Room ===\n");
    RoomType::displayInfo();
}

// Suite implementation
Suite::Suite(int roomNumber)
    : RoomType(roomNumber, "Suite", 300.0, 6) {
    setRoomFeatures("Luxury amenities, 4K Smart TV, Ultra-fast WiFi, Full bar, Ocean view, Jacuzzi");
}

void Suite::displayInfo() const {
    OutputDebugStringA("=== Suite ===\n");
    RoomType::displayInfo();
} 