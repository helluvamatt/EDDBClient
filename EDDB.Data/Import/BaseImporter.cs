using Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace EDDB.Data.Import
{
	// TODO Async processing
	public abstract class BaseImporter : IImporter
	{
		private IReportProgress _Progress;

		protected Database _Database { get; private set; }

		protected ILogger _Logger { get; private set; }

		public BaseImporter(ILogger logger, Database database, IReportProgress progress) : this(logger, database)
		{
			_Progress = progress;
		}

		public BaseImporter(ILogger logger, Database database)
		{
			_Database = database;
			_Logger = logger;
			Errors = new List<string>();
		}

		public List<string> Errors { get; private set; }

		public void Import(TextReader reader)
		{
			Errors.Clear();
			try
			{
				using (_Database.LockDatabase())
				{
					DoImport(reader);
				}
			}
			catch (Exception ex)
			{
				ReportError("Fatal error occurred during import", ex);
			}
		}

		protected abstract void DoImport(TextReader reader);

		protected void ReportError(string msg)
		{
			Errors.Add(msg);
			if (_Progress != null) _Progress.OnError(msg);
		}

		protected void ReportError(string msg, Exception ex)
		{
			string message = string.Format("{0}: {1}", msg, ex.Message);
			ReportError(message);
		}

		protected void ReportError(string msg, Exception ex, string debugLine)
		{
			ReportError(msg, ex);

		}

		protected void ReportProgress(float progress)
		{
			if (_Progress != null) _Progress.OnProgress(progress);
		}

		protected void ReportProgress(float progress, float total)
		{
			if (total <= 0) total = 1;
			if (progress <= 0) total = 0;
			ReportProgress(progress / total);
		}

		protected void ReportStarted()
		{
			if (_Progress != null) _Progress.OnStarted();
		}

		protected void ReportFinished(int recordsChanged)
		{
			if (_Progress != null) _Progress.OnFinished(recordsChanged);
		}
	}
}
