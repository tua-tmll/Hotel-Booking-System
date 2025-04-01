import QtQuick
import QtQuick.Controls.Material
import QtQuick.Layouts

Page {
    id: loginPage
    title: qsTr("Login")

    Rectangle {
        anchors.fill: parent
        color: MaterialStyle.background

        ColumnLayout {
            anchors.centerIn: parent
            width: Math.min(parent.width * 0.9, 400)
            spacing: MaterialStyle.spacing * 2

            Label {
                text: "Hotel Booking System"
                font.pixelSize: 24
                Layout.alignment: Qt.AlignHCenter
            }

            Card {
                Layout.fillWidth: true
                Layout.preferredHeight: loginForm.implicitHeight + MaterialStyle.padding * 2

                ColumnLayout {
                    id: loginForm
                    anchors.fill: parent
                    anchors.margins: MaterialStyle.padding
                    spacing: MaterialStyle.spacing

                    TextField {
                        id: emailField
                        placeholderText: qsTr("Email")
                        Layout.fillWidth: true
                        Material.accent: MaterialStyle.primary
                    }

                    TextField {
                        id: passwordField
                        placeholderText: qsTr("Password")
                        echoMode: TextInput.Password
                        Layout.fillWidth: true
                        Material.accent: MaterialStyle.primary
                    }

                    Button {
                        text: qsTr("Login")
                        Layout.fillWidth: true
                        Material.background: MaterialStyle.primary
                        Material.foreground: "white"
                        onClicked: {
                            if (backend.login(emailField.text, passwordField.text)) {
                                stackView.replace(null, "qrc:/qml/RoomGrid.qml")
                            }
                        }
                    }

                    Button {
                        text: qsTr("Register")
                        Layout.fillWidth: true
                        flat: true
                        onClicked: stackView.push("qrc:/qml/RegisterForm.qml")
                    }
                }
            }
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