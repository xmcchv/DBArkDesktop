using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;

/// <summary>
/// 一共可选择三种样式
/// </summary>
public enum enumWinStyle
{
    /// <summary>
    /// 置顶
    /// </summary>
    WinTop,
    /// <summary>
    /// 透明
    /// </summary>
    Apha,
    /// <summary>
    /// 置顶并且透明
    /// </summary>
    WinTopApha,
    /// <summary>
    /// 置顶透明并且可以穿透
    /// </summary>
    WinTopAphaPenetrate
}
public class WinSetting : MonoBehaviour
{

    #region Win函数常量
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, int bAlpha, int dwFlags);

    [DllImport("Dwmapi.dll")]
    static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    private const int WS_POPUP = 0x800000;
    private const int GWL_EXSTYLE = -20;
    private const int GWL_STYLE = -16;
    private const int WS_EX_LAYERED = 0x00080000;
    private const int WS_BORDER = 0x00800000;
    private const int WS_CAPTION = 0x00C00000;
    private const int SWP_SHOWWINDOW = 0x0040;
    private const int LWA_COLORKEY = 0x00000001;
    private const int LWA_ALPHA = 0x00000002;
    private const int WS_EX_TRANSPARENT = 0x20;
    //
    private const int ULW_COLORKEY = 0x00000001;
    private const int ULW_ALPHA = 0x00000002;
    private const int ULW_OPAQUE = 0x00000004;
    private const int ULW_EX_NORESIZE = 0x00000008;
    #endregion
    //
    public string strProduct;//项目名称
    public enumWinStyle WinStyle = enumWinStyle.WinTop;//窗体样式
    //
    public int ResWidth;//窗口宽度
    public int ResHeight;//窗口高度
    //
    public int currentX;//窗口左上角坐标x
    public int currentY;//窗口左上角坐标y
    //
    private bool isWinTop;//是否置顶
    private bool isApha;//是否透明
    private bool isAphaPenetrate;//是否要穿透窗体

    IntPtr hwnd;

    // Use this for initialization
    void Awake()
    {

        Screen.fullScreen = false;
        //#if UNITY_EDITOR
        //       print("编辑模式不更改窗体");
        //#else
        switch (WinStyle)
        {
            case enumWinStyle.WinTop:
                isApha = false;
                isAphaPenetrate = false;
                break;
            case enumWinStyle.Apha:
                isWinTop = false;
                isApha = true;
                isAphaPenetrate = false;
                break;
            case enumWinStyle.WinTopApha:
                isApha = true;
                isAphaPenetrate = false;
                break;
            case enumWinStyle.WinTopAphaPenetrate:
                isApha = true;
                isAphaPenetrate = true;
                break;
        }

        //
        //IntPtr hwnd = FindWindow(null, strProduct);
        hwnd = GetActiveWindow();

        //
        if (isApha)
        {
            //去边框并且透明
            SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED);
            int intExTemp = GetWindowLong(hwnd, GWL_EXSTYLE);
            if (isAphaPenetrate)//是否透明穿透窗体
            {
                SetWindowLong(hwnd, GWL_EXSTYLE, intExTemp | WS_EX_TRANSPARENT | WS_EX_LAYERED);
            }
            //
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_BORDER & ~WS_CAPTION);

            //保持中间位置：因为是从左上角算起的，所以获得屏幕像素后要减去窗体宽高的一半
            currentX = Screen.currentResolution.width / 2 - 320 / 2;
            currentY = Screen.currentResolution.height / 2 - 270 / 2;

            SetWindowPos(hwnd, -1, currentX, currentY, ResWidth, ResHeight, SWP_SHOWWINDOW);
            var margins = new MARGINS() { cxLeftWidth = -1 };
            //
            DwmExtendFrameIntoClientArea(hwnd, ref margins);

        }
        else
        {
            //单纯去边框
            SetWindowLong(hwnd, GWL_STYLE, WS_POPUP);
            SetWindowPos(hwnd, -1, currentX, currentY, ResWidth, ResHeight, SWP_SHOWWINDOW);
        }
        Debug.Log(WinStyle);
        //#endif
    }

    void OnApplicationQuit()
    {
        //程序退出的时候设置窗体为0像素，从打开到走到awake也需要一定是时间
        //会先有窗体边框，然后透明，这样会有闪一下的效果，
        //设置窗体为0像素后，下次打开是就是0像素，走到awake再设置回来正常的窗口大小
        //便能解决程序加载时会闪白色边框的现象
        SetWindowPos(hwnd, -1, currentX, currentY, 0, 0, SWP_SHOWWINDOW);
    }
}