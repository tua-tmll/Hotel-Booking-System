#pragma once
#include "Models/Person.h"
#include "Models/BookingRequest.h"
#include <vector>

class Client : public Person {
private:
    std::vector<BookingRequest*> bookingHistory;
    std::string password;
    double balance;

public:
    // Constructors
    Client();
    Client(const std::string& name, const std::string& email, 
           const std::string& phone, const std::string& address, 
           int age, const std::string& password);

    // Destructor
    ~Client() override;

    // Getters
    const std::vector<BookingRequest*>& getBookingHistory() const { return bookingHistory; }
    double getBalance() const { return balance; }

    // Setters
    void setPassword(const std::string& password) { this->password = password; }
    void setBalance(double balance) { this->balance = balance; }

    // Overridden virtual methods
    std::string getRole() const override { return "Client"; }
    bool authenticate(const std::string& password) const override;
    void displayInfo() const override;

    // Client-specific methods
    BookingRequest* createBookingRequest(int occupants, const std::string& roomType, 
                                       int duration);
    bool makePayment(double amount);
    void addBookingToHistory(BookingRequest* booking);
    void viewBookingHistory() const;
    bool cancelBooking(BookingRequest* booking);
}; 