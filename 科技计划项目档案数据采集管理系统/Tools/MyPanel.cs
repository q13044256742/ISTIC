using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Tools
{
    /// <summary>
    /// 加强版 Panel
    /// </summary>
    class MyPanel : Panel
    {
        /// <summary>
        /// OnPaintBackground 事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e) {
            // 重载基类的背景擦除函数，
            // 解决窗口刷新，放大，图像闪烁
            return;
        }

        /// <summary>
        /// OnPaint 事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e) {
            // 使用双缓冲
            this.DoubleBuffered = true;
            // 背景重绘移动到此
            if (this.BackgroundImage != null)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                e.Graphics.DrawImage(
                    this.BackgroundImage,
                    new System.Drawing.Rectangle(0, 0, this.Width, this.Height),
                    0,
                    0,
                    this.BackgroundImage.Width,
                    this.BackgroundImage.Height,
                    System.Drawing.GraphicsUnit.Pixel);
            }
            base.OnPaint(e);
        }
    }
}
