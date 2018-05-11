// Decompiled with JetBrains decompiler
// Type: MrBuggy6.EndForm
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
  public class EndForm : Form
  {
    private bool windowBarMove;
    private Point windowBarFirstPoint;
    private IContainer components;
    private Panel window;
    private Label bDefects;
    private Panel pNavigation;
    private Label label7;
    private PictureBox pbLogo;
    private Panel windowBar;
    private PictureBox bClose;
    private Label windowTitle;
    private Label bUnlock;

    public bool BackToCompetition { get; private set; }

    public EndForm()
    {
      this.InitializeComponent();
      this.windowTitle.Text = Resources.windowTitle;
    }

    private void blueButton_MouseEnter(object sender, EventArgs e)
    {
      (sender as Label).BackColor = Color.FromArgb(56, 83, 147);
    }

    private void blueButton_MouseLeave(object sender, EventArgs e)
    {
      (sender as Label).BackColor = Color.FromArgb(36, 53, 93);
    }

    private void redButton_MouseEnter(object sender, EventArgs e)
    {
      (sender as Label).BackColor = Color.FromArgb(231, 52, 52);
    }

    private void redButton_MouseLeave(object sender, EventArgs e)
    {
      (sender as Label).BackColor = Color.FromArgb(247, 84, 85);
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

    private void bUnlock_Click(object sender, EventArgs e)
    {
      using (UnlockCompetitionForm unlockCompetitionForm = new UnlockCompetitionForm())
      {
        if (unlockCompetitionForm.ShowDialog() != DialogResult.OK)
          return;
        this.BackToCompetition = true;
        this.Close();
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EndForm));
      this.window = new Panel();
      this.bUnlock = new Label();
      this.bDefects = new Label();
      this.pNavigation = new Panel();
      this.label7 = new Label();
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
      this.window.BackColor = Color.FromArgb(35, 39, 48);
      this.window.BorderStyle = BorderStyle.FixedSingle;
      this.window.Controls.Add((Control) this.bUnlock);
      this.window.Controls.Add((Control) this.bDefects);
      this.window.Controls.Add((Control) this.pNavigation);
      this.window.Controls.Add((Control) this.windowBar);
      this.window.Dock = DockStyle.Fill;
      this.window.Location = new Point(0, 0);
      this.window.Name = "window";
      this.window.Size = new Size(400, 246);
      this.window.TabIndex = 0;
      this.bUnlock.Anchor = AnchorStyles.Bottom;
      this.bUnlock.BackColor = Color.FromArgb(36, 53, 93);
      this.bUnlock.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bUnlock.ForeColor = Color.FromArgb(193, 184, 201);
      this.bUnlock.Location = new Point(84, 129);
      this.bUnlock.Name = "bUnlock";
      this.bUnlock.Size = new Size(220, 29);
      this.bUnlock.TabIndex = 35;
      this.bUnlock.Text = "Return to the competition";
      this.bUnlock.TextAlign = ContentAlignment.MiddleCenter;
      this.bUnlock.Click += new EventHandler(this.bUnlock_Click);
      this.bUnlock.MouseEnter += new EventHandler(this.blueButton_MouseEnter);
      this.bUnlock.MouseLeave += new EventHandler(this.blueButton_MouseLeave);
      this.bDefects.Anchor = AnchorStyles.Bottom;
      this.bDefects.BackColor = Color.FromArgb(247, 84, 85);
      this.bDefects.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bDefects.ForeColor = Color.White;
      this.bDefects.Location = new Point(124, 183);
      this.bDefects.Name = "bDefects";
      this.bDefects.Size = new Size(150, 40);
      this.bDefects.TabIndex = 34;
      this.bDefects.Text = "Close";
      this.bDefects.TextAlign = ContentAlignment.MiddleCenter;
      this.bDefects.Click += new EventHandler(this.bClose_Click);
      this.bDefects.MouseEnter += new EventHandler(this.redButton_MouseEnter);
      this.bDefects.MouseLeave += new EventHandler(this.redButton_MouseLeave);
      this.pNavigation.BackColor = Color.FromArgb(23, 25, 31);
      this.pNavigation.Controls.Add((Control) this.label7);
      this.pNavigation.Controls.Add((Control) this.pbLogo);
      this.pNavigation.Dock = DockStyle.Top;
      this.pNavigation.Location = new Point(0, 32);
      this.pNavigation.Name = "pNavigation";
      this.pNavigation.Size = new Size(398, 76);
      this.pNavigation.TabIndex = 9;
      this.label7.AutoSize = true;
      this.label7.Font = new Font("Arial", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.label7.ForeColor = Color.FromArgb(193, 184, 201);
      this.label7.Location = new Point(98, 26);
      this.label7.Name = "label7";
      this.label7.Size = new Size(164, 22);
      this.label7.TabIndex = 1;
      this.label7.Text = "End Competition";
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
      this.windowBar.Size = new Size(398, 32);
      this.windowBar.TabIndex = 8;
      this.windowBar.MouseDown += new MouseEventHandler(this.windowBar_MouseDown);
      this.windowBar.MouseMove += new MouseEventHandler(this.windowBar_MouseMove);
      this.windowBar.MouseUp += new MouseEventHandler(this.windowBar_MouseUp);
      this.bClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.bClose.Image = (Image) Resources.close;
      this.bClose.Location = new Point(371, 5);
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
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(35, 39, 48);
      this.ClientSize = new Size(400, 246);
      this.Controls.Add((Control) this.window);
      this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (EndForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "MrBuggy 6 - End Competition";
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
