using BUSK.Core.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BUSK.Core.Extensibility
{
    public sealed class ExtensionEnableChangedEventArgs : EventArgs
    {
        public ExtensionEnableChangedEventArgs(string cfglocation)
        {
            ConfigLocation = cfglocation;
        }

        public string ConfigLocation { get; private set; }
    }

    public sealed class ExtensionsManager
    {
        public static ExtensionsManager Instance;

        public ExtensionsManager()
        {
            ExtensionBases.CollectionChanged += CollectionChanged;
        }

        public ObservableCollection<ExtensionBase> ExtensionBases { get; private set; } = new ObservableCollection<ExtensionBase>();

        public event EventHandler<ExtensionEnableChangedEventArgs> EnableChangedRequested;

        public void Add(ExtensionBase extensionBase, string cfglocation, string asmlocation, string directory, string asmname)
        {
            extensionBase.ExtensionConfigLocation = cfglocation;
            extensionBase.ExtensionAssemblyLocation = asmlocation;
            extensionBase.ExtensionDirectoryPath = directory;
            extensionBase.ExtensionAssemblyName = asmname;
            ExtensionBases.Add(extensionBase);
        }

        public void Initialize()
        {
            foreach (ExtensionBase extensionBase in ExtensionBases)
            {
                extensionBase.Thodangu();
            }
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ExtensionBase extensionBase = (ExtensionBase)e.NewItems[0];
                ExtensionInfo extension = extensionBase.ExtensionInfo;
                if (extension.ImageSource == null) extension.ImageSource = ImageHelper.GetBitmapImage(extension.Image);
                extension.EnabledChanged += (s, ea) =>
                {
                    EnableChangedRequested?.Invoke(s, new ExtensionEnableChangedEventArgs(extensionBase.ExtensionConfigLocation));
                    if (!extensionBase.HasStarted)
                    {
                        extensionBase.Thodangu();
                    }
                };
            }
        }
    }
}
