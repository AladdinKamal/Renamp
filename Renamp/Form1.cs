using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            MessageBox.Show(@"-The default projects' directory is\n   'C:\Users\XXX\Documents\Visual Studio 2015\Projects\ '.\n-The new project's name can only contain letters, numbers, - and _. ", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '_' && e.KeyChar != '-' && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" && checkBox1.Checked == true)
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }
            else if (textBox1.Text == textBox2.Text)
            {
                MessageBox.Show("Please change new project name.");
            }

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

            foreach (string dirPath in Directory.GetDirectories(oldPath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(oldPath, Backup));

            foreach (string newPath1 in Directory.GetFiles(oldPath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath1, newPath1.Replace(oldPath, Backup), true);

            Directory.Move(oldPath, newPath);

            System.IO.File.Move(newPath + oldName1 + ".sln", newPath + newName + ".sln");

            fileEditor(newPath + newName + ".sln", oldName1, newName, oldName2);

            oldPath = newPath + oldName1 + @"\";
            newPath += newName + @"\";
            Directory.Move(oldPath, newPath);

            if (Directory.Exists(newPath + @"bin\"))
                DeleteDirectory(newPath + @"bin\");
            if (Directory.Exists(newPath + @"obj\"))
                DeleteDirectory(newPath + @"obj\");

            System.IO.File.Move(newPath + oldName1 + ".csproj", newPath + newName + ".csproj");

            fileEditor(newPath + newName + ".csproj", oldName1, newName, oldName2);
            fileEditor(newPath + "Program.cs", oldName1, newName, oldName2);
            fileEditor(newPath + "Form1.cs", oldName1, newName, oldName2);
            fileEditor(newPath + "Form1.Designer.cs", oldName1, newName, oldName2);

            if (File.Exists(newPath + "Form2.cs"))
            {
                fileEditor(newPath + "Form2.cs", oldName1, newName, oldName2);
                fileEditor(newPath + "Form2.Designer.cs", oldName1, newName, oldName2);
            }
            if (File.Exists(newPath + "Form3.cs"))
            {
                fileEditor(newPath + "Form3.cs", oldName1, newName, oldName2);
                fileEditor(newPath + "Form3.Designer.cs", oldName1, newName, oldName2);
            }
            if (File.Exists(newPath + "Form4.cs"))
            {
                fileEditor(newPath + "Form4.cs", oldName1, newName, oldName2);
                fileEditor(newPath + "Form4.Designer.cs", oldName1, newName, oldName2);
            }
            if (File.Exists(newPath + "Form5.cs"))
            {
                fileEditor(newPath + "Form5.cs", oldName1, newName, oldName2);
                fileEditor(newPath + "Form5.Designer.cs", oldName1, newName, oldName2);
            }
            if (File.Exists(newPath + "Form6.cs"))
            {
                fileEditor(newPath + "Form6.cs", oldName1, newName, oldName2);
                fileEditor(newPath + "Form6.Designer.cs", oldName1, newName, oldName2);
            }
            if (File.Exists(newPath + "Form7.cs"))
            {
                fileEditor(newPath + "Form7.cs", oldName1, newName, oldName2);
                fileEditor(newPath + "Form7.Designer.cs", oldName1, newName, oldName2);
            }
            if (File.Exists(newPath + "Form8.cs"))
            {
                fileEditor(newPath + "Form8.cs", oldName1, newName, oldName2);
                fileEditor(newPath + "Form8.Designer.cs", oldName1, newName, oldName2);
            }
            if (File.Exists(newPath + "Form9.cs"))
            {
                fileEditor(newPath + "Form9.cs", oldName1, newName, oldName2);
                fileEditor(newPath + "Form9.Designer.cs", oldName1, newName, oldName2);
            }
            if (File.Exists(newPath + "Form10.cs"))
            {
                fileEditor(newPath + "Form10.cs", oldName1, newName, oldName2);
                fileEditor(newPath + "Form10.Designer.cs", oldName1, newName, oldName2);
            }

            newPath = newPath + @"Properties\";

            fileEditor(newPath + "AssemblyInfo.cs", oldName1, newName, oldName2);
            fileEditor(newPath + "Resources.Designer.cs", oldName1, newName, oldName2);
            fileEditor(newPath + "Settings.Designer.cs", oldName1, newName, oldName2);

            DialogResult result = MessageBox.Show("Project name has been successfully changed.\nDo you want to change another project name ?", "SUCCESS!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.No)
            {
                Application.Exit();
            }
            else if (result == DialogResult.Yes)
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.ClientSize = new System.Drawing.Size(395, 180);
                button1.Location = new System.Drawing.Point(200, 140);
                textBox3.Visible = true;
            }
            else
            {
                this.ClientSize = new System.Drawing.Size(395, 150);
                button1.Location = new System.Drawing.Point(200, 110);
                textBox3.Visible = false;
                textBox3.Text = "";

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
    }
}
