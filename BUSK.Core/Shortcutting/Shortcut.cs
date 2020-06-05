using BUSK.Core.Shortcutting.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Serialization;

namespace BUSK.Core.Shortcutting
{
    public enum ShortcutType
    {
        UserDefined,
        ExtensionDefined,
        Inbuilt
    }

    public enum ShortcutEditorAction
    {
        Save,
        Cancel,
        Delete
    }

    public enum ShortcutPinUnpinAction
    {
        Pin,
        Unpin
    }

    public class ShortcutEndEditEventArgs : EventArgs
    {
        public ShortcutEndEditEventArgs(ShortcutEditorAction action)
        {
            Action = action;
        }

        public ShortcutEditorAction Action { get; private set; }
    }

    public class ShortcutPinUnpinEventArgs : EventArgs
    {
        public ShortcutPinUnpinEventArgs(ShortcutPinUnpinAction action)
        {
            Action = action;
        }

        public ShortcutPinUnpinAction Action { get; private set; }
    }

    [Serializable][XmlRoot]
    public class Shortcut : BindableBase, IEditableObject 
    {
        [XmlIgnore]
        public string ID { get; internal set; }

        [XmlIgnore]
        public bool IsNew { get; internal set; } = true;

        internal event EventHandler DeleteRequested;

        internal event EventHandler EditRequested;

        internal event EventHandler<ShortcutEndEditEventArgs> EndEditRequested;

        internal event EventHandler<ShortcutPinUnpinEventArgs> PinUnpinRequested;

        #region Properties

        [XmlArray]
        public ObservableCollection<Command> Commands { get; private set; } = new ObservableCollection<Command>();

        [XmlIgnore]
        public ICommand CancelCommand { get; internal set; }

        [XmlIgnore]
        public ICommand DeleteCommand { get; internal set; }

        [XmlIgnore]
        public ICommand EditCommand { get; internal set; }

        [XmlIgnore]
        public ICommand SaveCommand { get; internal set; }

        [XmlIgnore]
        public ICommand RunCommand { get; internal set; }

        [XmlIgnore]
        public ICommand PinCommand { get; internal set; }

        [XmlIgnore]
        public ICommand UnpinCommand { get; internal set; }

        private string title = "";

        [XmlAttribute]
        public string Title
        {
            get { return title; }
            set { SetPropertyValue(ref title, value); }
        }

        private string description = "";

        [XmlAttribute]
        public string Description
        {
            get { return description; }
            set { SetPropertyValue(ref description, value); }
        }

        private bool isenabled = true;

        [XmlAttribute]
        public bool IsEnabled
        {
            get { return isenabled; }
            set { SetPropertyValue(ref isenabled, value); }
        }

        private Hotkey hotkey = new Hotkey(Key.P, ModifierKeys.Control | ModifierKeys.Alt);

        [XmlElement]
        public Hotkey Hotkey
        {
            get { return hotkey; }
            set { SetPropertyValue(ref hotkey, value); }
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

        private ShortcutType shortcutType  = ShortcutType.UserDefined;

        [XmlIgnore]
        public ShortcutType ShortcutType
        {
            get { return shortcutType; }
            set { SetPropertyValue(ref shortcutType, value); }
        }

        private bool ispinned = false;

        [XmlIgnore]
        public bool IsPinned
        {
            get { return ispinned; }
            set { if (SetPropertyValue(ref ispinned, value)) { if (value) { Pin(); } else { Unpin(); } } }
        }

        #endregion 

        public Shortcut()
        {
            if (ShortcutType == ShortcutType.UserDefined) { ID = Guid.NewGuid().ToString(); }

            DeleteCommand = new RelayCommand((param) =>
            {
                EndEditRequested?.Invoke(this, new ShortcutEndEditEventArgs(ShortcutEditorAction.Delete));
                DeleteRequested?.Invoke(this, null);
            }, (param) => { return ShortcutType == ShortcutType.UserDefined; });

            EditCommand = new RelayCommand((param) =>
            {
                this.BeginEdit();
                EditRequested?.Invoke(this, null);
            }, (param) => { return ShortcutType == ShortcutType.UserDefined; });

            SaveCommand = new RelayCommand((param) =>
            {
                this.EndEdit();
                EndEditRequested?.Invoke(this, new ShortcutEndEditEventArgs(ShortcutEditorAction.Save));
            });

            CancelCommand = new RelayCommand((param) =>
            {
                this.CancelEdit();
                EndEditRequested?.Invoke(this, new ShortcutEndEditEventArgs(ShortcutEditorAction.Cancel));
            });

            RunCommand = new RelayCommand((param) => Start(), (param) => IsEnabled);

            PinCommand = new RelayCommand((param) => IsPinned = true, (param) => !IsPinned);

            UnpinCommand = new RelayCommand((param) => IsPinned = false);

            Commands.CollectionChanged += CollectionChanged;
        }

        public void Start()
        {
            if (!IsEnabled) { return; }
            foreach (Command command in Commands)
            {
                command.Execute();
            }
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            bool userdefined = (ShortcutType == ShortcutType.UserDefined);

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var added = (Command)e.NewItems[0];
                added.Owner = this;
                added.OnAdded();
                if (userdefined) added.DeleteCommand = new RelayCommand((param) => Commands.Remove(added));
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var removed = (Command)e.OldItems[0];
                removed.Owner = null;
                if (userdefined) removed.DeleteCommand = null;
                removed.OnRemoved();
            }
        }

        private Shortcut backupShortcut;

        private List<Command> backupCommands;

        private Hotkey backupHotkey;

        public void BeginEdit()
        {
            backupShortcut = (Shortcut)MemberwiseClone();
            backupCommands = new List<Command>();
            foreach (Command command in Commands)
            {
                var newCommand = command.Clone();
                newCommand.Owner = null;
                backupCommands.Add(newCommand);
            }
            backupHotkey = Hotkey.Clone();
        }

        public void CancelEdit()
        {
            if (IsNew) { return; }

            // Restoration
            Title = backupShortcut.Title; Description = backupShortcut.Description; IsEnabled = backupShortcut.IsEnabled; ImageSource = backupShortcut.ImageSource; Hotkey = backupHotkey;

            foreach (Command command in Commands.ToArray())
            {
                Commands.Remove(command);
            }

            foreach (Command command in backupCommands)
            {
                Commands.Add(command);
            }
        }

        public void EndEdit()
        {
            backupHotkey = null;
            backupCommands?.Clear();
            backupCommands = null;
            backupShortcut = null;
        }

        private void Pin()
        {
            PinUnpinRequested?.Invoke(this, new ShortcutPinUnpinEventArgs(ShortcutPinUnpinAction.Pin));
        }

        private void Unpin()
        {
            PinUnpinRequested?.Invoke(this, new ShortcutPinUnpinEventArgs(ShortcutPinUnpinAction.Unpin));
        }
    }
}
