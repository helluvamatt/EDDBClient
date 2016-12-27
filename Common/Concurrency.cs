using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Concurrency
{
	public interface ILock : IDisposable
	{
		void Release();
	}

	public sealed class ConcurrencyLock : ILock
	{
		private object _lockObject;

		private ConcurrencyLock(object lockObject)
		{
			_lockObject = lockObject;
		}

		public static ILock Obtain(object lockObject)
		{
			if (lockObject == null) throw new ArgumentNullException("lockObject");

			if (Monitor.IsEntered(lockObject))
			{
				return new ConcurrencyLockShill();
			}
			else
			{
				Monitor.Enter(lockObject);
				return new ConcurrencyLock(lockObject);
			}
		}

		public void Release()
		{
			Monitor.Exit(_lockObject);
		}

		public void Dispose()
		{
			Release();
		}

		private sealed class ConcurrencyLockShill : ILock
		{
			public void Release()
			{
				// Do nothing
			}

			public void Dispose()
			{
				// Do nothing
			}
		}
	}
}
