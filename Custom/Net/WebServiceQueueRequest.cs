// Decompiled with JetBrains decompiler
// Type: Custom.Net.WebServiceQueueRequest
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;
using System.Collections.Specialized;

namespace Custom.Net
{
  public class WebServiceQueueRequest
  {
    public Uri Url { get; private set; }

    public NameValueCollection Parameters { get; set; }

    public object Tag { get; set; }

    public WebServiceQueueRequest(string url)
    {
      this.Url = new Uri(url);
      this.Parameters = new NameValueCollection();
    }
  }
}
