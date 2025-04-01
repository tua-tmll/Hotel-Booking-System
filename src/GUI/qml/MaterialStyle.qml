pragma Singleton
import QtQuick
import QtQuick.Controls.Material

QtObject {
    readonly property color primary: "#1976D2"
    readonly property color primaryDark: "#1565C0"
    readonly property color accent: "#FF4081"
    readonly property color background: "#FAFAFA"
    readonly property color surface: "#FFFFFF"
    readonly property color error: "#B00020"

    readonly property int elevation1: 2
    readonly property int elevation2: 4
    readonly property int elevation3: 8
    readonly property int elevation4: 16

    readonly property real spacing: 8
    readonly property real padding: 16
    readonly property real radius: 4

    readonly property FontLoader materialIcons: FontLoader {
        source: "qrc:/fonts/MaterialIcons-Regular.ttf"
    }
} 