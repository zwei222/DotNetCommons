// ---------------------------------------------------------------------
// <copyright file="TaskExtension.cs" company="zwei222">
// Copyright (c) zwei222. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCommons.System.Threading.Tasks
{
    /// <summary>
    /// Class that extends Task class.
    /// </summary>
    public static class TaskExtension
    {
        /// <summary>
        /// Performs asynchronous processing synchronously.
        /// </summary>
        /// <param name="self">Myself.</param>
        /// <param name="action">Continuation tasks.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public static void ToSync(this Task self, Action<Task> action = null, CancellationToken cancellationToken = default)
        {
            self.ContinueWith(action ?? (_ => { }), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Convert to ValueTask type.
        /// </summary>
        /// <param name="self">Myself.</param>
        /// <returns>The converted ValueTask type object.</returns>
        public static ValueTask ToValueTask(this Task self)
        {
            return new ValueTask(self);
        }

        /// <summary>
        /// Convert to ValueTask type.
        /// </summary>
        /// <typeparam name="TResult">Type parameter.</typeparam>
        /// <param name="self">Myself.</param>
        /// <returns>The converted ValueTask type object.</returns>
        public static ValueTask<TResult> ToValueTask<TResult>(this Task<TResult> self)
        {
            return new ValueTask<TResult>(self);
        }
    }
}
