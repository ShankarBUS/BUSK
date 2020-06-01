namespace BUSK.Charting.Dtos
{
    internal struct StackedSum
    {
        public StackedSum(double value) : this()
        {
            if (value < 0)
            {
                Left = value;
            }
            else
            {
                Right = value;
            }
        }

        public double Left { get; set; }
        public double Right { get; set; }
    }
}