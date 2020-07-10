using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
/*xbb
 * 系统方法类
 * */
public class WindowsTool :MonoBehaviour
{

    //获取当前激活窗口
    [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
    public static extern System.IntPtr GetForegroundWindow();
    //查找窗口
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

    //设置窗口位置，大小
    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    //窗口拖动
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

    [DllImport("user32.dll")]
    private static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect);

    //边框参数
    public const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;
    const int WS_POPUP = 0x800000;
    const int SW_SHOWMINIMIZED = 2;//(最小化窗口)

    public static IntPtr Handle;
    public static bool bx;
    public Rect screenPosition;
    public static Rect sPosition;

    //
    public static string direction;
    //设置无边框，并设置框体大小，位置
    public static void SetNoFrameWindow(Rect rect)
    {
        //SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_POPUP);
        bool result = SetWindowPos(GetForegroundWindow(), 0, (int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height, SWP_SHOWWINDOW);
    }

    //拖动窗口
    public static void DragWindow(IntPtr window)
    {
        ReleaseCapture();
        SendMessage(window, 0xA1, 0x02, 0);
        SendMessage(window, 0x0202, 0, 0);
    }
    public void Start()
    {
#if UNITY_STANDALONE_WIN
        Handle = GetForegroundWindow();   //FindWindow ((string)null, "popu_windows");
#endif
    }
    public static Rect getrect()
    {
        GetWindowRect(Handle, out sPosition);
        return sPosition;
    }
    public void Update()
    {
#if UNITY_STANDALONE_WIN
        
        if (bx)
        { //这样做为了区分界面上面其它需要滑动的操作
            //DragWindow(Handle);
            //GetWindowRect(Handle, out screenPosition);
            int postionx = (int)screenPosition.x;
            var rt = gameObject.GetComponent<Transform>();
            switch (direction)
            {
                case "left": postionx -= 5; transform.Translate(Vector2.left * 5); break;// rt.position = new Vector2(rt.position.x-5,rt.position.y); break;
                case "right": postionx += 5; transform.Translate(Vector2.right * 5); break;// rt.position = new Vector2(rt.position.x + 5, rt.position.y); break;
            }
            print(postionx);
            Debug.LogWarning(postionx);
            if (postionx >= 1500 || postionx <= 50)
            {
                return;
            }
            //SetWindowPos(GetForegroundWindow(), 0, postionx, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
            
            //var rt = gameObject.GetComponent<RectTransform>();

            bx = false;
        }
#endif
    }
}
