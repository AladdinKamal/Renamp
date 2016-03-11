using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace Renamp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("-This application changes any C# project's name.. including all  properties and namespace.\n\n" + "-The default projects' directory is\n" + @"   ' C:\Users\XXX\Documents\Visual Studio 2015\Projects\ '" + "\n-The new project's name can only contain letters, numbers, - and _. \n\n -Please close all directories before renaming.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '_' && e.KeyChar != '-' && e.KeyChar != (char)8 && e.KeyChar != 'V' && !ModifierKeys.HasFlag(Keys.Control))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var regexItem = new Regex("~!@#$%^&*()+=/|,.?><");
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" && checkBox1.Checked == true)
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }
            else if (textBox1.Text == textBox2.Text)
            {
                MessageBox.Show("Please change new project name.");
                return;
            }
            else if (regexItem.IsMatch(textBox2.Text) || textBox2.Text.Contains(' ') || textBox2.Text.Contains('"') || textBox2.Text.Contains('/'))
            {
                MessageBox.Show("New project name can't contain spaces or special characters,\nonly letters, numbers, dashes '-' and underscores '_' are allowed.\nPlease try again..");
                return;
            }

            List<String> files = new List<String>();
            String newName = textBox2.Text;
            String oldName1 = textBox1.Text;
            String oldName2 = "";
            if (oldName1.Contains(' '))
            {
                oldName2 = oldName1;
                oldName2 = oldName2.Replace(' ', '_');
            }

            String oldPath;
            String newPath;
            String Backup;

            if (textBox3.Text.Trim() != "")
            {
                oldPath = textBox3.Text + @"\" + oldName1 + @"\";
                newPath = textBox3.Text + @"\" + newName + @"\";
                Backup = textBox3.Text + @"\Renamp Backup\" + oldName1 + @"\";
                String oldPathx = oldPath + oldName1 + @"\";
                if (!Directory.Exists(oldPathx))
                {
                    MessageBox.Show("Incorrect project name or directory, please try again.");
                    return;
                }
            }
            else
            {
                oldPath = @"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2015\Projects\" + oldName1 + @"\";
                newPath = @"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2015\Projects\" + newName + @"\";
                Backup = @"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2015\Projects\Renamp Backup\" + oldName1 + @"\";
                if (!Directory.Exists(oldPath))
                {
                    MessageBox.Show("Incorrect project name, please try again.");
                    return;
                }
            }

            if (Directory.Exists(newPath))
            {
                MessageBox.Show("Please change new project name.");
                return;
            }
            else if (Directory.Exists(Backup))
            {
                DialogResult error = MessageBox.Show("A backup already exists for this project, would you like to overwrite it ?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (error == DialogResult.Yes)
                    Directory.Delete(Backup);
                else
                    return;
            }

            try
            {
                foreach (String dirPath in Directory.GetDirectories(oldPath, "*", SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(oldPath, Backup));
                foreach (String filePath in Directory.GetFiles(oldPath, "*.*", SearchOption.AllDirectories))
                    File.Copy(filePath, filePath.Replace(oldPath, Backup), true);
            }
            catch (Exception error)
            {
                MessageBox.Show("Please close all directories and any open projects.");
                return;
            }

            Directory.Move(oldPath, newPath);
            Directory.Move(newPath + oldName1 + @"\", newPath + newName + @"\");

            if (Directory.Exists(newPath + newName + @"\bin"))
                DeleteDirectory(newPath + newName + @"\bin");
            if (Directory.Exists(newPath + newName + @"\obj"))
                DeleteDirectory(newPath + newName + @"\obj");

            System.IO.File.Move(newPath + oldName1 + ".sln", newPath + newName + ".sln");
            System.IO.File.Move(newPath + newName + @"\" + oldName1 + ".csproj", newPath + newName + @"\" + newName + ".csproj");

            files.Add(newPath + newName + ".sln");
            files.Add(newPath + newName + @"\" + newName + ".csproj");
            addFiles(files, newPath, "*.appxmanifest");
            addFiles(files, newPath, "*.cs");
            addFiles(files, newPath, "*.xaml");

            foreach (String f in files)
                fileEditor(f, oldName1, newName, oldName2);

            if (textBox3.Text != "")
                newPath = textBox3.Text;
            else
                newPath = @"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2015\Projects\";
            Backup = newPath + @"Renamp Backup\";
            DialogResult result = MessageBox.Show("Project name has been successfully changed.\nThe new project exists in ' " + newPath + " '\n" + "A backup was created in ' " + Backup + " '\n" + "Do you want to change another project name ?", "SUCCESS!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.No)
            {
                Application.Exit();
            }
            else if (result == DialogResult.Yes)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                checkBox1.Checked = false;
                files.Clear();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.ClientSize = new System.Drawing.Size(395, 180);
                button1.Location = new System.Drawing.Point(200, 140);
                textBox3.Visible = true;
                button2.Visible = true;
            }
            else
            {
                this.ClientSize = new System.Drawing.Size(395, 150);
                button1.Location = new System.Drawing.Point(200, 110);
                textBox3.Visible = false;
                textBox3.Text = "";
                button2.Visible = false;
            }
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        public static void fileEditor(String path, String oldN1, String newN, String oldN2)
        {
            StreamReader reader = new StreamReader(path);
            string input = reader.ReadToEnd();
            reader.Close();
            input = input.Replace(oldN1, newN);
            if (oldN2 != "")
                input = input.Replace(oldN2, newN);
            StreamWriter writer = new StreamWriter(path);
            writer.Write(input);
            writer.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult folderBrowserDialog = folderBrowserDialog1.ShowDialog();

            if (folderBrowserDialog == DialogResult.OK)
                textBox3.Text = folderBrowserDialog1.SelectedPath;
        }

        void addFiles(List<String> files, String Path, String extension)
        {
            try
            {
                foreach (String d in Directory.GetDirectories(Path))
                {
                    foreach (string f in Directory.GetFiles(d, extension))
                    {
                        files.Add(f);
                    }
                    addFiles(files, d, extension);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
    }
}
