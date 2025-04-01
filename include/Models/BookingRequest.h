#pragma once
#include <string>

// Forward declaration
class Client;

class BookingRequest {
private:
    Client* client;
    int occupants;
    std::string roomType;
    int duration;
    std::string status;
    double totalCost;
    BookingRequest* next;  // For linked list implementation
    static BookingRequest* requestList;

public:
    // Constructors
    BookingRequest();
    BookingRequest(Client* client, int occupants, const std::string& roomType, int duration);

    // Destructor
    ~BookingRequest();

    // Getters
    Client* getClient() const { return client; }
    int getOccupants() const { return occupants; }
    std::string getRoomType() const { return roomType; }
    int getDuration() const { return duration; }
    std::string getStatus() const { return status; }
    double getTotalCost() const { return totalCost; }

    // Setters
    void setClient(Client* client) { this->client = client; }
    void setOccupants(int occupants) { this->occupants = occupants; }
    void setRoomType(const std::string& roomType) { this->roomType = roomType; }
    void setDuration(int duration) { this->duration = duration; }
    void setStatus(const std::string& status) { this->status = status; }
    void setTotalCost(double cost) { this->totalCost = cost; }

    // Methods
    void displayInfo() const;
    void calculateTotalCost();
    bool validateRequest() const;
    
    // Static methods
    static void addToList(BookingRequest* request);
    static void displayList();
    static void saveToFile(const std::string& filename);
    static void loadFromFile(const std::string& filename);

    friend class Client;
    friend class Administrator;
}; 