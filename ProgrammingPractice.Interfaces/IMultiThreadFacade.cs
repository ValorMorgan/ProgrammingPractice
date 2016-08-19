using System.Threading;

namespace ProgrammingPractice.Interfaces
{
    public interface IMultiThreadFacade
    {
        int workload { get; }
        void SingleThread(CancellationToken token);
        void MultiThread(CancellationToken token);
    }
}
