#pragma once
#include <Windows.h>

class MainForm {
private:
    HWND hwnd;
    static LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

public:
    MainForm();
    ~MainForm();

    bool Initialize(HINSTANCE hInstance, int nCmdShow);
    void Run();
}; 