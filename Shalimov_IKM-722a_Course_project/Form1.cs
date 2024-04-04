using System;
using System.Windows.Forms;

namespace Shalimov_IKM_722a_Course_project
{
    public partial class Form1 : Form
    {
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
            var A = new About();
            A.tAbout.Start();
            A.ShowDialog();
            MajorObject = new MajorWork();
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
            }
            else
            {
                tClock.Stop();
                bStart.Text = "Пуск";
                this.Mode = true;
                MajorObject.Write(tbInput.Text);
                MajorObject.Task();
                label1.Text = MajorObject.Read();
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
    }
}
