using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace file_mover
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fb.SelectedPath))
            {
                Console.WriteLine("Parent folder: " + fb.SelectedPath.ToString());
                string path = fb.SelectedPath.ToString();
                //string[] files = System.IO.Directory.GetFiles(fb.SelectedPath);
                string[] folder = System.IO.Directory.GetDirectories(fb.SelectedPath);

                foreach (string foldername in folder)
                {

                    DirectoryInfo dirInfo = new DirectoryInfo(foldername);
                    string[] foldernest = System.IO.Directory.GetDirectories(foldername);


                    foreach (string item in foldernest)
                    {
                        Console.WriteLine(item);
                        List<string> images = Directory.GetFiles(item, "*.jpg", SearchOption.AllDirectories).ToList();
                        foreach (string file in images)
                        {
                            FileInfo mFile = new FileInfo(file);
                            // to remove name collisions
                            if (new FileInfo(dirInfo + "\\" + mFile.Name).Exists == false)
                            {
                                mFile.MoveTo(dirInfo + "\\" + mFile.Name);
                            }
                            else
                            {
                                System.Threading.Thread.Sleep(1000);
                                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                mFile.MoveTo(dirInfo + "\\" + timestamp + mFile.Name);
                            }
                        }

                    }
                }
              
            }
      
        }
    }
}
