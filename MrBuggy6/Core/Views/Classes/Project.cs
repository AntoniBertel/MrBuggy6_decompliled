// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.Views.Classes.Project
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System.Collections.Generic;

namespace MrBuggy6.Core.Views.Classes
{
  public class Project
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Employee> Employees { get; private set; } = new List<Employee>();

    public Client Client { get; set; }

    public Service Service { get; set; }
  }
}
