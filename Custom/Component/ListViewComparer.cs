// Decompiled with JetBrains decompiler
// Type: Custom.Component.ListViewComparer
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System.Collections;
using System.Windows.Forms;

namespace Custom.Component
{
  public class ListViewComparer : IComparer
  {
    private int _mainColumn;
    private int _defaultColumn;
    private SortOrder _defaultOrder;

    public int Column { get; private set; }

    public SortOrder Order { get; private set; }

    public ListView Owner { get; private set; }

    public IListViewComparer[] Comparers { get; private set; }

    public ListViewComparer(ListView owner)
    {
      this.Init(owner, 0, SortOrder.Ascending, 0);
    }

    public ListViewComparer(ListView owner, int column)
    {
      this.Init(owner, column, SortOrder.Ascending, column);
    }

    public ListViewComparer(ListView owner, int column, SortOrder order)
    {
      this.Init(owner, column, order, column);
    }

    public ListViewComparer(ListView owner, int column, SortOrder order, int mainColumn)
    {
      this.Init(owner, column, order, mainColumn);
    }

    private void Init(ListView owner, int column, SortOrder order, int mainColumn)
    {
      this.Owner = owner;
      this.Column = column;
      this.Order = order;
      this._defaultColumn = column;
      this._defaultOrder = order;
      this._mainColumn = mainColumn;
      this.Comparers = new IListViewComparer[owner.Columns.Count];
      for (int index = 0; index < this.Comparers.Length; ++index)
        this.Comparers[index] = (IListViewComparer) new ListViewTextComparer();
      this.Owner.ListViewItemSorter = (IComparer) this;
      this.Owner.SetSortIcon(this.Column, this.Order);
      this.Owner.Sort();
    }

    public void Reset()
    {
      this.Column = this._defaultColumn;
      this.Order = this._defaultOrder;
      this.Owner.SetSortIcon(this.Column, this.Order);
      this.Owner.Sort();
    }

    public void Sort(int column)
    {
      if (column != this.Column)
      {
        this.Column = column;
        this.Order = SortOrder.Ascending;
      }
      else
        this.Order = this.Order != SortOrder.Ascending ? SortOrder.Ascending : SortOrder.Descending;
      this.Owner.SetSortIcon(this.Column, this.Order);
      this.Owner.Sort();
    }

    public virtual int Compare(object x, object y)
    {
      ListViewItem itemX = x as ListViewItem;
      ListViewItem itemY = y as ListViewItem;
      int num = this.Comparers[this.Column].Compare(itemX, itemY, this.Column);
      if (num == 0 && this.Column != this._mainColumn)
        num = this.Comparers[this._mainColumn].Compare(itemX, itemY, this._mainColumn);
      if (this.Order == SortOrder.Descending)
        num *= -1;
      return num;
    }
  }
}
