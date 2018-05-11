// Decompiled with JetBrains decompiler
// Type: Custom.Net.WebServiceRequest
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Custom.Net
{
  public class WebServiceRequest
  {
    public WebClient _webClient = new WebClient();
    private Stopwatch _intervalCouter = new Stopwatch();
    private Stopwatch _timeoutCouter = new Stopwatch();
    public const int DefaultConnectionTimeout = 10;
    private SynchronizationContext _synchronizationContext;
    public bool _isBusy;
    private bool _cancellationPending;
    private bool _sendImmediately;

    public Uri Url { get; set; }

    public NameValueCollection Parameters { get; set; }

    public int ConnectionTimeout { get; set; }

    public int RepeatsInterval { get; set; }

    public bool RepeatMode { get; set; }

    public bool IsBusy
    {
      get
      {
        if (!this._isBusy)
          return this._webClient.IsBusy;
        return true;
      }
    }

    public bool Skip { get; set; }

    public event WebServiceRequestBeforeSendDelegate BeforeSend;

    public event WebServiceRequestCompletedDelegate Completed;

    public WebServiceRequest(string url)
    {
      this.Init(url, 30, false);
      this.Skip = false;
    }

    public WebServiceRequest(string url, int repeatsInterval)
    {
      this.Init(url, repeatsInterval, true);
      this.Skip = false;
    }

    public void SetCredentials(string userName, string password)
    {
      this._webClient.Headers[HttpRequestHeader.Authorization] = WebService.CreateCredentialsString(userName, password);
    }

    public void ClearCredentials()
    {
      this._webClient.Headers.Clear();
    }

    public WebServiceQueueRequest CreateQueueRequest()
    {
      return this.CreateQueueRequest((object) null);
    }

    public WebServiceQueueRequest CreateQueueRequest(object tag)
    {
      WebServiceQueueRequest serviceQueueRequest = new WebServiceQueueRequest(this.Url.ToString());
      serviceQueueRequest.Tag = tag;
      foreach (string allKey in this.Parameters.AllKeys)
        serviceQueueRequest.Parameters.Add(allKey, this.Parameters[allKey]);
      return serviceQueueRequest;
    }

    public void Start()
    {
      if (this._isBusy && this.RepeatMode && !this._webClient.IsBusy)
        this._sendImmediately = true;
      this.Stop();
      this._synchronizationContext = SynchronizationContext.Current;
      this.Send();
      this.StartProcess();
    }

    public void Stop()
    {
      this._timeoutCouter.Stop();
      this._intervalCouter.Stop();
      if (this._webClient.IsBusy)
        this._webClient.CancelAsync();
      if (!this._isBusy)
        return;
      this._cancellationPending = true;
    }

    private void Init(string url, int repeatsInterval, bool repeatMode)
    {
      this.Url = new Uri(url);
      this.Parameters = new NameValueCollection();
      this.ConnectionTimeout = 10;
      this.RepeatsInterval = repeatsInterval;
      this.RepeatMode = repeatMode;
      this._webClient.UploadValuesCompleted += new UploadValuesCompletedEventHandler(this.WebClient_Completed);
    }

    private void Send()
    {
      if (!this.Skip)
      {
        this.OnBeforeSend();
        this._webClient.UploadValuesAsync(this.Url, "POST", this.Parameters);
        this._timeoutCouter.Restart();
      }
      else
      {
        if (!this.RepeatMode)
          return;
        this._intervalCouter.Restart();
      }
    }

    private async void StartProcess()
    {
      this._isBusy = true;
      this._cancellationPending = false;
      await this.Process();
      this._isBusy = false;
    }

    private async Task Process()
    {
      await Task.Run((Action) (() =>
      {
        while (!this._cancellationPending)
        {
          Thread.Sleep(100);
          if (this._timeoutCouter.IsRunning)
          {
            if (!this._webClient.IsBusy)
              this._timeoutCouter.Stop();
            else if (this._timeoutCouter.Elapsed.TotalSeconds >= (double) this.ConnectionTimeout)
            {
              this._timeoutCouter.Stop();
              this._webClient.CancelAsync();
            }
          }
          if (!this._webClient.IsBusy)
          {
            if (!this.RepeatMode)
              this._cancellationPending = true;
            else if (this._sendImmediately || this._intervalCouter.IsRunning && this._intervalCouter.Elapsed.TotalSeconds >= (double) this.RepeatsInterval)
            {
              this._sendImmediately = false;
              this._intervalCouter.Stop();
              this.Send();
            }
          }
        }
      }));
    }

    private void WebClient_Completed(object sender, UploadValuesCompletedEventArgs e)
    {
      this.OnCompleted(new WebServiceRequestCompletedArgs(e));
      if (!this.RepeatMode)
        return;
      this._intervalCouter.Restart();
    }

    private void OnBeforeSend()
    {
      // ISSUE: reference to a compiler-generated field
      if (this.BeforeSend == null)
        return;
      if (this._synchronizationContext == SynchronizationContext.Current)
      {
        // ISSUE: reference to a compiler-generated field
        this.BeforeSend((object) this);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        this._synchronizationContext.Post((SendOrPostCallback) (p => this.BeforeSend((object) this)), (object) null);
      }
    }

    private void OnCompleted(WebServiceRequestCompletedArgs e)
    {
      // ISSUE: reference to a compiler-generated field
      if (this.Completed == null)
        return;
      if (this._synchronizationContext == SynchronizationContext.Current)
      {
        // ISSUE: reference to a compiler-generated field
        this.Completed((object) this, e);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        this._synchronizationContext.Post((SendOrPostCallback) (p => this.Completed((object) this, e)), (object) null);
      }
    }
  }
}
