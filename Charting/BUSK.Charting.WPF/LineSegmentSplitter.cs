using System.Windows.Media;

namespace BUSK.Charting.WPF
{
    internal class LineSegmentSplitter
    {
        public LineSegmentSplitter()
        {
            Bottom = new LineSegment { IsStroked = false };
            Left = new LineSegment { IsStroked = false };
            Right = new LineSegment { IsStroked = false };
        }

        public LineSegment Bottom { get; private set; }
        public LineSegment Left { get; private set; }
        public LineSegment Right { get; private set; }
        public int SplitterCollectorIndex { get; set; }
        public bool IsNew { get; set; }
    }
}
