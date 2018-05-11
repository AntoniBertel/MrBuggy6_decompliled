// Decompiled with JetBrains decompiler
// Type: Custom.Component.ListViewNumberComparer
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System.Windows.Forms;

namespace Custom.Component
{
  public class ListViewNumberComparer : IListViewComparer
  {
    public int Compare(ListViewItem itemX, ListViewItem itemY, int column)
    {
      int result1;
      if (!int.TryParse(itemX.SubItems[column].Text.ToString(), out result1))
        return -1;
      int result2;
      if (!int.TryParse(itemY.SubItems[column].Text.ToString(), out result2))
        return 1;
      if (result1 < result2)
        return -1;
      return result1 > result2 ? 1 : 0;
    }
  }
}
