#include "GUI/MainForm.h"

MainForm::MainForm() : hwnd(nullptr) {}

MainForm::~MainForm() {
    if (hwnd) {
        DestroyWindow(hwnd);
    }
}

LRESULT CALLBACK MainForm::WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
        case WM_DESTROY:
            PostQuitMessage(0);
            return 0;
        default:
            return DefWindowProc(hwnd, uMsg, wParam, lParam);
    }
}

bool MainForm::Initialize(HINSTANCE hInstance, int nCmdShow) {
    const wchar_t CLASS_NAME[] = L"Hotel Booking System";

    WNDCLASS wc = {};
    wc.lpfnWndProc = WindowProc;
    wc.hInstance = hInstance;
    wc.lpszClassName = CLASS_NAME;
    wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);

    RegisterClass(&wc);

    hwnd = CreateWindowEx(
        0,
        CLASS_NAME,
        L"Hotel Booking System",
        WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT, CW_USEDEFAULT, 800, 600,
        nullptr,
        nullptr,
        hInstance,
        nullptr
    );

    if (hwnd == nullptr) {
        return false;
    }

    ShowWindow(hwnd, nCmdShow);
    return true;
}

void MainForm::Run() {
    MSG msg = {};
    while (GetMessage(&msg, nullptr, 0, 0)) {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }
} 