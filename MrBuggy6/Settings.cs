// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Settings
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using MrBuggy6.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace MrBuggy6
{
  public static class Settings
  {
    private static readonly DateTime _dateLimit = new DateTime(2018, 5, 29, 19, 0, 0);
    public static User User = (User) null;
    public static ReportFile ReportFile = (ReportFile) null;
    public static List<IssueAttr1> Attr1List = new List<IssueAttr1>()
    {
      new IssueAttr1() { Id = 1U, Name = "Attr 1.1" },
      new IssueAttr1() { Id = 2U, Name = "Attr 1.2" },
      new IssueAttr1() { Id = 3U, Name = "Attr 1.3" }
    };
    public static List<IssueAttr2> Attr2List = new List<IssueAttr2>()
    {
      new IssueAttr2() { Id = 1U, Name = "Default" }
    };
    public static List<IssueAttr3> Attr3List = new List<IssueAttr3>()
    {
      new IssueAttr3() { Id = 1U, Name = "Default" }
    };
    public static List<IssueTask> Tasks = new List<IssueTask>()
    {
      new IssueTask()
      {
        Id = 1,
        Title = "Task 1",
        Description = "Task 1 description."
      },
      new IssueTask()
      {
        Id = 2,
        Title = "Task 2",
        Description = "Task 2 description."
      },
      new IssueTask()
      {
        Id = 3,
        Title = "Task 3",
        Description = "Task 3 description."
      }
    };
    public const int MaxChallenge = 2;
    public const string UserPassword = "demo";
    public const string UnlockCompetitionPassword = "demo";

    public static bool ExternalServerAddress
    {
      get
      {
        return DateTime.Now >= Settings._dateLimit;
      }
    }

    public static string AppVersion
    {
      get
      {
        return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
      }
    }

    public static string GetWebServiceAddress(string webServiceName)
    {
      return string.Empty;
    }
  }
}
