using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace testproject
{
    public partial class Form12 : Form
    {
        public static DataTable dt = new DataTable();
        XmlDocument doc;
        string file;

        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("From");
            dt.Columns.Add("To");
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.AllowUserToOrderColumns = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                file = openFileDialog1.FileName;
                try
                {
                    string text = File.ReadAllText(file);

                    dt.Rows.Clear();
                    doc = new XmlDocument();
                    doc.LoadXml(text);
                    XmlNode doc_node = doc.DocumentElement.SelectSingleNode("/");

                    XmlNode d_node = doc_node.SelectSingleNode("d");
                    XmlNodeList f_nodes = d_node.SelectNodes("f");

                    for (int i = 0; i < f_nodes.Count; i++)
                    {
                        string f_ds = f_nodes[i].Attributes["ds"].InnerText;

                        XmlNodeList to_nodes = f_nodes[i].SelectNodes("to");
                        for (int j = 0; j < to_nodes.Count; j++)
                        {
                            XmlNode ds = to_nodes[j].SelectSingleNode("ds");
                            string to_ds = ds.InnerText;

                            dt.Rows.Add(new object[] { f_ds, to_ds });
                        }
                    }
                    dataGridView1.DataSource = dt;
                }
                catch (IOException)
                {
                }
            }

            //openFileDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlNode doc_node = doc.DocumentElement.SelectSingleNode("/");

            XmlNode d_node = doc_node.SelectSingleNode("d");
            XmlNodeList f_nodes = d_node.SelectNodes("f");

            int index = 0;
            for (int i = 0; i < f_nodes.Count; i++)
            {
                //f_nodes[i].Attributes["ds"].InnerText = dataGridView1.Rows[index].Cells[0].Value.ToString();

                XmlNodeList to_nodes = f_nodes[i].SelectNodes("to");
                for (int j = 0; j < to_nodes.Count; j++)
                {
                    to_nodes[j].SelectSingleNode("ds").InnerText = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    index++;

                    f_nodes[i].SelectNodes("to")[j].InnerXml = to_nodes[j].InnerXml.ToString();
                }
                d_node.SelectNodes("f")[i].InnerXml = f_nodes[i].InnerXml.ToString();
            }

            doc_node.SelectSingleNode("d").InnerXml = d_node.InnerXml.ToString();

            doc.SelectSingleNode("/").InnerXml = doc_node.InnerXml.ToString();

            doc.Save(file);
            
        }
    }
}
