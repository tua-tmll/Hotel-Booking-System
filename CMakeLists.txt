cmake_minimum_required(VERSION 3.15)
project(HotelBookingSystem)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_AUTOUIC ON)

# Find Qt packages
find_package(Qt6 COMPONENTS Core Gui Widgets Quick QuickControls2 REQUIRED)

if(WIN32)
    # Set WIN32 GUI subsystem
    set(CMAKE_EXE_LINKER_FLAGS "${CMAKE_EXE_LINKER_FLAGS} /SUBSYSTEM:WINDOWS")
endif()

# Add source files
set(SOURCES
    src/main.cpp
    src/Models/Person.cpp
    src/Models/Client.cpp
    src/Models/Administrator.cpp
    src/Models/BookingRequest.cpp
    src/Models/Room.cpp
    src/GUI/MainWindow.cpp
    src/GUI/LoginDialog.cpp
    src/GUI/BookingDialog.cpp
    src/GUI/RoomView.cpp
    src/GUI/AdminPanel.cpp
)

# Add header files
set(HEADERS
    include/Models/Person.h
    include/Models/Client.h
    include/Models/Administrator.h
    include/Models/BookingRequest.h
    include/Models/Room.h
    include/GUI/MainWindow.h
    include/GUI/LoginDialog.h
    include/GUI/BookingDialog.h
    include/GUI/RoomView.h
    include/GUI/AdminPanel.h
)

# Add QML files
set(QML_FILES
    src/GUI/qml/main.qml
    src/GUI/qml/LoginForm.qml
    src/GUI/qml/BookingForm.qml
    src/GUI/qml/RoomGrid.qml
    src/GUI/qml/AdminDashboard.qml
    src/GUI/qml/MaterialStyle.qml
)

# Create executable
add_executable(${PROJECT_NAME}
    ${SOURCES}
    ${HEADERS}
    ${QML_FILES}
)

# Include directories
target_include_directories(${PROJECT_NAME} PRIVATE
    ${CMAKE_SOURCE_DIR}/include
)

# Link Qt libraries
target_link_libraries(${PROJECT_NAME} PRIVATE
    Qt6::Core
    Qt6::Gui
    Qt6::Widgets
    Qt6::Quick
    Qt6::QuickControls2
)

# Set output directories
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)

# Copy QML files to build directory
file(COPY ${QML_FILES} DESTINATION ${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/qml)

# Enable solution folder support
set_property(GLOBAL PROPERTY USE_FOLDERS ON) 