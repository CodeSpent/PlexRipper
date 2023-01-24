using System.Runtime.CompilerServices;
using Serilog.Events;

namespace Logging.Interface;

public interface ILog
{
    #region Debug



    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="memberName">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceFilePath">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceLineNumber">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <example><code>
    /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
    /// </code></example>
    void DebugLine(
        string messageTemplate,
        [CallerMemberName] string memberName = default!,
        [CallerFilePath] string sourceFilePath = default!,
        [CallerLineNumber] int sourceLineNumber = default!);

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="memberName">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceFilePath">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceLineNumber">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    /// <example>
    /// Log.Verbose("Staring into space, wondering if we're alone.");
    /// </example>
    void Debug<T>(
        string messageTemplate,
        T propertyValue = default!,
        [CallerMemberName] string memberName = default!,
        [CallerFilePath] string sourceFilePath = default!,
        [CallerLineNumber] int sourceLineNumber = default!);

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="memberName">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceFilePath">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceLineNumber">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <example>
    /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
    /// </example>
    void Debug<T0, T1>(
        string messageTemplate,
        T0 propertyValue0,
        T1 propertyValue1,
        [CallerMemberName] string memberName = default!,
        [CallerFilePath] string sourceFilePath = default!,
        [CallerLineNumber] int sourceLineNumber = default!);

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    /// <param name="memberName">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceFilePath">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceLineNumber">This is automatically passed by the Caller Information and should not be filled in.</param>    /// <example>
    /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
    /// </example>
    void Debug<T0, T1, T2>(
        string messageTemplate,
        T0 propertyValue0,
        T1 propertyValue1,
        T2 propertyValue2,
        [CallerMemberName] string memberName = default!,
        [CallerFilePath] string sourceFilePath = default!,
        [CallerLineNumber] int sourceLineNumber = default!);

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="memberName">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceFilePath">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceLineNumber">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <example>
    /// Log.Debug(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
    /// </example>
    void Debug(
        Exception exception,
        string messageTemplate,
        [CallerMemberName] string memberName = default!,
        [CallerFilePath] string sourceFilePath = default!,
        [CallerLineNumber] int sourceLineNumber = default!);


    #endregion

    #region Information



    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="memberName">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceFilePath">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceLineNumber">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <example><code>
    /// Log.Information("Starting up at {StartedAt}.", DateTime.Now);
    /// </code></example>
    void InformationLine(
        string messageTemplate,
        [CallerMemberName] string memberName = default!,
        [CallerFilePath] string sourceFilePath = default!,
        [CallerLineNumber] int sourceLineNumber = default!);

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="memberName">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceFilePath">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceLineNumber">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    /// <example>
    /// Log.Verbose("Staring into space, wondering if we're alone.");
    /// </example>
    void Information<T>(
        string messageTemplate,
        T propertyValue = default!,
        [CallerMemberName] string memberName = default!,
        [CallerFilePath] string sourceFilePath = default!,
        [CallerLineNumber] int sourceLineNumber = default!);

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="memberName">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceFilePath">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceLineNumber">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <example>
    /// Log.Information("Starting up at {StartedAt}.", DateTime.Now);
    /// </example>
    void Information<T0, T1>(
        string messageTemplate,
        T0 propertyValue0,
        T1 propertyValue1,
        [CallerMemberName] string memberName = default!,
        [CallerFilePath] string sourceFilePath = default!,
        [CallerLineNumber] int sourceLineNumber = default!);

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    /// <param name="memberName">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceFilePath">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceLineNumber">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <example>
    /// Log.Information("Starting up at {StartedAt}.", DateTime.Now);
    /// </example>
    void Information<T0, T1, T2>(
        string messageTemplate,
        T0 propertyValue0,
        T1 propertyValue1,
        T2 propertyValue2,
        [CallerMemberName] string memberName = default!,
        [CallerFilePath] string sourceFilePath = default!,
        [CallerLineNumber] int sourceLineNumber = default!);

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="memberName">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceFilePath">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <param name="sourceLineNumber">This is automatically passed by the Caller Information and should not be filled in.</param>
    /// <example>
    /// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
    /// </example>
    void Information(
        Exception exception,
        string messageTemplate,
        [CallerMemberName] string memberName = default!,
        [CallerFilePath] string sourceFilePath = default!,
        [CallerLineNumber] int sourceLineNumber = default!);


    #endregion
}
