using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Logging
{
	public interface ILogger
	{
		void Fatal(string msg, params object[] args);
		void Fatal(Exception ex, string msg, params object[] args);
		void Error(string msg, params object[] args);
		void Error(Exception ex, string msg, params object[] args);
		void Warn(string msg, params object[] args);
		void Warn(Exception ex, string msg, params object[] args);
		void Info(string msg, params object[] args);
		void Debug(string msg, params object[] args);
		void Verbose(string msg, params object[] args);
	}
}
