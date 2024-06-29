using System;


namespace f1
{
    public partial class Form1 : Form
    {
        private TreeView treeView;
        private ListView listView;

        public Form1()
        {
       
            this.Text = "Window";
            this.Size = new System.Drawing.Size(800, 600);

           
            this.treeView = new TreeView();
            this.treeView.Dock = DockStyle.Left;
            this.treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);

      
            this.listView = new ListView();
            this.listView.Dock = DockStyle.Fill;
            this.listView.View = View.List;
            this.listView.MouseDoubleClick += new MouseEventHandler(listView_MouseDoubleClick);

           
            this.Controls.Add(this.listView);
            this.Controls.Add(this.treeView);

      
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
                node.Nodes.Add("");  
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
              
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    TreeNode subNode = new TreeNode(di.Name);
                    subNode.Tag = di.FullName;
                    subNode.Nodes.Add("");
                    node.Nodes.Add(subNode);
                }

                
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