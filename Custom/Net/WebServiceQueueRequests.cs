// Decompiled with JetBrains decompiler
// Type: Custom.Net.WebServiceQueueRequests
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Custom.Net
{
  public class WebServiceQueueRequests
  {
    private WebClient _webClient = new WebClient();
    private Stopwatch _intervalCouter = new Stopwatch();
    private Stopwatch _timeoutCouter = new Stopwatch();
    private SynchronizationContext _synchronizationContext;
    private bool _isBusy;
    private bool _cancellationPending;
    private WebServiceQueueRequest _currentRequest;

    public Queue<WebServiceQueueRequest> Requests { get; private set; }

    public int ConnectionTimeout { get; set; }

    public int RepeatsInterval { get; set; }

    public bool IsBusy
    {
      get
      {
        if (!this._isBusy)
          return this._webClient.IsBusy;
        return true;
      }
    }

    public event WebServiceQueueRequestBeforeSendDelegate BeforeSend;

    public event WebServiceQueueRequestCompletedDelegate Completed;

    public WebServiceQueueRequests(int repeatsInterval)
    {
      this.ConnectionTimeout = 10;
      this.RepeatsInterval = repeatsInterval;
      this._webClient.UploadValuesCompleted += new UploadValuesCompletedEventHandler(this.WebClient_Completed);
      this.Requests = new Queue<WebServiceQueueRequest>();
    }

    public void SetCredentials(string userName, string password)
    {
      this._webClient.Headers[HttpRequestHeader.Authorization] = WebService.CreateCredentialsString(userName, password);
    }

    public void ClearCredentials()
    {
      this._webClient.Headers.Clear();
    }

    public void Start()
    {
      if (this._isBusy)
        throw new Exception("You can not run a task when another task has not been completed.");
      this._synchronizationContext = SynchronizationContext.Current;
      this.Send();
      this.StartProcess();
    }

    public void Stop()
    {
      this._timeoutCouter.Stop();
      this._intervalCouter.Stop();
      if (this._isBusy)
        this._cancellationPending = true;
      if (!this._webClient.IsBusy)
        return;
      this._webClient.CancelAsync();
    }

    private void Send()
    {
      if (this.Requests.Count > 0)
      {
        this.OnBeforeSend();
        this._currentRequest = this.Requests.Peek();
        this._webClient.UploadValuesAsync(this._currentRequest.Url, "POST", this._currentRequest.Parameters);
        this._timeoutCouter.Restart();
      }
      else
      {
        this._currentRequest = (WebServiceQueueRequest) null;
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
          TimeSpan elapsed;
          if (this._timeoutCouter.IsRunning)
          {
            if (!this._webClient.IsBusy)
            {
              this._timeoutCouter.Stop();
            }
            else
            {
              elapsed = this._timeoutCouter.Elapsed;
              if (elapsed.TotalSeconds >= (double) this.ConnectionTimeout)
              {
                this._timeoutCouter.Stop();
                this._webClient.CancelAsync();
              }
            }
          }
          if (!this._webClient.IsBusy && this._intervalCouter.IsRunning)
          {
            elapsed = this._intervalCouter.Elapsed;
            if (elapsed.TotalSeconds >= (double) this.RepeatsInterval)
            {
              this._intervalCouter.Stop();
              this.Send();
            }
          }
        }
      }));
    }

    private void WebClient_Completed(object sender, UploadValuesCompletedEventArgs e)
    {
      WebServiceQueueRequestCompletedArgs e1 = new WebServiceQueueRequestCompletedArgs(e, this._currentRequest);
      e1.DequeueRequest = e.Error == null && !e.Cancelled;
      this.OnCompleted(e1);
      if (e1.DequeueRequest)
        this.Requests.Dequeue();
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

    private void OnCompleted(WebServiceQueueRequestCompletedArgs e)
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
