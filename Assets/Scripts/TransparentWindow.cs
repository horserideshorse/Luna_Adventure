using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransparentWindow : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_LAYERED = 0x00080000;
    private const int WS_EX_TRANSPARENT = 0x00000020;
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOMOVE = 0x0002;
    private const uint SWP_SHOWWINDOW = 0x0040;

    private IntPtr hWnd;
    private MARGINS margins = new MARGINS { cxLeftWidth = -1 }; // 全透明区域

    void Start()
    {
#if !UNITY_EDITOR
        // 1. 获取窗口句柄
        hWnd = GetActiveWindow();

        // 2. 设置窗口透明（DWM 透明背景）
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        // 3. 设置窗口样式：支持透明 + 初始不可点击穿透
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);

        // 4. 设置窗口置顶
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);

        // 5. 设置相机背景透明
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color(0, 0, 0, 0);
#endif
    }

    void Update()
    {
#if !UNITY_EDITOR
        // 动态控制点击穿透：当鼠标悬停在 UI 或 3D 物体上时，允许点击；否则穿透
        bool overUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(MouseManager.Instance.MousePos);
        bool over3D = Physics2D.Raycast(worldPoint, Vector2.zero);
        //bool over3D = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _);

        SetClickThrough(!(overUI || over3D));
#endif
    }

    private void SetClickThrough(bool enabled)
    {
#if !UNITY_EDITOR
        int currentStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
        if (enabled)
        {
            // 开启点击穿透：添加 WS_EX_TRANSPARENT
            SetWindowLong(hWnd, GWL_EXSTYLE, currentStyle | WS_EX_TRANSPARENT);
        }
        else
        {
            // 关闭点击穿透：移除 WS_EX_TRANSPARENT
            SetWindowLong(hWnd, GWL_EXSTYLE, currentStyle & ~WS_EX_TRANSPARENT);
        }
#endif
    }

    // 可选：在 OnDestroy 中恢复窗口样式
    void OnDestroy()
    {
#if !UNITY_EDITOR
        if (hWnd != IntPtr.Zero)
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, 0);
        }
#endif
    }
}