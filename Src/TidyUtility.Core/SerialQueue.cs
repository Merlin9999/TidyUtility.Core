 #nullable disable
 using System;
 using System.Threading;
 using System.Threading.Tasks;

 namespace TidyUtility.Core
{
    // Adapted from: https://github.com/Gentlee/SerialQueue

    // License text below was copied directly from the Github page above.

    // The MIT License(MIT)

    // Copyright(c) 2016 Alexander

    // Permission is hereby granted, free of charge, to any person obtaining a copy
    // of this software and associated documentation files (the "Software"), to deal
    // in the Software without restriction, including without limitation the rights
    // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    // copies of the Software, and to permit persons to whom the Software is
    // furnished to do so, subject to the following conditions:

    // The above copyright notice and this permission notice shall be included in all
    // copies or substantial portions of the Software.

    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    // SOFTWARE.


    public class SerialQueue
    {
        SpinLock _spinLock = new(false);
        readonly WeakReference<Task?> _lastTask = new(null);

        public Task Enqueue(Action action)
        {
            bool gotLock = false;
            try
            {
                Task? lastTask;
                Task resultTask;

                _spinLock.Enter(ref gotLock);

                if (_lastTask.TryGetTarget(out lastTask))
                {
                    resultTask = lastTask.ContinueWith(_ => action(), TaskContinuationOptions.ExecuteSynchronously);
                }
                else
                {
                    resultTask = Task.Run(action);
                }

                _lastTask.SetTarget(resultTask);

                return resultTask;
            }
            finally
            {
                if (gotLock) _spinLock.Exit(false);
            }
        }

        public Task<T> Enqueue<T>(Func<T> function)
        {
            bool gotLock = false;
            try
            {
                Task? lastTask;
                Task<T> resultTask;

                _spinLock.Enter(ref gotLock);

                if (_lastTask.TryGetTarget(out lastTask))
                {
                    resultTask = lastTask.ContinueWith(_ => function(), TaskContinuationOptions.ExecuteSynchronously);
                }
                else
                {
                    resultTask = Task.Run(function);
                }

                _lastTask.SetTarget(resultTask);

                return resultTask;
            }
            finally
            {
                if (gotLock) _spinLock.Exit(false);
            }
        }

        public Task Enqueue(Func<Task> asyncAction)
        {
            bool gotLock = false;
            try
            {
                Task? lastTask;
                Task resultTask;

                _spinLock.Enter(ref gotLock);

                if (_lastTask.TryGetTarget(out lastTask))
                {
                    resultTask = lastTask.ContinueWith(_ => asyncAction(), TaskContinuationOptions.ExecuteSynchronously).Unwrap();
                }
                else
                {
                    resultTask = Task.Run(asyncAction);
                }

                _lastTask.SetTarget(resultTask);

                return resultTask;
            }
            finally
            {
                if (gotLock) _spinLock.Exit(false);
            }
        }

        public Task<T> Enqueue<T>(Func<Task<T>> asyncFunction)
        {
            bool gotLock = false;
            try
            {
                Task? lastTask;
                Task<T> resultTask;

                _spinLock.Enter(ref gotLock);

                if (_lastTask.TryGetTarget(out lastTask))
                {
                    resultTask = lastTask.ContinueWith(_ => asyncFunction(), TaskContinuationOptions.ExecuteSynchronously).Unwrap();
                }
                else
                {
                    resultTask = Task.Run(asyncFunction);
                }

                _lastTask.SetTarget(resultTask);

                return resultTask;
            }
            finally
            {
                if (gotLock) _spinLock.Exit(false);
            }
        }
    }
}
