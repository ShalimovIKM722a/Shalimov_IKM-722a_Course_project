using System;

namespace Shalimov_IKM_722a_Course_project
{
    class MajorWork
    {
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
    }
}
