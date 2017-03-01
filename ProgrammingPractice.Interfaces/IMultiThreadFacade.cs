using System.Threading;

namespace ProgrammingPractice.Interfaces
{
    public interface IMultiThreadFacade
    {
        void SingleThread(CancellationToken token);
        void MultiThread(CancellationToken token);
    }
}
