using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class frmFileManager : Form
    {
        private Station s;
        private MyConnectionTester p;
        public frmFileManager(Station s)
        {
            InitializeComponent();
            this.s = s;

            this.p = new MyConnectionTester(new SocketCommander(s.PCName, s.Port));
            this.p.Start();

            this.Text = s.PCName;
        }

        private void frmFileManager_Shown(object sender, EventArgs e)
        {
            UpdateLocalDrives();
            UpdateRemoteDrives();
        }

        private void UpdateRemoteDrives()
        {
            List<DriveInfo> drives = new List<DriveInfo>();
            toolStripComboBox1.Items.Clear();

            if (new RemoteFileManager().GetDrives(this.s, ref drives))
            {
                for (int i = 0; i < drives.Count; i++)
                {
                    toolStripComboBox1.Items.Add(drives[i].RootDirectory);
                }

                toolStripComboBox1.SelectedIndex = 0;
            }
        }

        private void UpdateRemoteList(string path)
        {

            listView1.Items.Clear();
            listView1.Items.Add(new ListViewItem("..."));

            List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            if (new RemoteFileManager().GetDir(this.s, path, ref dirs))
            {
                List<ListViewItem> l = new List<ListViewItem>();
                for (int i = 0; i < dirs.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem(dirs[i].Name);
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("");

                    l.Add(lvi);
                }

                listView1.Items.AddRange(l.ToArray());
            }

            List<FileInfo> files = new List<FileInfo>();
            if (new RemoteFileManager().GetFiles(this.s, path, ref files))
            {
                List<ListViewItem> l = new List<ListViewItem>();
                for (int i = 0; i < files.Count(); i++)
                {
                    ListViewItem lvi = new ListViewItem(files[i].Name);
                    lvi.SubItems.Add(files[i].Length.ToString());
                    lvi.SubItems.Add(files[i].Extension.ToUpper());
                    lvi.SubItems.Add(files[i].LastAccessTime.ToString());

                    l.Add(lvi);
                }
                listView1.Items.AddRange(l.ToArray());
            }
        }


        private void UpdateLocalList(string path)
        {

            listView4.Items.Clear();
            listView4.Items.Add(new ListViewItem("..."));

            DirectoryInfo[] dirs = new FileManager().GetDir(path);
            if (!(dirs is null))
            {
                List<ListViewItem> l = new List<ListViewItem>();
                for (int i = 0; i < dirs.Count(); i++)
                {
                    ListViewItem lvi = new ListViewItem(dirs[i].Name);
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("");
                    lvi.Tag = "D";

                    l.Add(lvi);
                }

                listView4.Items.AddRange(l.ToArray());
            }

            FileInfo[] files = (new FileManager().GetFiles(path));
            if (!(files is null))
            {
                List<ListViewItem> l = new List<ListViewItem>();
                for (int i = 0; i < files.Count(); i++)
                {
                    ListViewItem lvi = new ListViewItem(files[i].Name);
                    lvi.SubItems.Add(files[i].Length.ToString());
                    lvi.SubItems.Add(files[i].Extension.ToUpper());
                    lvi.SubItems.Add(files[i].LastAccessTime.ToString());

                    l.Add(lvi);
                }
                listView4.Items.AddRange(l.ToArray());
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {

            }
        }

        private void UpdateLocalDrives()
        {
            DriveInfo[] res = (new FileManager().GetDrives());

            toolStripComboBox2.Items.Clear();

            if (!(res is null))
            {
                if (res.Count() > 0)
                {
                    for (int i = 0; i < res.Count(); i++)
                    {
                        toolStripComboBox2.Items.Add(res[i].ToString());
                    }

                    toolStripComboBox2.SelectedIndex = 0;
                }
            }

        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel6.Text = toolStripComboBox2.Text;
        }

        private void toolStripStatusLabel6_TextChanged(object sender, EventArgs e)
        {
            UpdateLocalList(toolStripStatusLabel6.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.p.Ping == 0)
            {
                toolStripStatusLabel2.Text = " < 1ms";
            }
            else if (this.p.Ping >= 1)
            {
                toolStripStatusLabel2.Text = this.p.Ping.ToString() + " ms";
            }
            else
            {
                toolStripStatusLabel2.Text = "OPPS, TIMEOUT :(";
            }
        }

        private void frmFileManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.p.Stop();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = toolStripComboBox1.Text;
        }

        private void toolStripStatusLabel3_TextChanged(object sender, EventArgs e)
        {
            UpdateRemoteList(toolStripStatusLabel3.Text);
        }

        private void listView4_DoubleClick(object sender, EventArgs e)
        {
            if (listView4.Items.Count > 0)
            {
                if (listView4.SelectedItems.Count > 0)
                {
                    int index = listView4.SelectedItems[0].Index;

                    if (index == 0)
                    {
                        toolStripStatusLabel6.Text = GetLastPath();
                    }
                    else
                    {
                        toolStripStatusLabel6.Text += "\\" + listView4.Items[index].Text;
                    }
                }
            }
        }

        private string GetLastPath()
        {
            string res = toolStripStatusLabel6.Text;

            if(toolStripStatusLabel6.Text.Length > 3)
            {
                int index = toolStripStatusLabel6.Text.LastIndexOf("\\");
                if (index >= 3)
                {
                    res = toolStripStatusLabel6.Text.Substring(0, index);
                }
            }

            return res;
        }

        private void listView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    int index = listView1.SelectedItems[0].Index;

                    if (index == 0)
                    {
                        toolStripStatusLabel3.Text = GetLastPath();
                    }
                    else
                    {
                        toolStripStatusLabel3.Text += "\\" + listView1.Items[index].Text;
                    }
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel6.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel6.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SocketCommander sc = new SocketCommander(this.s.PCName, this.s.Port);
            byte[] buffer = new byte[] { 0 };
            if(sc.GetRemoteObject(new PacketLib.Packet(7),ref buffer))
            {
                toolStripStatusLabel3.Text = Encoding.UTF8.GetString(buffer);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SocketCommander sc = new SocketCommander(this.s.PCName, this.s.Port);
            byte[] buffer = new byte[] { 0 };
            if (sc.GetRemoteObject(new PacketLib.Packet(8), ref buffer))
            {
                toolStripStatusLabel3.Text = Encoding.UTF8.GetString(buffer);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            UpdateRemoteDrives();
        }
    }
}