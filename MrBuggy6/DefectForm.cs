// Decompiled with JetBrains decompiler
// Type: MrBuggy6.DefectForm
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
  public class DefectForm : Form
  {
    private bool windowBarMove;
    private Point windowBarFirstPoint;
    private IContainer components;
    private Panel window;
    private Label label6;
    private ComboBox cbAttr1;
    private Label lDescriptionCharactersCount;
    private ComboBox cbTask;
    private Label label5;
    private ComboBox cbAttr3;
    private Label label4;
    private ComboBox cbAttr2;
    private Label label3;
    private TextBox tbDescription;
    private Label label2;
    private TextBox tbTitle;
    private Label label1;
    private Label bCancel;
    private Label bSave;
    private Panel pNavigation;
    private Label label10;
    private PictureBox pbLogo;
    private Panel windowBar;
    private PictureBox bClose;
    private Label windowTitle;

    public Issue Issue { get; set; }

    public DefectForm()
    {
      this.InitializeComponent();
      this.windowTitle.Text = Resources.windowTitle;
      this.ActiveControl = (Control) this.bSave;
      foreach (IssueAttr1 attr1 in Settings.Attr1List)
        this.cbAttr1.Items.Add((object) attr1.Name);
      foreach (IssueAttr2 attr2 in Settings.Attr2List)
        this.cbAttr2.Items.Add((object) attr2.Name);
      foreach (IssueAttr3 attr3 in Settings.Attr3List)
        this.cbAttr3.Items.Add((object) attr3.Name);
      foreach (IssueTask task in Settings.Tasks)
        this.cbTask.Items.Add((object) task.Title);
      this.Issue = (Issue) null;
      this.lDescriptionCharactersCount.Text = Resources.numberOfCharacters + ": " + (object) this.tbDescription.Text.Length + "/" + (object) this.tbDescription.MaxLength;
      this.tbTitle.ContextMenu = new ContextMenu();
      this.tbDescription.ContextMenu = new ContextMenu();
    }

    private void DefectForm_Load(object sender, EventArgs e)
    {
      if (this.Issue == null)
        return;
      this.tbTitle.Text = this.Issue.Title;
      this.tbDescription.Text = this.Issue.Description;
      this.cbAttr1.SelectedIndex = Settings.Attr1List.FindIndex((Predicate<IssueAttr1>) (x => (int) x.Id == (int) this.Issue.Attr1.Id));
      this.cbAttr2.SelectedIndex = Settings.Attr2List.FindIndex((Predicate<IssueAttr2>) (x => (int) x.Id == (int) this.Issue.Attr2.Id));
      this.cbAttr3.SelectedIndex = Settings.Attr3List.FindIndex((Predicate<IssueAttr3>) (x => (int) x.Id == (int) this.Issue.Attr3.Id));
      this.cbTask.SelectedIndex = Settings.Tasks.FindIndex((Predicate<IssueTask>) (x => x.Id == this.Issue.Task.Id));
    }

    private void Save()
    {
      this.tbTitle.Text = this.tbTitle.Text.Trim();
      this.tbDescription.Text = this.tbDescription.Text.Trim();
      if (this.tbTitle.Text.Length == 0)
      {
        int num = (int) MessageBox.Show(string.Format(Resources.requierdField, (object) "Title"), Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.tbTitle.Focus();
        this.tbTitle.SelectAll();
      }
      else if (this.tbDescription.Text.Length == 0)
      {
        int num = (int) MessageBox.Show(string.Format(Resources.requierdField, (object) "Description"), Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.tbDescription.Focus();
        this.tbDescription.SelectAll();
      }
      else if (this.cbAttr1.SelectedIndex < 0)
      {
        int num = (int) MessageBox.Show(string.Format(Resources.requierdField, (object) "Attr 1"), Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.cbAttr1.Focus();
        this.cbAttr1.DroppedDown = true;
      }
      else if (this.cbAttr2.SelectedIndex < 0)
      {
        int num = (int) MessageBox.Show(string.Format(Resources.requierdField, (object) "Attr 2"), Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.cbAttr2.Focus();
        this.cbAttr2.DroppedDown = true;
      }
      else if (this.cbAttr3.SelectedIndex < 0)
      {
        int num = (int) MessageBox.Show(string.Format(Resources.requierdField, (object) "Attr 3"), Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.cbAttr3.Focus();
        this.cbAttr3.DroppedDown = true;
      }
      else if (this.cbTask.SelectedIndex < 0)
      {
        int num = (int) MessageBox.Show(string.Format(Resources.requierdField, (object) "Task"), Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.cbTask.Focus();
        this.cbTask.DroppedDown = true;
      }
      else
      {
        if (this.Issue == null)
        {
          this.Issue = new Issue();
          this.Issue.InternalId = Issue.NextId++;
          this.FillData();
          this.DialogResult = DialogResult.OK;
        }
        else if (this.Issue.Title == this.tbTitle.Text && this.Issue.Description == this.tbDescription.Text && ((int) this.Issue.Attr1.Id == (int) Settings.Attr1List[this.cbAttr1.SelectedIndex].Id && (int) this.Issue.Attr2.Id == (int) Settings.Attr2List[this.cbAttr2.SelectedIndex].Id) && ((int) this.Issue.Attr3.Id == (int) Settings.Attr3List[this.cbAttr3.SelectedIndex].Id && this.Issue.Task.Id == Settings.Tasks[this.cbTask.SelectedIndex].Id))
        {
          this.DialogResult = DialogResult.Cancel;
        }
        else
        {
          uint internalId = this.Issue.InternalId;
          this.Issue = new Issue();
          this.Issue.InternalId = internalId;
          this.FillData();
          this.DialogResult = DialogResult.OK;
        }
        this.Close();
      }
    }

    private void bSave_Click(object sender, EventArgs e)
    {
      this.Save();
    }

    private void FillData()
    {
      this.Issue.Sent = false;
      this.Issue.Title = this.tbTitle.Text;
      this.Issue.Description = this.tbDescription.Text;
      this.Issue.Attr1 = Settings.Attr1List[this.cbAttr1.SelectedIndex];
      this.Issue.Attr2 = Settings.Attr2List[this.cbAttr2.SelectedIndex];
      this.Issue.Attr3 = Settings.Attr3List[this.cbAttr3.SelectedIndex];
      this.Issue.Task = Settings.Tasks[this.cbTask.SelectedIndex];
    }

    private void bCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void tbDescription_TextChanged(object sender, EventArgs e)
    {
      this.lDescriptionCharactersCount.Text = Resources.numberOfCharacters + ": " + (object) this.tbDescription.Text.Length + "/" + (object) this.tbDescription.MaxLength;
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

    private void bClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void Text_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      e.Handled = true;
      this.Save();
    }

    private void tbTitle_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Return)
      {
        e.Handled = true;
        this.Save();
      }
      else
      {
        if (!e.Control || e.Alt || e.KeyCode != Keys.A)
          return;
        (sender as TextBox).SelectAll();
      }
    }

    private void tbDescription_KeyDown(object sender, KeyEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DefectForm));
      this.window = new Panel();
      this.label6 = new Label();
      this.cbAttr1 = new ComboBox();
      this.lDescriptionCharactersCount = new Label();
      this.cbTask = new ComboBox();
      this.label5 = new Label();
      this.cbAttr3 = new ComboBox();
      this.label4 = new Label();
      this.cbAttr2 = new ComboBox();
      this.label3 = new Label();
      this.tbDescription = new TextBox();
      this.label2 = new Label();
      this.tbTitle = new TextBox();
      this.label1 = new Label();
      this.bCancel = new Label();
      this.bSave = new Label();
      this.pNavigation = new Panel();
      this.label10 = new Label();
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
      this.window.Controls.Add((Control) this.label6);
      this.window.Controls.Add((Control) this.cbAttr1);
      this.window.Controls.Add((Control) this.lDescriptionCharactersCount);
      this.window.Controls.Add((Control) this.cbTask);
      this.window.Controls.Add((Control) this.label5);
      this.window.Controls.Add((Control) this.cbAttr3);
      this.window.Controls.Add((Control) this.label4);
      this.window.Controls.Add((Control) this.cbAttr2);
      this.window.Controls.Add((Control) this.label3);
      this.window.Controls.Add((Control) this.tbDescription);
      this.window.Controls.Add((Control) this.label2);
      this.window.Controls.Add((Control) this.tbTitle);
      this.window.Controls.Add((Control) this.label1);
      this.window.Controls.Add((Control) this.bCancel);
      this.window.Controls.Add((Control) this.bSave);
      this.window.Controls.Add((Control) this.pNavigation);
      this.window.Controls.Add((Control) this.windowBar);
      this.window.Dock = DockStyle.Fill;
      this.window.Location = new Point(0, 0);
      this.window.Name = "window";
      this.window.Size = new Size(400, 592);
      this.window.TabIndex = 0;
      this.label6.AutoSize = true;
      this.label6.ForeColor = Color.FromArgb(193, 184, 201);
      this.label6.Location = new Point(21, 316);
      this.label6.Name = "label6";
      this.label6.Size = new Size(45, 18);
      this.label6.TabIndex = 78;
      this.label6.Text = "Attr 1";
      this.cbAttr1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cbAttr1.BackColor = Color.White;
      this.cbAttr1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbAttr1.ForeColor = Color.Black;
      this.cbAttr1.FormattingEnabled = true;
      this.cbAttr1.Location = new Point(21, 337);
      this.cbAttr1.Name = "cbAttr1";
      this.cbAttr1.Size = new Size(351, 26);
      this.cbAttr1.TabIndex = 65;
      this.cbAttr1.KeyDown += new KeyEventHandler(this.Text_KeyDown);
      this.lDescriptionCharactersCount.AutoSize = true;
      this.lDescriptionCharactersCount.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.lDescriptionCharactersCount.ForeColor = Color.FromArgb(193, 184, 201);
      this.lDescriptionCharactersCount.Location = new Point(21, 302);
      this.lDescriptionCharactersCount.Name = "lDescriptionCharactersCount";
      this.lDescriptionCharactersCount.Size = new Size(134, 14);
      this.lDescriptionCharactersCount.TabIndex = 77;
      this.lDescriptionCharactersCount.Text = "Number of characters: x/x";
      this.cbTask.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cbTask.BackColor = Color.White;
      this.cbTask.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbTask.ForeColor = Color.Black;
      this.cbTask.FormattingEnabled = true;
      this.cbTask.Location = new Point(21, 487);
      this.cbTask.Name = "cbTask";
      this.cbTask.Size = new Size(351, 26);
      this.cbTask.TabIndex = 68;
      this.cbTask.KeyDown += new KeyEventHandler(this.Text_KeyDown);
      this.label5.AutoSize = true;
      this.label5.ForeColor = Color.FromArgb(193, 184, 201);
      this.label5.Location = new Point(21, 466);
      this.label5.Name = "label5";
      this.label5.Size = new Size(40, 18);
      this.label5.TabIndex = 76;
      this.label5.Text = "Task";
      this.cbAttr3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cbAttr3.BackColor = Color.White;
      this.cbAttr3.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbAttr3.ForeColor = Color.Black;
      this.cbAttr3.FormattingEnabled = true;
      this.cbAttr3.Location = new Point(21, 437);
      this.cbAttr3.Name = "cbAttr3";
      this.cbAttr3.Size = new Size(351, 26);
      this.cbAttr3.TabIndex = 67;
      this.cbAttr3.KeyDown += new KeyEventHandler(this.Text_KeyDown);
      this.label4.AutoSize = true;
      this.label4.ForeColor = Color.FromArgb(193, 184, 201);
      this.label4.Location = new Point(21, 416);
      this.label4.Name = "label4";
      this.label4.Size = new Size(45, 18);
      this.label4.TabIndex = 75;
      this.label4.Text = "Attr 3";
      this.cbAttr2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cbAttr2.BackColor = Color.White;
      this.cbAttr2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbAttr2.ForeColor = Color.Black;
      this.cbAttr2.FormattingEnabled = true;
      this.cbAttr2.Location = new Point(21, 387);
      this.cbAttr2.Name = "cbAttr2";
      this.cbAttr2.Size = new Size(351, 26);
      this.cbAttr2.TabIndex = 66;
      this.cbAttr2.KeyDown += new KeyEventHandler(this.Text_KeyDown);
      this.label3.AutoSize = true;
      this.label3.ForeColor = Color.FromArgb(193, 184, 201);
      this.label3.Location = new Point(21, 366);
      this.label3.Name = "label3";
      this.label3.Size = new Size(45, 18);
      this.label3.TabIndex = 74;
      this.label3.Text = "Attr 2";
      this.tbDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tbDescription.BackColor = Color.White;
      this.tbDescription.BorderStyle = BorderStyle.FixedSingle;
      this.tbDescription.ForeColor = Color.Black;
      this.tbDescription.Location = new Point(21, 194);
      this.tbDescription.MaxLength = 500;
      this.tbDescription.Multiline = true;
      this.tbDescription.Name = "tbDescription";
      this.tbDescription.Size = new Size(351, 105);
      this.tbDescription.TabIndex = 64;
      this.tbDescription.TextChanged += new EventHandler(this.tbDescription_TextChanged);
      this.tbDescription.KeyDown += new KeyEventHandler(this.tbDescription_KeyDown);
      this.label2.AutoSize = true;
      this.label2.ForeColor = Color.FromArgb(193, 184, 201);
      this.label2.Location = new Point(21, 173);
      this.label2.Name = "label2";
      this.label2.Size = new Size(88, 18);
      this.label2.TabIndex = 73;
      this.label2.Text = "Description";
      this.tbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tbTitle.BackColor = Color.White;
      this.tbTitle.BorderStyle = BorderStyle.FixedSingle;
      this.tbTitle.ForeColor = Color.Black;
      this.tbTitle.Location = new Point(21, 144);
      this.tbTitle.MaxLength = 50;
      this.tbTitle.Name = "tbTitle";
      this.tbTitle.Size = new Size(351, 26);
      this.tbTitle.TabIndex = 63;
      this.tbTitle.KeyDown += new KeyEventHandler(this.tbTitle_KeyDown);
      this.label1.AutoSize = true;
      this.label1.ForeColor = Color.FromArgb(193, 184, 201);
      this.label1.Location = new Point(21, 123);
      this.label1.Name = "label1";
      this.label1.Size = new Size(36, 18);
      this.label1.TabIndex = 72;
      this.label1.Text = "Title";
      this.bCancel.BackColor = Color.FromArgb(36, 53, 93);
      this.bCancel.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bCancel.ForeColor = Color.FromArgb(193, 184, 201);
      this.bCancel.Location = new Point(21, 531);
      this.bCancel.Name = "bCancel";
      this.bCancel.Size = new Size(150, 40);
      this.bCancel.TabIndex = 71;
      this.bCancel.Text = "Cancel";
      this.bCancel.TextAlign = ContentAlignment.MiddleCenter;
      this.bCancel.Click += new EventHandler(this.bCancel_Click);
      this.bCancel.MouseEnter += new EventHandler(this.blueButton_MouseEnter);
      this.bCancel.MouseLeave += new EventHandler(this.blueButton_MouseLeave);
      this.bSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.bSave.BackColor = Color.FromArgb(36, 201, 133);
      this.bSave.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.bSave.ForeColor = Color.White;
      this.bSave.Location = new Point(222, 531);
      this.bSave.Name = "bSave";
      this.bSave.Size = new Size(150, 40);
      this.bSave.TabIndex = 70;
      this.bSave.Text = "Save";
      this.bSave.TextAlign = ContentAlignment.MiddleCenter;
      this.bSave.Click += new EventHandler(this.bSave_Click);
      this.bSave.MouseEnter += new EventHandler(this.greenButton_MouseEnter);
      this.bSave.MouseLeave += new EventHandler(this.greenButton_MouseLeave);
      this.pNavigation.BackColor = Color.FromArgb(23, 25, 31);
      this.pNavigation.Controls.Add((Control) this.label10);
      this.pNavigation.Controls.Add((Control) this.pbLogo);
      this.pNavigation.Dock = DockStyle.Top;
      this.pNavigation.Location = new Point(0, 32);
      this.pNavigation.Name = "pNavigation";
      this.pNavigation.Size = new Size(398, 76);
      this.pNavigation.TabIndex = 69;
      this.label10.AutoSize = true;
      this.label10.Font = new Font("Arial", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.label10.ForeColor = Color.FromArgb(193, 184, 201);
      this.label10.Location = new Point(98, 28);
      this.label10.Name = "label10";
      this.label10.Size = new Size(69, 22);
      this.label10.TabIndex = 1;
      this.label10.Text = "Defect";
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
      this.windowBar.TabIndex = 79;
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
      this.windowTitle.MouseDown += new MouseEventHandler(this.windowBar_MouseDown);
      this.windowTitle.MouseMove += new MouseEventHandler(this.windowBar_MouseMove);
      this.windowTitle.MouseUp += new MouseEventHandler(this.windowBar_MouseUp);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(35, 39, 48);
      this.ClientSize = new Size(400, 592);
      this.Controls.Add((Control) this.window);
      this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Margin = new Padding(4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (DefectForm);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "MrBuggy 6 - Defect";
      this.Load += new EventHandler(this.DefectForm_Load);
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
