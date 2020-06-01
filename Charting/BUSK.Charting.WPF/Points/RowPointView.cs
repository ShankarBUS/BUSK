﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BUSK.Charting.Charts;
using BUSK.Charting.Definitions.Points;
using BUSK.Charting.Dtos;

namespace BUSK.Charting.WPF.Points
{
    internal class RowPointView : PointView, IRectanglePointView
    {
        public Rectangle Rectangle { get; set; }
        public CoreRectangle Data { get; set; }
        public double ZeroReference  { get; set; }
        public BarLabelPosition LabelPosition { get; set; }
        private RotateTransform Transform { get; set; }

        public override void DrawOrMove(ChartPoint previousDrawn, ChartPoint current, int index, ChartCore chart)
        {
            if (IsNew)
            {
                Canvas.SetTop(Rectangle, Data.Top);
                Canvas.SetLeft(Rectangle, ZeroReference);

                Rectangle.Width = 0;
                Rectangle.Height = Data.Height;
            }

            if (DataLabel != null && double.IsNaN(Canvas.GetLeft(DataLabel)))
            {
                Canvas.SetTop(DataLabel, Data.Top);
                Canvas.SetLeft(DataLabel, ZeroReference);
            }

            Func<double> getY = () =>
            {
                if (LabelPosition == BarLabelPosition.Perpendicular)
                {
                    if (Transform == null)
                        Transform = new RotateTransform(270);

                    DataLabel.RenderTransform = Transform;
                    return Data.Top + Data.Height/2 + DataLabel.ActualWidth*.5;
                }

                var r = Data.Top + Data.Height / 2 - DataLabel.ActualHeight / 2;

                if (r < 0) r = 2;
                if (r + DataLabel.ActualHeight > chart.DrawMargin.Height)
                    r -= r + DataLabel.ActualHeight - chart.DrawMargin.Height + 2;

                return r;
            };

            Func<double> getX = () =>
            {
                double r;

#pragma warning disable 618
                if (LabelPosition == BarLabelPosition.Parallel || LabelPosition == BarLabelPosition.Merged)
#pragma warning restore 618
                {
                    r = Data.Left + Data.Width/2 - DataLabel.ActualWidth/2;
                }
                else if (LabelPosition == BarLabelPosition.Perpendicular)
                {
                    r = Data.Left + Data.Width/2 - DataLabel.ActualHeight/2;
                }
                else
                {
                    if (Data.Left < ZeroReference)
                    {
                        r = Data.Left - DataLabel.ActualWidth - 5;
                        if (r < 0) r = Data.Left + 5;
                    }
                    else
                    {
                        r = Data.Left + Data.Width + 5;
                        if (r + DataLabel.ActualWidth > chart.DrawMargin.Width)
                            r -= DataLabel.ActualWidth + 10;
                    }
                }

                return r;
            };

            if (chart.View.DisableAnimations)
            {
                Rectangle.Width = Data.Width;
                Rectangle.Height = Data.Height;

                Canvas.SetTop(Rectangle, Data.Top);
                Canvas.SetLeft(Rectangle, Data.Left);

                if (DataLabel != null)
                {
                    DataLabel.UpdateLayout();

                    Canvas.SetTop(DataLabel, getY());
                    Canvas.SetLeft(DataLabel, getX());
                }

                if (HoverShape != null)
                {
                    Canvas.SetTop(HoverShape, Data.Top);
                    Canvas.SetLeft(HoverShape, Data.Left);
                    HoverShape.Height = Data.Height;
                    HoverShape.Width = Data.Width;
                }

                return;
            }

            var animSpeed = chart.View.AnimationsSpeed;

            if (DataLabel != null)
            {
                DataLabel.UpdateLayout();

                DataLabel.BeginAnimation(Canvas.LeftProperty, new DoubleAnimation(getX(), animSpeed));
                DataLabel.BeginAnimation(Canvas.TopProperty, new DoubleAnimation(getY(), animSpeed));
            }

            Rectangle.BeginAnimation(Canvas.TopProperty, 
                new DoubleAnimation(Data.Top, animSpeed));
            Rectangle.BeginAnimation(Canvas.LeftProperty,
                new DoubleAnimation(Data.Left, animSpeed));

            Rectangle.BeginAnimation(FrameworkElement.HeightProperty, 
                new DoubleAnimation(Data.Height, animSpeed));
            Rectangle.BeginAnimation(FrameworkElement.WidthProperty,
                new DoubleAnimation(Data.Width, animSpeed));

            if (HoverShape != null)
            {
                Canvas.SetTop(HoverShape, Data.Top);
                Canvas.SetLeft(HoverShape, Data.Left);
                HoverShape.Height = Data.Height;
                HoverShape.Width = Data.Width;
            }
        }

        public override void RemoveFromView(ChartCore chart)
        {
            chart.View.RemoveFromDrawMargin(HoverShape);
            chart.View.RemoveFromDrawMargin(Rectangle);
            chart.View.RemoveFromDrawMargin(DataLabel);
        }

        public override void OnHover(ChartPoint point)
        {
            var copy = Rectangle.Fill.Clone();
            copy.Opacity -= .15;
            Rectangle.Fill = copy;
        }

        public override void OnHoverLeave(ChartPoint point)
        {
            if (Rectangle == null) return;

            if (point.Fill != null)
            {
                Rectangle.Fill = (Brush)point.Fill;
            }
            else
            {
                Rectangle.Fill = ((Series) point.SeriesView).Fill;
            }
        }
    }
}
