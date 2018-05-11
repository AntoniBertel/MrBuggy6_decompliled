// Decompiled with JetBrains decompiler
// Type: Custom.Net.WebServiceQueueRequestCompletedArgs
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;
using System.Net;

namespace Custom.Net
{
  public class WebServiceQueueRequestCompletedArgs
  {
    public Exception Error { get; private set; }

    public bool Cancelled { get; private set; }

    public string Result { get; private set; }

    public bool DequeueRequest { get; set; }

    public WebServiceQueueRequest Request { get; private set; }

    public WebServiceQueueRequestCompletedArgs(UploadValuesCompletedEventArgs args, WebServiceQueueRequest request)
    {
      this.Error = args.Error;
      this.Cancelled = args.Cancelled;
      this.Request = request;
      this.DequeueRequest = true;
      if (this.Error == null && !this.Cancelled)
        this.Result = WebService.GetStringFormResponseData(args.Result);
      else
        this.Result = string.Empty;
    }
  }
}
