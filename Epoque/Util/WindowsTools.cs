using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
/*xbb
 * 系统方法类
 * */
public class WindowsTools : MonoBehaviour
{

    //设置当前窗口的显示状态
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(System.IntPtr hwnd, int nCmdShow);

    //获取当前激活窗口
    [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
    public static extern System.IntPtr GetForegroundWindow();

    //设置窗口边框
    [DllImport("user32.dll")]
    public static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);

    //设置窗口位置，大小
    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    //窗口拖动
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

    //边框参数
    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;
    const int WS_POPUP = 0x800000;
    const int SW_SHOWMINIMIZED = 2;//(最小化窗口)

    public static string direction = "";
    IntPtr Handle;
    float xx;
    bool bx;
    //最小化窗口
    public void SetMinWindows()
    {
        ShowWindow(GetForegroundWindow(), SW_SHOWMINIMIZED);
        //具体窗口参数看这     https://msdn.microsoft.com/en-us/library/windows/desktop/ms633548(v=vs.85).aspx
    }

    //设置无边框，并设置框体大小，位置
    public void SetNoFrameWindow(Rect rect)
    {
        SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_POPUP);
        bool result = SetWindowPos(GetForegroundWindow(), 0, (int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height, SWP_SHOWWINDOW);
    }

    //拖动窗口
    public void DragWindow(IntPtr window)
    {
        ReleaseCapture();
        SendMessage(window, 0xA1, 0x02, 0);
        SendMessage(window, 0x0202, 0, 0);
    }
    public void init()
    {
#if UNITY_STANDALONE_WIN
        Handle = GetForegroundWindow();   //FindWindow ((string)null, "popu_windows");
#endif
    }
    void Start()
    {
#if UNITY_STANDALONE_WIN
        Handle = GetForegroundWindow();   //FindWindow ((string)null, "popu_windows");
#endif
    }
    void Update()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {

            xx = 0f;
            bx = true;
        }
        if (bx && xx >= 0.2f)
        { //这样做为了区分界面上面其它需要滑动的操作
            DragWindow(Handle);
        }
        if (bx)
            xx += Time.deltaTime;
        if (Input.GetMouseButtonUp(0))
        {

            xx = 0f;
            bx = false;

        }
#endif
    }
}
