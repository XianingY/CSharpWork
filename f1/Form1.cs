using System;


namespace f1
{
    public partial class Form1 : Form
    {
        private TreeView treeView;
        private ListView listView;

        public Form1()
        {
            // 初始化窗口组件
            this.Text = "简单的文件浏览器";
            this.Size = new System.Drawing.Size(800, 600);

            // 创建并配置TreeView
            this.treeView = new TreeView();
            this.treeView.Dock = DockStyle.Left;
            this.treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);

            // 创建并配置ListView
            this.listView = new ListView();
            this.listView.Dock = DockStyle.Fill;
            this.listView.View = View.List;
            this.listView.MouseDoubleClick += new MouseEventHandler(listView_MouseDoubleClick);

            // 将TreeView和ListView添加到窗口
            this.Controls.Add(this.listView);
            this.Controls.Add(this.treeView);

            // 加载用户目录
            LoadDirectories(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadDirectories(string dir)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(dir);
                TreeNode node = new TreeNode(di.Name);
                node.Tag = di.FullName;
                node.Nodes.Add("");  // 添加一个空节点以启用展开图标
                this.treeView.Nodes.Add(node);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.listView.Items.Clear();
            TreeNode node = e.Node;
            string path = (string)node.Tag;
            node.Nodes.Clear();

            try
            {
                // 加载子目录
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    TreeNode subNode = new TreeNode(di.Name);
                    subNode.Tag = di.FullName;
                    subNode.Nodes.Add("");
                    node.Nodes.Add(subNode);
                }

                // 加载文件
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    ListViewItem item = new ListViewItem(fi.Name);
                    item.Tag = fi.FullName;
                    this.listView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = this.listView.GetItemAt(e.X, e.Y);
            string path = (string)item.Tag;

            if (Path.GetExtension(path) == ".txt")
            {
                System.Diagnostics.Process.Start("notepad.exe", path);
            }
        }
    }
}