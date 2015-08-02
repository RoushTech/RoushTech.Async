namespace System.Threading.Tasks
{
    public static class FinallyExtension
    {
        public static Task<TResult> Finally<TResult>(this Task<TResult> task, Action finalAction)
        {
            return (Task<TResult>)Finally((Task)task, finalAction);
        }

        public static Task Finally(this Task task, Action finalAction)
        {
            return task.ContinueWith(t =>
            {
                finalAction();
            });
        }
    }
}