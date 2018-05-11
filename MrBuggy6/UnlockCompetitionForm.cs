// Decompiled with JetBrains decompiler
// Type: MrBuggy6.UnlockCompetitionForm
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
  public class UnlockCompetitionForm : Form
  {
    private bool windowBarMove;
    private Point windowBarFirstPoint;
    private IContainer components;
    private Panel window;
    private TextBox tbPassword;
    private Label label2;
    private Label bCancel;
    private Panel pNavigation;
    private Label label7;
    private PictureBox pbLogo;
    private Label bUnlock;
    private Panel windowBar;
    private PictureBox bClose;
    private Label windowTitle;

    public UnlockCompetitionForm()
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

    private void greenButton_MouseEnter(object sender, EventArgs e)
    {
      if (!(sender as Label).Enabled)
        return;
      (sender as Label).BackColor = Color.FromArgb(19, 160, 102);
    }

    private void greenButton_MouseLeave(object sender, EventArgs e)
    {
      (sender as Label).BackColor = Color.FromArgb(36, 201, 133);
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

    private void bUnlock_Click(object sender, EventArgs e)
    {
      this.Unlock();
    }

    private void bCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void bClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void tbPassword_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      this.Unlock();
      e.Handled = true;
    }

    private void Unlock()
    {
      if (this.tbPassword.Text.Trim() == "demo")
      {
        this.DialogResult = DialogResult.OK;
      }
      else
      {
        int num = (int) MessageBox.Show("Incorrect password.", Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.tbPassword.Focus();
        this.tbPassword.SelectAll();
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UnlockCompetitionForm));
      this.window = new Panel();
      this.tbPassword = new TextBox();
      this.label2 = new Label();
      this.bCancel = new Label();
      this.pNavigation = new Panel();
      this.label7 = new Label();
      this.pbLogo = new PictureBox();
      this.bUnlock = new Label();
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
      this.window.Controls.Add((Control) this.tbPassword);
      this.window.Controls.Add((Control) this.label2);
      this.window.Controls.Add((Control) this.bCancel);
      this.window.Controls.Add((Control) this.pNavigation);
      this.window.Controls.Add((Control) this.bUnlock);
      this.window.Controls.Add((Control) this.windowBar);
      this.window.Dock = DockStyle.Fill;
      this.window.Location = new Point(0, 0);
      this.window.Name = "window";
      this.window.Size = new Size(400, 268);
      this.window.TabIndex = 0;
      this.tbPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tbPassword.BackColor = Color.White;
      this.tbPassword.BorderStyle = BorderStyle.FixedSingle;
      this.tbPassword.ForeColor = Color.Black;
      this.tbPassword.Location = new Point(30, 151);
      this.tbPassword.Name = "tbPassword";
      this.tbPassword.PasswordChar = '*';
      this.tbPassword.Size = new Size(340, 26);
      this.tbPassword.TabIndex = 38;
      this.tbPassword.KeyDown += new KeyEventHandler(this.tbPassword_KeyDown);
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.FromArgb(35, 39, 48);
      this.label2.ForeColor = Color.FromArgb(193, 184, 201);
      this.label2.Location = new Point(30, 130);
      this.label2.Name = "label2";
      this.label2.Size = new Size(78, 18);
      this.label2.TabIndex = 37;
      this.label2.Text = "Password";
      this.bCancel.BackColor = Color.FromArgb(36, 53, 93);
      this.bCancel.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bCancel.ForeColor = Color.FromArgb(193, 184, 201);
      this.bCancel.Location = new Point(30, 195);
      this.bCancel.Name = "bCancel";
      this.bCancel.Size = new Size(150, 40);
      this.bCancel.TabIndex = 34;
      this.bCancel.Text = "Cancel";
      this.bCancel.TextAlign = ContentAlignment.MiddleCenter;
      this.bCancel.Click += new EventHandler(this.bCancel_Click);
      this.bCancel.MouseEnter += new EventHandler(this.blueButton_MouseEnter);
      this.bCancel.MouseLeave += new EventHandler(this.blueButton_MouseLeave);
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
      this.label7.Size = new Size(188, 22);
      this.label7.TabIndex = 1;
      this.label7.Text = "Unlock competition";
      this.pbLogo.Image = (Image) Resources.logo;
      this.pbLogo.Location = new Point(6, 6);
      this.pbLogo.Name = "pbLogo";
      this.pbLogo.Size = new Size(64, 64);
      this.pbLogo.SizeMode = PictureBoxSizeMode.AutoSize;
      this.pbLogo.TabIndex = 0;
      this.pbLogo.TabStop = false;
      this.bUnlock.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.bUnlock.BackColor = Color.FromArgb(36, 201, 133);
      this.bUnlock.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bUnlock.ForeColor = Color.White;
      this.bUnlock.Location = new Point(220, 195);
      this.bUnlock.Name = "bUnlock";
      this.bUnlock.Size = new Size(150, 40);
      this.bUnlock.TabIndex = 33;
      this.bUnlock.Text = "Unlock";
      this.bUnlock.TextAlign = ContentAlignment.MiddleCenter;
      this.bUnlock.Click += new EventHandler(this.bUnlock_Click);
      this.bUnlock.MouseEnter += new EventHandler(this.greenButton_MouseEnter);
      this.bUnlock.MouseLeave += new EventHandler(this.greenButton_MouseLeave);
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
      this.ClientSize = new Size(400, 268);
      this.Controls.Add((Control) this.window);
      this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Margin = new Padding(4);
      this.Name = nameof (UnlockCompetitionForm);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "MrBuggy 6 - Unlock competition";
      this.window.ResumeLayout(false);
      this.window.PerformLayout();
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
