using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_CG : Form
    {
        public Frm_CG()
        {
            InitializeComponent();
        }

        private void Frm_CG_Load(object sender, EventArgs e)
        {
            InitialLeftMenu();

            LoadPCList(null, null);

            //列头样式
            dgv_WorkLog.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            //默认来源单位
            LoadCompanyList();
        }

        /// <summary>
        /// 加载来源单位ComboBox下拉表
        /// </summary>
        private void LoadCompanyList()
        {
            string querySql = "SELECT cs_id,cs_name FROM company_source ORDER BY sorting ASC";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            DataRow dataRow = table.NewRow();
            dataRow["cs_id"] = "all";
            dataRow["cs_name"] = "全部来源单位";
            table.Rows.InsertAt(dataRow, 0);
            cbo_CompanyList.DataSource = table;
            cbo_CompanyList.DisplayMember = "cs_name";
            cbo_CompanyList.ValueMember = "cs_id";
            cbo_CompanyList.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载批次列表
        /// </summary>
        /// <param name="querySql">指定的查询语句</param>
        /// <param name="csid">来源单位ID</param>
        private void LoadPCList(StringBuilder querySql, object csid)
        {
            dgv_WorkLog.DataSource = null;
            dgv_WorkLog.Columns.Clear();
            dgv_WorkLog.Rows.Clear();

            DataTable dataTable = null;
            if (querySql == null)
            {
                //加载实物登记数据【默认加载状态为2（已提交）的数据】
                querySql = new StringBuilder("SELECT ");
                querySql.Append("pc.trp_id,");
                querySql.Append("cs_name,");
                querySql.Append("trp_name,");
                querySql.Append("trp_code,");
                querySql.Append("trp_cd_amount");
                querySql.Append(" FROM transfer_registration_pc pc LEFT JOIN company_source cs ON pc.com_id = cs.cs_id");
                querySql.Append(" WHERE trp_status=2");
                if (csid != null)
                    querySql.Append($" AND cs.cs_id='{csid}'");
            }
            dataTable = SqlHelper.ExecuteQuery(querySql.ToString());
            //将数据源转化成DataGridView表数据
            dgv_WorkLog.Columns.Add("trp_id", "主键");
            dgv_WorkLog.Columns.Add("cs_name", "来源单位");
            dgv_WorkLog.Columns.Add("trp_name", "批次名称");
            dgv_WorkLog.Columns.Add("trp_code", "批次编号");
            dgv_WorkLog.Columns.Add("trp_finishtime", "完成时间");
            dgv_WorkLog.Columns.Add("trp_cd_amount", "光盘数");
            dgv_WorkLog.Columns.Add("trp_control", "操作");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                int index = dgv_WorkLog.Rows.Add();
                dgv_WorkLog.Rows[index].Cells["trp_id"].Value = row["trp_id"];
                dgv_WorkLog.Rows[index].Cells["cs_name"].Value = row["cs_name"];
                dgv_WorkLog.Rows[index].Cells["trp_name"].Value = row["trp_name"];
                dgv_WorkLog.Rows[index].Cells["trp_code"].Value = row["trp_code"];
                dgv_WorkLog.Rows[index].Cells["trp_finishtime"].Value = null;//完成时间字段待定
                dgv_WorkLog.Rows[index].Cells["trp_cd_amount"].Value = row["trp_cd_amount"];
                dgv_WorkLog.Rows[index].Cells["trp_control"].Value = "加工";
            }
            //设置最小列宽
            dgv_WorkLog.Columns["cs_name"].MinimumWidth = 200;
            dgv_WorkLog.Columns["trp_name"].MinimumWidth = 250;
            //设置链接按钮样式
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new string[] { "trp_cd_amount", "trp_control" });
            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkLog, new string[] { "trp_cd_amount", "trp_control" }, true);

            dgv_WorkLog.Columns["trp_id"].Visible = false;

            dgv_WorkLog.Tag = "PC";

        }

        /// <summary>
        /// 加载左侧菜单
        /// </summary>
        private void InitialLeftMenu()
        {
            Image[] imgs = new Image[] { Resources.pic1, Resources.pic2, Resources.pic3, Resources.pic4, Resources.pic5, Resources.pic6, Resources.pic7, Resources.pic8 };
            List<CreateKyoPanel.KyoPanel> list = new List<CreateKyoPanel.KyoPanel>();
            list.Add(new CreateKyoPanel.KyoPanel
            {
                Name = "CG",
                Text = "著录加工",
                Image = imgs[0],
                HasNext = true
            });
            CreateKyoPanel.SetPanel(pal_LeftMenu, list);

            list.Clear();
            list.AddRange(new CreateKyoPanel.KyoPanel[]
            {
                new CreateKyoPanel.KyoPanel
                {
                    Name ="CG_LOGIN",
                    Text="加工登记",
                    HasNext = false
                },
                new CreateKyoPanel.KyoPanel
                {
                    Name ="CG_WORK_ING",
                    Text="加工中",
                    HasNext = false
                },
                new CreateKyoPanel.KyoPanel
                {
                    Name="CG_WORK_ED",
                    Text="已返工",
                    HasNext = false
                }
            });
            Panel parentPanel = pal_LeftMenu.Controls.Find("CG", false)[0] as Panel;
            CreateKyoPanel.SetSubPanel(parentPanel, list, Sub_Menu_Click);
        }

        /// <summary>
        /// 二级菜单点击事件（加工登记/加工中/已返工）
        /// </summary>
        private void Sub_Menu_Click(object sender, EventArgs e)
        {
            Panel panel = null;
            if (sender is Panel) panel = sender as Panel;
            else if (sender is Label) panel = (sender as Label).Parent as Panel;
            if ("CG_LOGIN".Equals(panel.Name))//加工登记
            {
                LoadPCList(null, null);
            }
        }

        /// <summary>
        /// 单元格点击事件
        /// </summary>
        private void dgv_WorkLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1 && e.ColumnIndex != -1)
            {
                //光盘数
                if ("trp_cd_amount".Equals(dgv_WorkLog.Columns[e.ColumnIndex].Name))
                {
                    object trpid = dgv_WorkLog.Rows[e.RowIndex].Cells["trp_id"].Value;
                    LoadCDList(trpid);
                }
            }
        }

        /// <summary>
        /// 加载光盘列表
        /// </summary>
        private void LoadCDList(object trpId)
        {
            dgv_WorkLog.Rows.Clear();
            dgv_WorkLog.Columns.Clear();

            dgv_WorkLog.Columns.Add("trc_id", "主键");
            dgv_WorkLog.Columns.Add("cs_name", "来源单位");
            dgv_WorkLog.Columns.Add("trc_code", "光盘编号");
            dgv_WorkLog.Columns.Add("trc_name", "光盘名称");
            dgv_WorkLog.Columns.Add("trc_total_amount", "总数");
            dgv_WorkLog.Columns.Add("trc_receive_amount", "已领取数");
            dgv_WorkLog.Columns.Add("trc_file_amount", "文件数");
            dgv_WorkLog.Columns.Add("trc_control", "操作");

            DataTable table = null;
            StringBuilder querySql = new StringBuilder("SELECT trc_id,cs_name,trc_code,trc_name,trc_project_amount,trc_subject_amount,trc_status");
            querySql.Append(" FROM transfer_registraion_cd trc");
            querySql.Append(" LEFT JOIN(");
            querySql.Append(" SELECT trp.trp_id, cs_name,sorting FROM transfer_registration_pc trp, company_source cs WHERE trp.com_id = cs.cs_id ) tb");
            querySql.Append(" ON trc.trp_id = tb.trp_id");
            querySql.Append($" WHERE trc.trp_id='{trpId}'");
            querySql.Append(" ORDER BY sorting ASC, trc_code ASC");
            table = SqlHelper.ExecuteQuery(querySql.ToString());
            foreach (DataRow row in table.Rows)
            {
                int _index = dgv_WorkLog.Rows.Add();
                dgv_WorkLog.Rows[_index].Cells["trc_id"].Value = row["trc_id"];
                dgv_WorkLog.Rows[_index].Cells["cs_name"].Value = row["cs_name"];
                dgv_WorkLog.Rows[_index].Cells["trc_code"].Value = row["trc_code"];
                dgv_WorkLog.Rows[_index].Cells["trc_name"].Value = row["trc_name"];
                dgv_WorkLog.Rows[_index].Cells["trc_total_amount"].Value = GetInt32(row["trc_project_amount"]) + GetInt32(row["trc_subject_amount"]);
                dgv_WorkLog.Rows[_index].Cells["trc_receive_amount"].Value = 0;//已领取数待处理
                dgv_WorkLog.Rows[_index].Cells["trc_file_amount"].Value = 0;//文件数待处理
                dgv_WorkLog.Rows[_index].Cells["trc_control"].Value = "加工";
            }
            if (dgv_WorkLog.Columns.Count > 0)
                dgv_WorkLog.Columns[0].Visible = false;

            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            list.Add(new KeyValuePair<string, int>("cs_name", 250));
            list.Add(new KeyValuePair<string, int>("trc_code", 200));
            list.Add(new KeyValuePair<string, int>("trc_name", 200));

            list.Add(new KeyValuePair<string, int>("trc_total_amount", 90));
            list.Add(new KeyValuePair<string, int>("trc_receive_amount", 90));
            list.Add(new KeyValuePair<string, int>("trc_file_amount", 90));
            list.Add(new KeyValuePair<string, int>("trc_control", 100));
            DataGridViewStyleHelper.SetWidth(dgv_WorkLog, list);

            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkLog, new string[] { "trc_total_amount", "trc_control" }, true);
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new string[] { "trc_receive_amount", "trc_file_amount" });

            dgv_WorkLog.Tag = "CD";
        }

        /// <summary>
        /// 将Object对象转换成Int对象
        /// </summary>
        private int GetInt32(object _obj)
        {
            int temp = 0;
            if (_obj == null)
                return temp;
            int.TryParse(_obj.ToString(), out temp);
            return temp;
        }

        /// <summary>
        /// 返回上一页
        /// </summary>
        private void Btn_Back_Click(object sender, EventArgs e)
        {
            if ("CD".Equals(dgv_WorkLog.Tag))
            {
                LoadPCList(null, null);
            }
        }

        /// <summary>
        /// 来源单位切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_CompanyList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            object csid = cbo_CompanyList.SelectedValue;
            if ("all".Equals(csid))
            {
                LoadPCList(null, null);
            }
            else
            {
                LoadPCList(null, csid);
            }
        }
    }
}
