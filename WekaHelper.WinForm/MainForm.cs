using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WekaHelper.WinForm.Models;

namespace WekaHelper.WinForm
{
    public partial class MainForm : Form
    {
        private string m_filename = null;
        private List<ColumnModel> m_headers = new List<ColumnModel>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void tsmOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择文件";
            dialog.Filter = "CSV文件(*.csv)|*.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                m_filename = path;
                var items = File.ReadAllLines(path, Encoding.UTF8);

                m_headers.Clear();
                foreach (var item in items[0].Split(','))
                {
                    m_headers.Add(new ColumnModel(item));
                }
                var data = items[1].Split(',');
                for (int i = 0; i < data.Length; i++)
                {
                    if (String.IsNullOrWhiteSpace(m_headers[i].Name))
                    {
                        m_headers[i].Name = (i + 1).ToString();
                    }
                    m_headers[i].Example = data[i];
                }

                dgMain.DataSource = m_headers;
            }
        }

        private void tsmExport_Click(object sender, EventArgs e)
        {
            var items = File.ReadAllLines(m_filename, Encoding.UTF8);
            for (int i = 1; i < items.Length; i++)
            {
                var data = items[i].Split(',');
                if (data.Length != m_headers.Count)
                {
                    MessageBox.Show($"数据维度出现不同！行号：{i}");
                    return;
                }
                for (int k = 0; k < m_headers.Count; k++)
                {
                    if (m_headers[k].Type == ColumnType.Nominal && m_headers[k].ValueSet.Contains(data[k]) == false)
                        m_headers[k].ValueSet.Add(data[k]);
                }
            }

            using (StreamWriter sw = new StreamWriter("export.arff", false, new System.Text.UTF8Encoding(false)))
            {
                string name = Path.GetFileName(m_filename);
                sw.WriteLine($"@relation {name}");
                sw.WriteLine();

                StringBuilder sb = new StringBuilder();
                foreach (var h in m_headers)
                {
                    if (h.HasChosen == false)
                        continue;
                    if (h.Type == ColumnType.Nominal)
                    {
                        sb.Clear();
                        foreach (var k in h.ValueSet)
                        {
                            sb.Append(k);
                            sb.Append(',');
                        }
                        sw.WriteLine($"@attribute {h.Name} " + "{" + sb.ToString().TrimEnd(',') + "}");
                    }
                    else if (h.Type == ColumnType.Numeric)
                    {
                        sw.WriteLine($"@attribute {h.Name} numeric");
                    }
                    else if (h.Type == ColumnType.String)
                    {
                        sw.WriteLine($"@attribute {h.Name} string");
                    }

                }
                sw.WriteLine();

                sw.WriteLine("@data");

                for (int i = 1; i < items.Length; i++)
                {
                    var data = items[i].Split(',');
                    sb.Clear();
                    for (int k = 0; k < m_headers.Count; k++)
                    {
                        if (m_headers[k].HasChosen)
                        {
                            sb.Append(data[k]);
                            sb.Append(',');
                        }
                    }
                    sw.WriteLine(sb.ToString().TrimEnd(','));
                }
            }
            MessageBox.Show("导出成功!");
        }
    }
}
