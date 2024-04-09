﻿using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Shalimov_IKM_722a_Course_project
{
    class MajorWork
    {
        private DateTime TimeBegin;
        private string Data;
        private string Result;
        public bool Modify;
        private int Key;
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
            this.Modify = true; 
        }

        public void SetTime()
        {
            this.TimeBegin = DateTime.Now;
        }

        public DateTime GetTime()
        {
            return this.TimeBegin;
        }

        private string SaveFileName;
        private string OpenFileName;
        public void WriteSaveFileName(string S)
        {
            this.SaveFileName = S;
        }
        public void WriteOpenFileName(string S)
        {
            this.OpenFileName = S;
        }
        public void SaveToFile() // Запис даних до файлу
        {
            if (!this.Modify)
                return;
            try
            {
                Stream S; 
                if (File.Exists(this.SaveFileName))
                    S = File.Open(this.SaveFileName, FileMode.Append);
                else
                    S = File.Open(this.SaveFileName, FileMode.Create);
                Buffer D = new Buffer(); 
                D.Data = this.Data;
                D.Result = Convert.ToString(this.Result);
                D.Key = Key;
                Key++;
                BinaryFormatter BF = new BinaryFormatter(); 
                BF.Serialize(S, D);
                S.Flush(); 
                S.Close(); 
                this.Modify = false;
            }
            catch
            {

                MessageBox.Show("Помилка роботи з файлом"); 
            }
        }

        public void ReadFromFile()
        {
            try
            {
                if (!File.Exists(this.OpenFileName)) {
                    MessageBox.Show("Файлу немає");
                    return;
                }
                Stream S;
                S=File.Open(this.OpenFileName, FileMode.Open);
                Buffer D;
                BinaryFormatter BF ;
                object O;
                BF = new BinaryFormatter();
                while (S.Position < S.Length)
                {
                    O = BF.Deserialize(S); 
                    D = O as Buffer;
                    if (D == null) break;
                   
                }
                S.Close(); 
            }
            catch
            {
                MessageBox.Show("Помилка файлу"); // Виведення на екран повідомлення "Помилка файлу"
            }
        }

        public void Generator() {
            try
            {
                if (!File.Exists(this.SaveFileName)) 
                {
                    Key = 1;
                    return;
                }
                Stream S; 
                S = File.Open(this.SaveFileName, FileMode.Open);
                Buffer D;
                object O; 
                BinaryFormatter BF = new BinaryFormatter();
                while (S.Position < S.Length)
                {
                    O = BF.Deserialize(S);
                    D = O as Buffer;
                    if (D == null) break;
                    Key = D.Key;
                }
                Key++;
                S.Close();
            }
            catch
            {
                MessageBox.Show("Помилка файлу"); 
            }


        }

        public bool SaveFileNameExists()
        {
            if (this.SaveFileName == null)
                return false;
            else return true;
        }

        public void NewRec() // новий запис
        {
            this.Data = ""; // "" - ознака порожнього рядка
            this.Result = null; // для string- null
        }

    }
}
