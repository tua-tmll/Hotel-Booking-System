#include "Models/Person.h"
#include <fstream>
#include <Windows.h>

// Initialize static member
std::vector<Person*> Person::personList;

Person::Person() : name(""), email(""), phone(""), address(""), age(0) {
    personList.push_back(this);
}

Person::Person(const std::string& name, const std::string& email, 
               const std::string& phone, const std::string& address, int age)
    : name(name), email(email), phone(phone), address(address), age(age) {
    personList.push_back(this);
}

void Person::displayInfo() const {
    std::string info = "Name: " + name + "\n" +
                      "Email: " + email + "\n" +
                      "Phone: " + phone + "\n" +
                      "Address: " + address + "\n" +
                      "Age: " + std::to_string(age) + "\n";
    OutputDebugStringA(info.c_str());
}

bool Person::validateContact() const {
    return !email.empty() && !phone.empty();
}

void Person::updateContact(const std::string& newEmail, const std::string& newPhone) {
    email = newEmail;
    phone = newPhone;
}

void Person::saveToFile(const std::string& filename) {
    std::ofstream file(filename);
    if (file.is_open()) {
        for (const auto& person : personList) {
            file << person->name << ","
                 << person->email << ","
                 << person->phone << ","
                 << person->address << ","
                 << person->age << std::endl;
        }
        file.close();
    }
}

void Person::loadFromFile(const std::string& filename) {
    std::ifstream file(filename);
    if (file.is_open()) {
        std::string line;
        while (std::getline(file, line)) {
            // Implementation for loading data
        }
        file.close();
    }
} 