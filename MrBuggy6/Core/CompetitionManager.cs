// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.CompetitionManager
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using MrBuggy6.Core.Views;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace MrBuggy6.Core
{
  public class CompetitionManager
  {
    private List<View> Views = new List<View>();
    public bool CanSend = true;
    private const uint FirstViewId = 1;
    private WebBrowser WebBrowser;
    private View CurrentView;
    private SQLiteConnection Connection;
    private string AppPath;
    private int ConnectionCount;

    public event ManagerConnectionChangeStatusEvent ConnectionChangeStatus;

    public event ManagerAddIssueToFileCompletedEvent AddIssueToFileCompleted;

    public event ManagerSaveIssueToFileCompletedEvent SaveIssueToFileCompleted;

    public event ManagerDeleteIssueFromFileCompletedEvent DeleteIssueFromFileCompleted;

    public event ManagerSaveReportToFileCompletedEvent SaveReportToFileCompleted;

    public event ManagerEndCompetitionEvent EndCompetition;

    public ConnetcionStatus LastConnectionStatus { get; private set; }

    public CompetitionManager(WebBrowser webBrowser, ResourceManager resourceManager, string appPath)
    {
      this.AppPath = appPath;
      this.Connection = new SQLiteConnection("Data Source=" + appPath + "mrbuggy6.sqlite;Version=3;");
      this.CurrentView = this.GetViewById(1U);
      View.Init((IActionsContainer) Settings.ReportFile, resourceManager, this.Connection, appPath + "temp\\");
      this.WebBrowser = webBrowser;
      this.WebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.DocumentCompleted);
      this.WebBrowser.Navigate("about:blank");
      this.WebBrowser.Document.Write(string.Empty);
      this.WebBrowser.DocumentText = this.CurrentView.GetContent();
      View.Document = webBrowser.Document;
      this.LastConnectionStatus = ConnetcionStatus.Offilne;
    }

    public void InitWebServices()
    {
    }

    public void ResetReportFile()
    {
      Settings.ReportFile.Issues.Clear();
    }

    public void StartWebServices()
    {
    }

    public void StopWebServices()
    {
    }

    public void UpdateWebServiceAddresses()
    {
    }

    public void SaveActions()
    {
      Settings.ReportFile.SaveToFile();
    }

    public void AddIssue(Issue issue, object tag)
    {
      bool success = Settings.ReportFile.AddIssue(issue);
      // ISSUE: reference to a compiler-generated field
      this.AddIssueToFileCompleted((object) this, success, tag);
      if (!success)
        return;
      this.SaveIssueOnServer(issue, tag);
    }

    public void SaveIssue(Issue issue, object tag)
    {
      bool success = Settings.ReportFile.SaveIssue(issue);
      // ISSUE: reference to a compiler-generated field
      this.SaveIssueToFileCompleted((object) this, success, issue, tag);
      if (!success)
        return;
      this.SaveIssueOnServer(issue, tag);
    }

    public void DeleteIssue(Issue issue, object tag)
    {
      bool success = Settings.ReportFile.DeleteIssue(issue);
      // ISSUE: reference to a compiler-generated field
      this.DeleteIssueFromFileCompleted((object) this, success, tag);
      int num = success ? 1 : 0;
    }

    public void SaveReport(string content)
    {
      bool file = Settings.ReportFile.SaveToFile(content);
      // ISSUE: reference to a compiler-generated field
      this.SaveReportToFileCompleted((object) this, file);
      int num = file ? 1 : 0;
    }

    public void End()
    {
      Settings.ReportFile.CompetitionInProgress = false;
      Settings.ReportFile.SaveToFile();
      this.StopWebServices();
      // ISSUE: reference to a compiler-generated field
      this.EndCompetition((object) this);
    }

    private void OnChangeConnectionStatus(ConnetcionStatus connectionStatus)
    {
      this.LastConnectionStatus = connectionStatus;
      // ISSUE: reference to a compiler-generated field
      this.ConnectionChangeStatus((object) this, connectionStatus);
    }

    private void ShowConnectionStatus(bool successful)
    {
      if (--this.ConnectionCount != 0)
        return;
      if (successful)
        this.OnChangeConnectionStatus(ConnetcionStatus.Online);
      else
        this.OnChangeConnectionStatus(ConnetcionStatus.Offilne);
    }

    private void Request_BeforeSend(object sender)
    {
      if (this.ConnectionCount == 0)
        this.OnChangeConnectionStatus(ConnetcionStatus.Busy);
      ++this.ConnectionCount;
    }

    private void SaveIssueOnServer(Issue issue, object tag)
    {
    }

    private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      this.CurrentView.Init();
    }

    private View GetViewById(uint id)
    {
      View view = this.Views.FirstOrDefault<View>((Func<View, bool>) (x => (int) x.Id == (int) id));
      if (view == null)
      {
        if (id == 1U)
          view = (View) new Page1View();
        if (view != null)
          view.ChangeView += new ViewEvent(this.ChangeView);
      }
      return view;
    }

    private View GetViewByName(string name)
    {
      View view = this.Views.FirstOrDefault<View>((Func<View, bool>) (x => x.Name == name));
      if (view == null)
      {
        if (name == "page1")
          view = (View) new Page1View();
        if (view != null)
          view.ChangeView += new ViewEvent(this.ChangeView);
      }
      return view;
    }

    private void ChangeView(object sender, string name)
    {
      this.CurrentView.Release();
      this.CurrentView = this.GetViewByName(name);
      this.WebBrowser.Navigate("about:blank");
      this.WebBrowser.Document.Write(string.Empty);
      this.WebBrowser.DocumentText = this.CurrentView.GetContent();
      this.SaveActions();
    }
  }
}
