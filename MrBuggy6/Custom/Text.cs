// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Custom.Text
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;
using System.Security.Cryptography;
using System.Text;

namespace MrBuggy6.Custom
{
  public static class Text
  {
    public static string GetMd5Hash(string input)
    {
      StringBuilder stringBuilder = new StringBuilder();
      using (MD5 md5 = MD5.Create())
      {
        foreach (byte num in md5.ComputeHash(Encoding.UTF8.GetBytes(input)))
          stringBuilder.Append(num.ToString("x2"));
      }
      return stringBuilder.ToString();
    }

    public static bool VerifyMd5Hash(string input, string hash)
    {
      StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
      using (MD5.Create())
      {
        string md5Hash = MrBuggy6.Custom.Text.GetMd5Hash(input);
        return ordinalIgnoreCase.Compare(md5Hash, hash) == 0;
      }
    }
  }
}
