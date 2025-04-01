import QtQuick
import QtQuick.Controls.Material
import QtQuick.Layouts

Page {
    id: adminPage
    title: qsTr("Admin Dashboard")

    Rectangle {
        anchors.fill: parent
        color: MaterialStyle.background

        ColumnLayout {
            anchors.fill: parent
            anchors.margins: MaterialStyle.padding
            spacing: MaterialStyle.spacing * 2

            // Statistics Cards
            Row {
                Layout.fillWidth: true
                spacing: MaterialStyle.spacing

                Repeater {
                    model: [
                        { title: "Total Bookings", value: backend.totalBookings },
                        { title: "Active Bookings", value: backend.activeBookings },
                        { title: "Available Rooms", value: backend.availableRooms },
                        { title: "Total Revenue", value: "$" + backend.totalRevenue }
                    ]

                    Card {
                        width: (adminPage.width - MaterialStyle.padding * 5) / 4
                        height: 100
                        color: index === 0 ? MaterialStyle.primary :
                               index === 1 ? MaterialStyle.accent :
                               index === 2 ? "#4CAF50" : "#FF9800"

                        ColumnLayout {
                            anchors.fill: parent
                            anchors.margins: MaterialStyle.padding

                            Label {
                                text: modelData.title
                                color: "white"
                                font.pixelSize: 14
                                Layout.fillWidth: true
                            }

                            Label {
                                text: modelData.value
                                color: "white"
                                font.pixelSize: 24
                                font.bold: true
                                Layout.fillWidth: true
                            }
                        }
                    }
                }
            }

            // Pending Bookings
            Card {
                Layout.fillWidth: true
                Layout.preferredHeight: 300

                ColumnLayout {
                    anchors.fill: parent
                    anchors.margins: MaterialStyle.padding
                    spacing: MaterialStyle.spacing

                    Label {
                        text: "Pending Bookings"
                        font.pixelSize: 20
                        Layout.fillWidth: true
                    }

                    ListView {
                        Layout.fillWidth: true
                        Layout.fillHeight: true
                        model: backend.pendingBookings
                        clip: true
                        spacing: MaterialStyle.spacing

                        delegate: ItemDelegate {
                            width: parent.width
                            height: 80

                            RowLayout {
                                anchors.fill: parent
                                anchors.margins: MaterialStyle.padding
                                spacing: MaterialStyle.spacing

                                ColumnLayout {
                                    Layout.fillWidth: true
                                    spacing: 2

                                    Label {
                                        text: model.clientName
                                        font.bold: true
                                    }

                                    Label {
                                        text: "Room " + model.roomNumber + " - " + model.roomType
                                    }

                                    Label {
                                        text: model.checkInDate + " (" + model.duration + " nights)"
                                        font.pixelSize: 12
                                        opacity: 0.7
                                    }
                                }

                                Button {
                                    text: "Approve"
                                    Material.background: "#4CAF50"
                                    Material.foreground: "white"
                                    onClicked: backend.approveBooking(model.id)
                                }

                                Button {
                                    text: "Reject"
                                    Material.background: "#F44336"
                                    Material.foreground: "white"
                                    onClicked: backend.rejectBooking(model.id)
                                }
                            }
                        }
                    }
                }
            }

            // Room Management
            Card {
                Layout.fillWidth: true
                Layout.fillHeight: true

                ColumnLayout {
                    anchors.fill: parent
                    anchors.margins: MaterialStyle.padding
                    spacing: MaterialStyle.spacing

                    RowLayout {
                        Layout.fillWidth: true

                        Label {
                            text: "Room Management"
                            font.pixelSize: 20
                        }

                        Item { Layout.fillWidth: true }

                        Button {
                            text: "Add Room"
                            Material.background: MaterialStyle.primary
                            Material.foreground: "white"
                            onClicked: addRoomDialog.open()
                        }
                    }

                    TableView {
                        Layout.fillWidth: true
                        Layout.fillHeight: true
                        model: backend.roomModel
                        clip: true

                        delegate: ItemDelegate {
                            implicitWidth: 120
                            implicitHeight: 40
                            text: display
                        }
                    }
                }
            }
        }
    }

    Dialog {
        id: addRoomDialog
        title: "Add New Room"
        standardButtons: Dialog.Ok | Dialog.Cancel
        x: (parent.width - width) / 2
        y: (parent.height - height) / 2

        ColumnLayout {
            spacing: MaterialStyle.spacing

            TextField {
                id: roomNumberField
                placeholderText: "Room Number"
                Layout.fillWidth: true
                validator: IntValidator { bottom: 1; top: 999 }
            }

            ComboBox {
                id: roomTypeCombo
                model: ["Standard", "Deluxe", "Suite"]
                Layout.fillWidth: true
            }

            SpinBox {
                id: maxOccupantsSpinner
                from: 1
                to: 6
                value: 2
                Layout.fillWidth: true
            }

            TextField {
                id: priceField
                placeholderText: "Price per Night"
                Layout.fillWidth: true
                validator: DoubleValidator { bottom: 0; decimals: 2 }
            }
        }

        onAccepted: {
            backend.addRoom(roomNumberField.text,
                          roomTypeCombo.currentText,
                          maxOccupantsSpinner.value,
                          parseFloat(priceField.text))
        }
    }
}

// Card component with Material Design elevation
component Card: Rectangle {
    color: MaterialStyle.surface
    radius: MaterialStyle.radius
    layer.enabled: true
    layer.effect: ElevationEffect {
        elevation: MaterialStyle.elevation2
    }
} 