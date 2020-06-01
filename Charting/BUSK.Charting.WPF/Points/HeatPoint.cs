using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BUSK.Charting.Charts;
using BUSK.Charting.Definitions.Points;
using BUSK.Charting.Dtos;

namespace BUSK.Charting.WPF.Points
{
    internal class HeatPoint : PointView, IHeatPointView
    {
        public Rectangle Rectangle { get; set; }
        public CoreColor ColorComponents { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public override void DrawOrMove(ChartPoint previousDrawn, ChartPoint current, int index, ChartCore chart)
        {
            Canvas.SetTop(Rectangle, current.ChartLocation.Y);
            Canvas.SetLeft(Rectangle, current.ChartLocation.X);

            Rectangle.Width = Width;
            Rectangle.Height = Height;

            if (IsNew)
            {
                Rectangle.Fill = new SolidColorBrush(Colors.Transparent);
            }

            if (HoverShape != null)
            {
                HoverShape.Width = Width;
                HoverShape.Height = Height;
                Canvas.SetLeft(HoverShape, current.ChartLocation.X);
                Canvas.SetTop(HoverShape, current.ChartLocation.Y);
            }

            if (DataLabel != null)
            {
                DataLabel.UpdateLayout();
                Canvas.SetTop(DataLabel, current.ChartLocation.Y + (Height/2) - DataLabel.ActualHeight*.5);
                Canvas.SetLeft(DataLabel, current.ChartLocation.X + (Width/2) - DataLabel.ActualWidth*.5);
            }

            var targetColor = new Color
            {
                A = ColorComponents.A,
                R = ColorComponents.R,
                G = ColorComponents.G,
                B = ColorComponents.B
            };

            if (chart.View.DisableAnimations)
            {
                Rectangle.Fill = new SolidColorBrush(targetColor);
                return;
            }

            var animSpeed = chart.View.AnimationsSpeed;

            Rectangle.Fill.BeginAnimation(SolidColorBrush.ColorProperty,
                new ColorAnimation(targetColor, animSpeed));
        }

        public override void RemoveFromView(ChartCore chart)
        {
            chart.View.RemoveFromDrawMargin(HoverShape);
            chart.View.RemoveFromDrawMargin(Rectangle);
            chart.View.RemoveFromDrawMargin(DataLabel);
        }

        public override void OnHover(ChartPoint point)
        {
            Rectangle.StrokeThickness++;
        }

        public override void OnHoverLeave(ChartPoint point)
        {
            Rectangle.StrokeThickness = ((Series) point.SeriesView).StrokeThickness;
        }
    }
}
