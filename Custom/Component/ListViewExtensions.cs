// Decompiled with JetBrains decompiler
// Type: Custom.Component.ListViewExtensions
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Custom.Component
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public static class ListViewExtensions
  {
    public const int LVM_FIRST = 4096;
    public const int LVM_GETHEADER = 4127;
    public const int HDM_FIRST = 4608;
    public const int HDM_GETITEM = 4619;
    public const int HDM_SETITEM = 4620;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, ref ListViewExtensions.HDITEM lParam);

    public static void SetSortIcon(this ListView listViewControl, int columnIndex, SortOrder order)
    {
      IntPtr hWnd = ListViewExtensions.SendMessage(listViewControl.Handle, 4127U, IntPtr.Zero, IntPtr.Zero);
      for (int index = 0; index <= listViewControl.Columns.Count - 1; ++index)
      {
        IntPtr wParam = new IntPtr(index);
        ListViewExtensions.HDITEM lParam = new ListViewExtensions.HDITEM()
        {
          mask = ListViewExtensions.HDITEM.Mask.Format
        };
        if (ListViewExtensions.SendMessage(hWnd, 4619U, wParam, ref lParam) == IntPtr.Zero)
          throw new Win32Exception();
        if (order != SortOrder.None && index == columnIndex)
        {
          switch (order)
          {
            case SortOrder.Ascending:
              lParam.fmt &= ~ListViewExtensions.HDITEM.Format.SortDown;
              lParam.fmt |= ListViewExtensions.HDITEM.Format.SortUp;
              break;
            case SortOrder.Descending:
              lParam.fmt &= ~ListViewExtensions.HDITEM.Format.SortUp;
              lParam.fmt |= ListViewExtensions.HDITEM.Format.SortDown;
              break;
          }
        }
        else
          lParam.fmt &= ~(ListViewExtensions.HDITEM.Format.SortDown | ListViewExtensions.HDITEM.Format.SortUp);
        if (ListViewExtensions.SendMessage(hWnd, 4620U, wParam, ref lParam) == IntPtr.Zero)
          throw new Win32Exception();
      }
    }

    public struct HDITEM
    {
      public ListViewExtensions.HDITEM.Mask mask;
      public int cxy;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string pszText;
      public IntPtr hbm;
      public int cchTextMax;
      public ListViewExtensions.HDITEM.Format fmt;
      public IntPtr lParam;
      public int iImage;
      public int iOrder;
      public uint type;
      public IntPtr pvFilter;
      public uint state;

      [Flags]
      public enum Mask
      {
        Format = 4,
      }

      [Flags]
      public enum Format
      {
        SortDown = 512, // 0x00000200
        SortUp = 1024, // 0x00000400
      }
    }
  }
}
