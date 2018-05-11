// Decompiled with JetBrains decompiler
// Type: Custom.Net.WebServiceRequestCompletedArgs
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;
using System.Net;

namespace Custom.Net
{
  public class WebServiceRequestCompletedArgs
  {
    public Exception Error { get; private set; }

    public bool Cancelled { get; private set; }

    public string Result { get; private set; }

    public WebServiceRequestCompletedArgs(UploadValuesCompletedEventArgs args)
    {
      this.Error = args.Error;
      this.Cancelled = args.Cancelled;
      if (this.Error == null && !this.Cancelled)
        this.Result = WebService.GetStringFormResponseData(args.Result);
      else
        this.Result = string.Empty;
    }
  }
}
