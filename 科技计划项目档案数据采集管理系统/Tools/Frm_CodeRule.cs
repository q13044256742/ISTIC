using System;
using System.Data;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_CodeRule : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 专项ID
        /// </summary>
        private object specialId;
        public Frm_CodeRule(object specialId)
        {
            InitializeComponent();
            this.specialId = specialId;
        }

        private void chk_Water_CheckedChanged(object sender, EventArgs e)
        {
            num_Water.Enabled = chk_Water.Checked;
            chk_Year.Enabled = chk_KT_Code.Enabled = chk_ZX_Code.Enabled = chk_Unit.Enabled = !chk_Water.Checked;
            lbl_Template.Text = lbl_Template.Text.Replace("0", string.Empty);
            if(chk_Water.Checked)
            {
                int length = (int)num_Water.Value;
                for(int i = 0; i < length; i++)
                    lbl_Template.Text += "0";
            }
            else if(!string.IsNullOrEmpty(txt_Mdi.Text))
            {
                if(!string.IsNullOrEmpty(lbl_Template.Text))
                {
                    int startIndex = lbl_Template.Text.LastIndexOf(txt_Mdi.Text) + 1;
                    if(startIndex < lbl_Template.Text.Length)
                        lbl_Template.Text = lbl_Template.Text.Remove(startIndex);
                }
            }
        }

        private void Frm_CodeRule_Load(object sender, EventArgs e)
        {
            lbl_Template.Text = string.Empty;
            txt_Mdi.Tag = txt_Mdi.Text;
            cbo_Type.Items.AddRange(new string[]
            {
                "档号", "馆藏号"
            });
            cbo_Type.SelectedIndex = 0;
        }

        private void chk_Code_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_ZX_Code.Checked)
            {
                lbl_Template.Text += "AAAA" + txt_Mdi.Text;
            }
            else
            {
                lbl_Template.Text = lbl_Template.Text.Replace("AAAA" + txt_Mdi.Text, string.Empty);
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            chk_Year.Checked = chk_KT_Code.Checked = chk_ZX_Code.Checked = chk_Unit.Checked = chk_Water.Checked = false;
            num_Water.Value = num_Water.Minimum;
            lbl_Template.ResetText();
            txt_Fixed.ResetText();
        }

        private void num_Water_ValueChanged(object sender, EventArgs e)
        {
            if(chk_Water.Checked)
            {
                lbl_Template.Text = lbl_Template.Text.Replace("0", string.Empty);
                int length = (int)num_Water.Value;
                for(int i = 0; i < length; i++)
                    lbl_Template.Text += "0";
            }
        }

        private void chk_KT_Code_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_KT_Code.Checked)
            {
                lbl_Template.Text += "BBBB" + txt_Mdi.Text;
            }
            else
            {
                lbl_Template.Text = lbl_Template.Text.Replace("BBBB" + txt_Mdi.Text, string.Empty);
            }
        }

        private void chk_Unit_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_Unit.Checked)
            {
                lbl_Template.Text += "CCCC" + txt_Mdi.Text;
            }
            else
            {
                lbl_Template.Text = lbl_Template.Text.Replace("CCCC" + txt_Mdi.Text, string.Empty);
            }
        }

        private void chk_Year_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_Year.Checked)
            {
                lbl_Template.Text += "YYYY" + txt_Mdi.Text;
            }
            else
            {
                lbl_Template.Text = lbl_Template.Text.Replace("YYYY" + txt_Mdi.Text, string.Empty);
            }
        }

        private void txt_Mdi_TextChanged(object sender, EventArgs e)
        {
            if(txt_Mdi.Tag.ToString().Length > 0)
            {
                lbl_Template.Text = lbl_Template.Text.Replace(txt_Mdi.Tag.ToString(), txt_Mdi.Text);
                txt_Mdi.Tag = txt_Mdi.Text;
            }
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if(cbo_Type.Tag == null)
            {
                cbo_Type.Tag = Guid.NewGuid().ToString();
                object type = cbo_Type.SelectedIndex;
                object symbol = txt_Mdi.Text;
                object[] values = lbl_Template.Text.Split(txt_Mdi.Text.ToCharArray());
                string insertSql = "INSERT INTO code_rule(cr_id, cr_type, cr_fixed, ";
                for(int i = 0; i < values.Length; i++)
                    insertSql += $"cr_param_{i + 1},";
                insertSql += $" cr_split_symbol, cr_template, cr_create_date, cr_special_id) VALUES ('{cbo_Type.Tag}', '{type}', '{txt_Fixed.Text}', ";
                for(int i = 0; i < values.Length; i++)
                    insertSql += $"'{values[i]}', ";
                insertSql += $"'{symbol}', '{lbl_Template.Text}', '{DateTime.Now}', '{UserHelper.GetInstance().User.UserKey}')";

                SqlHelper.ExecuteNonQuery(insertSql);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功。");
            }
            else
            {
                SqlHelper.ExecuteNonQuery($"DELETE FROM code_rule WHERE cr_id='{cbo_Type.Tag}'");
                cbo_Type.Tag = null;
                Btn_Save_Click(sender, e);
            }
        }

        private void Cbo_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Reset_Click(sender, e);

            int index = cbo_Type.SelectedIndex;
            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT cr_id, cr_template, cr_fixed, cr_split_symbol FROM code_rule WHERE cr_type='{index}'");
            if(row != null)
            {
                cbo_Type.Tag = row["cr_id"];
                string template = GetValue(row["cr_template"]);
                if(template.Contains("AAAA"))
                    chk_ZX_Code.Checked = true;
                if(template.Contains("BBBB"))
                    chk_KT_Code.Checked = true;
                if(template.Contains("CCCC"))
                    chk_Unit.Checked = true;
                if(template.Contains("YYYY"))
                    chk_Year.Checked = true;
                num_Water.Value = num_Water.Minimum;
                string water = template.Replace("YYYY", string.Empty);
                if(water.Contains("0"))
                {
                    chk_Water.Checked = true;
                    foreach(char c in water)
                        if(c.Equals('0'))
                            num_Water.Value += 1;
                }
                lbl_Template.Text = template;
                txt_Fixed.Text = GetValue(row["cr_fixed"]);
                txt_Mdi.Text = GetValue(row["cr_split_symbol"]);
            }
        }

        private string GetValue(object v) => v == null ? string.Empty : v.ToString();
    }
}
