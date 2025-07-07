#nullable disable
namespace TidyUtility.Core.Extensions;

public interface IExceptionWithShortMessage
{
    string ShortMessage { get; }
}