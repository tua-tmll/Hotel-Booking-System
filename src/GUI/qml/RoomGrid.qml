import QtQuick
import QtQuick.Controls.Material
import QtQuick.Layouts

Page {
    id: roomGridPage
    title: qsTr("Available Rooms")

    Rectangle {
        anchors.fill: parent
        color: MaterialStyle.background

        ScrollView {
            anchors.fill: parent
            anchors.margins: MaterialStyle.padding
            clip: true

            GridView {
                id: roomGrid
                model: backend.roomModel
                cellWidth: Math.floor(width / Math.floor(width / 300))
                cellHeight: 200

                delegate: Card {
                    width: roomGrid.cellWidth - MaterialStyle.spacing
                    height: roomGrid.cellHeight - MaterialStyle.spacing

                    ColumnLayout {
                        anchors.fill: parent
                        anchors.margins: MaterialStyle.padding
                        spacing: MaterialStyle.spacing

                        Rectangle {
                            Layout.fillWidth: true
                            Layout.preferredHeight: 100
                            color: MaterialStyle.primary
                            radius: MaterialStyle.radius

                            Label {
                                anchors.centerIn: parent
                                text: model.type
                                color: "white"
                                font.pixelSize: 18
                            }
                        }

                        Label {
                            text: "Room " + model.number
                            font.pixelSize: 16
                            Layout.fillWidth: true
                        }

                        Label {
                            text: "Price: $" + model.price + "/night"
                            Layout.fillWidth: true
                        }

                        Label {
                            text: "Max Occupants: " + model.maxOccupants
                            Layout.fillWidth: true
                        }

                        Button {
                            text: qsTr("Book Now")
                            Layout.fillWidth: true
                            Material.background: MaterialStyle.accent
                            Material.foreground: "white"
                            enabled: !model.isOccupied
                            onClicked: {
                                stackView.push("qrc:/qml/BookingForm.qml", 
                                    { roomNumber: model.number, roomType: model.type })
                            }
                        }
                    }
                }
            }
        }

        FloatingActionButton {
            anchors.right: parent.right
            anchors.bottom: parent.bottom
            anchors.margins: MaterialStyle.padding * 2
            text: "+"
            onClicked: filterDialog.open()
        }
    }

    Dialog {
        id: filterDialog
        title: "Filter Rooms"
        standardButtons: Dialog.Ok | Dialog.Cancel
        x: (parent.width - width) / 2
        y: (parent.height - height) / 2

        ColumnLayout {
            spacing: MaterialStyle.spacing

            ComboBox {
                id: roomTypeFilter
                model: ["All", "Standard", "Deluxe", "Suite"]
                Layout.fillWidth: true
            }

            RangeSlider {
                id: priceRangeSlider
                from: 0
                to: 1000
                stepSize: 50
                snapMode: RangeSlider.SnapAlways
                Layout.fillWidth: true

                Label {
                    text: "Price Range: $" + priceRangeSlider.first.value + 
                          " - $" + priceRangeSlider.second.value
                    anchors.bottom: parent.top
                }
            }

            SpinBox {
                id: occupantsFilter
                from: 1
                to: 6
                value: 1
                Layout.fillWidth: true
                Label {
                    text: "Minimum Occupants"
                    anchors.bottom: parent.top
                }
            }
        }

        onAccepted: {
            backend.filterRooms(roomTypeFilter.currentText,
                              priceRangeSlider.first.value,
                              priceRangeSlider.second.value,
                              occupantsFilter.value)
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

// Floating Action Button component
component FloatingActionButton: Button {
    width: 56
    height: 56
    font.pixelSize: 24
    Material.background: MaterialStyle.accent
    Material.foreground: "white"
    layer.enabled: true
    layer.effect: ElevationEffect {
        elevation: MaterialStyle.elevation4
    }
} 