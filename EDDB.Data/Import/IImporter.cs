using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDDB.Data.Import
{
	public interface IImporter
	{
		void Import(TextReader reader);
		List<string> Errors { get; }
	}

	public interface IReportProgress
	{
		void OnStarted();
		void OnFinished(int recordsChanged);
		void OnProgress(float progress);
		void OnError(string message);
	}
}
