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
            imageList.Images.AddRange(new System.Drawing.Image[] {
                Resources.file2, Resources._lock, Resources.file, Resources.doc, Resources.xsl, Resources.pdf, Resources.rar
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
            string state = node.StateImageKey;//1:已加工
            if(type == 0 && !"1".Equals(state))
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
            List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT bfi_id, bfi_name, bfi_path, bfi_state, bfi_type FROM backup_files_info WHERE bfi_pid='{parentId}'", 5);
            for(int i = 0; i < list.Count; i++)
            {
                int state = Convert.ToInt32(list[i][3]);
                if(state != 1 || isShowAll)
                {
                    int imageIndex = GetFileIconIndex(state, GetValue(list[i][1]));
                    TreeNode treeNode = new TreeNode()
                    {
                        Name = GetValue(list[i][0]),
                        Text = GetValue(list[i][1]),
                        Tag = GetValue(list[i][2]),
                        ImageIndex = imageIndex,
                        SelectedImageIndex = imageIndex,
                        ToolTipText = GetValue(list[i][4]),
                        StateImageKey = state.ToString(),
                    };
                    parentNode.Nodes.Add(treeNode);
                    InitialTree(treeNode.Name, treeNode, isShowAll);
                }
            }
        }

        private int GetFileIconIndex(int state, string fileName)
        {
            //小锁
            if(state == 1)
                return 1;
            else
            {
                string format = System.IO.Path.GetExtension(fileName).ToUpper();
                if(".DOC".Equals(format) || ".DOCX".Equals(format))
                    return 3;
                else if(".XLS".Equals(format) || ".XLSX".Equals(format))
                    return 4;
                else if(".PDF".Equals(format))
                    return 5;
                else if(".RAR".Equals(format))
                    return 6;
            }
            return 2;
        }
    }
}
