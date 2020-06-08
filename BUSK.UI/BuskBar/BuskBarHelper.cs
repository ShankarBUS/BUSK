namespace BUSK.UI.BuskBar
{
    public class BuskBarHelper
    {
        internal static event BuskBarItemAdditionEventHandler BuskBarItemAdditionRequested;

        internal static event BuskBarItemRemovalEventHandler BuskBarItemRemovalRequested;

        public static bool RequestBuskBarItemAddition(IBuskBarItem buskBarItem)
        {
            if (buskBarItem == null) return false;

            var args = new BuskBarItemAdditionEventArgs(buskBarItem);
            BuskBarItemAdditionRequested?.Invoke(args);

            return args.ItemAdded;
        }

        public static bool RequestBuskBarItemRemoval(IBuskBarItem buskBarItem)
        {
            if (buskBarItem == null) return false;

            var args = new BuskBarItemRemovalEventArgs(buskBarItem);
            BuskBarItemRemovalRequested?.Invoke(args);

            return args.ItemRemoved;
        }
    }
}
