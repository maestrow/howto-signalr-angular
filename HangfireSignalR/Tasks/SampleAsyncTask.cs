using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRAngular.Tasks
{
    public static class SampleAsyncTask
    {
        public static async Task StartCalculation(int timeDelay, int failAfter, CancellationToken token, IProgress<int> progress)
        {
            for (int i = 0; i <= 100; i++)
            {
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
                
                if (progress != null)
                    progress.Report(i);

                if (failAfter > 0 && i > failAfter)
                    throw new Exception("task failed");

                await Task.Delay(timeDelay / 100);
            }
        }
    }
}