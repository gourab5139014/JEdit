using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace JEdit
{
    public partial class Form1 : Form
    {
        List<string> keywords;
        string current;
        public Form1()
        {
            keywords = new List<string>();
            InitializeComponent();
            XmlDocument xd=new XmlDocument();
            xd.Load("keywords.xml");
            XmlNode xn = xd.FirstChild;
            xn = xn.NextSibling;
            foreach (XmlNode cn in xn.ChildNodes) {
                keywords.Add(cn.InnerText);
            } 

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
          
            int pos1 = richTextBox1.SelectionStart;
            int len1 = richTextBox1.SelectionLength;
            int pos;
            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = Color.Black;
            /*
            foreach(string str in keywords){
                pos = 0;
                while (pos >= 0)
                {
                    pos = richTextBox1.Find(str, pos, RichTextBoxFinds.MatchCase);
                    if (pos >= 0)
                    {
                        richTextBox1.Select(pos, str.Length);

                        richTextBox1.SelectionColor = Color.Blue;
                    }
                    if(pos!=-1)pos++;
                }
            }*/
            pos = 0;
            int pose;
            while (pos < richTextBox1.Text.Length-1)
            {
                pose = pos;
                while (pose < richTextBox1.Text.Length && (richTextBox1.Text[pose] == '.' || (richTextBox1.Text[pose] <= 'z' && richTextBox1.Text[pose] >= 'a')
|| (richTextBox1.Text[pose] >= 'A' && richTextBox1.Text[pose] <= 'Z') || 
(richTextBox1.Text[pose] <= '9' && richTextBox1.Text[pose] >= '0') || richTextBox1.Text[pose] == '_' ) ) pose++;

                //while (pose<richTextBox1.Text.Length && (richTextBox1.Text[pose] <= 'z' && richTextBox1.Text[pose] >= 'a')
                //    || (richTextBox1.Text[pose] >= 'A' && richTextBox1.Text[pose] <= 'Z') ||
                  //  (richTextBox1.Text[pose] <= '9' && richTextBox1.Text[pose] >= '0') || richTextBox1.Text[pose] == '_') pose++;
                //keywords.Find(Text.Substring(pos, pose - pos));
                if (keywords.Contains(richTextBox1.Text.Substring(pos, pose - pos)))
                {
                    richTextBox1.Select(pos, pose - pos);
                    richTextBox1.SelectionColor = Color.Blue;

                }
                if (pose == pos) pose++;

                pos = pose;

                if (this.current == null) this.Text = "<untitled> JEdit :: Created by Gourab Mitra and Ankit Agarwalla";
                else this.Text = "<" + current + "> JEdit :: Created by Gourab Mitra and Ankit Agarwalla";
            
            }
            richTextBox1.SelectionLength = len1;
            richTextBox1.SelectionStart=pos1;
        }

        private void actionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void lToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.CheckFileExists)
            {              
                FileStream f = File.OpenRead(openFileDialog1.FileName);
                //FileStream ifile = File.Open(
                StreamReader s = new StreamReader(f);
                richTextBox1.Text = s.ReadToEnd();
                current=openFileDialog1.FileName;
            }
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (current == null)
            {
                saveFileDialog1.ShowDialog();
                current = saveFileDialog1.FileName;
            }
            StreamWriter s = new StreamWriter(File.OpenWrite(current));
            s.Write(richTextBox1.Text);
            s.Close();
            /* if (current == null)
            {
                saveFileDialog1.ShowDialog();
                current = saveFileDialog1.FileName;
            }
                StreamWriter s=new StreamWriter(File.OpenWrite(current));
                s.Write(richTextBox1.Text);
            */
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            current = saveFileDialog1.FileName;
            
                StreamWriter s=new StreamWriter(File.OpenWrite(current));
                s.Write(richTextBox1.Text);
        }

        private void saveTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // XmlDocument xd1 = new XmlDocument();
            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("JEdit analyses tokens in your JAVA code. The Editor has in-built code hilighting\nDeveloped in BIT Mesra by Gourab Mitra http://gourabmitra.co.cc and Ankit Agarwalla \nSpecial thanks to my .NET guru-JiiN", "ABout JEdit 1.0");
        }

        

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (this.current == null) this.Text = "<untitled1> JEdit :: Created by Gourab Mitra and Ankit Agarwalla";
            else this.Text = "<" + current + "2> JEdit :: Created by Gourab Mitra and Ankit Agarwalla";
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (this.current == null) this.Text = "<untitled1> JEdit :: Created by Gourab Mitra and Ankit Agarwalla";
            else this.Text = "<" + current + "2> JEdit :: Created by Gourab Mitra and Ankit Agarwalla";
        }

        
    }
}
