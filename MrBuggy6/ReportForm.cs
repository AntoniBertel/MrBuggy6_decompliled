// Decompiled with JetBrains decompiler
// Type: MrBuggy6.ReportForm
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using MrBuggy6.Core;
using MrBuggy6.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrBuggy6
{
  public class ReportForm : Form
  {
    private int lastStep = 1;
    private bool windowBarMove;
    private Point windowBarFirstPoint;
    private CompetitionManager Manager;
    private IContainer components;
    private Panel window;
    private Panel pNavigation;
    private Label lUserName;
    private PictureBox pbLogo;
    private Panel windowBar;
    private PictureBox bClose;
    private Label windowTitle;
    private TextBox tbContent;
    private Panel pFooter;
    private PictureBox pbCharactersCount;
    private Label lContentCharactersCount;
    private PictureBox status;
    private ToolTip toolTip;
    private PictureBox pbSync;
    private Label lSynchronizationDate;
    private PictureBox bSave;

    public ReportForm(CompetitionManager manager)
    {
      this.InitializeComponent();
      this.windowTitle.Text = Resources.windowTitle;
      this.toolTip.SetToolTip((Control) this.bSave, "Save report");
      this.toolTip.SetToolTip((Control) this.status, "Server status: offline");
      this.toolTip.SetToolTip((Control) this.pbCharactersCount, "Number of characters");
      this.toolTip.SetToolTip((Control) this.lContentCharactersCount, "Number of characters");
      this.toolTip.SetToolTip((Control) this.pbSync, "Date of synchronization with the server");
      this.toolTip.SetToolTip((Control) this.lSynchronizationDate, "Date of synchronization with the server");
      this.tbContent.ContextMenu = new ContextMenu();
      this.ActiveControl = (Control) this.window;
      this.Manager = manager;
      this.ChangeConnectionStatus(manager.LastConnectionStatus);
      this.Manager.ConnectionChangeStatus += new ManagerConnectionChangeStatusEvent(this.Manager_ConnectionChangeStatus);
      this.Manager.SaveReportToFileCompleted += new ManagerSaveReportToFileCompletedEvent(this.Manager_SaveReportToFileCompleted);
      this.tbContent.Text = Settings.ReportFile.Report.Content;
      this.lSynchronizationDate.Text = Settings.ReportFile.Report.SynchronizationDateAsString;
      if (this.lSynchronizationDate.Text != "none")
      {
        this.pbSync.Left -= 115;
        this.lSynchronizationDate.Left -= 115;
        this.lSynchronizationDate.Width += 115;
      }
      this.UpdateCharactersCount();
    }

    private void Manager_ConnectionChangeStatus(object sender, ConnetcionStatus connectionStatus)
    {
      this.ChangeConnectionStatus(connectionStatus);
    }

    private void Manager_SaveReportToFileCompleted(object sender, bool success)
    {
      if (success)
        return;
      int num = (int) MessageBox.Show(Resources.saveReportFileError, Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.bSave.Enabled = Settings.ReportFile.Report.Content != this.tbContent.Text.Trim();
    }

    private void ChangeConnectionStatus(ConnetcionStatus connectionStatus)
    {
      if (connectionStatus == ConnetcionStatus.Online)
      {
        this.status.Image = (Image) Resources.status_online;
        this.toolTip.SetToolTip((Control) this.status, "Server status: online");
      }
      else if (connectionStatus == ConnetcionStatus.Busy)
      {
        this.status.Image = (Image) Resources.status_busy;
        this.toolTip.SetToolTip((Control) this.status, "Server status: is busy");
      }
      else
      {
        this.status.Image = (Image) Resources.status_offline;
        this.toolTip.SetToolTip((Control) this.status, "Server status: offline");
      }
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

    private void UpdateCharactersCount()
    {
      int length = this.tbContent.Text.Length.ToString().Length;
      int num1 = length - this.lastStep;
      if (num1 != 0)
      {
        this.lastStep = length;
        int num2 = num1 * 9;
        this.pbCharactersCount.Left -= num2;
        this.lContentCharactersCount.Left -= num2;
        this.lContentCharactersCount.Width += num2;
        this.pbSync.Left -= num2;
        this.lSynchronizationDate.Left -= num2;
      }
      this.bSave.Enabled = Settings.ReportFile.Report.Content != this.tbContent.Text.Trim();
      this.lContentCharactersCount.Text = this.tbContent.Text.Length.ToString() + "/" + (object) this.tbContent.MaxLength;
    }

    private void tbContent_TextChanged(object sender, EventArgs e)
    {
      this.bSave.Enabled = true;
      this.UpdateCharactersCount();
    }

    private void bSave_MouseEnter(object sender, EventArgs e)
    {
      if (!this.bSave.Enabled)
        return;
      this.bSave.Image = (Image) Resources.save_hover;
    }

    private void bSave_MouseLeave(object sender, EventArgs e)
    {
      this.bSave.Image = (Image) Resources.save;
    }

    private void bSave_Click(object sender, EventArgs e)
    {
      this.tbContent.Text = this.tbContent.Text.Trim();
      this.bSave.Enabled = false;
      this.Manager.SaveReport(this.tbContent.Text);
    }

    private void ReportForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.tbContent.Text = this.tbContent.Text.Trim();
      if (Settings.ReportFile.Report.Content != this.tbContent.Text)
      {
        switch (MessageBox.Show(Resources.reportModifiedMessage, Resources.windowTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
        {
          case DialogResult.Cancel:
            e.Cancel = true;
            break;
          case DialogResult.Yes:
            this.Manager.SaveReport(this.tbContent.Text);
            break;
        }
      }
      if (e.Cancel)
        return;
      this.Manager.ConnectionChangeStatus -= new ManagerConnectionChangeStatusEvent(this.Manager_ConnectionChangeStatus);
      this.Manager.SaveReportToFileCompleted -= new ManagerSaveReportToFileCompletedEvent(this.Manager_SaveReportToFileCompleted);
    }

    private void tbContent_KeyDown(object sender, KeyEventArgs e)
    {
      if (!e.Control || e.Alt || e.KeyCode != Keys.A)
        return;
      (sender as TextBox).SelectAll();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReportForm));
      this.window = new Panel();
      this.tbContent = new TextBox();
      this.pFooter = new Panel();
      this.lSynchronizationDate = new Label();
      this.pbSync = new PictureBox();
      this.pbCharactersCount = new PictureBox();
      this.lContentCharactersCount = new Label();
      this.status = new PictureBox();
      this.pNavigation = new Panel();
      this.bSave = new PictureBox();
      this.lUserName = new Label();
      this.pbLogo = new PictureBox();
      this.windowBar = new Panel();
      this.bClose = new PictureBox();
      this.windowTitle = new Label();
      this.toolTip = new ToolTip(this.components);
      this.window.SuspendLayout();
      this.pFooter.SuspendLayout();
      ((ISupportInitialize) this.pbSync).BeginInit();
      ((ISupportInitialize) this.pbCharactersCount).BeginInit();
      ((ISupportInitialize) this.status).BeginInit();
      this.pNavigation.SuspendLayout();
      ((ISupportInitialize) this.bSave).BeginInit();
      ((ISupportInitialize) this.pbLogo).BeginInit();
      this.windowBar.SuspendLayout();
      ((ISupportInitialize) this.bClose).BeginInit();
      this.SuspendLayout();
      this.window.BorderStyle = BorderStyle.FixedSingle;
      this.window.Controls.Add((Control) this.tbContent);
      this.window.Controls.Add((Control) this.pFooter);
      this.window.Controls.Add((Control) this.pNavigation);
      this.window.Controls.Add((Control) this.windowBar);
      this.window.Dock = DockStyle.Fill;
      this.window.Location = new Point(0, 0);
      this.window.Name = "window";
      this.window.Size = new Size(800, 600);
      this.window.TabIndex = 1;
      this.tbContent.BackColor = Color.White;
      this.tbContent.BorderStyle = BorderStyle.None;
      this.tbContent.Dock = DockStyle.Fill;
      this.tbContent.ForeColor = Color.Black;
      this.tbContent.Location = new Point(0, 108);
      this.tbContent.MaxLength = 4000;
      this.tbContent.Multiline = true;
      this.tbContent.Name = "tbContent";
      this.tbContent.ScrollBars = ScrollBars.Both;
      this.tbContent.Size = new Size(798, 458);
      this.tbContent.TabIndex = 20;
      this.tbContent.TextChanged += new EventHandler(this.tbContent_TextChanged);
      this.tbContent.KeyDown += new KeyEventHandler(this.tbContent_KeyDown);
      this.pFooter.BackColor = Color.FromArgb(23, 25, 31);
      this.pFooter.Controls.Add((Control) this.lSynchronizationDate);
      this.pFooter.Controls.Add((Control) this.pbSync);
      this.pFooter.Controls.Add((Control) this.pbCharactersCount);
      this.pFooter.Controls.Add((Control) this.lContentCharactersCount);
      this.pFooter.Controls.Add((Control) this.status);
      this.pFooter.Dock = DockStyle.Bottom;
      this.pFooter.Location = new Point(0, 566);
      this.pFooter.Name = "pFooter";
      this.pFooter.Size = new Size(798, 32);
      this.pFooter.TabIndex = 19;
      this.lSynchronizationDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.lSynchronizationDate.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.lSynchronizationDate.ForeColor = Color.FromArgb(193, 184, 201);
      this.lSynchronizationDate.Location = new Point(651, 7);
      this.lSynchronizationDate.Name = "lSynchronizationDate";
      this.lSynchronizationDate.Size = new Size(42, 18);
      this.lSynchronizationDate.TabIndex = 39;
      this.lSynchronizationDate.Text = "none";
      this.pbSync.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.pbSync.Image = (Image) Resources.sync;
      this.pbSync.Location = new Point(634, 8);
      this.pbSync.Name = "pbSync";
      this.pbSync.Size = new Size(16, 16);
      this.pbSync.SizeMode = PictureBoxSizeMode.AutoSize;
      this.pbSync.TabIndex = 38;
      this.pbSync.TabStop = false;
      this.pbCharactersCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.pbCharactersCount.Image = (Image) Resources.characters_count;
      this.pbCharactersCount.Location = new Point(696, 8);
      this.pbCharactersCount.Name = "pbCharactersCount";
      this.pbCharactersCount.Size = new Size(16, 16);
      this.pbCharactersCount.SizeMode = PictureBoxSizeMode.AutoSize;
      this.pbCharactersCount.TabIndex = 37;
      this.pbCharactersCount.TabStop = false;
      this.lContentCharactersCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.lContentCharactersCount.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.lContentCharactersCount.ForeColor = Color.FromArgb(193, 184, 201);
      this.lContentCharactersCount.Location = new Point(710, 7);
      this.lContentCharactersCount.Name = "lContentCharactersCount";
      this.lContentCharactersCount.Size = new Size(58, 18);
      this.lContentCharactersCount.TabIndex = 36;
      this.lContentCharactersCount.Text = "0/4000";
      this.status.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.status.Image = (Image) Resources.status_offline;
      this.status.Location = new Point(774, 8);
      this.status.Name = "status";
      this.status.Size = new Size(16, 16);
      this.status.SizeMode = PictureBoxSizeMode.AutoSize;
      this.status.TabIndex = 35;
      this.status.TabStop = false;
      this.pNavigation.BackColor = Color.FromArgb(23, 25, 31);
      this.pNavigation.Controls.Add((Control) this.bSave);
      this.pNavigation.Controls.Add((Control) this.lUserName);
      this.pNavigation.Controls.Add((Control) this.pbLogo);
      this.pNavigation.Dock = DockStyle.Top;
      this.pNavigation.Location = new Point(0, 32);
      this.pNavigation.Name = "pNavigation";
      this.pNavigation.Size = new Size(798, 76);
      this.pNavigation.TabIndex = 8;
      this.bSave.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.bSave.Enabled = false;
      this.bSave.Image = (Image) Resources.save;
      this.bSave.Location = new Point(762, 26);
      this.bSave.Name = "bSave";
      this.bSave.Size = new Size(25, 25);
      this.bSave.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bSave.TabIndex = 6;
      this.bSave.TabStop = false;
      this.bSave.Click += new EventHandler(this.bSave_Click);
      this.bSave.MouseEnter += new EventHandler(this.bSave_MouseEnter);
      this.bSave.MouseLeave += new EventHandler(this.bSave_MouseLeave);
      this.lUserName.AutoSize = true;
      this.lUserName.Font = new Font("Arial", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.lUserName.ForeColor = Color.FromArgb(193, 184, 201);
      this.lUserName.Location = new Point(98, 28);
      this.lUserName.Name = "lUserName";
      this.lUserName.Size = new Size(72, 22);
      this.lUserName.TabIndex = 1;
      this.lUserName.Text = "Report";
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
      this.windowBar.Size = new Size(798, 32);
      this.windowBar.TabIndex = 7;
      this.windowBar.MouseDown += new MouseEventHandler(this.windowBar_MouseDown);
      this.windowBar.MouseMove += new MouseEventHandler(this.windowBar_MouseMove);
      this.windowBar.MouseUp += new MouseEventHandler(this.windowBar_MouseUp);
      this.bClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.bClose.Image = (Image) Resources.close;
      this.bClose.Location = new Point(770, 5);
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
      this.ClientSize = new Size(800, 600);
      this.Controls.Add((Control) this.window);
      this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Margin = new Padding(4);
      this.Name = nameof (ReportForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Report - MrBuggy 6";
      this.FormClosing += new FormClosingEventHandler(this.ReportForm_FormClosing);
      this.window.ResumeLayout(false);
      this.window.PerformLayout();
      this.pFooter.ResumeLayout(false);
      this.pFooter.PerformLayout();
      ((ISupportInitialize) this.pbSync).EndInit();
      ((ISupportInitialize) this.pbCharactersCount).EndInit();
      ((ISupportInitialize) this.status).EndInit();
      this.pNavigation.ResumeLayout(false);
      this.pNavigation.PerformLayout();
      ((ISupportInitialize) this.bSave).EndInit();
      ((ISupportInitialize) this.pbLogo).EndInit();
      this.windowBar.ResumeLayout(false);
      this.windowBar.PerformLayout();
      ((ISupportInitialize) this.bClose).EndInit();
      this.ResumeLayout(false);
    }
  }
}
