using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_Add : Form
    {
        private bool isAdd;
        private string pId;

            
        public Frm_Add(bool isAdd, string pId)
        {
            this.isAdd = isAdd;
            InitializeComponent();
            this.pId = pId;
            if (isAdd)
                Load_name(pId);
            //else
            //    LoadData(pId);
        }


        private void Load_name(string pId)
        {
            string querySql = $"SELECT dd_name FROM data_dictionary where dd_id = '{pId}'";
            String dd_name = (String)SqlHelper.ExecuteOnlyOneQuery(querySql);

            //给文本框赋值
            txt_Search.Text = dd_name;
            txt_Search.Tag = pId;
        }

        private void btn_save(object sender, EventArgs e)
        {
            if (!ValidData())
            {
                MessageBox.Show("请先将表单信息补充完整!");
                return;
            }
            if (MessageBox.Show("确定要保存当前数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                //保存基本信息      
                string dd_id = Guid.NewGuid().ToString();
                string dd_name = textBox1.Text.Trim();
                string dd_code = textBox2.Text.Trim();
                string dd_sort = textBox3.Text.Trim();
                string dd_node = txt_Remark.Text.Trim();
                string  dd_pId = (string)txt_Search.Tag;


                //新增信息
                if (isAdd)
                {
                    string querySql = $"insert into data_dictionary  (dd_id, dd_name,dd_pId,dd_code,dd_note,dd_sort)values('{dd_id}','{dd_name}','{dd_pId}','{dd_code}','{dd_node}','{dd_sort}')";
                    SqlHelper.ExecuteQuery(querySql);
                }
                //更新信息
                //else
                //{
                //    StringBuilder basicInfo_QuerySql = new StringBuilder("UPDATE transfer_registration_pc SET ");
                //    basicInfo_QuerySql.Append("com_id='" + registration.SourceUnit + "',");
                //    basicInfo_QuerySql.Append("trp_name='" + registration.BatchName + "',");
                //    basicInfo_QuerySql.Append("trp_code='" + registration.BatchCode + "',");
                //    basicInfo_QuerySql.Append("trp_log_data='" + registration.TransferTime + "',");
                //    basicInfo_QuerySql.Append("trp_receiver='" + registration.Receive + "',");
                //    basicInfo_QuerySql.Append("trp_giver='" + registration.Giver + "',");
                //    basicInfo_QuerySql.Append("trp_remark='" + registration.Remark + "',");
                //    basicInfo_QuerySql.Append("trp_cd_amount='" + registration.TrpCdAmount + "',");
                //    basicInfo_QuerySql.Append("trp_attachment_id='" + registration.FileUpload + "',");
                //    basicInfo_QuerySql.Append("trp_status=1,");
                //    basicInfo_QuerySql.Append("trp_people='" + string.Empty + "',");
                //    basicInfo_QuerySql.Append("trp_handle_time='" + DateTime.Now + "'");
                //    basicInfo_QuerySql.Append(" WHERE trp_id='" + unitCode + "'");
                //    SqlHelper.ExecuteNonQuery(basicInfo_QuerySql.ToString());

                //    //保存光盘基本信息【先删除当前批次下的所有光盘，再执行新增】
                //    SqlHelper.ExecuteNonQuery("DELETE FROM transfer_registraion_cd WHERE trp_id = '" + unitCode + "'");
                //    for (int i = 0; i < dgv_CDlist.RowCount - 1; i++)
                //    {
                //        string cdName = dgv_CDlist.Rows[i].Cells["gpmc"].Value.ToString();
                //        string cdCode = dgv_CDlist.Rows[i].Cells["gpbh"].Value.ToString();
                //        string cdRemark = GetString(dgv_CDlist.Rows[i].Cells["bz"].Value);
                //        CD cd = new CD()
                //        {
                //            TrcId = Guid.NewGuid().ToString(),
                //            TrcName = cdName,
                //            TrcCode = cdCode,
                //            TrpId = unitCode,
                //            TrcRemark = cdRemark,
                //            TrcStrtus = (int)ReadStatus.NonRead,
                //            TrcPeople = string.Empty,
                //            TrpHandleTime = DateTime.Now
                //        };
                //        StringBuilder cdInfo_querySql = new StringBuilder("INSERT INTO transfer_registraion_cd ");
                //        cdInfo_querySql.Append("(trc_id,trc_name,trc_code,trp_id,trc_remark,trc_status,trc_people,trc_handle_time)");
                //        cdInfo_querySql.Append(" VALUES(");
                //        cdInfo_querySql.Append("'" + cd.TrcId + "',");
                //        cdInfo_querySql.Append("'" + cd.TrcName + "',");
                //        cdInfo_querySql.Append("'" + cd.TrcCode + "',");
                //        cdInfo_querySql.Append("'" + cd.TrpId + "',");
                //        cdInfo_querySql.Append("'" + cd.TrcRemark + "',");
                //        cdInfo_querySql.Append("'" + cd.TrcStrtus + "',");
                //        cdInfo_querySql.Append("'" + cd.TrcPeople + "',");
                //        cdInfo_querySql.Append("'" + cd.TrpHandleTime + "')");
                //        SqlHelper.ExecuteNonQuery(cdInfo_querySql.ToString());
                //    }
                //}
                if (MessageBox.Show((isAdd ? "添加" : "更新") + "成功，是否返回列表页", "恭喜", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        /// <summary>
        /// 检验数据的完整性（名称和编码必填）
        /// </summary>
        private bool ValidData()
        {          
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) || string.IsNullOrEmpty(textBox2.Text.Trim()))
                return false;
            return true;
        }


    }  
}
