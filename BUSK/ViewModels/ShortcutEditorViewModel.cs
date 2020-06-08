using BUSK.Controls;
using BUSK.Controls.Shortcutting;
using BUSK.Core;
using BUSK.Core.Shortcutting;
using BUSK.Core.Shortcutting.Commands;
using ModernWpf.Controls;
using System;
using System.Windows.Input;

namespace BUSK.ViewModels
{
    public class ShortcutEditorViewModel : BindableBase
    {
        public static ShortcutEditorViewModel Instance { get; set; }

        #region Properties

        private Shortcut currentEdittingShortcut;

        public Shortcut CurrentEdittingShortcut
        {
            get { return currentEdittingShortcut; }
            set { SetPropertyValue(ref currentEdittingShortcut, value); }
        }

        private ICommand addCommandCommand;

        public ICommand AddCommandCommand
        {
            get { return addCommandCommand; }
            set { SetPropertyValue(ref addCommandCommand, value); }
        }

        private ICommand selectImageCommand;

        public ICommand SelectImageCommand
        {
            get { return selectImageCommand; }
            set { SetPropertyValue(ref selectImageCommand, value); }
        }

        #endregion

        public ShortcutEditorViewModel()
        {
            SelectImageCommand = new RelayCommand((param) => SelectImage(), (param) => CurrentEdittingShortcut != null);
            AddCommandCommand = new RelayCommand((param) => AddCommandAsync(), (param) => CurrentEdittingShortcut != null);
        }

        private void SelectImage()
        {
            if (CurrentEdittingShortcut == null) return;

            var ipd = new ImagePickerDialog();
            var result = ipd.ShowDialog();
            if (result.HasValue & result.Value)
            {
                CurrentEdittingShortcut.ImageSource = ipd.ImageSource;
            }
        }

        private async void AddCommandAsync()
        {
            var cmdItems = CommandsHelper.GetCommandItems();

            SettingsWindow.EnsureInstanceAndShow();

            CommandSelectionDialog commandSelectionDialog  = new CommandSelectionDialog()
            {
                Owner = SettingsWindow.Instance
            };
            commandSelectionDialog.CommandItems.ItemsSource = cmdItems;

            var result = await commandSelectionDialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
            {
                return;
            }
            if (commandSelectionDialog.CommandItems.SelectedItem is CommandItem cmdItem)
            {
                var command = (Command)Activator.CreateInstance(cmdItem.CommandType);

                if (command != null)
                {
                    CurrentEdittingShortcut.Commands.Add(command);
                }
            }
        }
    }
}
