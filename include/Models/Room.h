#pragma once
#include <string>

// Base Room class
class Room {
protected:
    int roomNumber;
    std::string type;
    bool isOccupied;
    double pricePerDay;
    int maxOccupants;

public:
    // Constructors
    Room();
    Room(int roomNumber, const std::string& type, double pricePerDay, int maxOccupants);

    // Virtual destructor
    virtual ~Room() = default;

    // Getters
    int getRoomNumber() const { return roomNumber; }
    std::string getType() const { return type; }
    bool getIsOccupied() const { return isOccupied; }
    double getPricePerDay() const { return pricePerDay; }
    int getMaxOccupants() const { return maxOccupants; }

    // Setters
    void setRoomNumber(int number) { roomNumber = number; }
    void setType(const std::string& type) { this->type = type; }
    void setIsOccupied(bool occupied) { isOccupied = occupied; }
    void setPricePerDay(double price) { pricePerDay = price; }
    void setMaxOccupants(int max) { maxOccupants = max; }

    // Virtual methods
    virtual void displayInfo() const;
    virtual bool isAvailable() const;
    virtual double calculatePrice(int days) const;
    virtual bool canAccommodate(int occupants) const;
};

// Sealed RoomType class
class RoomType final : public Room {
private:
    std::string features;

public:
    // Constructors
    RoomType();
    RoomType(int roomNumber, const std::string& type, double pricePerDay, int maxOccupants);

    // Override methods
    void displayInfo() const override;
    bool isAvailable() const override;
    double calculatePrice(int days) const override;
    bool canAccommodate(int occupants) const override;

    // RoomType specific methods
    void setRoomFeatures(const std::string& features);
    std::string getRoomFeatures() const;
};

// Derived room classes
class StandardRoom : public RoomType {
public:
    StandardRoom(int roomNumber);
    void displayInfo() const override;
};

class DeluxeRoom : public RoomType {
public:
    DeluxeRoom(int roomNumber);
    void displayInfo() const override;
};

class Suite : public RoomType {
public:
    Suite(int roomNumber);
    void displayInfo() const override;
}; 