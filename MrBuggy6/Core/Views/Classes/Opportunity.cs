// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.Views.Classes.Opportunity
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;

namespace MrBuggy6.Core.Views.Classes
{
  public class Opportunity
  {
    public int Id { get; set; }

    public User User { get; set; }

    public OpportunityStage Stage { get; set; } = OpportunityStage.New;

    public LostReason LostReason { get; set; }

    public DateTime ModificationDate { get; set; }

    public string Name { get; set; }

    public Decimal Cost { get; set; }

    public int Margin { get; set; }

    public int Discount { get; set; }

    public Decimal Amount
    {
      get
      {
        Decimal num = this.Cost + this.Cost * (Decimal) this.Margin / new Decimal(100);
        if (this.Discount > 0)
          num -= num * (Decimal) this.Discount / new Decimal(100);
        return num;
      }
    }

    public Client Client { get; set; }

    public DateTime DeliveryStartDate { get; set; }

    public DateTime DeliveryCloseDate { get; set; }

    public string Description { get; set; }

    public int Probability { get; set; }
  }
}
