using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_Print : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 打印类型
        /// 1：齐备 
        /// -：不齐备
        /// </summary>
        private int type;
        private object trpId;
        public Frm_Print(int type, object trpId)
        {
            InitializeComponent();
            this.type = type;
            this.trpId = trpId;
            if(type == 1)//齐备
            {
                lbl_1.Text = "档案接收确认函";
                lbl_2.Text = "文件列表清单";
                lbl_3.Visible = chk_3.Visible = false;
            }
            else
            {
                lbl_1.Text = "档案催报单";
                lbl_2.Text = "缺失文件清单";
                lbl_3.Text = "文件列表清单";
            }
        }

        private void Lbl_1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel label = (sender as LinkLabel);
            if(type == 1)
            {
                if("1".Equals(label.Tag))
                {
                    new Frm_DomRec(trpId).ShowDialog();
                }
                else if("2".Equals(label.Tag))
                {
                    if(MessageBox.Show("是否开始合成文件清单?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        fileRowList.Clear();
                        LoadFileList(trpId);
                        /* ----合成文件清单----*/
                        string filePath = Application.StartupPath + "\\重大专项项目（课题）档案交接清单";
                        MicrosoftWordHelper.WriteDocument(ref filePath, fileRowList);
                        if(MessageBox.Show("合成完毕, 是否需要现在打开?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                            Process.Start(filePath);
                        Close();
                    }
                }
            }
            else
            {
                if("1".Equals(label.Tag))
                {
                    new Frm_DomNeed(trpId).ShowDialog();
                }
                else if("2".Equals(label.Tag))
                {
                }
            }
        }
        private List<DataRow> fileRowList = new List<DataRow>();
        private void LoadFileList(object trpId)
        {
            string querySql = "SELECT wm.wm_type, wm.wm_obj_id FROM work_myreg wm " +
                "LEFT JOIN work_registration wr ON wm.wr_id = wr.wr_id " +
                $"WHERE wr.trp_id = '{trpId}' AND wm.wm_status = 3";
            List<object[]> vs = SqlHelper.ExecuteColumnsQuery(querySql, 2);
            for(int i = 0; i < vs.Count; i++)
            {
                int type = (int)vs[i][0];
                if(type == 0 || type == 1)
                    GetFileRow(vs[i][1]);
                else if(type == 2)
                {
                    GetFileRow(vs[i][1]);
                    List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{vs[i][1]}'", 1);
                    for(int j = 0; j < list2.Count; j++)
                    {
                        GetFileRow(list2[j][0]);
                        List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{list2[j][0]}'", 1);
                        for(int k = 0; k < list3.Count; k++)
                            GetFileRow(list3[k][0]);
                    }
                }
            }
        }

        private void GetFileRow(object objId)
        {
            DataRowCollection collection = SqlHelper.ExecuteQuery($"SELECT * FROM processing_file_list WHERE pfl_obj_id='{objId}'").Rows;
            foreach(DataRow item in collection)
                fileRowList.Add(item);
        }
    }
}
