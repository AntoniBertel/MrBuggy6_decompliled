// Decompiled with JetBrains decompiler
// Type: MrBuggy6.MainForm
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
  public class MainForm : Form
  {
    private bool windowBarMove;
    private Point windowBarFirstPoint;
    private Rectangle NormalBounds;
    private bool IsMaximized;
    private DefectsForm defectsForm;
    private ReportForm reportForm;
    private EndForm endForm;
    private CompetitionManager Manager;
    private IContainer components;
    private Panel window;
    private Label bDefects;
    private Label bNext;
    private Label bPrevious;
    private Label testCharterTitle;
    private TextBox tbTaskDescription;
    private Panel pFooter;
    private Label bEnd;
    private Panel pNavigation;
    private PictureBox bHelp;
    private PictureBox bResetData;
    private PictureBox bMessages;
    private PictureBox bUserSettings;
    private Label lUserName;
    private PictureBox pbLogo;
    private Panel windowBar;
    private PictureBox bMinimize;
    private Label windowTitle;
    private PictureBox bMaximize;
    private PictureBox bClose;
    private PictureBox status;
    private SplitContainer splitContainer1;
    private Timer tServerAddressController;
    private ToolTip toolTip;
    private Label bReport;
    private WebBrowser viewBox;
    private int currentTestCharterIndex;

    public MainForm()
    {
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.InitializeComponent();
      this.windowTitle.Text = Resources.windowTitle;
      this.Text = this.Text + " [demo version " + Settings.AppVersion + "]";
      this.toolTip.SetToolTip((Control) this.bUserSettings, "User settings");
      this.toolTip.SetToolTip((Control) this.bMessages, "Messages");
      this.toolTip.SetToolTip((Control) this.bResetData, "Reset data");
      this.toolTip.SetToolTip((Control) this.bHelp, "Help");
      this.tbTaskDescription.ContextMenu = new ContextMenu();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      if (Settings.User != null)
      {
        string appPath = Application.StartupPath.TrimEnd('\\') + "\\";
        Settings.ReportFile = new ReportFile(appPath + Settings.User.Identifier + ".tcr", Settings.User);
        if (Settings.User.IsAdmin && !Settings.ReportFile.LoadFromFile())
        {
          if (Settings.ReportFile.Status == ReportFileStatus.Corrupted)
          {
            if (MessageBox.Show(Resources.reportFileCorruptedMessage, Resources.windowTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
              this.Manager.ResetReportFile();
            Settings.ReportFile.SaveToFile();
            Settings.ReportFile.LoadFromFile();
          }
          else if (Settings.ReportFile.Status == ReportFileStatus.Fragmentary)
          {
            int num = (int) MessageBox.Show(Resources.reportFileDamagedMessage, Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            Settings.ReportFile.SaveToFile();
            Settings.ReportFile.LoadFromFile();
          }
        }
        if (Settings.ReportFile.Status == ReportFileStatus.OK)
        {
          this.Manager = new CompetitionManager(this.viewBox, Resources.ResourceManager, appPath);
          this.Manager.ConnectionChangeStatus += new ManagerConnectionChangeStatusEvent(this.Manager_ConnectionChangeStatus);
          this.Manager.EndCompetition += new ManagerEndCompetitionEvent(this.Manager_EndCompetition);
          this.Manager.InitWebServices();
          this.lUserName.Text = Settings.User.ToString();
          if (!Settings.User.IsAdmin)
          {
            this.bDefects.Visible = this.bReport.Visible = this.bEnd.Visible = this.bHelp.Visible = false;
            this.tbTaskDescription.Height = 450;
          }
          this.ChangeTask(0);
          if (!Settings.ReportFile.CompetitionInProgress)
            return;
          this.Manager.StartWebServices();
        }
        else
        {
          int num = (int) MessageBox.Show(Resources.saveReportFileError, Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this.Close();
        }
      }
      else
        this.Close();
    }

    private void MainForm_Shown(object sender, EventArgs e)
    {
      if (Settings.ReportFile.CompetitionInProgress)
        return;
      this.Manager.End();
    }

    private void Manager_ConnectionChangeStatus(object sender, ConnetcionStatus connectionStatus)
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

    private void Manager_EndCompetition(object sender)
    {
      if (this.defectsForm != null)
      {
        if (this.defectsForm.DefectFormIsOpen)
        {
          this.defectsForm.Focus();
          int num = (int) MessageBox.Show("The competition ended. It is not possible to add and modify defects.", Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        this.defectsForm.Close();
      }
      if (this.reportForm != null)
        this.reportForm.Close();
      this.Hide();
      if (this.endForm == null)
      {
        this.endForm = new EndForm();
        this.endForm.FormClosed += (FormClosedEventHandler) ((internalSender, e) =>
        {
          if (this.endForm.BackToCompetition)
          {
            this.endForm = (EndForm) null;
            this.Show();
            Settings.ReportFile.CompetitionInProgress = true;
            Settings.ReportFile.SaveToFile();
            if (Settings.ExternalServerAddress)
            {
              this.tServerAddressController.Enabled = false;
              this.Manager.UpdateWebServiceAddresses();
            }
            this.Manager.StartWebServices();
          }
          else
            this.Close();
        });
      }
      this.endForm.Show();
      this.endForm.Left = this.Left + (this.Width - this.endForm.Width) / 2;
      this.endForm.Top = this.Top + (this.Height - this.endForm.Height) / 2;
    }

    private void bUserSettings_Click(object sender, EventArgs e)
    {
    }

    private void bMessages_Click(object sender, EventArgs e)
    {
    }

    private void bResetData_Click(object sender, EventArgs e)
    {
    }

    private void bHelp_Click(object sender, EventArgs e)
    {
      using (HelpForm helpForm = new HelpForm())
      {
        int num = (int) helpForm.ShowDialog();
      }
    }

    private void bPrevious_Click(object sender, EventArgs e)
    {
      this.ChangeTask(--this.currentTestCharterIndex);
    }

    private void bNext_Click(object sender, EventArgs e)
    {
      this.ChangeTask(++this.currentTestCharterIndex);
    }

    private void bDefects_Click(object sender, EventArgs e)
    {
      if (this.defectsForm == null)
      {
        this.defectsForm = new DefectsForm(this.Manager);
        this.defectsForm.FormClosed += (FormClosedEventHandler) ((internalSender, iternalE) =>
        {
          this.defectsForm.Dispose();
          this.defectsForm = (DefectsForm) null;
        });
        this.defectsForm.Show();
      }
      else
        this.defectsForm.Focus();
    }

    private void bReport_Click(object sender, EventArgs e)
    {
      if (this.reportForm == null)
      {
        this.reportForm = new ReportForm(this.Manager);
        this.reportForm.FormClosed += (FormClosedEventHandler) ((internalSender, iternalE) =>
        {
          this.reportForm.Dispose();
          this.reportForm = (ReportForm) null;
        });
        this.reportForm.Show();
      }
      else
        this.reportForm.Focus();
    }

    private void bEnd_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(Resources.endCompetitionMessage, Resources.windowTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      this.Manager.End();
    }

    private void bMinimize_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void bMaximize_Click(object sender, EventArgs e)
    {
      if (this.IsMaximized)
      {
        this.IsMaximized = false;
        this.Bounds = this.NormalBounds;
      }
      else
      {
        this.IsMaximized = true;
        this.NormalBounds = this.Bounds;
        this.Bounds = Screen.PrimaryScreen.WorkingArea;
      }
    }

    private void bClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void bUserSettings_MouseEnter(object sender, EventArgs e)
    {
      this.bUserSettings.Image = (Image) Resources.user_settings_hover;
    }

    private void bUserSettings_MouseLeave(object sender, EventArgs e)
    {
      this.bUserSettings.Image = (Image) Resources.user_settings;
    }

    private void bMessages_MouseEnter(object sender, EventArgs e)
    {
      this.bMessages.Image = (Image) Resources.messages_hover;
    }

    private void bMessages_MouseLeave(object sender, EventArgs e)
    {
      this.bMessages.Image = (Image) Resources.messages;
    }

    private void bResetData_MouseEnter(object sender, EventArgs e)
    {
      this.bResetData.Image = (Image) Resources.reset_data_hover;
    }

    private void bResetData_MouseLeave(object sender, EventArgs e)
    {
      this.bResetData.Image = (Image) Resources.reset_data;
    }

    private void bHelp_MouseEnter(object sender, EventArgs e)
    {
      this.bHelp.Image = (Image) Resources.help_hover;
    }

    private void bHelp_MouseLeave(object sender, EventArgs e)
    {
      this.bHelp.Image = (Image) Resources.help;
    }

    private void blueButton_MouseEnter(object sender, EventArgs e)
    {
      Label label = sender as Label;
      if (!label.Enabled)
        return;
      label.BackColor = Color.FromArgb(56, 83, 147);
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

    private void greenButton_MouseEnter(object sender, EventArgs e)
    {
      (sender as Label).BackColor = Color.FromArgb(19, 160, 102);
    }

    private void greenButton_MouseLeave(object sender, EventArgs e)
    {
      (sender as Label).BackColor = Color.FromArgb(36, 201, 133);
    }

    private void bMinimize_MouseEnter(object sender, EventArgs e)
    {
      (sender as PictureBox).Image = (Image) Resources.minimize_hover;
    }

    private void bMinimize_MouseLeave(object sender, EventArgs e)
    {
      (sender as PictureBox).Image = (Image) Resources.minimize;
    }

    private void bMaximize_MouseEnter(object sender, EventArgs e)
    {
      (sender as PictureBox).Image = (Image) Resources.maximize_hover;
    }

    private void bMaximize_MouseLeave(object sender, EventArgs e)
    {
      (sender as PictureBox).Image = (Image) Resources.maximize;
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

    private void tServerAddressController_Tick(object sender, EventArgs e)
    {
      if (!Settings.ExternalServerAddress)
        return;
      this.tServerAddressController.Enabled = false;
      this.Manager.UpdateWebServiceAddresses();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (Settings.User != null && !Settings.ReportFile.SaveToFile())
        e.Cancel = true;
      if (e.Cancel || this.Manager == null)
        return;
      this.Manager.StopWebServices();
    }

    private void testCharterDescription_KeyDown(object sender, KeyEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainForm));
      this.window = new Panel();
      this.splitContainer1 = new SplitContainer();
      this.viewBox = new WebBrowser();
      this.bReport = new Label();
      this.bDefects = new Label();
      this.testCharterTitle = new Label();
      this.bNext = new Label();
      this.tbTaskDescription = new TextBox();
      this.bPrevious = new Label();
      this.pFooter = new Panel();
      this.status = new PictureBox();
      this.pNavigation = new Panel();
      this.bHelp = new PictureBox();
      this.bResetData = new PictureBox();
      this.bEnd = new Label();
      this.bMessages = new PictureBox();
      this.bUserSettings = new PictureBox();
      this.lUserName = new Label();
      this.pbLogo = new PictureBox();
      this.windowBar = new Panel();
      this.bClose = new PictureBox();
      this.bMaximize = new PictureBox();
      this.bMinimize = new PictureBox();
      this.windowTitle = new Label();
      this.tServerAddressController = new Timer(this.components);
      this.toolTip = new ToolTip(this.components);
      this.window.SuspendLayout();
      this.splitContainer1.BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.pFooter.SuspendLayout();
      ((ISupportInitialize) this.status).BeginInit();
      this.pNavigation.SuspendLayout();
      ((ISupportInitialize) this.bHelp).BeginInit();
      ((ISupportInitialize) this.bResetData).BeginInit();
      ((ISupportInitialize) this.bMessages).BeginInit();
      ((ISupportInitialize) this.bUserSettings).BeginInit();
      ((ISupportInitialize) this.pbLogo).BeginInit();
      this.windowBar.SuspendLayout();
      ((ISupportInitialize) this.bClose).BeginInit();
      ((ISupportInitialize) this.bMaximize).BeginInit();
      ((ISupportInitialize) this.bMinimize).BeginInit();
      this.SuspendLayout();
      this.window.BorderStyle = BorderStyle.FixedSingle;
      this.window.Controls.Add((Control) this.splitContainer1);
      this.window.Controls.Add((Control) this.pFooter);
      this.window.Controls.Add((Control) this.pNavigation);
      this.window.Controls.Add((Control) this.windowBar);
      this.window.Dock = DockStyle.Fill;
      this.window.Location = new Point(0, 0);
      this.window.Name = "window";
      this.window.Size = new Size(1000, 700);
      this.window.TabIndex = 0;
      this.splitContainer1.Dock = DockStyle.Fill;
      this.splitContainer1.Location = new Point(0, 108);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Panel1.Controls.Add((Control) this.viewBox);
      this.splitContainer1.Panel1MinSize = 400;
      this.splitContainer1.Panel2.Controls.Add((Control) this.bReport);
      this.splitContainer1.Panel2.Controls.Add((Control) this.bDefects);
      this.splitContainer1.Panel2.Controls.Add((Control) this.testCharterTitle);
      this.splitContainer1.Panel2.Controls.Add((Control) this.bNext);
      this.splitContainer1.Panel2.Controls.Add((Control) this.tbTaskDescription);
      this.splitContainer1.Panel2.Controls.Add((Control) this.bPrevious);
      this.splitContainer1.Panel2MinSize = 270;
      this.splitContainer1.Size = new Size(998, 558);
      this.splitContainer1.SplitterDistance = 698;
      this.splitContainer1.TabIndex = 12;
      this.viewBox.AllowWebBrowserDrop = false;
      this.viewBox.Dock = DockStyle.Fill;
      this.viewBox.IsWebBrowserContextMenuEnabled = false;
      this.viewBox.Location = new Point(0, 0);
      this.viewBox.MinimumSize = new Size(20, 20);
      this.viewBox.Name = "viewBox";
      this.viewBox.Size = new Size(698, 558);
      this.viewBox.TabIndex = 7;
      this.bReport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.bReport.BackColor = Color.FromArgb(247, 84, 85);
      this.bReport.Cursor = Cursors.Default;
      this.bReport.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bReport.ForeColor = Color.White;
      this.bReport.Location = new Point(169, 506);
      this.bReport.Name = "bReport";
      this.bReport.Size = new Size(115, 35);
      this.bReport.TabIndex = 34;
      this.bReport.Text = "Report";
      this.bReport.TextAlign = ContentAlignment.MiddleCenter;
      this.bReport.Click += new EventHandler(this.bReport_Click);
      this.bReport.MouseEnter += new EventHandler(this.redButton_MouseEnter);
      this.bReport.MouseLeave += new EventHandler(this.redButton_MouseLeave);
      this.bDefects.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.bDefects.BackColor = Color.FromArgb(247, 84, 85);
      this.bDefects.Cursor = Cursors.Default;
      this.bDefects.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bDefects.ForeColor = Color.White;
      this.bDefects.Location = new Point(17, 506);
      this.bDefects.Name = "bDefects";
      this.bDefects.Size = new Size(115, 35);
      this.bDefects.TabIndex = 32;
      this.bDefects.Text = "Defects";
      this.bDefects.TextAlign = ContentAlignment.MiddleCenter;
      this.bDefects.Click += new EventHandler(this.bDefects_Click);
      this.bDefects.MouseEnter += new EventHandler(this.redButton_MouseEnter);
      this.bDefects.MouseLeave += new EventHandler(this.redButton_MouseLeave);
      this.testCharterTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.testCharterTitle.Font = new Font("Arial", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.testCharterTitle.ForeColor = Color.FromArgb(193, 184, 201);
      this.testCharterTitle.Location = new Point(15, 17);
      this.testCharterTitle.Name = "testCharterTitle";
      this.testCharterTitle.Size = new Size(454, 22);
      this.testCharterTitle.TabIndex = 29;
      this.testCharterTitle.Text = "Test Charter #1";
      this.bNext.BackColor = Color.FromArgb(36, 53, 93);
      this.bNext.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bNext.ForeColor = Color.FromArgb(193, 184, 201);
      this.bNext.Location = new Point(107, 49);
      this.bNext.Name = "bNext";
      this.bNext.Size = new Size(81, 29);
      this.bNext.TabIndex = 31;
      this.bNext.Text = "Next";
      this.bNext.TextAlign = ContentAlignment.MiddleCenter;
      this.bNext.Click += new EventHandler(this.bNext_Click);
      this.bNext.MouseEnter += new EventHandler(this.blueButton_MouseEnter);
      this.bNext.MouseLeave += new EventHandler(this.blueButton_MouseLeave);
      this.tbTaskDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.tbTaskDescription.BackColor = Color.FromArgb(35, 39, 48);
      this.tbTaskDescription.BorderStyle = BorderStyle.None;
      this.tbTaskDescription.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.tbTaskDescription.ForeColor = Color.FromArgb(193, 184, 201);
      this.tbTaskDescription.Location = new Point(16, 91);
      this.tbTaskDescription.Multiline = true;
      this.tbTaskDescription.Name = "tbTaskDescription";
      this.tbTaskDescription.ReadOnly = true;
      this.tbTaskDescription.ScrollBars = ScrollBars.Vertical;
      this.tbTaskDescription.Size = new Size(272, 380);
      this.tbTaskDescription.TabIndex = 28;
      this.tbTaskDescription.TabStop = false;
      this.tbTaskDescription.KeyDown += new KeyEventHandler(this.testCharterDescription_KeyDown);
      this.bPrevious.BackColor = Color.FromArgb(36, 53, 93);
      this.bPrevious.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bPrevious.ForeColor = Color.FromArgb(193, 184, 201);
      this.bPrevious.Location = new Point(16, 49);
      this.bPrevious.Name = "bPrevious";
      this.bPrevious.Size = new Size(85, 29);
      this.bPrevious.TabIndex = 30;
      this.bPrevious.Text = "Previous";
      this.bPrevious.TextAlign = ContentAlignment.MiddleCenter;
      this.bPrevious.Click += new EventHandler(this.bPrevious_Click);
      this.bPrevious.MouseEnter += new EventHandler(this.blueButton_MouseEnter);
      this.bPrevious.MouseLeave += new EventHandler(this.blueButton_MouseLeave);
      this.pFooter.BackColor = Color.FromArgb(23, 25, 31);
      this.pFooter.Controls.Add((Control) this.status);
      this.pFooter.Dock = DockStyle.Bottom;
      this.pFooter.Location = new Point(0, 666);
      this.pFooter.Name = "pFooter";
      this.pFooter.Size = new Size(998, 32);
      this.pFooter.TabIndex = 9;
      this.status.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.status.Image = (Image) Resources.status_offline;
      this.status.Location = new Point(974, 8);
      this.status.Name = "status";
      this.status.Size = new Size(16, 16);
      this.status.SizeMode = PictureBoxSizeMode.AutoSize;
      this.status.TabIndex = 35;
      this.status.TabStop = false;
      this.pNavigation.BackColor = Color.FromArgb(23, 25, 31);
      this.pNavigation.Controls.Add((Control) this.bHelp);
      this.pNavigation.Controls.Add((Control) this.bResetData);
      this.pNavigation.Controls.Add((Control) this.bEnd);
      this.pNavigation.Controls.Add((Control) this.bMessages);
      this.pNavigation.Controls.Add((Control) this.bUserSettings);
      this.pNavigation.Controls.Add((Control) this.lUserName);
      this.pNavigation.Controls.Add((Control) this.pbLogo);
      this.pNavigation.Dock = DockStyle.Top;
      this.pNavigation.Location = new Point(0, 32);
      this.pNavigation.Name = "pNavigation";
      this.pNavigation.Size = new Size(998, 76);
      this.pNavigation.TabIndex = 8;
      this.bHelp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.bHelp.Cursor = Cursors.Default;
      this.bHelp.Image = (Image) Resources.help;
      this.bHelp.Location = new Point(818, 25);
      this.bHelp.Name = "bHelp";
      this.bHelp.Size = new Size(25, 25);
      this.bHelp.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bHelp.TabIndex = 5;
      this.bHelp.TabStop = false;
      this.bHelp.Visible = false;
      this.bHelp.Click += new EventHandler(this.bHelp_Click);
      this.bHelp.MouseEnter += new EventHandler(this.bHelp_MouseEnter);
      this.bHelp.MouseLeave += new EventHandler(this.bHelp_MouseLeave);
      this.bResetData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.bResetData.Enabled = false;
      this.bResetData.Image = (Image) Resources.reset_data;
      this.bResetData.Location = new Point(787, 25);
      this.bResetData.Name = "bResetData";
      this.bResetData.Size = new Size(25, 25);
      this.bResetData.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bResetData.TabIndex = 4;
      this.bResetData.TabStop = false;
      this.bResetData.Visible = false;
      this.bResetData.Click += new EventHandler(this.bResetData_Click);
      this.bResetData.MouseEnter += new EventHandler(this.bResetData_MouseEnter);
      this.bResetData.MouseLeave += new EventHandler(this.bResetData_MouseLeave);
      this.bEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.bEnd.BackColor = Color.FromArgb(36, 201, 133);
      this.bEnd.Cursor = Cursors.Default;
      this.bEnd.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bEnd.ForeColor = Color.White;
      this.bEnd.Location = new Point(851, 25);
      this.bEnd.Name = "bEnd";
      this.bEnd.Size = new Size(139, 25);
      this.bEnd.TabIndex = 33;
      this.bEnd.Text = "End Competition";
      this.bEnd.TextAlign = ContentAlignment.MiddleCenter;
      this.bEnd.Click += new EventHandler(this.bEnd_Click);
      this.bEnd.MouseEnter += new EventHandler(this.greenButton_MouseEnter);
      this.bEnd.MouseLeave += new EventHandler(this.greenButton_MouseLeave);
      this.bMessages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.bMessages.Enabled = false;
      this.bMessages.Image = (Image) Resources.messages;
      this.bMessages.Location = new Point(756, 25);
      this.bMessages.Name = "bMessages";
      this.bMessages.Size = new Size(25, 25);
      this.bMessages.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bMessages.TabIndex = 3;
      this.bMessages.TabStop = false;
      this.bMessages.Visible = false;
      this.bMessages.Click += new EventHandler(this.bMessages_Click);
      this.bMessages.MouseEnter += new EventHandler(this.bMessages_MouseEnter);
      this.bMessages.MouseLeave += new EventHandler(this.bMessages_MouseLeave);
      this.bUserSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.bUserSettings.Enabled = false;
      this.bUserSettings.Image = (Image) Resources.user_settings;
      this.bUserSettings.Location = new Point(725, 25);
      this.bUserSettings.Name = "bUserSettings";
      this.bUserSettings.Size = new Size(25, 25);
      this.bUserSettings.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bUserSettings.TabIndex = 2;
      this.bUserSettings.TabStop = false;
      this.bUserSettings.Visible = false;
      this.bUserSettings.Click += new EventHandler(this.bUserSettings_Click);
      this.bUserSettings.MouseEnter += new EventHandler(this.bUserSettings_MouseEnter);
      this.bUserSettings.MouseLeave += new EventHandler(this.bUserSettings_MouseLeave);
      this.lUserName.AutoSize = true;
      this.lUserName.Font = new Font("Arial", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.lUserName.ForeColor = Color.FromArgb(193, 184, 201);
      this.lUserName.Location = new Point(98, 28);
      this.lUserName.Name = "lUserName";
      this.lUserName.Size = new Size(130, 22);
      this.lUserName.TabIndex = 1;
      this.lUserName.Text = "Adam Nowak";
      this.pbLogo.Image = (Image) Resources.logo;
      this.pbLogo.Location = new Point(6, 6);
      this.pbLogo.Name = "pbLogo";
      this.pbLogo.Size = new Size(64, 64);
      this.pbLogo.SizeMode = PictureBoxSizeMode.AutoSize;
      this.pbLogo.TabIndex = 0;
      this.pbLogo.TabStop = false;
      this.windowBar.BackColor = Color.FromArgb(35, 39, 48);
      this.windowBar.Controls.Add((Control) this.bClose);
      this.windowBar.Controls.Add((Control) this.bMaximize);
      this.windowBar.Controls.Add((Control) this.bMinimize);
      this.windowBar.Controls.Add((Control) this.windowTitle);
      this.windowBar.Dock = DockStyle.Top;
      this.windowBar.ForeColor = Color.White;
      this.windowBar.Location = new Point(0, 0);
      this.windowBar.Name = "windowBar";
      this.windowBar.Size = new Size(998, 32);
      this.windowBar.TabIndex = 7;
      this.windowBar.DoubleClick += new EventHandler(this.bMaximize_Click);
      this.windowBar.MouseDown += new MouseEventHandler(this.windowBar_MouseDown);
      this.windowBar.MouseMove += new MouseEventHandler(this.windowBar_MouseMove);
      this.windowBar.MouseUp += new MouseEventHandler(this.windowBar_MouseUp);
      this.bClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.bClose.Image = (Image) Resources.close;
      this.bClose.Location = new Point(970, 5);
      this.bClose.Name = "bClose";
      this.bClose.Size = new Size(21, 21);
      this.bClose.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bClose.TabIndex = 2;
      this.bClose.TabStop = false;
      this.bClose.Click += new EventHandler(this.bClose_Click);
      this.bClose.MouseEnter += new EventHandler(this.bClose_MouseEnter);
      this.bClose.MouseLeave += new EventHandler(this.bClose_MouseLeave);
      this.bMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.bMaximize.Image = (Image) Resources.maximize;
      this.bMaximize.Location = new Point(943, 5);
      this.bMaximize.Name = "bMaximize";
      this.bMaximize.Size = new Size(21, 21);
      this.bMaximize.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bMaximize.TabIndex = 1;
      this.bMaximize.TabStop = false;
      this.bMaximize.Click += new EventHandler(this.bMaximize_Click);
      this.bMaximize.MouseEnter += new EventHandler(this.bMaximize_MouseEnter);
      this.bMaximize.MouseLeave += new EventHandler(this.bMaximize_MouseLeave);
      this.bMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.bMinimize.Image = (Image) Resources.minimize;
      this.bMinimize.Location = new Point(916, 5);
      this.bMinimize.Name = "bMinimize";
      this.bMinimize.Size = new Size(21, 21);
      this.bMinimize.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bMinimize.TabIndex = 0;
      this.bMinimize.TabStop = false;
      this.bMinimize.Click += new EventHandler(this.bMinimize_Click);
      this.bMinimize.MouseEnter += new EventHandler(this.bMinimize_MouseEnter);
      this.bMinimize.MouseLeave += new EventHandler(this.bMinimize_MouseLeave);
      this.windowTitle.AutoSize = true;
      this.windowTitle.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.windowTitle.ForeColor = Color.FromArgb(147, 147, 147);
      this.windowTitle.Location = new Point(4, 7);
      this.windowTitle.Name = "windowTitle";
      this.windowTitle.Size = new Size(96, 19);
      this.windowTitle.TabIndex = 0;
      this.windowTitle.Text = "Mr Buggy 6";
      this.windowTitle.Click += new EventHandler(this.bMaximize_Click);
      this.windowTitle.MouseDown += new MouseEventHandler(this.windowBar_MouseDown);
      this.windowTitle.MouseMove += new MouseEventHandler(this.windowBar_MouseMove);
      this.windowTitle.MouseUp += new MouseEventHandler(this.windowBar_MouseUp);
      this.tServerAddressController.Enabled = true;
      this.tServerAddressController.Interval = 1000;
      this.tServerAddressController.Tick += new EventHandler(this.tServerAddressController_Tick);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(35, 39, 48);
      this.ClientSize = new Size(1000, 700);
      this.Controls.Add((Control) this.window);
      this.DoubleBuffered = true;
      this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.ForeColor = Color.Black;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(1000, 700);
      this.Name = nameof (MainForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Mr Buggy 6";
      this.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
      this.Load += new EventHandler(this.MainForm_Load);
      this.Shown += new EventHandler(this.MainForm_Shown);
      this.window.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.pFooter.ResumeLayout(false);
      this.pFooter.PerformLayout();
      ((ISupportInitialize) this.status).EndInit();
      this.pNavigation.ResumeLayout(false);
      this.pNavigation.PerformLayout();
      ((ISupportInitialize) this.bHelp).EndInit();
      ((ISupportInitialize) this.bResetData).EndInit();
      ((ISupportInitialize) this.bMessages).EndInit();
      ((ISupportInitialize) this.bUserSettings).EndInit();
      ((ISupportInitialize) this.pbLogo).EndInit();
      this.windowBar.ResumeLayout(false);
      this.windowBar.PerformLayout();
      ((ISupportInitialize) this.bClose).EndInit();
      ((ISupportInitialize) this.bMaximize).EndInit();
      ((ISupportInitialize) this.bMinimize).EndInit();
      this.ResumeLayout(false);
    }

    private void ChangeTask(int index)
    {
      this.testCharterTitle.Text = Settings.Tasks[this.currentTestCharterIndex].Title;
      this.tbTaskDescription.Text = Settings.Tasks[this.currentTestCharterIndex].Description;
      this.bPrevious.Enabled = this.currentTestCharterIndex > 0;
      this.bNext.Enabled = this.currentTestCharterIndex < Settings.Tasks.Count - 1;
    }
  }
}
