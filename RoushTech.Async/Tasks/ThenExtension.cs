namespace System.Threading.Tasks
{
    public static class ThenExtension
    {
        // Inspired by http://blogs.msdn.com/b/pfxteam/archive/2010/11/21/10094564.aspx

        public static Task Then(this Task task, Action<Task> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<AsyncVoid>();
            task.ContinueWith(previousTask =>
            {
                if (previousTask.IsFaulted)
                {
                    tcs.TrySetException(previousTask.Exception);
                }
                else if (previousTask.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    try
                    {
                        next(previousTask);
                        tcs.TrySetResult(default(AsyncVoid));
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }
            });

            return tcs.Task;
        }

        public static Task Then(this Task task, Func<Task, Task> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<AsyncVoid>();

            task.ContinueWith(previousTask =>
            {
                if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
                else if (previousTask.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    try
                    {
                        next(previousTask).ContinueWith(nextTask =>
                        {
                            if (nextTask.IsFaulted) tcs.TrySetException(nextTask.Exception);
                            else if (nextTask.IsCanceled) tcs.TrySetCanceled();
                            else tcs.TrySetResult(default(AsyncVoid));
                        });
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }
            });

            return tcs.Task;
        }

        public static Task<TNextResult> Then<TNextResult>(this Task task, Func<Task, TNextResult> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<TNextResult>();

            task.ContinueWith(previousTask =>
            {
                if (previousTask.IsFaulted)
                {
                    tcs.TrySetException(previousTask.Exception);
                }
                else if (previousTask.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    try
                    {
                        tcs.TrySetResult(next(previousTask));
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }
            });

            return tcs.Task;
        }

        public static Task<TNextResult> Then<TNextResult>(this Task task, Func<Task, Task<TNextResult>> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<TNextResult>();

            task.ContinueWith(previousTask =>
            {
                if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
                else if (previousTask.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    try
                    {
                        next(previousTask).ContinueWith(nextTask =>
                        {
                            if (nextTask.IsFaulted) tcs.TrySetException(nextTask.Exception);
                            else if (nextTask.IsCanceled) tcs.TrySetCanceled();
                            else
                            {
                                try
                                {
                                    tcs.TrySetResult(nextTask.Result);
                                }
                                catch (Exception ex)
                                {
                                    tcs.TrySetException(ex);
                                }
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }
            });

            return tcs.Task;
        }

        public static Task Then<TResult>(this Task<TResult> task, Action<Task<TResult>> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<AsyncVoid>();

            task.ContinueWith(previousTask =>
            {
                if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
                else if (previousTask.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    try
                    {
                        next(previousTask);
                        tcs.TrySetResult(default(AsyncVoid));
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }
            });

            return tcs.Task;
        }

        public static Task Then<TResult>(this Task<TResult> task, Func<Task<TResult>, Task> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<AsyncVoid>();

            task.ContinueWith(previousTask =>
            {
                if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
                else if (previousTask.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    try
                    {
                        next(previousTask).ContinueWith(nextTask =>
                        {
                            if (nextTask.IsFaulted) tcs.TrySetException(nextTask.Exception);
                            else if (nextTask.IsCanceled) tcs.TrySetCanceled();
                            else tcs.TrySetResult(default(AsyncVoid));
                        });
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }
            });

            return tcs.Task;
        }

        public static Task<TNextResult> Then<TResult, TNextResult>(this Task<TResult> task, Func<Task<TResult>, TNextResult> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<TNextResult>();

            task.ContinueWith(previousTask =>
            {
                if (previousTask.IsFaulted)
                {
                    tcs.TrySetException(previousTask.Exception);
                }
                else if (previousTask.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    try
                    {
                        tcs.TrySetResult(next(previousTask));
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }
            });

            return tcs.Task;
        }

        public static Task<TNextResult> Then<TResult, TNextResult>(this Task<TResult> task, Func<Task<TResult>, Task<TNextResult>> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<TNextResult>();

            task.ContinueWith(previousTask =>
            {
                if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception);
                else if (previousTask.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    try
                    {
                        next(previousTask).ContinueWith(nextTask =>
                        {
                            if (nextTask.IsFaulted) tcs.TrySetException(nextTask.Exception);
                            else if (nextTask.IsCanceled) tcs.TrySetCanceled();
                            else
                            {
                                try
                                {
                                    tcs.TrySetResult(nextTask.Result);
                                }
                                catch (Exception ex)
                                {
                                    tcs.TrySetException(ex);
                                }
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }
            });

            return tcs.Task;
        }
                
    }
}