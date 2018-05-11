// Decompiled with JetBrains decompiler
// Type: MrBuggy6.DefectsForm
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using Custom.Component;
using MrBuggy6.Core;
using MrBuggy6.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrBuggy6
{
  public class DefectsForm : Form
  {
    private bool windowBarMove;
    private Point windowBarFirstPoint;
    private Rectangle NormalBounds;
    private bool IsMaximized;
    private bool wideDefectStatus;
    private DefectForm defectForm;
    private CompetitionManager Manager;
    private IContainer components;
    private Panel window;
    private Panel pNavigation;
    private PictureBox bDelete;
    private PictureBox bEdit;
    private PictureBox bAdd;
    private Label label7;
    private PictureBox pbLogo;
    private Panel windowBar;
    private PictureBox bClose;
    private PictureBox bMaximize;
    private PictureBox bMinimize;
    private Label windowTitle;
    private ListView lvDefects;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader5;
    private ColumnHeader columnHeader6;
    private Panel pFooter;
    private PictureBox status;
    private Label lDefectCount;
    private PictureBox pbBug;
    private ToolTip toolTip;
    private ColumnHeader sent;

    public bool DefectFormIsOpen
    {
      get
      {
        return this.defectForm != null;
      }
    }

    public DefectsForm(CompetitionManager manager)
    {
      this.InitializeComponent();
      this.windowTitle.Text = Resources.windowTitle;
      this.toolTip.SetToolTip((Control) this.bAdd, "Add defect");
      this.toolTip.SetToolTip((Control) this.bEdit, "Edit defect");
      this.toolTip.SetToolTip((Control) this.bDelete, "Delete defect");
      this.toolTip.SetToolTip((Control) this.status, "Server status: offline");
      this.toolTip.SetToolTip((Control) this.pbBug, "Number of defects");
      this.toolTip.SetToolTip((Control) this.lDefectCount, "Number of defects");
      new ListViewComparer(this.lvDefects, 0, SortOrder.Ascending, 0).Comparers[0] = (IListViewComparer) new ListViewNumberComparer();
      this.Manager = manager;
      this.ChangeConnectionStatus(manager.LastConnectionStatus);
      this.Manager.ConnectionChangeStatus += new ManagerConnectionChangeStatusEvent(this.Manager_ConnectionChangeStatus);
      this.Manager.AddIssueToFileCompleted += new ManagerAddIssueToFileCompletedEvent(this.Manager_AddIssueToFileCompleted);
      this.Manager.SaveIssueToFileCompleted += new ManagerSaveIssueToFileCompletedEvent(this.Manager_SaveIssueToFileCompleted);
      this.Manager.DeleteIssueFromFileCompleted += new ManagerDeleteIssueFromFileCompletedEvent(this.Manager_DeleteIssueFromFileCompleted);
      foreach (Issue issue in Settings.ReportFile.Issues)
        this.lvDefects.Items.Add(this.CreateListViewItem(issue));
      this.UpdateDefectCount();
    }

    private void Manager_ConnectionChangeStatus(object sender, ConnetcionStatus connectionStatus)
    {
      this.ChangeConnectionStatus(connectionStatus);
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

    private void Manager_AddIssueToFileCompleted(object sender, bool success, object tag)
    {
      if (success)
      {
        this.bEdit.Enabled = this.bDelete.Enabled = this.lvDefects.SelectedItems.Count == 1;
        this.lvDefects.Items.Add(tag as ListViewItem);
        this.UpdateDefectCount();
      }
      else
      {
        int num = (int) MessageBox.Show(Resources.saveReportFileError, Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      this.bAdd.Enabled = true;
      this.bEdit.Enabled = this.bDelete.Enabled = this.lvDefects.SelectedItems.Count == 1;
    }

    private void Manager_SaveIssueToFileCompleted(object sender, bool success, Issue issue, object tag)
    {
      if (success)
      {
        int num1 = 1;
        ListViewItem listViewItem = tag as ListViewItem;
        ListViewItem.ListViewSubItemCollection subItems1 = listViewItem.SubItems;
        int index1 = num1;
        int num2 = index1 + 1;
        subItems1[index1].Text = issue.Title;
        ListViewItem.ListViewSubItemCollection subItems2 = listViewItem.SubItems;
        int index2 = num2;
        int num3 = index2 + 1;
        subItems2[index2].Text = issue.Attr1.Name;
        ListViewItem.ListViewSubItemCollection subItems3 = listViewItem.SubItems;
        int index3 = num3;
        int num4 = index3 + 1;
        subItems3[index3].Text = issue.Attr2.Name;
        ListViewItem.ListViewSubItemCollection subItems4 = listViewItem.SubItems;
        int index4 = num4;
        int num5 = index4 + 1;
        subItems4[index4].Text = issue.Attr3.Name;
        ListViewItem.ListViewSubItemCollection subItems5 = listViewItem.SubItems;
        int index5 = num5;
        int num6 = index5 + 1;
        subItems5[index5].Text = issue.Task.Title;
        ListViewItem.ListViewSubItemCollection subItems6 = listViewItem.SubItems;
        int index6 = num6;
        int num7 = index6 + 1;
        subItems6[index6].Text = "No";
        listViewItem.Tag = (object) issue;
      }
      else
      {
        int num = (int) MessageBox.Show(Resources.saveReportFileError, Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      this.bAdd.Enabled = true;
      this.bEdit.Enabled = this.bDelete.Enabled = this.lvDefects.SelectedItems.Count == 1;
    }

    private void Manager_DeleteIssueFromFileCompleted(object sender, bool success, object tag)
    {
      if (success)
      {
        this.lvDefects.Items.Remove(tag as ListViewItem);
        this.UpdateDefectCount();
      }
      else
      {
        int num = (int) MessageBox.Show(Resources.saveReportFileError, Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      this.bAdd.Enabled = true;
      this.bEdit.Enabled = this.bDelete.Enabled = this.lvDefects.SelectedItems.Count == 1;
    }

    private void bAdd_Click(object sender, EventArgs e)
    {
      if (this.defectForm != null)
        return;
      this.bAdd.Enabled = this.bEdit.Enabled = this.bDelete.Enabled = false;
      this.defectForm = new DefectForm();
      this.defectForm.FormClosed += (FormClosedEventHandler) ((internalSender, iternalE) =>
      {
        if (this.defectForm.DialogResult == DialogResult.OK)
        {
          this.Manager.AddIssue(this.defectForm.Issue, (object) this.CreateListViewItem(this.defectForm.Issue));
        }
        else
        {
          this.bAdd.Enabled = true;
          this.bEdit.Enabled = this.bDelete.Enabled = this.lvDefects.SelectedItems.Count == 1;
        }
        this.defectForm.Dispose();
        this.defectForm = (DefectForm) null;
      });
      this.defectForm.Show();
      this.defectForm.Left = this.Left + (this.Width - this.defectForm.Width) / 2;
      this.defectForm.Top = this.Top + (this.Height - this.defectForm.Height) / 2;
    }

    private void bEdit_Click(object sender, EventArgs e)
    {
      this.EditDefect();
    }

    private void bDelete_Click(object sender, EventArgs e)
    {
      this.DeleteDefect();
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

    private void bAdd_MouseEnter(object sender, EventArgs e)
    {
      this.bAdd.Image = (Image) Resources.add_hover;
    }

    private void bAdd_MouseLeave(object sender, EventArgs e)
    {
      this.bAdd.Image = (Image) Resources.add;
    }

    private void bEdit_MouseEnter(object sender, EventArgs e)
    {
      if (!this.bEdit.Enabled)
        return;
      this.bEdit.Image = (Image) Resources.edit_hover;
    }

    private void bEdit_MouseLeave(object sender, EventArgs e)
    {
      this.bEdit.Image = (Image) Resources.edit;
    }

    private void bDelete_MouseEnter(object sender, EventArgs e)
    {
      if (!this.bDelete.Enabled)
        return;
      this.bDelete.Image = (Image) Resources.remove_hover;
    }

    private void bDelete_MouseLeave(object sender, EventArgs e)
    {
      this.bDelete.Image = (Image) Resources.remove;
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

    private void lvDefects_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.DefectFormIsOpen)
        return;
      this.bEdit.Enabled = this.bDelete.Enabled = this.lvDefects.SelectedItems.Count == 1;
    }

    private void lvDefects_DoubleClick(object sender, EventArgs e)
    {
      if (!this.Manager.CanSend || this.lvDefects.SelectedItems.Count != 1 || !this.bEdit.Enabled)
        return;
      this.EditDefect();
    }

    private void lvDefects_KeyDown(object sender, KeyEventArgs e)
    {
      if (!this.Manager.CanSend || this.lvDefects.SelectedItems.Count != 1)
        return;
      if (e.KeyCode == Keys.Return && this.bEdit.Enabled)
      {
        this.EditDefect();
        e.Handled = true;
      }
      else
      {
        if (e.KeyCode != Keys.Delete || !this.bDelete.Enabled)
          return;
        this.DeleteDefect();
        e.Handled = true;
      }
    }

    private ListViewItem CreateListViewItem(Issue issue)
    {
      return new ListViewItem(issue.InternalId.ToString())
      {
        SubItems = {
          issue.Title,
          issue.Attr1.Name,
          issue.Attr2.Name,
          issue.Attr3.Name,
          issue.Task.Title,
          issue.Sent ? "Yes" : "No"
        },
        Tag = (object) issue
      };
    }

    private void EditDefect()
    {
      if (this.defectForm != null)
        return;
      this.bAdd.Enabled = this.bEdit.Enabled = this.bDelete.Enabled = false;
      this.defectForm = new DefectForm();
      this.defectForm.FormClosed += (FormClosedEventHandler) ((internalSender, iternalE) =>
      {
        if (this.defectForm.DialogResult == DialogResult.OK)
        {
          this.Manager.SaveIssue(this.defectForm.Issue, (object) this.lvDefects.SelectedItems[0]);
        }
        else
        {
          this.bAdd.Enabled = true;
          this.bEdit.Enabled = this.bDelete.Enabled = this.lvDefects.SelectedItems.Count == 1;
        }
        this.defectForm.Dispose();
        this.defectForm = (DefectForm) null;
      });
      this.defectForm.Issue = this.lvDefects.SelectedItems[0].Tag as Issue;
      this.defectForm.Show();
      this.defectForm.Left = this.Left + (this.Width - this.defectForm.Width) / 2;
      this.defectForm.Top = this.Top + (this.Height - this.defectForm.Height) / 2;
    }

    private void DeleteDefect()
    {
      if (MessageBox.Show(Resources.deleteDefectMessage, Resources.windowTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      this.Manager.DeleteIssue(this.lvDefects.SelectedItems[0].Tag as Issue, (object) this.lvDefects.SelectedItems[0]);
    }

    private void UpdateDefectCount()
    {
      if (!this.wideDefectStatus && Settings.ReportFile.Issues.Count > 9)
      {
        this.pbBug.Left -= 9;
        this.lDefectCount.Left -= 9;
        this.lDefectCount.Width = 26;
        this.wideDefectStatus = true;
      }
      else if (this.wideDefectStatus && Settings.ReportFile.Issues.Count < 10)
      {
        this.pbBug.Left += 9;
        this.lDefectCount.Left += 9;
        this.lDefectCount.Width = 15;
        this.wideDefectStatus = false;
      }
      this.lDefectCount.Text = Settings.ReportFile.Issues.Count.ToString();
    }

    private void lvDefects_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      ((sender as ListView).ListViewItemSorter as ListViewComparer).Sort(e.Column);
    }

    private void lvDefects_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
    {
      ListView listView = sender as ListView;
      if (listView.Columns[e.ColumnIndex].Width >= 30)
        return;
      listView.Columns[e.ColumnIndex].Width = 30;
    }

    private void lvDefects_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
    {
      if ((sender as ListView).Columns[e.ColumnIndex].Width > 30)
        return;
      e.Cancel = true;
    }

    private void DefectsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.defectForm != null)
      {
        this.defectForm.DialogResult = DialogResult.Cancel;
        this.Focus();
        int num = (int) MessageBox.Show("The competition ended. It is not possible to add and modify defects.", Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      this.Manager.ConnectionChangeStatus -= new ManagerConnectionChangeStatusEvent(this.Manager_ConnectionChangeStatus);
      this.Manager.AddIssueToFileCompleted -= new ManagerAddIssueToFileCompletedEvent(this.Manager_AddIssueToFileCompleted);
      this.Manager.SaveIssueToFileCompleted -= new ManagerSaveIssueToFileCompletedEvent(this.Manager_SaveIssueToFileCompleted);
      this.Manager.DeleteIssueFromFileCompleted -= new ManagerDeleteIssueFromFileCompletedEvent(this.Manager_DeleteIssueFromFileCompleted);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DefectsForm));
      this.window = new Panel();
      this.lvDefects = new ListView();
      this.columnHeader1 = new ColumnHeader();
      this.columnHeader2 = new ColumnHeader();
      this.columnHeader4 = new ColumnHeader();
      this.columnHeader3 = new ColumnHeader();
      this.columnHeader5 = new ColumnHeader();
      this.columnHeader6 = new ColumnHeader();
      this.sent = new ColumnHeader();
      this.pFooter = new Panel();
      this.pbBug = new PictureBox();
      this.lDefectCount = new Label();
      this.status = new PictureBox();
      this.pNavigation = new Panel();
      this.bDelete = new PictureBox();
      this.bEdit = new PictureBox();
      this.bAdd = new PictureBox();
      this.label7 = new Label();
      this.pbLogo = new PictureBox();
      this.windowBar = new Panel();
      this.bClose = new PictureBox();
      this.bMaximize = new PictureBox();
      this.bMinimize = new PictureBox();
      this.windowTitle = new Label();
      this.toolTip = new ToolTip(this.components);
      this.window.SuspendLayout();
      this.pFooter.SuspendLayout();
      ((ISupportInitialize) this.pbBug).BeginInit();
      ((ISupportInitialize) this.status).BeginInit();
      this.pNavigation.SuspendLayout();
      ((ISupportInitialize) this.bDelete).BeginInit();
      ((ISupportInitialize) this.bEdit).BeginInit();
      ((ISupportInitialize) this.bAdd).BeginInit();
      ((ISupportInitialize) this.pbLogo).BeginInit();
      this.windowBar.SuspendLayout();
      ((ISupportInitialize) this.bClose).BeginInit();
      ((ISupportInitialize) this.bMaximize).BeginInit();
      ((ISupportInitialize) this.bMinimize).BeginInit();
      this.SuspendLayout();
      this.window.BorderStyle = BorderStyle.FixedSingle;
      this.window.Controls.Add((Control) this.lvDefects);
      this.window.Controls.Add((Control) this.pFooter);
      this.window.Controls.Add((Control) this.pNavigation);
      this.window.Controls.Add((Control) this.windowBar);
      this.window.Dock = DockStyle.Fill;
      this.window.Location = new Point(0, 0);
      this.window.Name = "window";
      this.window.Size = new Size(900, 600);
      this.window.TabIndex = 0;
      this.lvDefects.BackColor = Color.White;
      this.lvDefects.BorderStyle = BorderStyle.None;
      this.lvDefects.Columns.AddRange(new ColumnHeader[7]
      {
        this.columnHeader1,
        this.columnHeader2,
        this.columnHeader4,
        this.columnHeader3,
        this.columnHeader5,
        this.columnHeader6,
        this.sent
      });
      this.lvDefects.Dock = DockStyle.Fill;
      this.lvDefects.ForeColor = Color.Black;
      this.lvDefects.FullRowSelect = true;
      this.lvDefects.HideSelection = false;
      this.lvDefects.Location = new Point(0, 108);
      this.lvDefects.MultiSelect = false;
      this.lvDefects.Name = "lvDefects";
      this.lvDefects.Size = new Size(898, 458);
      this.lvDefects.TabIndex = 19;
      this.lvDefects.UseCompatibleStateImageBehavior = false;
      this.lvDefects.View = System.Windows.Forms.View.Details;
      this.lvDefects.ColumnClick += new ColumnClickEventHandler(this.lvDefects_ColumnClick);
      this.lvDefects.ColumnWidthChanged += new ColumnWidthChangedEventHandler(this.lvDefects_ColumnWidthChanged);
      this.lvDefects.ColumnWidthChanging += new ColumnWidthChangingEventHandler(this.lvDefects_ColumnWidthChanging);
      this.lvDefects.SelectedIndexChanged += new EventHandler(this.lvDefects_SelectedIndexChanged);
      this.lvDefects.DoubleClick += new EventHandler(this.lvDefects_DoubleClick);
      this.lvDefects.KeyDown += new KeyEventHandler(this.lvDefects_KeyDown);
      this.columnHeader1.Text = "#";
      this.columnHeader1.Width = 40;
      this.columnHeader2.Text = "Title";
      this.columnHeader2.Width = 200;
      this.columnHeader4.Text = "Attr 1";
      this.columnHeader4.Width = 140;
      this.columnHeader3.Text = "Attr 2";
      this.columnHeader3.Width = 140;
      this.columnHeader5.Text = "Attr 3";
      this.columnHeader5.Width = 140;
      this.columnHeader6.Text = "Task";
      this.columnHeader6.Width = 140;
      this.sent.Name = "sent";
      this.sent.Text = "Sent";
      this.pFooter.BackColor = Color.FromArgb(23, 25, 31);
      this.pFooter.Controls.Add((Control) this.pbBug);
      this.pFooter.Controls.Add((Control) this.lDefectCount);
      this.pFooter.Controls.Add((Control) this.status);
      this.pFooter.Dock = DockStyle.Bottom;
      this.pFooter.Location = new Point(0, 566);
      this.pFooter.Name = "pFooter";
      this.pFooter.Size = new Size(898, 32);
      this.pFooter.TabIndex = 18;
      this.pbBug.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.pbBug.Image = (Image) Resources.bug;
      this.pbBug.Location = new Point(834, 9);
      this.pbBug.Name = "pbBug";
      this.pbBug.Size = new Size(16, 16);
      this.pbBug.SizeMode = PictureBoxSizeMode.AutoSize;
      this.pbBug.TabIndex = 37;
      this.pbBug.TabStop = false;
      this.lDefectCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.lDefectCount.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.lDefectCount.ForeColor = Color.FromArgb(193, 184, 201);
      this.lDefectCount.Location = new Point(851, 7);
      this.lDefectCount.Name = "lDefectCount";
      this.lDefectCount.Size = new Size(15, 18);
      this.lDefectCount.TabIndex = 36;
      this.lDefectCount.Text = "99";
      this.status.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.status.Image = (Image) Resources.status_offline;
      this.status.Location = new Point(874, 8);
      this.status.Name = "status";
      this.status.Size = new Size(16, 16);
      this.status.SizeMode = PictureBoxSizeMode.AutoSize;
      this.status.TabIndex = 35;
      this.status.TabStop = false;
      this.pNavigation.BackColor = Color.FromArgb(23, 25, 31);
      this.pNavigation.Controls.Add((Control) this.bDelete);
      this.pNavigation.Controls.Add((Control) this.bEdit);
      this.pNavigation.Controls.Add((Control) this.bAdd);
      this.pNavigation.Controls.Add((Control) this.label7);
      this.pNavigation.Controls.Add((Control) this.pbLogo);
      this.pNavigation.Dock = DockStyle.Top;
      this.pNavigation.Location = new Point(0, 32);
      this.pNavigation.Name = "pNavigation";
      this.pNavigation.Size = new Size(898, 76);
      this.pNavigation.TabIndex = 14;
      this.bDelete.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.bDelete.Enabled = false;
      this.bDelete.Image = (Image) Resources.remove;
      this.bDelete.Location = new Point(859, 25);
      this.bDelete.Name = "bDelete";
      this.bDelete.Size = new Size(25, 25);
      this.bDelete.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bDelete.TabIndex = 4;
      this.bDelete.TabStop = false;
      this.bDelete.Click += new EventHandler(this.bDelete_Click);
      this.bDelete.MouseEnter += new EventHandler(this.bDelete_MouseEnter);
      this.bDelete.MouseLeave += new EventHandler(this.bDelete_MouseLeave);
      this.bEdit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.bEdit.Enabled = false;
      this.bEdit.Image = (Image) Resources.edit;
      this.bEdit.Location = new Point(828, 25);
      this.bEdit.Name = "bEdit";
      this.bEdit.Size = new Size(25, 25);
      this.bEdit.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bEdit.TabIndex = 3;
      this.bEdit.TabStop = false;
      this.bEdit.Click += new EventHandler(this.bEdit_Click);
      this.bEdit.MouseEnter += new EventHandler(this.bEdit_MouseEnter);
      this.bEdit.MouseLeave += new EventHandler(this.bEdit_MouseLeave);
      this.bAdd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.bAdd.Image = (Image) Resources.add;
      this.bAdd.Location = new Point(797, 25);
      this.bAdd.Name = "bAdd";
      this.bAdd.Size = new Size(25, 25);
      this.bAdd.SizeMode = PictureBoxSizeMode.AutoSize;
      this.bAdd.TabIndex = 2;
      this.bAdd.TabStop = false;
      this.bAdd.Click += new EventHandler(this.bAdd_Click);
      this.bAdd.MouseEnter += new EventHandler(this.bAdd_MouseEnter);
      this.bAdd.MouseLeave += new EventHandler(this.bAdd_MouseLeave);
      this.label7.AutoSize = true;
      this.label7.Font = new Font("Arial", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.label7.ForeColor = Color.FromArgb(193, 184, 201);
      this.label7.Location = new Point(98, 28);
      this.label7.Name = "label7";
      this.label7.Size = new Size(80, 22);
      this.label7.TabIndex = 1;
      this.label7.Text = "Defects";
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
      this.windowBar.Size = new Size(898, 32);
      this.windowBar.TabIndex = 13;
      this.windowBar.DoubleClick += new EventHandler(this.bMaximize_Click);
      this.windowBar.MouseDown += new MouseEventHandler(this.windowBar_MouseDown);
      this.windowBar.MouseMove += new MouseEventHandler(this.windowBar_MouseMove);
      this.windowBar.MouseUp += new MouseEventHandler(this.windowBar_MouseUp);
      this.bClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.bClose.Image = (Image) Resources.close;
      this.bClose.Location = new Point(872, 5);
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
      this.bMaximize.Location = new Point(845, 5);
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
      this.bMinimize.Location = new Point(818, 5);
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
      this.windowTitle.DoubleClick += new EventHandler(this.bMaximize_Click);
      this.windowTitle.MouseDown += new MouseEventHandler(this.windowBar_MouseDown);
      this.windowTitle.MouseMove += new MouseEventHandler(this.windowBar_MouseMove);
      this.windowTitle.MouseUp += new MouseEventHandler(this.windowBar_MouseUp);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(35, 39, 48);
      this.ClientSize = new Size(900, 600);
      this.Controls.Add((Control) this.window);
      this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Margin = new Padding(4);
      this.MinimumSize = new Size(900, 600);
      this.Name = nameof (DefectsForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Defects - MrBuggy 6";
      this.FormClosing += new FormClosingEventHandler(this.DefectsForm_FormClosing);
      this.window.ResumeLayout(false);
      this.pFooter.ResumeLayout(false);
      this.pFooter.PerformLayout();
      ((ISupportInitialize) this.pbBug).EndInit();
      ((ISupportInitialize) this.status).EndInit();
      this.pNavigation.ResumeLayout(false);
      this.pNavigation.PerformLayout();
      ((ISupportInitialize) this.bDelete).EndInit();
      ((ISupportInitialize) this.bEdit).EndInit();
      ((ISupportInitialize) this.bAdd).EndInit();
      ((ISupportInitialize) this.pbLogo).EndInit();
      this.windowBar.ResumeLayout(false);
      this.windowBar.PerformLayout();
      ((ISupportInitialize) this.bClose).EndInit();
      ((ISupportInitialize) this.bMaximize).EndInit();
      ((ISupportInitialize) this.bMinimize).EndInit();
      this.ResumeLayout(false);
    }
  }
}
