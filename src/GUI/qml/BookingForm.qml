import QtQuick
import QtQuick.Controls.Material
import QtQuick.Layouts

Page {
    id: bookingPage
    title: qsTr("Book a Room")

    property int roomNumber
    property string roomType

    Rectangle {
        anchors.fill: parent
        color: MaterialStyle.background

        ScrollView {
            anchors.fill: parent
            anchors.margins: MaterialStyle.padding
            clip: true

            ColumnLayout {
                width: Math.min(parent.width, 600)
                anchors.horizontalCenter: parent.horizontalCenter
                spacing: MaterialStyle.spacing * 2

                Card {
                    Layout.fillWidth: true
                    Layout.preferredHeight: bookingForm.implicitHeight + MaterialStyle.padding * 2

                    ColumnLayout {
                        id: bookingForm
                        anchors.fill: parent
                        anchors.margins: MaterialStyle.padding
                        spacing: MaterialStyle.spacing

                        Label {
                            text: "Room Details"
                            font.pixelSize: 20
                            Layout.fillWidth: true
                        }

                        Label {
                            text: "Room " + roomNumber + " - " + roomType
                            Layout.fillWidth: true
                        }

                        SpinBox {
                            id: occupantsSpinner
                            from: 1
                            to: 6
                            value: 1
                            Layout.fillWidth: true
                            Label {
                                text: "Number of Occupants"
                                anchors.bottom: parent.top
                            }
                        }

                        Label {
                            text: "Check-in Date"
                            Layout.fillWidth: true
                        }

                        TextField {
                            id: checkInField
                            placeholderText: "YYYY-MM-DD"
                            Layout.fillWidth: true
                            inputMask: "9999-99-99"
                            Material.accent: MaterialStyle.primary
                        }

                        Label {
                            text: "Duration (nights)"
                            Layout.fillWidth: true
                        }

                        SpinBox {
                            id: durationSpinner
                            from: 1
                            to: 30
                            value: 1
                            Layout.fillWidth: true
                        }

                        Label {
                            text: "Special Requests"
                            Layout.fillWidth: true
                        }

                        TextArea {
                            id: specialRequestsField
                            placeholderText: "Enter any special requests..."
                            Layout.fillWidth: true
                            Layout.preferredHeight: 100
                            Material.accent: MaterialStyle.primary
                            wrapMode: TextArea.Wrap
                        }

                        RowLayout {
                            Layout.fillWidth: true
                            spacing: MaterialStyle.spacing

                            Label {
                                text: "Total Cost:"
                                font.bold: true
                            }

                            Label {
                                text: "$" + backend.calculateTotalCost(roomType, durationSpinner.value)
                                font.bold: true
                            }
                        }

                        Button {
                            text: qsTr("Book Now")
                            Layout.fillWidth: true
                            Material.background: MaterialStyle.primary
                            Material.foreground: "white"
                            onClicked: {
                                if (backend.createBooking(roomNumber, 
                                                        occupantsSpinner.value,
                                                        checkInField.text,
                                                        durationSpinner.value,
                                                        specialRequestsField.text)) {
                                    confirmationDialog.open()
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    Dialog {
        id: confirmationDialog
        title: "Booking Confirmed"
        standardButtons: Dialog.Ok
        x: (parent.width - width) / 2
        y: (parent.height - height) / 2
        modal: true

        onAccepted: {
            stackView.pop()
        }

        Label {
            text: "Your booking has been confirmed!\n" +
                  "A confirmation email has been sent to your registered email address."
            horizontalAlignment: Text.AlignHCenter
            wrapMode: Text.Wrap
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