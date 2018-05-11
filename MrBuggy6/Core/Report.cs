// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.Report
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;

namespace MrBuggy6.Core
{
  public class Report
  {
    public const string NotSynchronizationDateString = "none";

    public string Content { get; set; }

    public bool Sent { get; set; }

    public DateTime SynchronizationDate { get; set; }

    public string SynchronizationDateAsString
    {
      get
      {
        if (this.SynchronizationDate == new DateTime(1, 1, 1, 0, 0, 0))
          return "none";
        return this.SynchronizationDate.ToString("yyyy-MM-dd HH:mm:ss");
      }
    }

    public Report()
    {
      this.Content = string.Empty;
      this.Sent = false;
    }
  }
}
