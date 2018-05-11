// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.Views.Page1View
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

namespace MrBuggy6.Core.Views
{
  public class Page1View : View
  {
    public Page1View()
    {
      this.Id = 1U;
      this.Name = "page1";
    }

    public override string GetContent()
    {
      return View.ResourceManager.GetString(this.Name);
    }

    public override void Init()
    {
      base.Init();
      View.Document.GetElementById("logo").SetAttribute("src", this.LoadImage("logo_400"));
    }
  }
}
