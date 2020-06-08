using BUSK.Core.Shortcutting.Commands;
using BUSK.Core.Utilities;
using Shell32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace BUSK.Core.Shortcutting
{
    public class ShortcutsManager
    {
        private readonly KeyboardHook KeyboardHook = new KeyboardHook();

        public ImageSource DefaultShortcutImageSource;

        public Image DefaultShortcutImage;

        public static ShortcutsManager Instance;

        internal event EventHandler ShortcutEditRequested;

        internal event EventHandler ShortcutDeleteRequested;

        internal event EventHandler<ShortcutEndEditEventArgs> ShortcutEndEditRequested;

        internal event EventHandler<ShortcutPinUnpinEventArgs> ShortcutPinUnpinRequested;

        public ShortcutsManager()
        {
            BuskInterop.AddSplashScreenMessage("Initializing Shortcuts");
            DefaultShortcutImageSource = new BitmapImage(new Uri(@"pack://application:,,,/BUSK.Core;component/Assets/Images/64.png"));
            DefaultShortcutImage = ImageHelper.GetBitmap((BitmapImage)DefaultShortcutImageSource);
            KeyboardHook.KeyDown += KeyPressed;
            Shortcuts.CollectionChanged += CollectionChanged;
        }

        private void KeyPressed(Key Key)
        {
            foreach (Shortcut shortcut in Shortcuts)
            {
                if (shortcut.Hotkey.Key == Key && shortcut.Hotkey.Modifiers == Keyboard.Modifiers)
                {
                    shortcut.Start();
                }
            }
        }

        public ObservableCollection<Shortcut> Shortcuts { get; private set; } = new ObservableCollection<Shortcut>();

        internal async void LoadAllShortcutsAync()
        {
            await LoadInbuiltShortcutsAsync();
            await LoadShortcutsAsync();
            StartLinkingShortcutLinkCommands();
        }

        private async Task LoadInbuiltShortcutsAsync()
        {
            var u = await GetInbuildShorcutsAsync();
            foreach (Shortcut shortcut in u)
            {
                Shortcuts.Add(shortcut);
            }
        }

        private async Task<List<Shortcut>> GetInbuildShorcutsAsync()
        {
            return await Task.Run(() => InbuiltShortcuts.GetShortcuts());
        }

        private async Task LoadShortcutsAsync()
        {
            var u = await GetShorcutsAsync();
            foreach (Shortcut shortcut in u)
            {
                Shortcuts.Add(shortcut);
            }
        }

        private async Task<List<Shortcut>> GetShorcutsAsync()
        {
            return await Task.Run(() => GetShortcutsFromFolder());
        }

        private List<Shortcut> GetShortcutsFromFolder()
        {
            List<Shortcut> shortcuts = new List<Shortcut>();

            var d = new DirectoryInfo(@".\Shortcuts");
            foreach (var f in d.GetFiles("*.xml"))
            {
                Shortcut shortcut = GetShortcutFromShortcutXml(f.FullName);
                if (shortcut != null)
                {
                    shortcut.IsNew = false;
                    shortcuts.Add(shortcut);
                }
            }

            return shortcuts;
        }

        public Shortcut GetShortcutFromShortcutXml(string location)
        {
            try
            {
                if (!File.Exists(location)) { return null; }

                var x = new XmlSerializer(typeof(Shortcut), GetInheritedCommandTypes());
                var w = new StreamReader(location);
                Shortcut shortcut = (Shortcut)x.Deserialize(w);
                shortcut.ID = Path.GetFileNameWithoutExtension(location);
                try
                {
                    using var fs = new FileStream(GetImageFile(location), FileMode.Open, FileAccess.Read);
                    shortcut.Image = Image.FromStream(fs);
                }
                catch (Exception ex)
                {
                    Debug.Write("ShortcutsManager : " + ex.Message);
                }

                return shortcut;
            }
            catch (Exception ex)
            {
                Debug.Write("ShortcutsManager : " + ex.Message);
                return null;
            }
        }

        public Shortcut GetShortcutFromFile(string location)
        {
            if (!File.Exists(location)) { return null; }
            IconExtractor ie;
            Icon ico = null;
            try
            {
                ie = new IconExtractor(location);
                ico = ie.GetIcon(0);
            } catch { }

            if (ico == null)
            {
                ico = Icon.ExtractAssociatedIcon(location);
            }

            Image i = IconUtil.GetImageFromIconBiggerThan(48, ico);
            var shortcut = new Shortcut() { Title = Path.GetFileNameWithoutExtension(location), ShortcutType = ShortcutType.UserDefined, Image = i };
            shortcut.Commands.Add(new ProcessStartCommand() { Arguments = "", Target = location });
            return shortcut;
        }

        public void LoadShortcutFromFile(string location)
        {
            var shortcut = GetShortcutFromFile(location);
            if (shortcut != null) { Shortcuts.Add(shortcut); }
            else { throw new NullReferenceException(); }
        }

        public Shortcut GetShortcutFromlnkFile(string location)
        {
            if (!File.Exists(location)) { return null; }

            string pathOnly = Path.GetDirectoryName(location);
            string filenameOnly = Path.GetFileName(location);
            var shell = new Shell();
            var folder = shell.NameSpace(pathOnly);
            var folderItem = folder.ParseName(filenameOnly);
            if (folderItem.IsLink)
            {
                ShellLinkObject oShellLink = (ShellLinkObject)folderItem.GetLink;
                string args = oShellLink.Arguments;
                string desc = oShellLink.Description;
                string icopath = folderItem.Path;
                int num = oShellLink.GetIconLocation(out icopath);
                IconExtractor ie;
                Icon ico = null;
                try
                {
                    ie = new IconExtractor(icopath);
                    ico = ie.GetIcon(num);
                } catch { }

                if (ico == null)
                {
                    ico = Icon.ExtractAssociatedIcon(location);
                }

                Image i = IconUtil.GetImageFromIconBiggerThan(48, ico);
                string pt = oShellLink.Path;
                var shortcut = new Shortcut() { Title = Path.GetFileNameWithoutExtension(location), Description = desc, ShortcutType = ShortcutType.UserDefined, Image = i };
                shortcut.Commands.Add(new ProcessStartCommand() { Arguments = args, Target = pt });
                return shortcut;
            }
            else
            {
                Debug.WriteLine("ShortcutsManager : " + folderItem + " is not a lnk shortcut file");
                return GetShortcutFromFile(location);
            }
        }

        public void LoadShortcutFromlnkFile(string location)
        {
            Shortcut shortcut = GetShortcutFromlnkFile(location);
            if (shortcut != null) { Shortcuts.Add(shortcut); }
            else { throw new NullReferenceException(); }
        }

        public static bool CheckShortcutXmlValidity(string location)
        {
            try
            {
                var x = new XmlSerializer(typeof(Shortcut), GetInheritedCommandTypes());
                var w = new StreamReader(location);
                Shortcut shortcut = (Shortcut)x.Deserialize(w);
                if (shortcut != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public void LoadShortcutFromShortcutXml(string location)
        {
            Shortcut shortcut = GetShortcutFromShortcutXml(location);
            if (shortcut != null) { Shortcuts.Add(shortcut); }
            else { throw new NullReferenceException(); }
        }

        private string GetImageFile(string path)
        {
            var pathSplit = path.Split('.');
            string o = "";
            for (int i = 0, loopTo = pathSplit.Length - 2; i <= loopTo; i++)
                o += pathSplit[i] + ".";
            o += "png";
            return o;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Shortcut shortcut = (Shortcut)e.NewItems[0];
                if (shortcut.ShortcutType == ShortcutType.UserDefined)
                {
                    shortcut.DeleteRequested += OnDeleteRequested;
                    shortcut.EditRequested += OnEditRequested;
                    shortcut.EndEditRequested += OnEndEditRequested;
                    BuskInterop.GetUIDispatcher().Invoke(() =>
                    {
                        if (shortcut.ImageSource == null) { shortcut.ImageSource = Utilities.ImageHelper.GetBitmapImage(shortcut.Image); }
                    });
                }
                else if (shortcut.ShortcutType == ShortcutType.ExtensionDefined)
                {
                    BuskInterop.GetUIDispatcher().Invoke(() =>
                    {
                        if (shortcut.ImageSource == null) { shortcut.ImageSource = Utilities.ImageHelper.GetBitmapImage(shortcut.Image); }
                    });
                    shortcut.IsNew = false;
                }
                else if (shortcut.ShortcutType == ShortcutType.Inbuilt)
                {
                    shortcut.ImageSource = DefaultShortcutImageSource;
                    shortcut.IsNew = false;
                }
                shortcut.PinUnpinRequested += OnPinUnpinRequested;
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Shortcut shortcut = (Shortcut)e.OldItems[0];
                if (shortcut.ShortcutType == ShortcutType.UserDefined)
                {
                    shortcut.DeleteRequested -= OnDeleteRequested;
                    shortcut.EditRequested -= OnEditRequested;
                    shortcut.EndEditRequested -= OnEndEditRequested;
                }
                shortcut.PinUnpinRequested -= OnPinUnpinRequested;
            }
        }

        private void OnDeleteRequested(object sender, EventArgs e)
        {
            ShortcutDeleteRequested?.Invoke(sender, e);
        }

        private void OnEditRequested(object sender, EventArgs e)
        {
            ShortcutEditRequested?.Invoke(sender, e);
        }

        private void OnEndEditRequested(object sender, ShortcutEndEditEventArgs e)
        {
            Shortcut shortcut = (Shortcut)sender;
            if (e.Action == ShortcutEditorAction.Save)
            {
                shortcut.IsNew = false;
                SaveShortcut(shortcut);
                if (!Shortcuts.Contains(shortcut))
                {
                    Shortcuts.Add(shortcut);
                }
            }
            else if (e.Action == ShortcutEditorAction.Cancel)
            {
                if (shortcut.IsNew)
                {
                    Shortcuts.Remove(shortcut);
                }
            }

            ShortcutEndEditRequested?.Invoke(sender, e);
        }

        private void OnPinUnpinRequested(object sender, ShortcutPinUnpinEventArgs e)
        {
            ShortcutPinUnpinRequested?.Invoke(sender, e);
        }

        internal void DeleteShortcut(Shortcut shortcut)
        {
            if (shortcut.IsNew) { return; }
            if (File.Exists($@".\Shortcuts\{shortcut.ID}.xml"))
            {
                File.Delete($@".\Shortcuts\{shortcut.ID}.xml");
            }

            if (File.Exists($@".\Shortcuts\{shortcut.ID}.png"))
            {
                File.Delete($@".\Shortcuts\{shortcut.ID}.png");
            }
        }

        internal void SaveShortcut(Shortcut shortcut)
        {
            var x = new XmlSerializer(typeof(Shortcut), GetInheritedCommandTypes());
            var w = new StreamWriter($@".\Shortcuts\{shortcut.ID}.xml");
            ImageHelper.SaveBitmapImage((BitmapImage)shortcut.ImageSource, $@".\Shortcuts\{shortcut.ID}.png");
            x.Serialize(w, shortcut);
            w.Flush();
            w.Close();
            w.Dispose();
        }

        internal static Type[] GetInheritedCommandTypes()
        {
            var alltypes = Assembly.GetExecutingAssembly().GetTypes();
            var commandTypes = new List<Type>();
            foreach(var commandType in alltypes)
            {
                if (commandType.IsSubclassOf(typeof(Command)))
                {
                    commandTypes.Add(commandType);
                }
            }
            return commandTypes.ToArray();
        }

        internal void StartLinkingShortcutLinkCommands()
        {
            var cmdlinks = ShortcutLinker.CommandLinks;
            foreach(var pair in cmdlinks)
            {
                var id = pair.Key;
                var tasksources = pair.Value;
                var shortuct = Shortcuts.FirstOrDefault(s => s.ShortcutType == ShortcutType.UserDefined && s.ID == id);
                foreach(var tasksource in tasksources)
                {
                    tasksource.SetResult(shortuct);
                }
            }
        }
    }
}
