using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BriefcamParser
{
    public partial class Form1 : Form
    {

        string pathToSave = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
        string calendar = "";
        DirectoryInfo infoDir;
        string directoryDestination = "";
        bool maximized = false;


        public Form1()
        {
            InitializeComponent();
            maskedTextBox1.Focus();
           
            //calendar = ((DateTime)(dateTimePicker1.Value)).ToShortDateString();
            //dateTimePicker1.Format = DateTimePickerFormat.Custom;
            //dateTimePicker1.CustomFormat = "dd/MM/yyyy hh:mm:ss";
        }

        private void panel4_DragDrop(object sender, DragEventArgs e)
        {
            directoryDestination = "";      
            
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            int i = 0;
            string toFichier ="";
            //on créé un répertoire à la date du calendar
            FileInfo pathCurrentDirectory = new FileInfo(fileList[0]);
            infoDir = pathCurrentDirectory.Directory;         


            foreach (string fi in fileList)
            {
                FileInfo fichier = new FileInfo(fi);
                
                //Get the Attributes of the file 
                DateTime timeCreated = File.GetCreationTime(fichier.FullName);
                DateTime timeLastWrite = File.GetLastWriteTime(fichier.FullName);
                DateTime timeLastAccess = File.GetLastAccessTime(fichier.FullName);

                //toFichier += fichier.FullName + ";" + timeCreated + ";" + timeLastWrite + ";" + timeLastAccess + ";\n";
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[3].Value = timeCreated;
                dataGridView1.Rows[i].Cells[2].Value = timeLastWrite;
                dataGridView1.Rows[i].Cells[1].Value = timeLastAccess;
                dataGridView1.Rows[i].Cells[0].Value = fichier.Name;
                dataGridView1.Rows[i].Cells[5].Value = true;
                dataGridView1.Rows[i].Cells[6].Value = fichier.FullName;
                dataGridView1.Rows[i].Cells[4].Value = CheckNameBis(fichier.Name);


                //ChangeTime(fichier.FullName, "", i);

                i++;

            }

            //WriteFile(toFichier);
            
        }

        private void WriteFile(string toFichier)
        {
            string nomFichier = ("Backup_" + DateTime.Now + ".txt").Replace(":","_");

            if (!Directory.Exists(pathToSave + "\\BriefcamBackups\\" + calendar))
                Directory.CreateDirectory(pathToSave + "\\BriefcamBackups\\" + calendar);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathToSave + "\\BriefcamBackups\\" + calendar + "\\"+ nomFichier, false))
            {
               
                file.Write(toFichier);
            }
        }
        private void FillResult()
        {
            int i = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {


                FileInfo fichier = new FileInfo(row.Cells[6].Value.ToString());

                //Get the Attributes of the file 
                DateTime timeCreated = File.GetCreationTime(fichier.FullName);
                DateTime timeLastWrite = File.GetLastWriteTime(fichier.FullName);
                DateTime timeLastAccess = File.GetLastAccessTime(fichier.FullName);

                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[3].Value = timeCreated;
                dataGridView2.Rows[i].Cells[2].Value = timeLastWrite;
                dataGridView2.Rows[i].Cells[1].Value = timeLastAccess;
                dataGridView2.Rows[i].Cells[0].Value = fichier.Name;



                //ChangeTime(fichier.FullName, "", i);

                i++;
                                
            }
            //foreach (string fi in Directory.GetFiles(infoDir.FullName + "\\" + calendar))
            //{
            //    FileInfo fichier = new FileInfo(fi);

            //    //Get the Attributes of the file 
            //    DateTime timeCreated = File.GetCreationTime(fichier.FullName);
            //    DateTime timeLastWrite = File.GetLastWriteTime(fichier.FullName);
            //    DateTime timeLastAccess = File.GetLastAccessTime(fichier.FullName);

            //    //toFichier += fichier.FullName + ";" + timeCreated + ";" + timeLastWrite + ";" + timeLastAccess + ";\n";
            //    dataGridView2.Rows.Add();
            //    dataGridView2.Rows[i].Cells[3].Value = timeCreated;
            //    dataGridView2.Rows[i].Cells[2].Value = timeLastWrite;
            //    dataGridView2.Rows[i].Cells[1].Value = timeLastAccess;
            //    dataGridView2.Rows[i].Cells[0].Value = fichier.Name;
                


            //    //ChangeTime(fichier.FullName, "", i);

            //    i++;

            //}
        }
        private DateTime CheckName(string nomFichier)
        {
            string annee = "";
            string mois = "";
            string jour = "";
            string heure = "";
            string minutes = "";
            string seconde = "";
            string datum = "";

            DateTime temps;
            try
            {
                string[] infos = nomFichier.Split('_');
                annee = infos[3].Substring(0, 4);
                mois = infos[3].Substring(5, 2);
                jour = infos[3].Substring(8, 2);
                heure = infos[4].Substring(0,2);
                minutes = infos[4].Substring(2, 2);
                seconde = infos[4].Substring(4, 2);
                

                temps = new DateTime(Int16.Parse(annee), Int16.Parse(mois), Int16.Parse(jour), Int16.Parse(heure), Int16.Parse(minutes), Int16.Parse(seconde));

            }
            catch
            {
                return new DateTime(99,99,99);
            }
            
            return temps;
        }
        private DateTime CheckNameBis(string nomFichier)
        {
            string annee = "";
            string mois = "";
            string jour = "";
            string heure = "";
            string minutes = "";
            string seconde = "";
            string datum = "";
            string datededebut = "";
            string datedefin = "";

            DateTime debut = new DateTime();
            DateTime tmp;
            DateTime fin = new DateTime();
            string heureDebut = "";
            DateTime heureDebutTmp;
            String heureFin = "";
            int tmpp;

            DateTime temps;
            try
            {
                string[] infos = nomFichier.Split('_');
                


                foreach(string tt in infos)
                {
                    if(DateTime.TryParse(tt, out tmp))
                    {
                        if (datededebut == "")
                        {
                            //debut = tmp;
                            datededebut = tt;
                        }

                        else
                            datedefin = tt;
                    }
                    
                    if(Int32.TryParse(tt,out tmpp))
                    {
                        if(tt.ToString().Length == 6)
                        {
                            heure = tt.ToString().Substring(0, 2);
                            minutes = tt.ToString().Substring(2, 2);
                            seconde = tt.ToString().Substring(4, 2);

                            if (heureDebut == "")
                                heureDebut = heure + "-" + minutes + "-" + seconde;

                            else
                                heureFin = heure + "-" + minutes + "-" + seconde;

                        }
                    }

                    if (datedefin != "" && heureFin != "")
                        break;
                }

                if (datedefin != "" && heureFin != "")
                {
                    
                    heure = heureFin.Substring(0, 2);
                    minutes = heureFin.Substring(3, 2);
                    seconde = heureFin.Substring(6, 2);

                    annee = datedefin.Substring(0, 4);
                    mois = datedefin.Substring(5, 2);
                    jour = datedefin.Substring(8, 2);

                    temps = new DateTime(Int16.Parse(annee), Int16.Parse(mois), Int16.Parse(jour), Int16.Parse(heure), Int16.Parse(minutes), Int16.Parse(seconde));

                }
                else
                    temps = new DateTime(01, 01, 01);





                //temps = new DateTime(datedefin,heureFin);

            }
            catch
            {
                return new DateTime(01, 01, 01);
            }

            return temps;
        }

        private void FillTable(DateTime fichier)
        {
           

            


        }

        private void ChangeTime(string path, string nouvelledate,int i)
        {
            DateTime tt = new DateTime(2020, 12, 12,01,05,25);
            File.SetCreationTime(path, tt);
            File.SetLastAccessTime(path, tt);
            File.SetLastWriteTime(path, tt);
            dataGridView1.Rows[i].Cells[4].Value = tt;
        }

        private void ChangeTime(string path, DateTime nouvelledate)
        {
            DateTime tt = nouvelledate;
            //File.SetCreationTime(path, tt);
            //File.SetLastAccessTime(path, tt);
            File.SetLastWriteTime(path, tt);         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells[5].Value);
                if (isSelected)
                {
                    row.Cells[5].Value = false;
                }
                else
                    row.Cells[5].Value = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
                return;

            if (maskedTextBox1.Text == "")
            {
                maskedTextBox1.BackColor = Color.Red;
                MessageBox.Show("Veuillez remplir le champ OPS sivoplé");
                return;
            }
            else
                maskedTextBox1.BackColor = Color.White;
            
            if (MessageBox.Show("Etes-vous certain de modifier votre sélection ?" , "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CopyFiles();
            }
        }

        private void CopyFiles()
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            
            //backup format original
            string toFichier = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {


                FileInfo fichier = new FileInfo(row.Cells[6].Value.ToString());

                //Get the Attributes of the file 
                DateTime timeCreated = File.GetCreationTime(fichier.FullName);
                DateTime timeLastWrite = File.GetLastWriteTime(fichier.FullName);
                DateTime timeLastAccess = File.GetLastAccessTime(fichier.FullName);

                toFichier += fichier.FullName + ";" + timeCreated + ";" + timeLastWrite + ";" + timeLastAccess + ";\n";
                
                
            }
            //WriteFile(toFichier);

            //maintenant on modifie le timestamp et déplace dans nouveau répertoire

            //if (!Directory.Exists(infoDir.FullName + "\\" + calendar))
            //    Directory.CreateDirectory(infoDir.FullName + "\\" + calendar);

            //si coché on modifie et déplace
            int i = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                FileInfo fichier = new FileInfo(row.Cells[6].Value.ToString());
                string nomDossier = "";

                if (maskedTextBox1.Text != "")
                    nomDossier = GetValidPath(maskedTextBox1.Text) + "_";

                bool isSelected = Convert.ToBoolean(row.Cells[5].Value);
                if (isSelected)
                {
                    fichier = new FileInfo(row.Cells[6].Value.ToString());
                    ChangeTime(fichier.FullName, (DateTime)(row.Cells[4].Value));
                    File.Move(fichier.FullName, fichier.DirectoryName +"\\OPS_" + nomDossier.ToUpper() + fichier.Name);

                   
                    FileInfo fichierBis = new FileInfo(fichier.DirectoryName + "\\OPS_" + nomDossier.ToUpper() + fichier.Name);

                    //Get the Attributes of the file 
                    DateTime timeCreated = File.GetCreationTime(fichierBis.FullName);
                    DateTime timeLastWrite = File.GetLastWriteTime(fichierBis.FullName);
                    DateTime timeLastAccess = File.GetLastAccessTime(fichierBis.FullName);

                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[3].Value = timeCreated;
                    dataGridView2.Rows[i].Cells[2].Value = timeLastWrite;
                    dataGridView2.Rows[i].Cells[1].Value = timeLastAccess;
                    dataGridView2.Rows[i].Cells[0].Value = fichierBis.Name;

                    i++;

                }
                else
                    continue;
            }

            //FillResult();
            //directoryDestination = infoDir.FullName + "\\" + calendar + "\\";
            directoryDestination = infoDir.FullName ;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (directoryDestination == "")
                return;
            
            // opens the folder in explorer
            Process.Start(directoryDestination);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public string GetValidPath(string invalidPath)
        {
            string validPath = "";
            StringBuilder sb = new StringBuilder(invalidPath);

            foreach (char c in Path.GetInvalidPathChars())
            {
                if (invalidPath.Contains(c.ToString()))
                {
                    if (c.ToString() != @"\")
                    {
                        sb.Replace(c.ToString(), "");
                    }
                }
            }
            validPath = sb.ToString();
            return validPath;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            maskedTextBox1.Text = "";
            directoryDestination = "";
            maskedTextBox1.Focus();

        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((e.KeyChar >= 'a') && (e.KeyChar <= 'z'))
            //{
            //    int iPos = maskedTextBox1.SelectionStart;
            //    int iLen = maskedTextBox1.SelectionLength;
            //    maskedTextBox1.Text = maskedTextBox1.Text.Remove(iPos, iLen).Insert(iPos, Char.ToUpper(e.KeyChar).ToString());
            //    maskedTextBox1.SelectionStart = iPos + 1;
            //    e.Handled = true;
            //}
        }

        private void maskedTextBox1_MouseEnter(object sender, EventArgs e)
        {
            maskedTextBox1.Text = "";
            
        }

        private void Form1_MaximumSizeChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            FormWindowState LastWindowState = FormWindowState.Minimized;
            Rectangle resolution = Screen.PrimaryScreen.WorkingArea;
            string width = this.Size.Width.ToString();
            int espace = 150;
            


            //Size sizeDatagridview1 = dataGridView1.Size;
            // When window state changes
            if (WindowState != LastWindowState)
            {
                LastWindowState = WindowState;
                
                if (maximized && (WindowState == FormWindowState.Normal))
                    maximized = true;


                if (WindowState == FormWindowState.Maximized)
                {
                    //this.Size = new Size(resolution.Width,resolution.Height);

                    dataGridView1.Size = new Size(new Point((resolution.Width - espace - 13)/2,dataGridView1.Size.Height));
                    dataGridView1.Columns[0].Width = ((dataGridView1.Size.Width/3)*2);
                    dataGridView1.Columns[2].Width = dataGridView1.Columns[2].Width + (dataGridView1.Size.Width / 3);

                    pictureBox1.Location = new Point((pictureBox1.Location.X * 2)-30, pictureBox1.Location.Y);
                    pictureBox3.Location = new Point((pictureBox3.Location.X) + 10, pictureBox3.Location.Y);
                    button1.Location = new Point(dataGridView1.Width - button1.Width, button1.Location.Y);
                    label2.Location = new Point((label2.Location.X * 2) + 140, label2.Location.Y);

                    dataGridView2.Size = new Size(new Point((resolution.Width - espace - 26) / 2, dataGridView2.Size.Height));
                    dataGridView2.Location = new Point(dataGridView2.Size.Width + 13 + espace, dataGridView2.Location.Y);
                    
                    dataGridView2.Columns[0].Width = ((dataGridView2.Size.Width / 3) * 2 + (dataGridView2.Columns[2].Width));
                    maximized = true;
                }
                if (WindowState == FormWindowState.Normal && maximized)
                {

                    dataGridView1.Size = new Size((629), (434));
                    dataGridView1.Columns[0].Width = 364;
                    dataGridView1.Columns[2].Width = dataGridView1.Columns[2].Width / 2;

                    pictureBox1.Location = new Point(642, 191);
                    pictureBox3.Location = new Point((pictureBox3.Location.X) - 10, pictureBox3.Location.Y);
                    button1.Location = new Point(595, button1.Location.Y);
                    label2.Location = new Point(1165, label2.Location.Y);


                    dataGridView2.Location = new Point(692,191);
                    dataGridView2.Size = new Size(549, 434);
                    dataGridView2.Columns[0].Width = 444;
                }

                //if (WindowState == FormWindowState.Maximized)
                //{
                //    dataGridView1.Size = new Size((dataGridView1.Size.Width * 2), (dataGridView1.Size.Height));
                //    dataGridView1.Columns[0].Width = (dataGridView1.Columns[0].Width * 2) + 200;
                //    dataGridView1.Columns[2].Width = dataGridView1.Columns[2].Width * 2;

                //    pictureBox1.Location = new Point((pictureBox1.Location.X * 2) + 20, pictureBox1.Location.Y);
                //    pictureBox3.Location = new Point((pictureBox3.Location.X) + 35, pictureBox3.Location.Y);
                //    button1.Location = new Point((button1.Location.X * 2) - 30, button1.Location.Y);
                //    label2.Location = new Point((label2.Location.X * 2) + 70, label2.Location.Y);




                //    dataGridView2.Location = new Point(dataGridView2.Location.X * 2, dataGridView2.Location.Y);
                //    dataGridView2.Size = new Size((dataGridView2.Size.Width * 2), (dataGridView2.Size.Height));
                //    dataGridView2.Columns[0].Width = (dataGridView2.Columns[0].Width * 2) + 100;
                //    maximized = true;
                //}
                //if (WindowState == FormWindowState.Normal && maximized)
                //{

                //    dataGridView1.Size = new Size((dataGridView1.Size.Width / 2), (dataGridView1.Size.Height));
                //    dataGridView1.Columns[0].Width = 364;
                //    dataGridView1.Columns[2].Width = dataGridView1.Columns[2].Width / 2;

                //    pictureBox1.Location = new Point((pictureBox1.Location.X / 2) - 10, pictureBox1.Location.Y);
                //    pictureBox3.Location = new Point((pictureBox3.Location.X) - 35, pictureBox3.Location.Y);
                //    button1.Location = new Point(595, button1.Location.Y);
                //    label2.Location = new Point(1165, label2.Location.Y);


                //    dataGridView2.Location = new Point(dataGridView2.Location.X / 2, dataGridView2.Location.Y);
                //    dataGridView2.Size = new Size((dataGridView2.Size.Width / 2), (dataGridView2.Size.Height));
                //    dataGridView2.Columns[0].Width = 444;
                //}
            }
        }
    }
}
