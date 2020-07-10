using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using UnityEngine;
using System.Xml.Serialization;

public class WinMod : MonoBehaviour
{
	public Rect screenRect;
	[DllImport("User32.dll")]
	private static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);
	[DllImport("user32.dll")]
	private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
	[DllImport("user32.dll")]
	private static extern IntPtr GetForegroundWindow();
	[DllImport("User32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
	private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
	[System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
	public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体

	// not used rigth now
	//const uint SWP_NOMOVE = 0x2;
	//const uint SWP_NOSIZE = 1;
	//const uint SWP_NOZORDER = 0x4;
	//const uint SWP_HIDEWINDOW = 0x0080;
	private const uint SWP_SHOWWINDOW = 0x0040;
	private const int GWL_STYLE = -16;
	private const int WS_BORDER = 1;

	private const string folder = "/WinXML/";
	private const string file = "win.xml";
	private const int defaultWidth = 1280;
	private const int defaultHeight = 720;
	private IntPtr handler;

	private void Start()
	{
		#if !UNITY_EDITOR     
		string name = Application.productName;
		handler = FindWindow(null, name);
		if (handler == IntPtr.Zero){
		handler = GetForegroundWindow();
		}
		SetForegroundWindow(handler);
		SetWindowLong(handler, GWL_STYLE, WS_BORDER);
		CreateDirectory(Application.streamingAssetsPath + folder);
		ScreenInfo info = LoadXml(Application.streamingAssetsPath + folder + file);
		bool result = SetWindowPos(handler, -1, (int)info.posX, (int)info.posY, (int)info.width, (int)info.height, SWP_SHOWWINDOW);
		#endif
	}

	private void CreateDirectory(string path)
	{
		if (!Directory.Exists(path)) Directory.CreateDirectory(path);
	}

	private ScreenInfo LoadXml(string path)
	{
		ScreenInfo info;
		if (File.Exists(path))
		{
			//TODO 文件存在 加载XML
			string text = File.ReadAllText(path);
			StringReader sr = new StringReader(text);
			XmlSerializer ser = new XmlSerializer(typeof(ScreenInfo));
			info = (ScreenInfo)ser.Deserialize(sr);
			sr.Close();
		}
		else
		{
			//TODO 文件不存在 创建XML
			info = new ScreenInfo()
			{
				height = screenRect.height == 0 ? defaultHeight : screenRect.height,
				width = screenRect.width == 0 ? defaultWidth : screenRect.width,
				posX = screenRect.x,
				posY = screenRect.y
			};
			XmlSerializer ser = new XmlSerializer(typeof(ScreenInfo));
			StringWriter sw = new StringWriter();
			ser.Serialize(sw, info);
			sw.Close();
			string text = sw.ToString();
			File.WriteAllText(path, text);
		}
		return info;
	}


}

[Serializable]
public class ScreenInfo
{
	public float width;
	public float height;
	public float posX;
	public float posY;
}


