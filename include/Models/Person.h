#pragma once
#include <string>
#include <vector>

class Person {
protected:
    std::string name;
    std::string email;
    std::string phone;
    std::string address;
    int age;
    static std::vector<Person*> personList;

public:
    // Constructors
    Person();
    Person(const std::string& name, const std::string& email, 
           const std::string& phone, const std::string& address, int age);

    // Virtual destructor
    virtual ~Person() = default;

    // Getters
    std::string getName() const { return name; }
    std::string getEmail() const { return email; }
    std::string getPhone() const { return phone; }
    std::string getAddress() const { return address; }
    int getAge() const { return age; }

    // Setters
    void setName(const std::string& name) { this->name = name; }
    void setEmail(const std::string& email) { this->email = email; }
    void setPhone(const std::string& phone) { this->phone = phone; }
    void setAddress(const std::string& address) { this->address = address; }
    void setAge(int age) { this->age = age; }

    // Virtual methods
    virtual void displayInfo() const;
    virtual bool validateContact() const;
    virtual void updateContact(const std::string& newEmail, const std::string& newPhone);
    virtual std::string getRole() const = 0;
    virtual bool authenticate(const std::string& password) const = 0;

    // Static methods
    static void saveToFile(const std::string& filename);
    static void loadFromFile(const std::string& filename);
}; 