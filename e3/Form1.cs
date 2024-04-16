using System;
using System.IO;
using System.Windows.Forms;

namespace e3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 添加文件1选择按钮
            Button btnBrowseFile1 = new Button();
            btnBrowseFile1.Text = "选择文件1";
            btnBrowseFile1.Location = new System.Drawing.Point(20, 20);
            btnBrowseFile1.Click += BtnBrowseFile1_Click;
            Controls.Add(btnBrowseFile1);

            // 添加文件1路径显示文本框
            TextBox txtFile1 = new TextBox();
            txtFile1.Name = "txtFile1"; // 添加控件名字
            txtFile1.Location = new System.Drawing.Point(140, 20);
            txtFile1.Width = 300;
            Controls.Add(txtFile1);

            // 添加文件2选择按钮
            Button btnBrowseFile2 = new Button();
            btnBrowseFile2.Text = "选择文件2";
            btnBrowseFile2.Location = new System.Drawing.Point(20, 60);
            btnBrowseFile2.Click += BtnBrowseFile2_Click;
            Controls.Add(btnBrowseFile2);

            // 添加文件2路径显示文本框
            TextBox txtFile2 = new TextBox();
            txtFile2.Name = "txtFile2"; // 添加控件名字
            txtFile2.Location = new System.Drawing.Point(140, 60);
            txtFile2.Width = 300;
            Controls.Add(txtFile2);

            // 添加合并按钮
            Button btnMerge = new Button();
            btnMerge.Text = "合并文件";
            btnMerge.Location = new System.Drawing.Point(140, 100);
            btnMerge.Click += BtnMerge_Click;
            Controls.Add(btnMerge);


            btnBrowseFile1.Size = new System.Drawing.Size(100, 30);
            btnBrowseFile2.Size = new System.Drawing.Size(100, 30);
            btnMerge.Size = new System.Drawing.Size(100,30);
        }



        private void BtnBrowseFile1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 显示选择的文件路径到文件1的文本框
                TextBox txtFile1 = Controls.Find("txtFile1", true)[0] as TextBox;
                txtFile1.Text = openFileDialog.FileName;
            }
        }

        private void BtnBrowseFile2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 显示选择的文件路径到文件2的文本框
                TextBox txtFile2 = Controls.Find("txtFile2", true)[0] as TextBox;
                txtFile2.Text = openFileDialog.FileName;
            }
        }

        private void BtnMerge_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取文件1和文件2的路径
                TextBox txtFile1 = Controls.Find("txtFile1", true)[0] as TextBox;
                TextBox txtFile2 = Controls.Find("txtFile2", true)[0] as TextBox;

                string file1Path = txtFile1.Text;
                string file2Path = txtFile2.Text;

                // 检查文件是否存在
                if (!File.Exists(file1Path) || !File.Exists(file2Path))
                {
                    MessageBox.Show("请选择两个存在的文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 读取文件内容
                string file1Content = File.ReadAllText(file1Path);
                string file2Content = File.ReadAllText(file2Path);

                // 创建新文件路径
                string newFilePath = Path.Combine(Application.StartupPath, "Data", "merged_file.txt");

                // 合并文件内容
                string mergedContent = file1Content + Environment.NewLine + file2Content;

                // 写入新文件
                File.WriteAllText(newFilePath, mergedContent);

                MessageBox.Show("文件合并完成！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
