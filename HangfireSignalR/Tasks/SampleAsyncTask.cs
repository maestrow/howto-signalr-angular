using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HangfireSignalR.Tasks
{
    public static class SampleAsyncTask
    {
        public static async Task StartCalculation(int timeDelay, CancellationToken token, IProgress<int> progress)
        {
            for (int i = 0; i <= 100; i++)
            {
                if (token.IsCancellationRequested)
                {
                    if (progress != null)
                        progress.Report(100);
                    token.ThrowIfCancellationRequested();
                }
                if (progress != null)
                    progress.Report(i);

                await Task.Delay(timeDelay / 100);
            }

            if (progress != null)
                progress.Report(100);
        }
    }
}