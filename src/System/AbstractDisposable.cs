using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCommons.System
{
    public abstract class AbstractDisposable : IDisposable, IAsyncDisposable
    {
        ~AbstractDisposable()
        {
            this.Dispose(false);
        }

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsync(true).ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }

        protected abstract void DisposeManaged();

        protected abstract ValueTask DisposeManagedAsync();

        protected abstract void DisposeUnmanaged();

        protected abstract ValueTask DisposeUnmanagedAsync();

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
