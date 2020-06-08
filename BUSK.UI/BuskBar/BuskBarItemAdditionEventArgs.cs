namespace BUSK.UI.BuskBar
{
    internal class BuskBarItemAdditionEventArgs
    {
        public BuskBarItemAdditionEventArgs(IBuskBarItem buskBarItem)
        {
            BuskBarItem = buskBarItem;
        }

        public IBuskBarItem BuskBarItem { get; set; }

        public bool ItemAdded { get; set; } = false;
    }
}
