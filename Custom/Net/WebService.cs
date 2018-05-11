// Decompiled with JetBrains decompiler
// Type: Custom.Net.WebService
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;
using System.Text;

namespace Custom.Net
{
  public static class WebService
  {
    public static string GetStringFormResponseData(byte[] data)
    {
      if (data.Length < 3 || data[0] != (byte) 239 || (data[1] != (byte) 187 || data[2] != (byte) 191))
        return Encoding.UTF8.GetString(data);
      byte[] bytes = new byte[data.Length - 3];
      Array.Copy((Array) data, 3, (Array) bytes, 0, bytes.Length);
      return Encoding.UTF8.GetString(bytes);
    }

    public static string CreateCredentialsString(string userName, string password)
    {
      return "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(userName + ":" + password));
    }
  }
}
