using System.Collections.Generic;
using BUSK.Charting.Helpers;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// 
    /// </summary>
    public class AxisWindowCollection : NoisyCollection<AxisWindow>
    {
        public AxisWindowCollection()
        {
            NoisyCollectionChanged += OnNoisyCollectionChanged;
        }

        private void OnNoisyCollectionChanged(IEnumerable<AxisWindow> oldItems, IEnumerable<AxisWindow> newItems)
        {
            
        }
    }
}