using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.CompilerServices;

namespace MMUIEditor
{
    public partial class Form1 : Form
    {
        ObjectManage m_ObjManager = new ObjectManage();

        public Form1()
        {
            InitializeComponent();

            // Add default root node
            treeViewObjects.Nodes.Add("ROOT");
        }

        private void newImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewNode("image");
        }

        private void newGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewNode("group");
        }

        //
        private void AddNewNode(string strCategory)
        {
            try
            {
                TreeNode node = treeViewObjects.SelectedNode;

                //
                if (node == null)
                {
                    Util.Log("Not have any node selected.");
                    return;
                }

                //
                String new_label = "";

                switch (strCategory)
                {
                    case "group":
                        new_label = m_ObjManager.AddNewGroup(node.Text, OnNotifyPropertyChanged);
                        break;
                    case "image":
                        new_label = m_ObjManager.AddNewImage(node.Text, OnNotifyPropertyChanged);
                        break;
                    case "test":
                        new_label = m_ObjManager.AddNewTest(node.Text, OnNotifyPropertyChanged);
                        break;
                    default:
                        break;
                }

                if (new_label != "")
                {
                    node.Nodes.Add(new_label);
                    node.Expand();
                }
            }
            catch (Exception e)
            {
                Util.Log(string.Format("Exception : {0}", e.Message));
            }
        }

        private void treeViewObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                object obj = m_ObjManager.GetObject(treeViewObjects.SelectedNode.Text);

                if (obj == null) return;

                propertyGrid1.SelectedObject = obj;

                //if (obj is ImageStruct)
                //{
                //    propertyGrid1.SelectedObject = obj as ImageStruct;
                //    return;
                //}
                //if (obj is GroupStruct)
                //{
                //    propertyGrid1.SelectedObject = obj as GroupStruct;
                //    return;
                //}
                //if (obj is TestStruct)
                //{
                //    propertyGrid1.SelectedObject = obj as TestStruct;
                //    return;
                //}
            }
            catch (Exception ex)
            {
                Util.Log(string.Format("Exception : {0}", ex.Message));
            }
        }

        //
        private void OnNotifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string[] piece = e.PropertyName.Split('_');

            try
            {
                if(piece.Length < 2) return;

                if (piece[0] == "Name")
                {
                    TreeNode node = treeViewObjects.SelectedNode;
                    if (node == null)
                    {
                        Util.Log("Not have any node selected.");
                        return;
                    }
                    string[] node_piece = node.Text.Split('_');
                    if(node_piece.Length >= 2)
                    {
                        node.Text = string.Format("{0}_{1}", node_piece[0], piece[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                Util.Log(string.Format("Exception : {0}", ex.Message));
            }
        }

        private void newTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewNode("test");
        }
    }
}
