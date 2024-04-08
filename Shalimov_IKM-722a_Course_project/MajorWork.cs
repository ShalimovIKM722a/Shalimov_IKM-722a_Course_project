using System;
using System.Windows.Forms;

namespace Shalimov_IKM_722a_Course_project
{
    class MajorWork
    {
        private DateTime TimeBegin;
        private string Data;
        private string Result;
        public void Write(string D)
        {
            this.Data = D;
        }
        public string Read()
        {
            return this.Result;
        }
        public void Task() {
            if (this.Data.Length > 5) { 
            this.Result=Convert.ToString(true);
            }
            else
            {
                this.Result=Convert.ToString(false);
            }
        }

        public void SetTime()
        {
            this.TimeBegin = DateTime.Now;
        }

        public DateTime GetTime()
        {
            return this.TimeBegin;
        }
    }
}
