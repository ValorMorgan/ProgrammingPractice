using System.Threading;

namespace ProgrammingPractice.Interfaces
{
    public interface IMultiThreadService
    {
        void SingleThread(CancellationToken token);
        void MultiThread(CancellationToken token);
    }
}
