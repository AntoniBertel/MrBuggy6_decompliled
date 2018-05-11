// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.User
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace MrBuggy6.Core
{
  public class User
  {
    private static Dictionary<string, bool> Users = new Dictionary<string, bool>()
    {
      {
        "demo_admin",
        true
      },
      {
        "demo_member1",
        false
      },
      {
        "demo_member2",
        false
      }
    };

    public uint Id { get; set; }

    public string Identifier { get; private set; }

    public string MAC { get; private set; }

    public bool Logged { get; set; }

    public string FullName { get; set; }

    public string PhoneNumber { get; set; }

    public bool EvaluationAccepted { get; set; }

    public bool IsAdmin { get; private set; }

    public User(string identifier)
    {
      this.Id = 0U;
      this.Identifier = identifier;
      this.FullName = string.Empty;
      this.MAC = string.Empty;
      this.EvaluationAccepted = false;
      this.IsAdmin = User.IsCorrectIdentifier(identifier) && User.Users[identifier];
      foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
      {
        if (!networkInterface.Description.Contains("Virtual") && !networkInterface.Description.Contains("Pseudo") && networkInterface.GetPhysicalAddress().ToString() != string.Empty)
        {
          if (networkInterface.OperationalStatus == OperationalStatus.Up)
          {
            this.MAC = networkInterface.GetPhysicalAddress().ToString();
            break;
          }
          if (this.MAC.Length == 0)
            this.MAC = networkInterface.GetPhysicalAddress().ToString();
        }
      }
    }

    public static bool IsCorrectIdentifier(string identifier)
    {
      return User.Users.ContainsKey(identifier);
    }

    public override string ToString()
    {
      if (!(this.FullName != string.Empty))
        return this.Identifier;
      return this.FullName;
    }
  }
}
