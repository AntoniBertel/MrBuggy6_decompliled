// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.Views.Classes.User
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

namespace MrBuggy6.Core.Views.Classes
{
  public class User
  {
    public const string MainManagerUsername = "manager2";
    public const int MainManagerId = 1;

    public int Id { get; set; }

    public UserRole Role { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }
  }
}
