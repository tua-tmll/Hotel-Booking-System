#pragma once
#include "Models/Person.h"
#include "Models/BookingRequest.h"
#include <vector>

class Administrator : public Person {
private:
    std::string password;
    std::vector<BookingRequest*> pendingRequests;
    std::vector<BookingRequest*> processedRequests;

public:
    // Constructors
    Administrator();
    Administrator(const std::string& name, const std::string& email, 
                 const std::string& phone, const std::string& address, 
                 int age, const std::string& password);

    // Destructor
    ~Administrator() override;

    // Getters
    const std::vector<BookingRequest*>& getPendingRequests() const { return pendingRequests; }
    const std::vector<BookingRequest*>& getProcessedRequests() const { return processedRequests; }

    // Setters
    void setPassword(const std::string& password) { this->password = password; }

    // Overridden virtual methods
    std::string getRole() const override { return "Administrator"; }
    bool authenticate(const std::string& password) const override;
    void displayInfo() const override;

    // Administrator-specific methods
    void addPendingRequest(BookingRequest* request);
    bool approveRequest(BookingRequest* request);
    bool rejectRequest(BookingRequest* request);
    void viewPendingRequests() const;
    void viewProcessedRequests() const;
    void processAllPendingRequests();
    void generateReport() const;
}; 