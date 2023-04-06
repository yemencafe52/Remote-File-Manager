namespace ClientUI
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    public partial class frmMain : Form
    {
        private List<Station> stations = new List<Station>();
        public frmMain()
        {
            InitializeComponent();
            Preparing();
        }

        private bool Preparing()
        {
            bool res = false;
            stations.Add(new Station("Pc01",1234,"admin","admin"));
            stations.Add(new Station("Pc02", 1234, "admin", "admin"));
            stations.Add(new Station("Pc03", 1234, "admin", "admin"));

            PrintStations();
            return res;
        }

        private void PrintStations()
        {
            listView1.Items.Clear();

            for(int i =0;i<stations.Count;i++)
            {
                ListViewItem lvi = new ListViewItem(i.ToString());
                lvi.SubItems.Add(stations[i].PCName);
                listView1.Items.Add(lvi);
            }

            toolStripStatusLabel2.Text = listView1.Items.Count.ToString();
        }//frmFileManager(new Station("127.0.0.1",1234,"",""))

        private void listView1_DoubleClick(object sender, System.EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    int index = listView1.SelectedItems[0].Index;
                    frmFileManager fFM = new frmFileManager(stations[index]);
                    fFM.Show();
                }
            }
        }
    }
}
