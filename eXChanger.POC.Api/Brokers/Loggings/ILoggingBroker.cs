using System;

namespace eXChanger.POC.Brokers.Loggings
{
	public interface ILoggingBroker
	{
		void LogError(Exception exception);
		void LogCritical(Exception exception);
	}
}
