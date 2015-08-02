namespace System.Threading.Tasks
{
    public static class FinallyExtension
    {
        public static Task<TResult> Finally<TResult>(this Task<TResult> task, Action finalAction)
        {
            var tcs = new TaskCompletionSource<TResult>();
            task.ContinueWith(t =>
            {
                finalAction();
                tcs.SetResult(t.Result);
            });
            return tcs.Task;
        }

        public static Task Finally(this Task task, Action finalAction)
        {
            var tcs = new TaskCompletionSource<AsyncVoid>();
            task.ContinueWith(t =>
            {
                finalAction();
                tcs.SetResult(default(AsyncVoid));
            });
            return tcs.Task;
        }
    }
}