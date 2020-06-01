using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace BUSK.Core.Extensibility
{
    [Serializable]
    [XmlRoot]
    public class ExtensionInfo : BindableBase
    {
        public ExtensionInfo()
        {
            VersionDescription = GetVersionDescription();
        }

        #region Properties

        private string title = "";

        [XmlIgnore]
        public string Title
        {
            get { return title; }
            set { SetPropertyValue(ref title, value); }
        }

        private string description = "";

        [XmlIgnore]
        public string Description
        {
            get { return description; }
            set { SetPropertyValue(ref description, value); }
        }

        private bool isEnabled = true;

        [XmlAttribute]
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { if (SetPropertyValue(ref isEnabled, value)) { EnabledChanged?.Invoke(this, null); } }
        }

        private System.Drawing.Image image;

        /// <summary>
        /// Don't Set any value to this. Set value to <see cref="ImageSource"/> property instead.
        /// </summary>
        [XmlIgnore]
        internal System.Drawing.Image Image
        {
            get { return image; }
            set { SetPropertyValue(ref image, value); }
        }

        private System.Windows.Media.ImageSource imageSource;

        [XmlIgnore]
        public System.Windows.Media.ImageSource ImageSource
        {
            get { return imageSource; }
            set { SetPropertyValue(ref imageSource, value); }
        }

        private VersionInfo minimumWindowsVersion = default;

        /// <summary>
        /// If no minimum requirement don't set any value to this
        /// </summary>
        [XmlElement]
        public VersionInfo MinimumWindowsVersion
        {
            get { return minimumWindowsVersion; }
            set { if (SetPropertyValue(ref minimumWindowsVersion, value)) { VersionDescription = GetVersionDescription(); } }
        }

        private VersionInfo maximumWindowsVersion = default;

        /// <summary>
        /// If no maximum requirement don't set any value to this
        /// </summary>
        [XmlElement]
        public VersionInfo MaximumWindowsVersion
        {
            get { return maximumWindowsVersion; }
            set { if (SetPropertyValue(ref maximumWindowsVersion, value)) { VersionDescription = GetVersionDescription(); } }
        }

        private string verdesc = "";

        [XmlIgnore]
        public string VersionDescription
        { 
            get { return verdesc; }
            private set { SetPropertyValue(ref verdesc, value); }
        }

        #endregion

        private string GetVersionDescription()
        {
            var mi = minimumWindowsVersion == default ? "" : minimumWindowsVersion.ToString();
            var ma = maximumWindowsVersion == default ? "" : maximumWindowsVersion.ToString();
            string o;
            if (mi == "" && ma == "") { o = "Compatible with all Windows Versions"; }
            else if (mi == "" && ma != "") { o = $"Compatible with all Windows Version less than {ma}"; }
            else if (mi != "" && ma == "") { o = $"Compatible with all Windows Version greater than {mi}"; }
            else { o = $"Compatible with Windows Versions between : {mi} - {ma}"; }
            return o;
        }

        public event EventHandler EnabledChanged;
    }
}
