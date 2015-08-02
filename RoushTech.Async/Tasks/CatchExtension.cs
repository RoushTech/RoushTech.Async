namespace System.Threading.Tasks
{
    using Linq;

    public static class CatchExtension
    {
        public static Task<TResult> Catch<TResult>(this Task<TResult> task, Action<Exception> exceptionHandler)
        {
            var tcs = new TaskCompletionSource<TResult>();

            task.ContinueWith(t =>
            {
                tcs.SetResult(t.Result);
                if (t.IsCanceled || !t.IsFaulted || exceptionHandler == null)
                {
                    return;
                }

                var innerException = t.Exception.Flatten().InnerExceptions.FirstOrDefault();
                exceptionHandler(innerException ?? t.Exception);
            });

            return tcs.Task;
        }

        public static Task Catch(this Task task, Action<Exception> exceptionHandler)
        {
            return task.ContinueWith(t =>
            {
                if (t.IsCanceled || !t.IsFaulted || exceptionHandler == null)
                {
                    return;
                }

                var innerException = t.Exception.Flatten().InnerExceptions.FirstOrDefault();
                exceptionHandler(innerException ?? t.Exception);
            });
        }
    }
}