using System;

namespace BUSK.Core.Extensibility
{
    public class ExtensionBase
    {
        public string ExtensionConfigLocation { get; internal set; }

        public string ExtensionAssemblyLocation { get; internal set; }

        public string ExtensionDirectoryPath { get; internal set; }

        public string ExtensionAssemblyName { get; internal set; }

        public event EventHandler StartUp;

        internal bool HasStarted = false;

        public ExtensionInfo ExtensionInfo { get; set; }

        internal void Thodangu()
        {
            if (ExtensionInfo.IsEnabled)
            {
                StartUp?.Invoke(null, null);
                HasStarted = true;
            }
        }
    }
}
