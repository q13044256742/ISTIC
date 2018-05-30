using System;
using System.Collections.Generic;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_AddFile_FileSelect : DevExpress.XtraEditors.XtraForm
    {
        public string SelectedFileName;
        public string SelectedFileId;
        private ImageList imageList;
        private object[] rootId;
        public Frm_AddFile_FileSelect(object[] rootId)
        {
            InitializeComponent();
            this.rootId = rootId;
            LoadRootTree(chk_ShowAll.Checked);
        }

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();

        private void Frm_AddFile_FileSelect_Load(object sender, EventArgs e)
        {
            imageList = new ImageList();
            //0：文件夹关闭 1：文件夹打开 2：文件 3：已加工
            imageList.Images.AddRange(new System.Drawing.Image[] {
               // Resources.file2, Resources.file, Resources.file, Resources._lock
            });
            tv_file.ImageList = imageList;
        }

        /// <summary>
        /// 判断指定文件夹节点下的所有文件是否全部已加工，如果是，则移除此文件夹
        /// </summary>
        private bool ClearHasWordedWithFolder(TreeNode node)
        {
            bool result = true;
            foreach(TreeNode item in node.Nodes)
            {
                int type = Convert.ToInt32(item.ToolTipText);//0:文件 1:文件夹
                if(type == 1)
                    result = ClearHasWordedWithFolder(item);
            }
            if(result)
            {
                foreach(TreeNode item in node.Nodes)
                {
                    int type = Convert.ToInt32(item.ToolTipText);//0:文件 1:文件夹
                    int state = item.ImageIndex;//3:已加工
                    if(type == 0 && state != 3)
                    {
                        result = false;
                        break;
                    }
                }
            }
            if(result)
                node.Remove();
            return result;
        }

        private void tv_file_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            int type = Convert.ToInt32(node.ToolTipText);//0:文件 1:文件夹
            int state = node.ImageIndex;//3:已加工
            if(type == 0 && state != 3)
            {
                SelectedFileId = node.Name;
                lbl_filename.Text = node.Text;
                SelectedFileName = node.Tag + "\\" + node.Text;
            }
            else
            {
                lbl_filename.Text = string.Empty;
                SelectedFileName = string.Empty;
                SelectedFileId = string.Empty;
            }
        }

        private void btn_sure_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(SelectedFileName))
                DialogResult = DialogResult.OK;
            Close();
        }

        private void chk_ShowAll_CheckedChanged(object sender, EventArgs e)
        {
            LoadRootTree(chk_ShowAll.Checked);
        }

        /// <summary>
        /// 加载根节点树（调用树节点方法）
        /// </summary>
        /// <param name="isShowAll">是否显示已加工节点</param>
        private void LoadRootTree(bool isShowAll)
        {
            tv_file.Nodes.Clear();
            for(int i = 0; i < rootId.Length; i++)
            {
                object[] objs = SqlHelper.ExecuteRowsQuery($"SELECT bfi_id, bfi_name, bfi_path, bfi_type FROM backup_files_info WHERE bfi_id='{rootId[i]}'");
                TreeNode treeNode = new TreeNode()
                {
                    Name = GetValue(objs[0]),
                    Text = GetValue(objs[1]),
                    Tag = GetValue(objs[2]),
                    ToolTipText = GetValue(objs[3]),
                };
                tv_file.Nodes.Add(treeNode);
                InitialTree(rootId[i], treeNode, isShowAll);
            }
            if(tv_file.Nodes.Count > 0)
            {
                tv_file.Nodes[0].Expand();
                if(!chk_ShowAll.Checked)
                {
                    ClearHasWordedWithFolder(tv_file.Nodes[0]);
                }
            }
        }

        /// <summary>
        /// 生成树节点
        /// </summary>
        /// <param name="parentId">父级节点ID</param>
        /// <param name="parentNode">父级节点</param>
        /// <param name="isShowAll">是否显示已加工节点</param>
        private void InitialTree(object parentId, TreeNode parentNode, bool isShowAll)
        {
            List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT bfi_id, bfi_name, bfi_path, bfi_state, bfi_type FROM backup_files_info WHERE bfi_pid='{parentId}' ORDER BY rowid", 5);
            for(int i = 0; i < list.Count; i++)
            {
                int state = Convert.ToInt32(list[i][3]);
                if(state != 1 || isShowAll)
                {
                    TreeNode treeNode = new TreeNode()
                    {
                        Name = GetValue(list[i][0]),
                        Text = GetValue(list[i][1]),
                        Tag = GetValue(list[i][2]),
                        ImageIndex = (state == 1) ? 3 : -1,
                        ToolTipText = GetValue(list[i][4]),
                    };
                    parentNode.Nodes.Add(treeNode);
                    InitialTree(treeNode.Name, treeNode, isShowAll);
                }
            }
            if(list.Count == 0)
            {
                if(parentNode.ImageIndex != 3)
                    parentNode.ImageIndex = parentNode.SelectedImageIndex = 2;
                else if(parentNode.ImageIndex == 3)
                    parentNode.SelectedImageIndex = 3;
            }
        }
    }
}
