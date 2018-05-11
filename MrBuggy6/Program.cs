// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Program
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using Microsoft.Win32;
using MrBuggy6.Properties;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MrBuggy6
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      try
      {
        bool createdNew;
        using (Mutex mutex = new Mutex(true, "MrBuggy6_AF47QP08", out createdNew))
        {
          if (createdNew)
          {
            Application.ThreadException += new ThreadExceptionEventHandler(Program.UIThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program.SetupBrowser();
            int num = (int) new LoginForm().ShowDialog();
            Application.Run((Form) new MainForm());
            mutex.ReleaseMutex();
          }
          else
          {
            int num1 = (int) MessageBox.Show("The program is already running.", Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          }
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        File.AppendAllText("log.txt", ex.Message + "\r\n" + ex.StackTrace + "\r\n\r\n");
      }
      finally
      {
        Application.Exit();
      }
    }

    public static void UIThreadException(object sender, ThreadExceptionEventArgs t)
    {
      try
      {
        int num = (int) MessageBox.Show(t.Exception.Message, Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        File.AppendAllText("log.txt", t.Exception.Message + "\r\n" + t.Exception.StackTrace + "\r\n\r\n");
      }
      catch
      {
        try
        {
          int num = (int) MessageBox.Show("Windows Forms Fatal error", Resources.windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        finally
        {
          Application.Exit();
        }
      }
      Application.Exit();
    }

    private static void SetupBrowser()
    {
      int num = new WebBrowser().Version.Major * 1000;
      string fileName = Path.GetFileName(Application.ExecutablePath);
      RegistryKey registryKey1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
      if (registryKey1 != null)
        registryKey1.SetValue(fileName, (object) num, RegistryValueKind.DWord);
      RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
      if (registryKey2 != null)
        registryKey2.SetValue(fileName, (object) num, RegistryValueKind.DWord);
      RegistryKey registryKey3 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
      if (registryKey3 == null)
        return;
      registryKey3.SetValue(fileName, (object) num, RegistryValueKind.DWord);
    }
  }
}
