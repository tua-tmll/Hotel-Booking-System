#include "Models/Administrator.h"
#include <Windows.h>
#include <algorithm>

Administrator::Administrator() : Person(), password("") {}

Administrator::Administrator(const std::string& name, const std::string& email, 
                           const std::string& phone, const std::string& address, 
                           int age, const std::string& password)
    : Person(name, email, phone, address, age), password(password) {}

Administrator::~Administrator() {
    // Clean up requests
    for (auto request : pendingRequests) {
        delete request;
    }
    pendingRequests.clear();

    for (auto request : processedRequests) {
        delete request;
    }
    processedRequests.clear();
}

bool Administrator::authenticate(const std::string& password) const {
    return this->password == password;
}

void Administrator::displayInfo() const {
    Person::displayInfo();
    std::string info = "Role: Administrator\n"
                      "Pending Requests: " + std::to_string(pendingRequests.size()) + "\n"
                      "Processed Requests: " + std::to_string(processedRequests.size()) + "\n";
    OutputDebugStringA(info.c_str());
}

void Administrator::addPendingRequest(BookingRequest* request) {
    pendingRequests.push_back(request);
}

bool Administrator::approveRequest(BookingRequest* request) {
    auto it = std::find(pendingRequests.begin(), pendingRequests.end(), request);
    if (it != pendingRequests.end()) {
        pendingRequests.erase(it);
        request->setStatus("Approved");
        processedRequests.push_back(request);
        return true;
    }
    return false;
}

bool Administrator::rejectRequest(BookingRequest* request) {
    auto it = std::find(pendingRequests.begin(), pendingRequests.end(), request);
    if (it != pendingRequests.end()) {
        pendingRequests.erase(it);
        request->setStatus("Rejected");
        processedRequests.push_back(request);
        return true;
    }
    return false;
}

void Administrator::viewPendingRequests() const {
    OutputDebugStringA("Pending Requests:\n");
    for (const auto& request : pendingRequests) {
        request->displayInfo();
    }
}

void Administrator::viewProcessedRequests() const {
    OutputDebugStringA("Processed Requests:\n");
    for (const auto& request : processedRequests) {
        request->displayInfo();
    }
}

void Administrator::processAllPendingRequests() {
    while (!pendingRequests.empty()) {
        approveRequest(pendingRequests.front());
    }
}

void Administrator::generateReport() const {
    std::string report = "Administrator Report\n"
                        "===================\n"
                        "Name: " + getName() + "\n"
                        "Total Pending Requests: " + std::to_string(pendingRequests.size()) + "\n"
                        "Total Processed Requests: " + std::to_string(processedRequests.size()) + "\n";
    
    int approvedCount = 0;
    int rejectedCount = 0;
    
    for (const auto& request : processedRequests) {
        if (request->getStatus() == "Approved") {
            approvedCount++;
        } else if (request->getStatus() == "Rejected") {
            rejectedCount++;
        }
    }
    
    report += "Approved Requests: " + std::to_string(approvedCount) + "\n"
              "Rejected Requests: " + std::to_string(rejectedCount) + "\n";
    
    OutputDebugStringA(report.c_str());
} 