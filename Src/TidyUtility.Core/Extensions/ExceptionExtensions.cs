using System;
using System.Collections.Generic;
using System.Linq;

namespace TidyUtility.Core.Extensions;

public static class ExceptionExtensions
{
    public static IEnumerable<Exception> GetInnerExceptions(this Exception ex)
    {
        Exception? exc = ex;

        while (exc != null)
        {
            yield return exc;

            if (exc is AggregateException aggregateException)
            {
                foreach (Exception innerExc in aggregateException.InnerExceptions)
                    foreach (Exception aggregateInner in innerExc.GetInnerExceptions())
                        yield return aggregateInner;
            }
            else
            {
                exc = exc.InnerException;
            }
        }
    }

    public static IEnumerable<string> GetExceptionMessages(this Exception ex)
    {
        return ex.GetInnerExceptions()
            .Select(exc =>
            {
                if (exc is IExceptionWithShortMessage excWithShortMessage)
                    return excWithShortMessage.ShortMessage;
                else
                    return exc.Message;
            });
    }
}
