using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FileBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDirectoryTree();
        }

        private void LoadDirectoryTree()
        {
            DirectoryInfo rootDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            TreeViewItem rootItem = CreateDirectoryNode(rootDirectory);
            DirectoryTree.Items.Add(rootItem);
        }

        private TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            TreeViewItem directoryNode = new TreeViewItem
            {
                Header = directoryInfo.Name,
                Tag = directoryInfo
            };
            directoryNode.Items.Add(null); 
            directoryNode.Expanded += DirectoryNode_Expanded;
            return directoryNode;
        }

        private void DirectoryNode_Expanded(object sender, RoutedEventArgs e)
        {
            if (sender is TreeViewItem item && item.Items[0] == null)
            {
                item.Items.Clear();
                DirectoryInfo directoryInfo = (DirectoryInfo)item.Tag;
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    item.Items.Add(CreateDirectoryNode(directory));
                }
            }
        }

        private void DirectoryTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DirectoryTree.SelectedItem is TreeViewItem selectedItem)
            {
                DirectoryInfo directoryInfo = (DirectoryInfo)selectedItem.Tag;
                FileList.Items.Clear();
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    FileList.Items.Add(new FileItem { Name = directory.Name, Type = "Folder" });
                }
                foreach (var file in directoryInfo.GetFiles())
                {
                    FileList.Items.Add(new FileItem { Name = file.Name, Type = file.Extension });
                }
            }
        }

        private void FileList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (FileList.SelectedItem is FileItem selectedItem && selectedItem.Type == ".txt")
            {
                string filePath = Path.Combine(((DirectoryInfo)((TreeViewItem)DirectoryTree.SelectedItem).Tag).FullName, selectedItem.Name);
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            DirectoryTree.Items.Clear();
            LoadDirectoryTree();
        }
    }

    public class FileItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
