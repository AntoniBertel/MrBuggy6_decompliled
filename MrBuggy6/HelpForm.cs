// Decompiled with JetBrains decompiler
// Type: MrBuggy6.HelpForm
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using MrBuggy6.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrBuggy6
{
  public class HelpForm : Form
  {
    private bool windowBarMove;
    private Point windowBarFirstPoint;
    private IContainer components;
    private Panel window;
    private Panel pNavigation;
    private Label lUserName;
    private PictureBox pbLogo;
    private Panel windowBar;
    private PictureBox bClose;
    private Label windowTitle;

    public HelpForm()
    {
      this.InitializeComponent();
      this.windowTitle.Text = Resources.windowTitle;
    }

    private void bClose_MouseEnter(object sender, EventArgs e)
    {
      (sender as PictureBox).Image = (Image) Resources.close_hover;
    }

    private void bClose_MouseLeave(object sender, EventArgs e)
    {
      (sender as PictureBox).Image = (Image) Resources.close;
    }

    private void windowBar_MouseDown(object sender, MouseEventArgs e)
    {
      this.windowBarFirstPoint = e.Location;
      this.windowBarMove = true;
    }

    private void windowBar_MouseMove(object sender, MouseEventArgs e)
    {
      if (!this.windowBarMove)
        return;
      this.Location = new Point(this.Location.X - (this.windowBarFirstPoint.X - e.Location.X), this.Location.Y - (this.windowBarFirstPoint.Y - e.Location.Y));
    }

    private void windowBar_MouseUp(object sender, MouseEventArgs e)
    {
      this.windowBarMove = false;
    }

    private void bClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (HelpForm));
      this.window = new Panel();
      this.pNavigation = new Panel();
      this.lUserName = new Label();
      this.pbLogo = new PictureBox();
      this.windowBar = new Panel();
      this.bClose = new PictureBox();
      this.windowTitle = new Label();
      this.window.SuspendLayout();
      this.pNavigation.SuspendLayout();
      ((ISupportInitialize) this.pbLogo).BeginInit();
      this.windowBar.SuspendLayout();
      ((ISupportInitialize) this.bClose).BeginInit();
      this.SuspendLayout();
      this.window.BorderStyle = BorderStyle.FixedSingle;
      this.window.Controls.Add((Control) this.pNavigation);
      this.window.Controls.Add((Control) this.windowBar);
      this.window.Dock = DockStyle.Fill;
      this.window.Location = new Point(0, 0);
      this.window.Name = "window";
      this.window.Size = new Size(529, 424);
      this.window.TabIndex = 0;
      this.pNavigation.BackColor = Color.FromArgb(23, 25, 31);
      this.pNavigation.Controls.Add((Control) this.lUserName);
      this.pNavigation.Controls.Add((Control) this.pbLogo);
      this.pNavigation.Dock = DockStyle.Top;
      this.pNavigation.Location = new Point(0, 32);
      this.pNavigation.Name = "pNavigation";
      this.pNavigation.Size = new Size(527, 76);
      this.pNavigation.TabIndex = 8;
      this.lUserName.AutoSize = true;
      this.lUserName.Font = new Font("Arial", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.lUserName.ForeColor = Color.FromArgb(193, 184, 201);
      this.lUserName.Location = new Point(98, 28);
      this.lUserName.Name = "lUserName";
      this.lUserName.Size = new Size(51, 22);
      this.lUserName.TabIndex = 1;
      this.lUserName.Text = "Help";
      this.pbLogo.Image = (Image) Resources.logo;
      this.pbLogo.Location = new Point(6, 6);
      this.pbLogo.Name = "pbLogo";
      this.pbLogo.Size = new Size(64, 64);
      this.pbLogo.SizeMode = PictureBoxSizeMode.AutoSize;
      this.pbLogo.TabIndex = 0;
      this.pbLogo.TabStop = false;
      this.windowBar.BackColor = Color.FromArgb(35, 39, 48);
      this.windowBar.Controls.Add((Control) this.bClose);
      this.windowBar.Controls.Add((Control) this.windowTitle);
      this.windowBar.Dock = DockStyle.Top;
      this.windowBar.ForeColor = Color.White;
      this.windowBar.Location = new Point(0, 0);
      this.windowBar.Name = "windowBar";
      this.windowBar.Size = new Size(527, 32);
      this.windowBar.TabIndex = 7;
      this.windowBar.MouseDown += new MouseEventHandler(this.windowBar_MouseDown);
      this.windowBar.MouseMove += new MouseEventHandler(this.windowBar_MouseMove);
      this.windowBar.MouseUp += new MouseEventHandler(this.windowBar_MouseUp);
      this.bClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.bClose.Image = (Image) Resources.close;
      this.bClose.Location = new Point(499, 5);
      this.bClose.Name = "bClose";
      this.bClose.Size = new Size(21, 21);
      this.bClose.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bClose.TabIndex = 2;
      this.bClose.TabStop = false;
      this.bClose.Click += new EventHandler(this.bClose_Click);
      this.bClose.MouseEnter += new EventHandler(this.bClose_MouseEnter);
      this.bClose.MouseLeave += new EventHandler(this.bClose_MouseLeave);
      this.windowTitle.AutoSize = true;
      this.windowTitle.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.windowTitle.ForeColor = Color.FromArgb(147, 147, 147);
      this.windowTitle.Location = new Point(4, 7);
      this.windowTitle.Name = "windowTitle";
      this.windowTitle.Size = new Size(96, 19);
      this.windowTitle.TabIndex = 0;
      this.windowTitle.Text = "Mr Buggy 6";
      this.windowTitle.MouseDown += new MouseEventHandler(this.windowBar_MouseDown);
      this.windowTitle.MouseMove += new MouseEventHandler(this.windowBar_MouseMove);
      this.windowTitle.MouseUp += new MouseEventHandler(this.windowBar_MouseUp);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(35, 39, 48);
      this.ClientSize = new Size(529, 424);
      this.Controls.Add((Control) this.window);
      this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Margin = new Padding(4);
      this.Name = nameof (HelpForm);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "MrBuggy 6 - Help";
      this.window.ResumeLayout(false);
      this.pNavigation.ResumeLayout(false);
      this.pNavigation.PerformLayout();
      ((ISupportInitialize) this.pbLogo).EndInit();
      this.windowBar.ResumeLayout(false);
      this.windowBar.PerformLayout();
      ((ISupportInitialize) this.bClose).EndInit();
      this.ResumeLayout(false);
    }
  }
}
