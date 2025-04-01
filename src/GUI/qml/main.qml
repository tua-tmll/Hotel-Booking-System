import QtQuick
import QtQuick.Controls.Material
import QtQuick.Layouts

ApplicationWindow {
    id: window
    visible: true
    width: 1280
    height: 720
    title: "Hotel Booking System"

    Material.theme: Material.Light
    Material.accent: MaterialStyle.accent
    Material.primary: MaterialStyle.primary

    header: ToolBar {
        RowLayout {
            anchors.fill: parent
            ToolButton {
                text: qsTr("≡")
                onClicked: drawer.open()
            }
            Label {
                text: "Hotel Booking System"
                elide: Label.ElideRight
                horizontalAlignment: Qt.AlignHCenter
                verticalAlignment: Qt.AlignVCenter
                Layout.fillWidth: true
            }
            ToolButton {
                text: qsTr("⋮")
                onClicked: optionsMenu.open()
            }
        }
    }

    Drawer {
        id: drawer
        width: Math.min(window.width, 300)
        height: window.height

        Column {
            anchors.fill: parent

            ItemDelegate {
                text: qsTr("Home")
                width: parent.width
                onClicked: {
                    stackView.push("qrc:/qml/RoomGrid.qml")
                    drawer.close()
                }
            }
            ItemDelegate {
                text: qsTr("My Bookings")
                width: parent.width
                onClicked: {
                    stackView.push("qrc:/qml/BookingForm.qml")
                    drawer.close()
                }
            }
            ItemDelegate {
                text: qsTr("Admin Panel")
                width: parent.width
                visible: backend.isAdmin
                onClicked: {
                    stackView.push("qrc:/qml/AdminDashboard.qml")
                    drawer.close()
                }
            }
        }
    }

    StackView {
        id: stackView
        anchors.fill: parent
        initialItem: LoginForm {}
    }

    Menu {
        id: optionsMenu
        x: parent.width - width
        transformOrigin: Menu.TopRight

        MenuItem {
            text: qsTr("Settings")
            onTriggered: settingsDialog.open()
        }
        MenuItem {
            text: qsTr("About")
            onTriggered: aboutDialog.open()
        }
        MenuItem {
            text: qsTr("Logout")
            onTriggered: {
                backend.logout()
                stackView.replace(null, "qrc:/qml/LoginForm.qml")
            }
        }
    }

    Dialog {
        id: settingsDialog
        title: "Settings"
        standardButtons: Dialog.Ok | Dialog.Cancel
        x: (window.width - width) / 2
        y: (window.height - height) / 2

        ColumnLayout {
            spacing: MaterialStyle.spacing

            Switch {
                text: qsTr("Dark Theme")
                onCheckedChanged: window.Material.theme = checked ? Material.Dark : Material.Light
            }
        }
    }

    Dialog {
        id: aboutDialog
        title: "About"
        standardButtons: Dialog.Ok
        x: (window.width - width) / 2
        y: (window.height - height) / 2

        Label {
            text: qsTr("Hotel Booking System\nVersion 1.0")
            horizontalAlignment: Text.AlignHCenter
        }
    }
} 