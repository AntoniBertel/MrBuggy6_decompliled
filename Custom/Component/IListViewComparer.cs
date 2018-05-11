// Decompiled with JetBrains decompiler
// Type: Custom.Component.IListViewComparer
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System.Windows.Forms;

namespace Custom.Component
{
  public interface IListViewComparer
  {
    int Compare(ListViewItem itemX, ListViewItem itemY, int column);
  }
}
