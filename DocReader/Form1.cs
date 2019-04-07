using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DocReader
{
    public partial class Form1 : Form
    {
        string path;
        XDocument doc;

        public Form1()
        {
            InitializeComponent();
            path = @"..\..\Data\content.xml";
            doc = XDocument.Load(path);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XElement book = doc.Element("book");
            TreeNode root = new TreeNode()
            {
                Text = book.Attribute("name").Value,
                ImageIndex = Convert.ToInt32(book.Attribute("icon1").Value),
                SelectedImageIndex = Convert.ToInt32(book.Attribute("icon2").Value),
            };

            foreach(XElement x in book.Elements("chapter"))
            {
                TreeNode item = new TreeNode()
                {
                    Text = x.Attribute("name").Value,
                    ImageIndex = Convert.ToInt32(x.Attribute("icon1").Value),
                    SelectedImageIndex = Convert.ToInt32(x.Attribute("icon1").Value),
                };

                foreach(XElement y in x.Elements("page"))
                {
                    TreeNode sub = new TreeNode()
                    {
                        Text = y.Attribute("id").Value + ".rtf",
                        ImageIndex = Convert.ToInt32(y.Attribute("icon1").Value),
                        SelectedImageIndex = Convert.ToInt32(y.Attribute("icon1").Value),
                    };
                    item.Nodes.Add(sub);
                }
                root.Nodes.Add(item);
            }
            treeView1.Nodes.Add(root);
            treeView1.ExpandAll();

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            string path = @"..\..\Data\";
            string text = node.Text;

            if (text == "WinForms Guide")
            {
                path += "Main.rtf";
            }
            else if(text.IndexOf("Chapter") == 0)
            {
                string num = text.Split()[1];
                int k = Convert.ToInt32(num) * 100;
                path += k.ToString() + ".rtf";
            }
            else
            {
                path += text;
            }
            richTextBox1.LoadFile(path);
        }
    }
}
