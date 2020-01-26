using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCommons.System.Threading.Tasks
{
    public static class TaskExtension
    {
        public static void ToSync(this Task self, Action<Task> action = null, CancellationToken cancellationToken = default)
        {
            self.ContinueWith(action ?? (_ => { }), cancellationToken).ConfigureAwait(false);
        }

        public static ValueTask ToValueTask(this Task self)
        {
            return new ValueTask(self);
        }

        public static ValueTask<T> ToValueTask<T>(this Task<T> self)
        {
            return new ValueTask<T>(self);
        }
    }
}
