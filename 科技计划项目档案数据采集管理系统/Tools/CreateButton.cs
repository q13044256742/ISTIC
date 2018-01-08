using System.Drawing;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Tools
{
    class CreateButton
    {
        public static Button ByName(string name, Bitmap map) {
            Button btn = new Button();
            btn.BackColor = Color.Transparent;
            btn.Image = map;
            return btn;
        }
    }
}
