using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardHistory.App
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.NotifyIcon taskbarIcon = new NotifyIcon();

        public Form1()
        {
            InitializeComponent();
            _clipboardRegister = new ClipboardRegister(this.Handle);
            _clipboardRegister.OnClipboardCopy += _clipboardRegister_ClipboardCopy;

            //new System.Resources.ResourceReader()

            this.taskbarIcon.Icon = new Icon("Resources/notifyIcon.ico");
            taskbarIcon.DoubleClick += TaskbarIcon_DoubleClick;
        }

        private void TaskbarIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            taskbarIcon.Visible = false;
        }

        private void _clipboardRegister_ClipboardCopy(object sender, ClipboardCopyEventArgs e)
        {
            lstClipboard.Items.Add(e.Text);
            
        }

        ClipboardRegister _clipboardRegister;
        private void Form1_Load(object sender, EventArgs e)
        {
            _clipboardRegister.RegisterClipboardViewer();

        }

        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }


        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            switch ((RAD.ClipMon.Win32.Msgs)m.Msg)
            {
                //
                // The WM_DRAWCLIPBOARD message is sent to the first window 
                // in the clipboard viewer chain when the content of the 
                // clipboard changes. This enables a clipboard viewer 
                // window to display the new content of the clipboard. 
                //
                case RAD.ClipMon.Win32.Msgs.WM_DRAWCLIPBOARD:

                    Debug.WriteLine("WindowProc DRAWCLIPBOARD: " + m.Msg, "WndProc");

                    _clipboardRegister.GetClipboardData();

                    //
                    // Each window that receives the WM_DRAWCLIPBOARD message 
                    // must call the SendMessage function to pass the message 
                    // on to the next window in the clipboard viewer chain.
                    //
                    RAD.ClipMon.Win32.User32.SendMessage(_clipboardRegister.ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    break;


                //
                // The WM_CHANGECBCHAIN message is sent to the first window 
                // in the clipboard viewer chain when a window is being 
                // removed from the chain. 
                //
                case RAD.ClipMon.Win32.Msgs.WM_CHANGECBCHAIN:
                    Debug.WriteLine("WM_CHANGECBCHAIN: lParam: " + m.LParam, "WndProc");

                    // When a clipboard viewer window receives the WM_CHANGECBCHAIN message, 
                    // it should call the SendMessage function to pass the message to the 
                    // next window in the chain, unless the next window is the window 
                    // being removed. In this case, the clipboard viewer should save 
                    // the handle specified by the lParam parameter as the next window in the chain. 

                    //
                    // wParam is the Handle to the window being removed from 
                    // the clipboard viewer chain 
                    // lParam is the Handle to the next window in the chain 
                    // following the window being removed. 
                    if (m.WParam == _clipboardRegister.ClipboardViewerNext)
                    {
                        //
                        // If wParam is the next clipboard viewer then it
                        // is being removed so update pointer to the next
                        // window in the clipboard chain
                        //
                        _clipboardRegister.ClipboardViewerNext = m.LParam;
                    }
                    else
                    {
                        RAD.ClipMon.Win32.User32.SendMessage(_clipboardRegister.ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    }
                    break;

                default:
                    //
                    // Let the form process the messages that we are
                    // not interested in
                    //
                    base.WndProc(ref m);
                    break;

            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            
            if (FormWindowState.Minimized == this.WindowState)
            {
                //taskbarIcon.BalloonTipText = "App has been minimized";
                //taskbarIcon.BalloonTipTitle = "App has been minimized";
                taskbarIcon.Visible = true;
                taskbarIcon.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                taskbarIcon.Visible = false;
            }
        }

        private void lstClipboard_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lstClipboard.Text))
                _clipboardRegister.Copy(lstClipboard.Text);
        }
    }
}
