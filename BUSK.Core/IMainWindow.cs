using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace BUSK.Core
{
    public interface IMainWindow
    {
        bool Visible { get; set; }

        bool Paused { get; set; }

        bool IsSettingsWindowVisible { get; }

        ContextMenu TrayContextMenu { get; }

        void PauseCounters();

        void ResumeCounters();

        void ToggleCounterState();

        void ToggleVisible();

        void ToggleSettingsWindowVisibility();

        void ShowSettingsWindow();

        void HideSettingsWindow();
    }
}
