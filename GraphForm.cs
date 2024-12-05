using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace GraphPlotter
{
    public class GraphForm : Form
    {
        private ComboBox functionSelector;
        private Button plotButton;
        private Panel graphPanel;
        private Func<double, double> selectedFunction;

        public GraphForm()
        {
            this.Text = "Построение графиков";
            this.Size = new Size(800, 600);

            functionSelector = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(10, 10),
                Width = 200
            };

            functionSelector.Items.AddRange(new string[]
            {
                "y = x^2",
                "y = sin(x)",
                "y = cos(x)",
                "y = e^x"
            });
            functionSelector.SelectedIndex = 0;
            selectedFunction = x => x * x;

            plotButton = new Button
            {
                Text = "Построить график",
                Location = new Point(220, 10),
                Width = 150
            };

            plotButton.Click += PlotButton_Click;

            graphPanel = new Panel
            {
                Location = new Point(10, 50),
                Size = new Size(760, 500),
                BorderStyle = BorderStyle.FixedSingle
            };

            graphPanel.Paint += DrawGraph;

            this.Controls.Add(functionSelector);
            this.Controls.Add(plotButton);
            this.Controls.Add(graphPanel);
        }


        private void PlotButton_Click(object sender, EventArgs e)
        {
            switch (functionSelector.SelectedIndex)
            {
                case 0:
                    selectedFunction = x => x * x;
                    break;
                case 1:
                    selectedFunction = Math.Sin;
                    break;
                case 2:
                    selectedFunction = Math.Cos;
                    break;
                case 3:
                    selectedFunction = Math.Exp;
                    break;
                default:
                    MessageBox.Show("Выберите функцию!");
                    return;
            }

            graphPanel.Invalidate();
        }


        private void InitializeComponent()
        {

        }

        private void DrawGraph(object sender, PaintEventArgs e)
        {
            if (selectedFunction == null)
            {
                return;
            }

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen axisPen = new Pen(Color.Black, 2);
            Pen graphPen = new Pen(Color.Blue, 2);

            int width = graphPanel.Width;
            int height = graphPanel.Height;

            int centerX = width / 2;
            int centerY = height / 2;

            double scaleX = 40.0;
            double scaleY = 40.0;

            g.DrawLine(axisPen, 0, centerY, width, centerY);
            g.DrawLine(axisPen, centerX, 0, centerX, height);

            List<PointF> points = new List<PointF>();
            try
            {
                for (double x = -centerX / scaleX; x <= centerX / scaleX; x += 0.01)
                {
                    double y = selectedFunction(x);
                    float screenX = (float)(centerX + x * scaleX);
                    float screenY = (float)(centerY - y * scaleY);
                    points.Add(new PointF(screenX, screenY));
                }
                g.DrawLines(graphPen, points.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при построении графика: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
