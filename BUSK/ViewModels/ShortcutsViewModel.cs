using BUSK.Core;
using BUSK.Core.Shortcutting;
using BUSK.Core.Shortcutting.Commands;
using BUSK.Navigation.Pages;
using ModernWpf.Controls;
using ModernWpf.Media.Animation;
using System;
using System.Windows.Input;

namespace BUSK.ViewModels
{
    public class ShortcutsViewModel : BindableBase
    {
        public static ShortcutsViewModel Instance { get; set; }

        #region Properties

        private bool isEditorEnabled = false;

        public bool IsEditorEnabled
        {
            get { return isEditorEnabled; }
            set { SetPropertyValue(ref isEditorEnabled, value); }
        }

        private ICommand addShortcutCommand;

        public ICommand AddShortcutCommand
        {
            get { return addShortcutCommand; }
            set { SetPropertyValue(ref addShortcutCommand, value); }
        }

        #endregion

        public ShortcutsViewModel()
        {
            ShortcutsManager.Instance.ShortcutEditRequested += Instance_ShortcutEditRequested;
            ShortcutsManager.Instance.ShortcutEndEditRequested += Instance_ShortcutEndEditRequestedAsync;
            ShortcutsManager.Instance.ShortcutPinUnpinRequested += Instance_ShortcutPinUnpinRequested;
            ShortcutsManager.Instance.ShortcutDeleteRequested += Instance_ShortcutDeleteRequestedAsync;

            AddShortcutCommand = new RelayCommand((param) =>
            {
                Shortcut shortcut = new Shortcut() { Title = "New Shortcut", ImageSource = ShortcutsManager.Instance.DefaultShortcutImageSource };
                shortcut.Commands.Add(new ProcessStartCommand() { Target = "cmd.exe" });
                ShortcutsManager.Instance.Shortcuts.Add(shortcut);
                IsEditorEnabled = true;
                ShortcutEditorViewModel.Instance.CurrentEdittingShortcut = shortcut;
                SettingsWindow.Instance?.ContentFrame.Navigate(typeof(ShortcutEditorPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            });
        }

        private async void Instance_ShortcutDeleteRequestedAsync(object sender, EventArgs e)
        {
            Shortcut shortcut = (Shortcut)sender;

            SettingsWindow.EnsureInstanceAndShow();

            ContentDialog contentDialog = new ContentDialog()
            {
                Owner = SettingsWindow.Instance,
                Title = "Delete shortcut?",
                Content = "Shortcut will be removed but you can choose to keep it to load next time.",
                PrimaryButtonText = "Delete file",
                SecondaryButtonText = "Don't delete file",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Secondary
            };
            var result = await contentDialog.ShowAsync();

            void RemoveAndCloseEditor()
            {
                ShortcutsManager.Instance.Shortcuts.Remove(shortcut);
                CloseEditor();
            }
            if (result == ContentDialogResult.Primary)
            {
                RemoveAndCloseEditor();
                ShortcutsManager.Instance.DeleteShortcut(shortcut);
            }
            else if (result == ContentDialogResult.Secondary)
            {
                RemoveAndCloseEditor();
            }
            else
            {
                return;
            }
        }

        private void Instance_ShortcutEditRequested(object sender, EventArgs e)
        {
            IsEditorEnabled = true;
            ShortcutEditorViewModel.Instance.CurrentEdittingShortcut = sender as Shortcut;
            SettingsWindow.Instance?.ContentFrame.Navigate(typeof(ShortcutEditorPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void Instance_ShortcutEndEditRequestedAsync(object sender, ShortcutEndEditEventArgs e)
        {
            if (e.Action == ShortcutEditorAction.Save)
            {
                e.EndedEditing = true;
                CloseEditor();
            }
            else if (e.Action == ShortcutEditorAction.Cancel)
            {
                SettingsWindow.EnsureInstanceAndShow();

                ContentDialog contentDialog = new ContentDialog()
                {
                    Owner = SettingsWindow.Instance,
                    Title = "Cancel edit?",
                    Content = "Changes in shortcut will not be saved, in case of shortcuts that are not already saved they will be removed.",
                    PrimaryButtonText = "Cancel editing",
                    CloseButtonText = "Continue editing",
                    DefaultButton = ContentDialogButton.Close
                };
                var result = await contentDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    e.EndedEditing = true;
                }
                else
                {
                    return;
                }
            }
        }

        private void CloseEditor()
        {
            ShortcutEditorViewModel.Instance.CurrentEdittingShortcut = null;
            if (SettingsWindow.Instance?.ContentFrame.SourcePageType == typeof(ShortcutEditorPage))
            {
                SettingsWindow.Instance?.ContentFrame.Navigate(typeof(ShortcutsPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
            }
            IsEditorEnabled = false;
        }

        private void Instance_ShortcutPinUnpinRequested(object sender, ShortcutPinUnpinEventArgs e)
        {
            
        }
    }
}
