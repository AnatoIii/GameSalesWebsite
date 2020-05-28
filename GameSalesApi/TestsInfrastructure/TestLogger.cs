using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace TestsInfrastructure
{
    /// <summary>
    /// Implementation of <see cref="ILogger{TCategoryName}"/> for tests
    /// </summary>
    /// <typeparam name="TCategoryName"></typeparam>
    public class TestLogger<TCategoryName> : ILogger<TCategoryName>
    {
        public List<string> LoggedMessages { get; set; } = new List<string>();

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            LoggedMessages.Add($"{logLevel.ToString()} {state.ToString()} {exception?.Message} {formatter.ToString()}");
        }
    }
}
