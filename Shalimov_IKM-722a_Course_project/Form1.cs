using System;
using System.Windows.Forms;

namespace Shalimov_IKM_722a_Course_project
{
    public partial class Form1 : Form
    {
        private DateTime TimeBegin;
        private bool Mode;
        private MajorWork MajorObject;


        public Form1()
        {
            InitializeComponent();
            
        }

        private void tClock_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Минуло 15 секунд", "Увага!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MajorObject = new MajorWork();
            MajorObject.SetTime();
            MajorObject.Modify = false;
            var A = new About();
            A.tAbout.Start();
            A.ShowDialog();
            this.Mode = true;
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if (Mode)
            {
                tClock.Start();
                bStart.Text = "Стоп";
                this.Mode = false;
                tbInput.Enabled = true;
                tbInput.Focus();
                MajorObject.Write(tbInput.Text);
                MajorObject.Task();
                label1.Text = MajorObject.Read();
                пускToolStripMenuItem.Text = "Стоп";
            }
            else
            {
                tClock.Stop();
                bStart.Text = "Пуск";
                this.Mode = true;
                MajorObject.Write(tbInput.Text);
                MajorObject.Task();
                label1.Text = MajorObject.Read();
                пускToolStripMenuItem.Text = "Старт";
            }
        }

        private void tbInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            tClock.Stop();
            tClock.Start();

            if ((e.KeyChar >= '0') & (e.KeyChar <= '9') | (e.KeyChar == (char)8))
            {
                return;
            }
            else
            {
                tClock.Stop() ;
                MessageBox.Show("Неправильнинй символ","Помилка");
                tClock.Start();
                e.KeyChar = (char)0;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string s;
            s = (DateTime.Now - MajorObject.GetTime()).ToString();
            MessageBox.Show(s,"Час роботи програми");
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void проПрограмиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About A = new About();
            A.ShowDialog();
        }

        private void зберегтиЯкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdSave.ShowDialog() == DialogResult.OK)
            {
                MajorObject.WriteSaveFileName(sfdSave.FileName);
                MajorObject.SaveToFile();
            }
        }

        private void відкритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if(ofdOpen.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(ofdOpen.FileName);
            }
        }

        private void проНакопичувачиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] disks = System.IO.Directory.GetLogicalDrives();
            string disk = "";
            for(int i = 0; i < disks.Length; i++)
            {
                try {
                    var D = new System.IO.DriveInfo(disks[i]);
                    disk += D.Name + "-" + D.TotalSize.ToString() + "-"
                        + D.TotalFreeSpace.ToString()+ (char)13;
                }
                catch { 
                disk += disks[i] + "- не готовий" + (char)13;
                }
            }
            MessageBox.Show(disk,"Про накопичувачи");
        }
    }
}
