// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.View
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace MrBuggy6.Core
{
  public abstract class View
  {
    public const string Page1Name = "page1";
    private static IActionsContainer ActionContainer;
    protected static ResourceManager ResourceManager;
    protected static SQLiteConnection Connection;
    protected static string TempPath;
    public static HtmlDocument Document;

    public event ViewEvent ChangeView;

    public uint Id { get; protected set; }

    public string Name { get; protected set; }

    public virtual void Init()
    {
    }

    public virtual void Release()
    {
    }

    public static void Init(IActionsContainer actionsContainer, ResourceManager resourceManager, SQLiteConnection connection, string tempPath)
    {
      View.ActionContainer = actionsContainer;
      View.ResourceManager = resourceManager;
      View.Connection = connection;
      View.TempPath = tempPath;
      if (Directory.Exists(View.TempPath))
        return;
      Directory.CreateDirectory(View.TempPath);
    }

    protected void OnChangeView(string name)
    {
      // ISSUE: reference to a compiler-generated field
      this.ChangeView((object) this, name);
    }

    protected void AddAction(uint id)
    {
      View.ActionContainer.ActionsSent = false;
      View.ActionContainer.Actions.Add(new Action(this.Id, id));
    }

    protected void AddAction(uint id, uint viewId)
    {
      View.ActionContainer.ActionsSent = false;
      View.ActionContainer.Actions.Add(new Action(viewId, id));
    }

    public virtual string GetContent()
    {
      return "";
    }

    protected string PrepareContent(string content)
    {
      return content.Replace("__content__", View.ResourceManager.GetString(this.Name));
    }

    protected string LoadImage(string name)
    {
      string str = View.TempPath + name + ".png";
      if (!File.Exists(str))
        ((Image) View.ResourceManager.GetObject(name)).Save(str);
      return "file://" + str;
    }

    protected void AppendCell(HtmlElement tr, string text)
    {
      HtmlElement element = View.Document.CreateElement("td");
      element.InnerText = text;
      tr.AppendChild(element);
    }

    protected void AppendButton(HtmlElement parent, string text, string idAttr, string classAttr, HtmlElementEventHandler clickEvent)
    {
      HtmlElement element = View.Document.CreateElement("a");
      element.InnerText = text;
      element.SetAttribute("href", "#");
      element.SetAttribute("id", idAttr);
      element.SetAttribute("class", classAttr);
      element.Click += clickEvent;
      parent.AppendChild(element);
    }
  }
}
