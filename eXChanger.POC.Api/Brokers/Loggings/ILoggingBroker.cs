using System;

namespace eXChanger.POC.Api.Brokers.Loggings
{
	public interface ILoggingBroker
	{
		void LogError(Exception exception);
		void LogCritical(Exception exception);
	}
}
