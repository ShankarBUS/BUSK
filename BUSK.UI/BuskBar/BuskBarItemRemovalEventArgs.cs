namespace BUSK.UI.BuskBar
{
    internal class BuskBarItemRemovalEventArgs
    {
        public BuskBarItemRemovalEventArgs(IBuskBarItem buskBarItem)
        {
            BuskBarItem = buskBarItem;
        }

        public IBuskBarItem BuskBarItem { get; set; }

        public bool ItemRemoved { get; set; } = false;
    }
}
