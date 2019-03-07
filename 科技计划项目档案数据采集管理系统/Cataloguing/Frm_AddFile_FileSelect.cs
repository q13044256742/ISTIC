using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_FileSelect : DevExpress.XtraEditors.XtraForm
    {
        public string[] SelectedFileName;
        public string[] SelectedFileId;
        private ImageList imageList;
        private object[] rootId;

        /// <summary>
        /// 标记是否是移动模式[默认false]
        /// </summary>
        public bool IsMoveMode = false;
        /// <summary>
        /// 批次ID
        /// </summary>
        public object BatchID;
        /// <summary>
        /// 光盘编号
        /// </summary>
        public object DiskCode;
        public Frm_FileSelect(object[] rootId)
        {
            InitializeComponent();
            this.rootId = rootId;
            LoadRootTree(chk_ShowAll.Checked);
        }

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
            bool flag = true;
            for(int i = 0; i < node.Nodes.Count; i++)
            {
                TreeNode item = node.Nodes[i];
                int type = Convert.ToInt32(item.ToolTipText);//0:文件 1:文件夹
                if(type == 0)
                    flag = false;
                else if(type == 1)
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
            if(result && flag)
                if(!string.IsNullOrEmpty(ToolHelper.GetValue(node.Tag)))//批次名称永不消逝
                    node.Remove();
            return result;
        }

        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            SelectedFileId = new string[lsv_Selected.Items.Count];
            SelectedFileName = new string[lsv_Selected.Items.Count];
            for(int i = 0; i < SelectedFileId.Length; i++)
            {
                SelectedFileId[i] = lsv_Selected.Items[i].Name;
                SelectedFileName[i] = ToolHelper.GetValue(lsv_Selected.Items[i].Tag);
            }
            if(SelectedFileId.Length > 0)
                DialogResult = DialogResult.OK;
            Close();
        }

        private void Chk_ShowAll_CheckedChanged(object sender, EventArgs e)
        {
            LoadRootTree(chk_ShowAll.Checked);
        }

        /// <summary>
        /// 加载根节点树（调用树节点方法）
        /// </summary>
        /// <param name="isShowAll">是否显示已加工节点</param>
        private void LoadRootTree(bool isShowAll)
        {
            object batchCode = SqlHelper.ExecuteOnlyOneQuery($"SELECT bfi_name FROM backup_files_info WHERE bfi_id='{rootId[0]}'");
            tv_file.Nodes.Clear();
            //一级节点【批次编号】
            TreeNode batchNode = new TreeNode()
            {
                Text = ToolHelper.GetValue(batchCode),
                ToolTipText = "-1",
            };
            tv_file.Nodes.Add(batchNode);
            for(int i = 0; i < rootId.Length; i++)
            {
                object[] objs = SqlHelper.ExecuteRowsQuery("SELECT bfi_id, bfi_name, bfi_path, bfi_type, trc_code FROM backup_files_info a " +
                    $"LEFT JOIN transfer_registraion_cd b on a.bfi_trcid = b.trc_id WHERE bfi_id='{rootId[i]}'");
                //二级节点【光盘编号】
                string rootFileName = ToolHelper.GetValue(objs[4]);
                TreeNode diskNode = new TreeNode()
                {
                    Text = rootFileName,
                    ToolTipText = ToolHelper.GetValue(objs[3]),
                };
                batchNode.Nodes.Add(diskNode);
                InitialTree(rootId[i], diskNode, isShowAll);
            }
            if(tv_file.Nodes.Count > 0)
            {
                tv_file.Nodes[0].Expand();
                if(!chk_ShowAll.Checked)
                {
                    ClearHasWordedWithFolder(tv_file.Nodes[0]);
                }
                if (IsMoveMode)
                {
                    tv_file.ExpandAll();
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
            List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT bfi_id, bfi_name, bfi_path, bfi_state, bfi_type FROM backup_files_info WHERE bfi_pid='{parentId}' ORDER BY bfi_type, bfi_name", 5);
            for(int i = 0; i < list.Count; i++)
            {
                int state = ToolHelper.GetIntValue(list[i][3], 0);
                if(state != 1 || isShowAll)
                {
                    int imageIndex = GetFileIconIndex(state, ToolHelper.GetValue(list[i][1]), list[i][4]);
                    TreeNode treeNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(list[i][0]),
                        Text = ToolHelper.GetValue(list[i][1]),
                        Tag = ToolHelper.GetValue(list[i][2]),
                        ImageIndex = imageIndex,
                        SelectedImageIndex = imageIndex,
                        ToolTipText = ToolHelper.GetValue(list[i][4]),
                        StateImageKey = state.ToString(),
                    };
                    parentNode.Nodes.Add(treeNode);
                    InitialTree(treeNode.Name, treeNode, isShowAll);
                }
            }
        }

        private int GetFileIconIndex(int state, string fileName, object type)
        {
            int _type = Convert.ToInt32(type);
            //文件夹
            if(_type == 1)
                return 0;
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

        private void Tv_file_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Tv_file_NodeMouseClick(sender, e);
            if(lsv_Selected.Items.Count > 0)
                Btn_Sure_Click(null, null);
        }

        private void Tv_file_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            int type = Convert.ToInt32(node.ToolTipText);//0:文件 1:文件夹
            if (e.Button == MouseButtons.Left)
            {
                string state = node.StateImageKey;//1:已加工
                if (type == 0 && !"1".Equals(state))
                {
                    ListViewItem item = new ListViewItem()
                    {
                        Name = node.Name,
                        Text = node.Text,
                        Tag = node.Tag + "\\" + node.Text
                    };
                    if (Control.ModifierKeys != Keys.Control)
                        lsv_Selected.Items.Clear();
                    if (IsNotExist(item))
                        ChangeItem(item, true);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (IsMoveMode && type == 0)//只能操作文件
                {
                    tv_file.SelectedNode = e.Node;
                    contextMenuStrip1.Show(MousePosition);
                }
            }
        }
        private bool IsNotExist(ListViewItem item)
        {
            bool flag = true;
            foreach(ListViewItem _item in lsv_Selected.Items)
            {
                if(_item.Name.Equals(item.Name))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        /// <summary>
        /// 添加Item
        /// </summary>
        /// <param name="isAdd">是否新增</param>
        private void ChangeItem(ListViewItem item, bool isAdd)
        {
            lsv_Selected.BeginUpdate();
            if(isAdd)
                lsv_Selected.Items.Add(item);
            else
                lsv_Selected.Items.Remove(item);
            lsv_Selected.EndUpdate();
            label1.Text = $"当前已选择文件数({lsv_Selected.Items.Count})：";
        }

        private void Lb_Selected_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                foreach(ListViewItem item in lsv_Selected.SelectedItems)
                    ChangeItem(item, false);
            }
        }

        private void 移动MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object value = tv_file.SelectedNode.Text;
            object id = tv_file.SelectedNode.Name;
            Frm_FileMove fileMove = new Frm_FileMove(BatchID, DiskCode);
            fileMove.Text = $"将[{value}]移动至...";
            fileMove.objectName = value;
            fileMove.objectId = id;
            if(fileMove.ShowDialog() == DialogResult.OK)
            {
                LoadRootTree(chk_ShowAll.Checked);
            }
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object value = tv_file.SelectedNode.Text;
            object id = tv_file.SelectedNode.Name;
            string queryString = $"确定要删除文件[{value}]吗？";
            if (XtraMessageBox.Show(queryString, "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string deleteSQL = $"DELETE FROM backup_files_info WHERE bfi_id='{id}'";
                SqlHelper.ExecuteNonQuery(deleteSQL);
                XtraMessageBox.Show("操作成功。");
                tv_file.SelectedNode.Remove();
            }
        }
    }
}
