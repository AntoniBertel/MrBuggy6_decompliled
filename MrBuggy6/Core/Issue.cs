// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.Issue
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

namespace MrBuggy6.Core
{
  public class Issue
  {
    public static uint NextId = 1;

    public uint InternalId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IssueAttr1 Attr1 { get; set; }

    public IssueAttr2 Attr2 { get; set; }

    public IssueAttr3 Attr3 { get; set; }

    public IssueTask Task { get; set; }

    public bool Sent { get; set; }
  }
}
