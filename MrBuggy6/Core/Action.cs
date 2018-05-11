// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.Action
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

namespace MrBuggy6.Core
{
  public class Action
  {
    public uint ViewId { get; set; }

    public uint Id { get; set; }

    public Action()
    {
    }

    public Action(uint viewid, uint id)
    {
      this.ViewId = viewid;
      this.Id = id;
    }

    public override string ToString()
    {
      return ((int) this.ViewId).ToString() + "." + (object) this.Id;
    }
  }
}
