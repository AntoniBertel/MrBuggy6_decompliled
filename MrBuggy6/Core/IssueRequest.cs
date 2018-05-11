// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.IssueRequest
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

namespace MrBuggy6.Core
{
  public class IssueRequest
  {
    public IssueRequestType Type { get; private set; }

    public Issue Issue { get; private set; }

    public object Tag { get; private set; }

    public IssueRequest(IssueRequestType type, Issue issue, object tag)
    {
      this.Type = type;
      this.Issue = issue;
      this.Tag = tag;
    }

    public IssueRequest(IssueRequestType type)
    {
      this.Type = type;
      this.Issue = (Issue) null;
      this.Tag = (object) null;
    }
  }
}
