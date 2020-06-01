using System.Collections.Generic;
using System.Threading.Tasks;

namespace BUSK.Core.Shortcutting
{
    internal class ShortcutLinker
    {
        internal static Dictionary<string, List<TaskCompletionSource<Shortcut>>> CommandLinks = new Dictionary<string, List<TaskCompletionSource<Shortcut>>>();

        internal static async Task<Shortcut> GetShortcutAsync(string id)
        {
            TaskCompletionSource<Shortcut> taskCompletionSource = new TaskCompletionSource<Shortcut>();
            if (!CommandLinks.ContainsKey(id))
            {
                CommandLinks.Add(id, new List<TaskCompletionSource<Shortcut>>());
            }
            CommandLinks[id].Add(taskCompletionSource);
            var s = await taskCompletionSource.Task;
            CommandLinks.Remove(id);
            return s;
        }
    }
}
