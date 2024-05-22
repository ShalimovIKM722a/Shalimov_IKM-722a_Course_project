﻿using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace Shalimov_IKM_722a_Course_project
{
    class MajorWork
    {
        private DateTime TimeBegin;
        private string Data;
        private string Result;
        public bool Modify;
        private int Key;

        public Stack myStack = new Stack();
        public string[] myArr = new string[100];

        public Queue myQueue = new Queue();
        public string[] smyQueue = new string[100];

        private string SaveTextFileName;
        public void WriteSaveTextFileName(string S)
        {
            this.SaveTextFileName = S;
        }

        public bool SaveTextFileNameExists()
        {
            if (this.SaveTextFileName == null)
                return false;
            else return true;
        }

        private SaveFileDialog sf;

        public void Write(string D)
        {
            this.Data = D;
        }
        public string Read()
        {
            return this.Result;
        }
        public void Task() {
            try
            {
                if (this.Data.Length != 0)
                {
                    this.Result = "Кожне п'яте слово вашого тексту: ";
                    char[] separators = new char[] { ' ', ',', '.', ';', '-' };
                    string[] words = Data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (words.Length < 50)
                    {
                        for (int i = 0; i < words.Length; i++)
                        {
                            if ((i + 1) % 5 == 0)
                            {
                                this.Result += " " + words[i];
                            }
                        }
                        this.Modify = true;
                    }
                    else
                    {
                        this.Result = null;
                        MessageBox.Show("Введіть текст до 50 слів", "Попередження!");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Порожній рядок", "Помилка!");
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
        public void SaveToFile() 
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

        public void ReadFromFile(System.Windows.Forms.DataGridView DG) 
        {
            try
            {
                if (!File.Exists(this.OpenFileName))
                {
                    MessageBox.Show("Файлу немає"); 
                    return;
                }
                Stream S; 
                S = File.Open(this.OpenFileName, FileMode.Open); 
                Buffer D;
                object O; 
                BinaryFormatter BF = new BinaryFormatter(); 
                DataTable MT = new DataTable();
                DataColumn cKey = new DataColumn("Ключ");
                DataColumn cInput = new DataColumn("Вхідні дані");
                DataColumn cResult = new DataColumn("Результат");
                MT.Columns.Add(cKey);
                MT.Columns.Add(cInput);
                MT.Columns.Add(cResult);

                while (S.Position < S.Length)
                {
                    O = BF.Deserialize(S);
                    D = O as Buffer;
                    if (D == null) break;
                    DataRow MR;
                    MR = MT.NewRow();
                    MR["Ключ"] = D.Key; 
                    MR["Вхідні дані"] = D.Data;
                    MR["Результат"] = D.Result;
                    MT.Rows.Add(MR);
                }
                DG.DataSource = MT;
                S.Close();
            }
            catch
            {
                MessageBox.Show("Помилка файлу"); 
            }
           
        }

        public string ReadSaveTextFileName()
        {
            return SaveTextFileName;
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

        public void NewRec() 
        {
            this.Data = ""; 
            this.Result = null;
        }
 
        public void Find(string Num) {
            int N;
            try
            {
                N = Convert.ToInt16(Num);
            }
            catch
            {
                MessageBox.Show("Помилка пошукового запиту");
                return;
            }
            try
            {
                if (!File.Exists(this.OpenFileName))
                {
                    MessageBox.Show("файлу немає");
                    return;
                }
                Stream S; 
                S = File.Open(this.OpenFileName, FileMode.Open); 
                Buffer D;
                object O; 
                BinaryFormatter BF = new BinaryFormatter(); 

               while (S.Position < S.Length)
                {
                    O = BF.Deserialize(S);
                    D = O as Buffer;
                    if (D == null) break;
                    if (D.Key == N) {
                        string ST;
                        ST = "Запис містить:" + (char)13 + "No" + Num + "Вхідні дані:" +

                        D.Data + "Результат:" + D.Result;

                        MessageBox.Show(ST, "Запис знайдена"); 
                        S.Close();
                        return;
                    }
                }
                S.Close();
                MessageBox.Show("Запис не знайдена"); 
            }
            catch
            {
                MessageBox.Show("Помилка файлу"); 
            }
        }

        public void SaveToTextFile(string name, System.Windows.Forms.DataGridView D)
        {
            try
            {
                System.IO.StreamWriter textFile;
                if (!File.Exists(name))
                {
                    textFile = new System.IO.StreamWriter(name);
                }
                else
                {
                    textFile = new System.IO.StreamWriter(name, true);
                }

                for (int i = 0; i < D.RowCount - 1; i++)
                {
                    textFile.WriteLine("{0};{1};{2}", D[0, i].Value.ToString(),

                    D[1, i].Value.ToString(), D[2, i].Value.ToString());

                }
                textFile.Close();
            }
            catch
            {
                MessageBox.Show("Помилка роботи з файлом");
            }
        }

    }

    }
