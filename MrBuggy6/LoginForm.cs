// Decompiled with JetBrains decompiler
// Type: MrBuggy6.LoginForm
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using Microsoft.Win32;
using MrBuggy6.Core;
using MrBuggy6.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MrBuggy6
{
  public class LoginForm : Form
  {
    private bool windowBarMove;
    private Point windowBarFirstPoint;
    private User user;
    private IContainer components;
    private Panel windowArea;
    private Panel windowBar;
    private PictureBox bClose;
    private Label windowTitle;
    private Panel pNavigation;
    private Label label7;
    private PictureBox pbLogo;
    private Label bSignIn;
    private TextBox tbPassword;
    private Label label2;
    private Label label1;
    private TextBox tbIdentifier;
    private Label bCancel;
    private Panel window;
    private Label lPhoneNumber;
    private MaskedTextBox tbPhoneNumber;

    public LoginForm()
    {
      this.InitializeComponent();
      this.windowTitle.Text = Resources.windowTitle;
      this.Height = 340;
      Settings.User = (User) null;
      if (Registry.CurrentUser.OpenSubKey("SOFTWARE\\MrBuggy6_DEMO", true) == null)
        Registry.CurrentUser.CreateSubKey("SOFTWARE\\MrBuggy6_DEMO");
      object obj1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MrBuggy6_DEMO").GetValue("identifier");
      if (obj1 != null)
        this.tbIdentifier.Text = obj1.ToString();
      object obj2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MrBuggy6_DEMO").GetValue("phone_number");
      if (obj2 == null)
        return;
      this.tbPhoneNumber.Text = obj2.ToString();
    }

    private void SignIn()
    {
      if (this.user == null)
      {
        this.tbIdentifier.Text = this.tbIdentifier.Text.Trim();
        if (!User.IsCorrectIdentifier(this.tbIdentifier.Text) || this.tbPassword.Text != "demo")
        {
          int num = (int) MessageBox.Show("Incorrect identifier and/or password.", Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          this.user = new User(this.tbIdentifier.Text);
          if (this.user.IsAdmin)
          {
            this.Height = 405;
            this.tbIdentifier.Enabled = false;
            this.tbPassword.Enabled = false;
            this.lPhoneNumber.Show();
            this.tbPhoneNumber.Show();
            this.tbPhoneNumber.Enabled = true;
            this.bSignIn.Text = "Continue";
          }
          else
            this.EndLogin();
        }
      }
      else
      {
        this.tbPhoneNumber.Text = this.tbPhoneNumber.Text.Trim();
        if (!Regex.IsMatch(this.tbPhoneNumber.Text, "^\\+\\d{2}\\s{1}[\\d]{3}-[\\d]{3}-[\\d]{3}$"))
        {
          int num = (int) MessageBox.Show("A telephone number is needed for the commission to contact the team captain/contestant in case of problems with the work evaluation.", Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this.tbPhoneNumber.Focus();
          this.tbPhoneNumber.SelectAll();
        }
        else
        {
          this.user.PhoneNumber = this.tbPhoneNumber.Text;
          this.EndLogin();
        }
      }
    }

    private void EndLogin()
    {
      Settings.User = this.user;
      Registry.CurrentUser.OpenSubKey("SOFTWARE\\MrBuggy6_DEMO", true).SetValue("identifier", (object) this.tbIdentifier.Text);
      Registry.CurrentUser.OpenSubKey("SOFTWARE\\MrBuggy6_DEMO", true).SetValue("phone_number", (object) this.tbPhoneNumber.Text);
      this.DialogResult = DialogResult.OK;
    }

    private void bSignIn_Click(object sender, EventArgs e)
    {
      this.SignIn();
    }

    private void bCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void blueButton_MouseEnter(object sender, EventArgs e)
    {
      (sender as Label).BackColor = System.Drawing.Color.FromArgb(56, 83, 147);
    }

    private void blueButton_MouseLeave(object sender, EventArgs e)
    {
      (sender as Label).BackColor = System.Drawing.Color.FromArgb(36, 53, 93);
    }

    private void greenButton_MouseEnter(object sender, EventArgs e)
    {
      if (!(sender as Label).Enabled)
        return;
      (sender as Label).BackColor = System.Drawing.Color.FromArgb(19, 160, 102);
    }

    private void greenButton_MouseLeave(object sender, EventArgs e)
    {
      (sender as Label).BackColor = System.Drawing.Color.FromArgb(36, 201, 133);
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

    private void LoginForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      e.Handled = true;
      this.SignIn();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LoginForm));
      this.windowArea = new Panel();
      this.lPhoneNumber = new Label();
      this.tbPhoneNumber = new MaskedTextBox();
      this.tbPassword = new TextBox();
      this.label2 = new Label();
      this.label1 = new Label();
      this.tbIdentifier = new TextBox();
      this.bCancel = new Label();
      this.pNavigation = new Panel();
      this.label7 = new Label();
      this.pbLogo = new PictureBox();
      this.bSignIn = new Label();
      this.windowBar = new Panel();
      this.bClose = new PictureBox();
      this.windowTitle = new Label();
      this.window = new Panel();
      this.windowArea.SuspendLayout();
      this.pNavigation.SuspendLayout();
      ((ISupportInitialize) this.pbLogo).BeginInit();
      this.windowBar.SuspendLayout();
      ((ISupportInitialize) this.bClose).BeginInit();
      this.window.SuspendLayout();
      this.SuspendLayout();
      this.windowArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.windowArea.Controls.Add((Control) this.lPhoneNumber);
      this.windowArea.Controls.Add((Control) this.tbPhoneNumber);
      this.windowArea.Controls.Add((Control) this.tbPassword);
      this.windowArea.Controls.Add((Control) this.label2);
      this.windowArea.Controls.Add((Control) this.label1);
      this.windowArea.Controls.Add((Control) this.tbIdentifier);
      this.windowArea.Controls.Add((Control) this.bCancel);
      this.windowArea.Controls.Add((Control) this.pNavigation);
      this.windowArea.Controls.Add((Control) this.bSignIn);
      this.windowArea.Controls.Add((Control) this.windowBar);
      this.windowArea.Location = new Point(5, 5);
      this.windowArea.Name = "windowArea";
      this.windowArea.Size = new Size(388, 393);
      this.windowArea.TabIndex = 0;
      this.lPhoneNumber.AutoSize = true;
      this.lPhoneNumber.ForeColor = System.Drawing.Color.FromArgb(193, 184, 201);
      this.lPhoneNumber.Location = new Point(30, 254);
      this.lPhoneNumber.Name = "lPhoneNumber";
      this.lPhoneNumber.Size = new Size(109, 18);
      this.lPhoneNumber.TabIndex = 40;
      this.lPhoneNumber.Text = "Phone number";
      this.lPhoneNumber.Visible = false;
      this.tbPhoneNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tbPhoneNumber.BackColor = System.Drawing.Color.White;
      this.tbPhoneNumber.BorderStyle = BorderStyle.FixedSingle;
      this.tbPhoneNumber.Enabled = false;
      this.tbPhoneNumber.ForeColor = System.Drawing.Color.Black;
      this.tbPhoneNumber.Location = new Point(30, 275);
      this.tbPhoneNumber.Mask = "+99 000-000-000";
      this.tbPhoneNumber.Name = "tbPhoneNumber";
      this.tbPhoneNumber.Size = new Size(330, 26);
      this.tbPhoneNumber.TabIndex = 39;
      this.tbPhoneNumber.Visible = false;
      this.tbPhoneNumber.KeyDown += new KeyEventHandler(this.LoginForm_KeyDown);
      this.tbPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tbPassword.BackColor = System.Drawing.Color.White;
      this.tbPassword.BorderStyle = BorderStyle.FixedSingle;
      this.tbPassword.ForeColor = System.Drawing.Color.Black;
      this.tbPassword.Location = new Point(30, 209);
      this.tbPassword.Name = "tbPassword";
      this.tbPassword.PasswordChar = '*';
      this.tbPassword.Size = new Size(330, 26);
      this.tbPassword.TabIndex = 38;
      this.tbPassword.KeyDown += new KeyEventHandler(this.LoginForm_KeyDown);
      this.label2.AutoSize = true;
      this.label2.ForeColor = System.Drawing.Color.FromArgb(193, 184, 201);
      this.label2.Location = new Point(30, 188);
      this.label2.Name = "label2";
      this.label2.Size = new Size(78, 18);
      this.label2.TabIndex = 37;
      this.label2.Text = "Password";
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.FromArgb(193, 184, 201);
      this.label1.Location = new Point(30, 125);
      this.label1.Name = "label1";
      this.label1.Size = new Size(67, 18);
      this.label1.TabIndex = 36;
      this.label1.Text = "Identifier";
      this.tbIdentifier.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tbIdentifier.BackColor = System.Drawing.Color.White;
      this.tbIdentifier.BorderStyle = BorderStyle.FixedSingle;
      this.tbIdentifier.ForeColor = System.Drawing.Color.Black;
      this.tbIdentifier.Location = new Point(30, 146);
      this.tbIdentifier.Name = "tbIdentifier";
      this.tbIdentifier.Size = new Size(330, 26);
      this.tbIdentifier.TabIndex = 35;
      this.tbIdentifier.KeyDown += new KeyEventHandler(this.LoginForm_KeyDown);
      this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.bCancel.BackColor = System.Drawing.Color.FromArgb(36, 53, 93);
      this.bCancel.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bCancel.ForeColor = System.Drawing.Color.FromArgb(193, 184, 201);
      this.bCancel.Location = new Point(30, 327);
      this.bCancel.Name = "bCancel";
      this.bCancel.Size = new Size(150, 40);
      this.bCancel.TabIndex = 34;
      this.bCancel.Text = "Cancel";
      this.bCancel.TextAlign = ContentAlignment.MiddleCenter;
      this.bCancel.Click += new EventHandler(this.bCancel_Click);
      this.bCancel.MouseEnter += new EventHandler(this.blueButton_MouseEnter);
      this.bCancel.MouseLeave += new EventHandler(this.blueButton_MouseLeave);
      this.pNavigation.BackColor = System.Drawing.Color.FromArgb(45, 51, 65);
      this.pNavigation.Controls.Add((Control) this.label7);
      this.pNavigation.Controls.Add((Control) this.pbLogo);
      this.pNavigation.Dock = DockStyle.Top;
      this.pNavigation.Location = new Point(0, 32);
      this.pNavigation.Name = "pNavigation";
      this.pNavigation.Size = new Size(388, 76);
      this.pNavigation.TabIndex = 9;
      this.label7.AutoSize = true;
      this.label7.Font = new Font("Arial", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.label7.ForeColor = System.Drawing.Color.FromArgb(193, 184, 201);
      this.label7.Location = new Point(98, 26);
      this.label7.Name = "label7";
      this.label7.Size = new Size(63, 22);
      this.label7.TabIndex = 1;
      this.label7.Text = "Login";
      this.pbLogo.Image = (Image) Resources.logo;
      this.pbLogo.Location = new Point(6, 6);
      this.pbLogo.Name = "pbLogo";
      this.pbLogo.Size = new Size(64, 64);
      this.pbLogo.SizeMode = PictureBoxSizeMode.AutoSize;
      this.pbLogo.TabIndex = 0;
      this.pbLogo.TabStop = false;
      this.bSignIn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.bSignIn.BackColor = System.Drawing.Color.FromArgb(36, 201, 133);
      this.bSignIn.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bSignIn.ForeColor = System.Drawing.Color.White;
      this.bSignIn.Location = new Point(210, 327);
      this.bSignIn.Name = "bSignIn";
      this.bSignIn.Size = new Size(150, 40);
      this.bSignIn.TabIndex = 33;
      this.bSignIn.Text = "Sign in";
      this.bSignIn.TextAlign = ContentAlignment.MiddleCenter;
      this.bSignIn.Click += new EventHandler(this.bSignIn_Click);
      this.bSignIn.MouseEnter += new EventHandler(this.greenButton_MouseEnter);
      this.bSignIn.MouseLeave += new EventHandler(this.greenButton_MouseLeave);
      this.windowBar.BackColor = System.Drawing.Color.FromArgb(35, 39, 48);
      this.windowBar.Controls.Add((Control) this.bClose);
      this.windowBar.Controls.Add((Control) this.windowTitle);
      this.windowBar.Dock = DockStyle.Top;
      this.windowBar.ForeColor = System.Drawing.Color.White;
      this.windowBar.Location = new Point(0, 0);
      this.windowBar.Name = "windowBar";
      this.windowBar.Size = new Size(388, 32);
      this.windowBar.TabIndex = 8;
      this.windowBar.MouseDown += new MouseEventHandler(this.windowBar_MouseDown);
      this.windowBar.MouseMove += new MouseEventHandler(this.windowBar_MouseMove);
      this.windowBar.MouseUp += new MouseEventHandler(this.windowBar_MouseUp);
      this.bClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.bClose.Image = (Image) Resources.close;
      this.bClose.Location = new Point(361, 5);
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
      this.windowTitle.ForeColor = System.Drawing.Color.FromArgb(147, 147, 147);
      this.windowTitle.Location = new Point(4, 7);
      this.windowTitle.Name = "windowTitle";
      this.windowTitle.Size = new Size(96, 19);
      this.windowTitle.TabIndex = 0;
      this.windowTitle.Text = "Mr Buggy 6";
      this.windowTitle.MouseDown += new MouseEventHandler(this.windowBar_MouseDown);
      this.windowTitle.MouseMove += new MouseEventHandler(this.windowBar_MouseMove);
      this.windowTitle.MouseUp += new MouseEventHandler(this.windowBar_MouseUp);
      this.window.BorderStyle = BorderStyle.FixedSingle;
      this.window.Controls.Add((Control) this.windowArea);
      this.window.Dock = DockStyle.Fill;
      this.window.Location = new Point(0, 0);
      this.window.Name = "window";
      this.window.Size = new Size(400, 405);
      this.window.TabIndex = 1;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.FromArgb(35, 39, 48);
      this.ClientSize = new Size(400, 405);
      this.Controls.Add((Control) this.window);
      this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (LoginForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "MrBuggy 6 - Login";
      this.Click += new EventHandler(this.bClose_Click);
      this.KeyDown += new KeyEventHandler(this.LoginForm_KeyDown);
      this.windowArea.ResumeLayout(false);
      this.windowArea.PerformLayout();
      this.pNavigation.ResumeLayout(false);
      this.pNavigation.PerformLayout();
      ((ISupportInitialize) this.pbLogo).EndInit();
      this.windowBar.ResumeLayout(false);
      this.windowBar.PerformLayout();
      ((ISupportInitialize) this.bClose).EndInit();
      this.window.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
