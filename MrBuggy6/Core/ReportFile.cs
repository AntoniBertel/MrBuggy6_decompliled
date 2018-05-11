// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.ReportFile
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MrBuggy6.Core
{
  public class ReportFile : IActionsContainer
  {
    private byte[] salt = new byte[13]
    {
      (byte) 43,
      (byte) 138,
      (byte) 100,
      (byte) 17,
      (byte) 154,
      (byte) 124,
      (byte) 222,
      (byte) 163,
      (byte) 18,
      (byte) 113,
      (byte) 241,
      (byte) 88,
      (byte) 175
    };

    public string Path { get; private set; }

    public User User { get; private set; }

    public List<Issue> Issues { get; private set; }

    public Report Report { get; set; }

    public List<Action> Actions { get; set; }

    public bool ActionsSent { get; set; }

    public bool CompetitionInProgress { get; set; }

    public ReportFileStatus Status { get; private set; }

    public ReportFile(string path, User user)
    {
      this.Path = path;
      this.User = user;
      this.Issues = new List<Issue>();
      this.Report = new Report();
      this.Actions = new List<Action>();
      this.ActionsSent = true;
      this.Status = ReportFileStatus.OK;
      this.CompetitionInProgress = true;
    }

    public string GetActionsAsString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Action action in this.Actions)
        stringBuilder.Append(action.ToString() + ",");
      if (stringBuilder.Length > 0)
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
      return stringBuilder.ToString();
    }

    public bool AddIssue(Issue issue)
    {
      this.Issues.Add(issue);
      if (this.SaveToFile())
        return true;
      this.Issues.Remove(issue);
      return false;
    }

    public bool SaveIssue(Issue issue)
    {
      int index = this.Issues.FindIndex((Predicate<Issue>) (x => (int) x.InternalId == (int) issue.InternalId));
      Issue issue1 = this.Issues[index];
      this.Issues[index] = issue;
      if (this.SaveToFile())
        return true;
      this.Issues[index] = issue1;
      return false;
    }

    public bool DeleteIssue(Issue issue)
    {
      int index = this.Issues.FindIndex((Predicate<Issue>) (x => (int) x.InternalId == (int) issue.InternalId));
      this.Issues.Remove(issue);
      if (this.SaveToFile())
        return true;
      this.Issues.Insert(index, issue);
      return false;
    }

    public bool SaveToFile(string reportContent)
    {
      string content = this.Report.Content;
      bool sent = this.Report.Sent;
      this.Report.Content = reportContent;
      this.Report.Sent = false;
      if (this.SaveToFile())
        return true;
      this.Report.Content = content;
      this.Report.Sent = sent;
      return false;
    }

    public bool SaveToFile()
    {
      if (!this.User.IsAdmin)
        return true;
      bool flag = true;
      MrBuggy6.Core.Cryptography cryptography = new MrBuggy6.Core.Cryptography(Settings.User.Identifier, this.salt);
      FileStream fileStream = (FileStream) null;
      BinaryWriter binaryWriter = (BinaryWriter) null;
      try
      {
        fileStream = new FileStream(this.Path, FileMode.OpenOrCreate, FileAccess.Write);
        binaryWriter = new BinaryWriter((Stream) fileStream);
        binaryWriter.Write(cryptography.Encrypt(this.User.Identifier));
        binaryWriter.Write(cryptography.Encrypt(this.User.MAC));
        binaryWriter.Write(cryptography.Encrypt(this.User.PhoneNumber));
        binaryWriter.Write(cryptography.Encrypt(this.CompetitionInProgress));
        binaryWriter.Write(cryptography.Encrypt(Issue.NextId));
        binaryWriter.Write(cryptography.Encrypt((uint) this.Issues.Count));
        foreach (Issue issue in this.Issues)
        {
          binaryWriter.Write(cryptography.Encrypt(issue.InternalId));
          binaryWriter.Write(cryptography.Encrypt(issue.Title));
          binaryWriter.Write(cryptography.Encrypt(issue.Description));
          binaryWriter.Write(cryptography.Encrypt(issue.Attr1.Id));
          binaryWriter.Write(cryptography.Encrypt(issue.Attr2.Id));
          binaryWriter.Write(cryptography.Encrypt(issue.Attr3.Id));
          binaryWriter.Write(cryptography.Encrypt(issue.Task.Id));
          binaryWriter.Write(cryptography.Encrypt(issue.Sent));
        }
        binaryWriter.Write(cryptography.Encrypt(this.ActionsSent));
        binaryWriter.Write(cryptography.Encrypt((uint) this.Actions.Count));
        foreach (Action action in this.Actions)
        {
          binaryWriter.Write(cryptography.Encrypt(action.ViewId));
          binaryWriter.Write(cryptography.Encrypt(action.Id));
        }
        binaryWriter.Write(cryptography.Encrypt(this.Report.Content));
        binaryWriter.Write(cryptography.Encrypt(this.Report.Sent));
        binaryWriter.Write(cryptography.Encrypt(this.Report.SynchronizationDate.ToBinary()));
      }
      catch (Exception ex)
      {
        flag = false;
      }
      finally
      {
        if (fileStream != null)
        {
          binaryWriter.Close();
          fileStream.Close();
        }
      }
      return flag;
    }

    public bool LoadFromFile()
    {
      if (!this.User.IsAdmin)
        return true;
      this.Report.Content = string.Empty;
      if (File.Exists(this.Path))
      {
        FileStream fileStream = new FileStream(this.Path, FileMode.Open, FileAccess.Read);
        BinaryReader binaryReader = new BinaryReader((Stream) fileStream);
        MrBuggy6.Core.Cryptography cryptography = new MrBuggy6.Core.Cryptography(this.User.Identifier, this.salt);
        try
        {
          if (Settings.User.Identifier != cryptography.DecryptToString(binaryReader.ReadString()))
            throw new CryptographicException();
          binaryReader.ReadString();
          binaryReader.ReadString();
          this.CompetitionInProgress = cryptography.DecryptToBoolean(binaryReader.ReadString());
          Issue.NextId = cryptography.DecryptToUInt32(binaryReader.ReadString());
          uint uint32_1 = cryptography.DecryptToUInt32(binaryReader.ReadString());
          for (int index = 0; (long) index < (long) uint32_1; ++index)
          {
            Issue issue = new Issue();
            issue.InternalId = cryptography.DecryptToUInt32(binaryReader.ReadString());
            issue.Title = cryptography.DecryptToString(binaryReader.ReadString());
            issue.Description = cryptography.DecryptToString(binaryReader.ReadString());
            uint id = cryptography.DecryptToUInt32(binaryReader.ReadString());
            issue.Attr1 = Settings.Attr1List.Find((Predicate<IssueAttr1>) (x => (int) x.Id == (int) id));
            if (issue.Attr1 == null)
              throw new Exception();
            id = cryptography.DecryptToUInt32(binaryReader.ReadString());
            issue.Attr2 = Settings.Attr2List.Find((Predicate<IssueAttr2>) (x => (int) x.Id == (int) id));
            if (issue.Attr2 == null)
              throw new Exception();
            id = cryptography.DecryptToUInt32(binaryReader.ReadString());
            issue.Attr3 = Settings.Attr3List.Find((Predicate<IssueAttr3>) (x => (int) x.Id == (int) id));
            if (issue.Attr3 == null)
              throw new Exception();
            id = cryptography.DecryptToUInt32(binaryReader.ReadString());
            issue.Task = Settings.Tasks.Find((Predicate<IssueTask>) (x => (long) x.Id == (long) id));
            if (issue.Task == null)
              throw new Exception();
            issue.Sent = cryptography.DecryptToBoolean(binaryReader.ReadString());
            this.Issues.Add(issue);
          }
          this.ActionsSent = cryptography.DecryptToBoolean(binaryReader.ReadString());
          uint uint32_2 = cryptography.DecryptToUInt32(binaryReader.ReadString());
          for (int index = 0; (long) index < (long) uint32_2; ++index)
            this.Actions.Add(new Action()
            {
              ViewId = cryptography.DecryptToUInt32(binaryReader.ReadString()),
              Id = cryptography.DecryptToUInt32(binaryReader.ReadString())
            });
          this.Report.Content = cryptography.DecryptToString(binaryReader.ReadString());
          this.Report.Sent = cryptography.DecryptToBoolean(binaryReader.ReadString());
          this.Report.SynchronizationDate = DateTime.FromBinary(cryptography.DecryptToInt64(binaryReader.ReadString()));
        }
        catch (CryptographicException ex)
        {
          this.Status = ReportFileStatus.Corrupted;
          return false;
        }
        catch (Exception ex)
        {
          this.Status = ReportFileStatus.Fragmentary;
          return false;
        }
        finally
        {
          binaryReader.Close();
          fileStream.Close();
        }
      }
      this.Status = ReportFileStatus.OK;
      return true;
    }
  }
}
