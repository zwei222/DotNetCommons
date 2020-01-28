// ---------------------------------------------------------------------
// <copyright file="AbstractDisposable.cs" company="zwei222">
// Copyright (c) zwei222. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace DotNetCommons.System
{
    /// <summary>
    /// Abstract class on which disposable objects are based.
    /// </summary>
    public abstract class AbstractDisposable : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Finalizes an instance of the <see cref="AbstractDisposable"/> class.
        /// </summary>
        ~AbstractDisposable()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Indicates whether the resource has been disposed of.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Dispose of resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of resources asynchronously.
        /// </summary>
        /// <returns>An awaitable result.</returns>
        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsync(true).ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of managed resources.
        /// </summary>
        protected abstract void DisposeManaged();

        /// <summary>
        /// Dispose of managed resources asynchronously.
        /// </summary>
        /// <returns>An awaitable result.</returns>
        protected abstract ValueTask DisposeManagedAsync();

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        protected abstract void DisposeUnmanaged();

        /// <summary>
        /// Dispose of unmanaged resources asynchronously.
        /// </summary>
        /// <returns>An awaitable result.</returns>
        protected abstract ValueTask DisposeUnmanagedAsync();

        /// <summary>
        /// Dispose of resources.
        /// </summary>
        /// <param name="disposing">Indicates whether it was called from the Dispose method or the finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                this.DisposeManaged();
            }

            this.DisposeUnmanaged();
            this.IsDisposed = true;
        }

        /// <summary>
        /// Dispose of resources asynchronously.
        /// </summary>
        /// <param name="disposing">Indicates whether it was called from the Dispose method or the finalizer.</param>
        /// <returns>An awaitable result.</returns>
        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                await this.DisposeManagedAsync().ConfigureAwait(false);
            }

            await this.DisposeUnmanagedAsync().ConfigureAwait(false);
            this.IsDisposed = true;
        }
    }
}
