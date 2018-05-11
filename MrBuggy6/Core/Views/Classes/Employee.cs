// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.Views.Classes.Employee
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System.Collections.Generic;

namespace MrBuggy6.Core.Views.Classes
{
  public class Employee
  {
    public int Id { get; set; }

    public bool Active { get; set; }

    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string YearOfExperience { get; set; }

    public Dictionary<int, string> Skills { get; private set; } = new Dictionary<int, string>();
  }
}
