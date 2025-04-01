#include "Models/Client.h"
#include <Windows.h>
#include <algorithm>

Client::Client() : Person(), password(""), balance(0.0) {}

Client::Client(const std::string& name, const std::string& email, 
               const std::string& phone, const std::string& address, 
               int age, const std::string& password)
    : Person(name, email, phone, address, age), password(password), balance(0.0) {}

Client::~Client() {
    // Clean up booking history
    for (auto booking : bookingHistory) {
        delete booking;
    }
    bookingHistory.clear();
}

bool Client::authenticate(const std::string& password) const {
    return this->password == password;
}

void Client::displayInfo() const {
    Person::displayInfo();
    std::string info = "Role: Client\n"
                      "Balance: $" + std::to_string(balance) + "\n"
                      "Number of Bookings: " + std::to_string(bookingHistory.size()) + "\n";
    OutputDebugStringA(info.c_str());
}

BookingRequest* Client::createBookingRequest(int occupants, const std::string& roomType, 
                                          int duration) {
    BookingRequest* request = new BookingRequest(this, occupants, roomType, duration);
    addBookingToHistory(request);
    return request;
}

bool Client::makePayment(double amount) {
    if (balance >= amount) {
        balance -= amount;
        return true;
    }
    return false;
}

void Client::addBookingToHistory(BookingRequest* booking) {
    bookingHistory.push_back(booking);
}

void Client::viewBookingHistory() const {
    std::string info = "Booking History for " + getName() + ":\n";
    OutputDebugStringA(info.c_str());
    for (const auto& booking : bookingHistory) {
        booking->displayInfo();
    }
}

bool Client::cancelBooking(BookingRequest* booking) {
    auto it = std::find(bookingHistory.begin(), bookingHistory.end(), booking);
    if (it != bookingHistory.end()) {
        bookingHistory.erase(it);
        delete booking;
        return true;
    }
    return false;
} 