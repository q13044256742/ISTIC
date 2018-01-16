using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public class CreateKyoPanel
    {
        /// <summary>
        /// 默认整体背景色
        /// </summary>
        private static Color DEFAULT_BGCOLOR = Color.FromArgb(0, 91, 172);
        /// <summary>
        /// 默认选项板高度
        /// </summary>
        private static int DEFAULT_PANEL_HEIGHT = 75;
        /// <summary>
        /// 默认选项板背景色
        /// </summary>
        private static Color DEFAULT_PANEL_BGCOLOR = Color.FromArgb(90, 158, 223);
        /// <summary>
        /// 默认图标高度
        /// </summary>
        private static int DEFAULT_PIC_HEIGHT = 25;
        /// <summary>
        /// 默认图标宽度
        /// </summary>
        private static int DEFAULT_PIC_WIDTH = 25;
        /// <summary>
        /// 默认文本字体颜色
        /// </summary>
        private static Color DEFAULT_LABEL_FORECOLOR = Color.White;
        /// <summary>
        /// 默认文本字体
        /// </summary>
        private static Font DEFAULT_LABEL_FONT = new Font("微软雅黑", 11f);
        /// <summary>
        /// 默认二级菜单高度
        /// </summary>
        private static int DEFAULT_SUB_LABEL_HEIGHT = 40;
        /// <summary>
        /// 设置一级菜单
        /// </summary>
        /// <param name="parentPanel"></param>
        /// <param name="list"></param>
        public static void SetPanel(Panel parentPanel, List<KyoPanel> list)
        {
            parentPanel.BackColor = DEFAULT_BGCOLOR;

            for (int i = 0; i < list.Count; i++)
            {
                KyoPanel kyoPane = list[i];
                Panel panel = new Panel
                {
                    Width = parentPanel.Width,
                    Height = DEFAULT_PANEL_HEIGHT,
                    Name = kyoPane.Name,
                    Left = 0,
                    Top = i * DEFAULT_PANEL_HEIGHT - i,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = DEFAULT_LABEL_FONT,
                };

                PictureBox box = new PictureBox
                {
                    Image = kyoPane.Image,
                    Height = DEFAULT_PIC_HEIGHT,
                    Width = DEFAULT_PIC_WIDTH,
                    Location = new Point(30, 22),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                };

                Label label = new Label
                {
                    Text = kyoPane.Text,
                    AutoSize = true,
                    Location = new Point(80, 26),
                    ForeColor = DEFAULT_LABEL_FORECOLOR
                };
                if (kyoPane.HasNext)
                {
                    Label arrow = new Label
                    {
                        Name = kyoPane.Name + "_ARROW",
                        Text = "▲",
                        ForeColor = Color.White,
                        Location = new Point(207, 25),
                        Size = new Size(22, 22),
                        Font = new Font("微软雅黑", 10, FontStyle.Bold)
                    };
                    arrow.MouseClick += Panel_MouseClick;
                    panel.Controls.Add(arrow);
                }
                panel.MouseClick += Panel_MouseClick;
                box.MouseClick += Panel_MouseClick;
                label.MouseClick += Panel_MouseClick;

                panel.Controls.Add(box);
                panel.Controls.Add(label);
                parentPanel.Controls.Add(panel);
            }
        }

        private static void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            Panel panel = null;
            if (sender is Panel)
                panel = sender as Panel;
            else
                panel = (sender as Control).Parent as Panel;
            foreach (Panel item in panel.Parent.Controls)
                item.BackColor = Color.Transparent;
            panel.BackColor = DEFAULT_PANEL_BGCOLOR;
        }

        /// <summary>
        /// 设置二级菜单
        /// </summary>
        /// <param name="parentPanel"></param>
        /// <param name="list"></param>
        public static Panel SetSubPanel(Panel parentPanel, List<KyoPanel> list, Action<object, EventArgs> click)
        {
            Panel basicPanel = new Panel
            {
                Width = parentPanel.Width,
                Height = list.Count * DEFAULT_SUB_LABEL_HEIGHT,
                Left = 0,
                Top = parentPanel.Top + parentPanel.Height,
                BorderStyle = BorderStyle.FixedSingle,
                Name = parentPanel.Name + "_SUB",
                Tag = false
            };

            parentPanel.Click += delegate (object sender, System.EventArgs e)
            {
                Panel panel = (sender as Panel);
                Control[] cs = panel.Parent.Controls.Find(panel.Name + "_SUB", false);
                if (cs.Length == 0)
                {
                    ExpandSub(parentPanel, basicPanel, list, click);
                }
                else
                {
                    DexpandSub(parentPanel, basicPanel);
                }
            };
            return basicPanel;
        }

        /// <summary>
        /// 设置三级菜单
        /// </summary>
        public static Panel SetThreePanel(Panel parentPanel, List<KyoPanel> list, Action<object, EventArgs> click)
        {
            Panel basicPanel = new Panel
            {
                Width = parentPanel.Width,
                Height = list.Count * DEFAULT_SUB_LABEL_HEIGHT,
                Left = 0,
                Top = parentPanel.Top + parentPanel.Height,
                BorderStyle = BorderStyle.FixedSingle,
                Name = parentPanel.Name + "_THREE"
            };

            parentPanel.Click += delegate (object sender, System.EventArgs e)
            {
                Panel panel = (sender as Panel);
                Control[] cs = panel.Parent.Controls.Find($"{panel.Name}_THREE", false);
                if (cs.Length == 0)
                {
                    ExpandThree(parentPanel, basicPanel, list, click);
                }
                else
                {
                    DexpandThree(parentPanel, basicPanel);

                }
            };
            ExpandThree(parentPanel, basicPanel, list, click);
            return basicPanel;
        }

        /// <summary>
        /// 移除二级菜单并向上合并
        /// </summary>
        /// <param name="parentPanel"></param>
        /// <param name="basicPanel"></param>
        private static void DexpandSub(Panel parentPanel, Panel basicPanel)
        {
            Panel _panel = parentPanel.Parent as Panel;
            //将当前Panel下的所有选项板上移
            foreach (Control item in _panel.Controls)
            {
                if (item.Top > basicPanel.Top)
                {
                    item.Top -= basicPanel.Height;
                }
            }
            _panel.Controls.Remove(basicPanel);

            Control[] cs = parentPanel.Controls.Find(parentPanel.Name + "_ARROW", false);
            if (cs.Length > 0)
            {
                cs[0].Text = "▲";
            }
        }

        /// <summary>
        /// 移除三级菜单并向上合并
        /// </summary>
        private static void DexpandThree(Panel parentPanel, Panel basicPanel)
        {
            Panel _panel = parentPanel.Parent as Panel;
            //将当前Panel下的所有选项板上移
            foreach (Control item in _panel.Controls)
            {
                if (item.Top > basicPanel.Top)
                {
                    item.Top -= basicPanel.Height;
                }
            }
            _panel.Controls.Remove(basicPanel);

            Control[] cs = parentPanel.Controls.Find(parentPanel.Name + "_ARROW", false);
            if (cs.Length > 0)
            {
                cs[0].Text = "▲";
            }
        }

        private static void Panel_MouseEnter(object sender, System.EventArgs e)
        {
            Panel panel = sender as Panel;
            foreach (Control item in panel.Parent.Controls)
            {
                if (!(bool)item.Tag)
                    item.BackColor = Color.Transparent;
            }
            if (!(bool)panel.Tag)
                panel.BackColor = Color.FromArgb(0, 120, 215);
        }

        /// <summary>
        /// 创建二级菜单并展开
        /// </summary>
        /// <param name="parentPanel">菜单所属父级</param>
        /// <param name="basicPanel">菜单容器</param>
        /// <param name="list">二级菜单列表</param>
        private static void ExpandSub(Panel parentPanel, Panel basicPanel, List<KyoPanel> list, Action<object, EventArgs> click)
        {
            Panel _panel = parentPanel.Parent as Panel;
            for (int i = 0; i < list.Count; i++)
            {
                KyoPanel kyoPanel = list[i];
                Panel panel = new Panel
                {
                    Width = basicPanel.Width,
                    Height = DEFAULT_SUB_LABEL_HEIGHT,
                    Left = 0,
                    Top = i * DEFAULT_SUB_LABEL_HEIGHT,
                    Tag = false,
                    Name = kyoPanel.Name,
                };
                panel.MouseEnter += Panel_MouseEnter;
                panel.Click += new EventHandler(click);
                basicPanel.Controls.Add(panel);

                Label _label = new Label
                {
                    Text = kyoPanel.Text,
                    ForeColor = Color.White,
                    Location = new Point(77, 13),
                    AutoSize = true
                };
                if (kyoPanel.HasNext)
                {
                    Label arrow = new Label
                    {
                        Name = panel.Name + "_ARROW",
                        Text = "▲",
                        ForeColor = Color.White,
                        Location = new Point(207, 12),
                        Size = new Size(22, 22),
                        Font = new Font("微软雅黑", 10, FontStyle.Bold)
                    };
                    arrow.Click += new EventHandler(click);
                    panel.Controls.Add(arrow);
                }

                _label.Click += new EventHandler(click);
                panel.Controls.Add(_label);
            }
            _panel.Controls.Add(basicPanel);

            //将当前Panel下的所有选项板下移
            foreach (Control item in _panel.Controls)
            {
                if (item != basicPanel)
                    if (item.Top > parentPanel.Top)
                    {
                        item.Top += basicPanel.Height;
                    }
            }

            Control[] cs = parentPanel.Controls.Find(parentPanel.Name + "_ARROW", false);
            if (cs.Length > 0)
            {
                cs[0].Text = "▼";
            }
        }

        /// <summary>
        /// 创建三级菜单并展开
        /// </summary>
        /// <param name="parentPanel">菜单所属父级</param>
        /// <param name="basicPanel">菜单容器</param>
        /// <param name="list">三级菜单列表</param>
        private static void ExpandThree(Panel parentPanel, Panel basicPanel, List<KyoPanel> list, Action<object, EventArgs> click)
        {
            Panel _panel = parentPanel.Parent as Panel;
            for (int i = 0; i < list.Count; i++)
            {
                KyoPanel kyoPanel = list[i];
                Panel panel = new Panel
                {
                    Width = basicPanel.Width,
                    Height = DEFAULT_SUB_LABEL_HEIGHT,
                    Left = 0,
                    Top = i * DEFAULT_SUB_LABEL_HEIGHT,
                    Tag = false,
                    Name = kyoPanel.Name,
                };
                panel.MouseEnter += Panel_MouseEnter;
                panel.Click += new EventHandler(click);
                basicPanel.Controls.Add(panel);

                Label _label = new Label
                {
                    Text = kyoPanel.Text,
                    ForeColor = Color.White,
                    Location = new Point(77, 13),
                    AutoSize = true
                };
                if (kyoPanel.HasNext)
                {
                    Label arrow = new Label
                    {
                        Name = panel.Name + "_ARROW",
                        Text = "▲",
                        ForeColor = Color.White,
                        Location = new Point(207, 12),
                        Size = new Size(22, 22),
                        Font = new Font("微软雅黑", 10, FontStyle.Bold)
                    };
                    arrow.Click += new EventHandler(click);
                    panel.Controls.Add(arrow);
                }

                _label.Click += new EventHandler(click);
                panel.Controls.Add(_label);
            }
            _panel.Controls.Add(basicPanel);

            //将当前Panel下的所有选项板下移
            foreach (Control item in _panel.Controls)
            {
                if (item != basicPanel)
                    if (item.Top > parentPanel.Top)
                    {
                        item.Top += basicPanel.Height;
                    }
            }

            Control[] cs = parentPanel.Controls.Find(parentPanel.Name + "_ARROW", false);
            if (cs.Length > 0)
            {
                cs[0].Text = "▼";
            }
        }

        /// <summary>
        /// 自定义原型Panel
        /// </summary>
        public class KyoPanel
        {
            private string name;
            private string text;
            private Image image;
            private bool hasNext;

            public string Name { get => name; set => name = value; }
            public string Text { get => text; set => text = value; }
            public Image Image { get => image; set => image = value; }
            public bool HasNext { get => hasNext; set => hasNext = value; }
        }
    }
}
