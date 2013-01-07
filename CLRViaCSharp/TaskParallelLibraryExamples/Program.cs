using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskParallelLibraryExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskParallelisation();

            DataParallelisation();
        }

        private static void DataParallelisation()
        {
            // For and ForEach members of Parallel class partition the source collection, so that multiple threads can work concurrently.

            // Do not have to create threads or queue work items

            BasicForEach();

            BasicFor();

            ForWithBreak();

            ThreadLocalVariablesFor();

            ThreadLocalVariablesForEach();

            CancellationOfParallelFor();
        }

        private static void CancellationOfParallelFor()
        {
            var nums = Enumerable.Range(0, 100000);

            var cts = new CancellationTokenSource();
            var options = new ParallelOptions()
            {
                CancellationToken = cts.Token,
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            Console.WriteLine("Press any key to start, Press 'c' to cancel");
            Console.ReadKey();

            Task.Factory.StartNew(() =>
            {
                if (Console.ReadKey().KeyChar == 'c')
                    cts.Cancel();
            });

            try
            {
                Parallel.ForEach(nums, options, num =>
                {
                    var d = Math.Sqrt(num);
                    Console.WriteLine("{0} on {1}", d,
                                      Thread.CurrentThread.ManagedThreadId);
                    options.CancellationToken.ThrowIfCancellationRequested();
                });
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ThreadLocalVariablesForEach()
        {
            var nums = Enumerable.Range(0, 100000).ToArray();

            long total = 0;
            Parallel.ForEach<int, long>(nums,
                                        () => 0,
                                        (j, loop, subtotal) =>
                                        {
                                            subtotal += nums[j];
                                            return subtotal;
                                        },
                                        (finalresult) => Interlocked.Add(ref total, finalresult));
            Console.WriteLine("The total from Parallel.ForEach is {0}", total);
        }

        private static void ThreadLocalVariablesFor()
        {
            var nums = Enumerable.Range(0, 100000).ToArray();
            long total = 0;
            Parallel.For<long>(0, nums.Length, () => 0, (j, loop, subtotal) =>
            {
                subtotal += nums[j];
                return subtotal;
            },
                //x => Interlocked.Add(ref total, x) 
                                                            x => total += x
                                                            );

            Console.WriteLine("The thread local example total is {0}", total);
        }

        private static void ForWithBreak()
        {
            Parallel.For(0, 10000, (i, state) =>
            {
                if (i % 4 == 0)
                {
                    Console.WriteLine("Breaking on {0} because {1}", i, i % 4);
                    state.Stop();
                }
                Console.WriteLine("{0}", i);
            });
        }

        private static void BasicFor()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Parallel For");
            Parallel.For(0, 10, n => Console.WriteLine("{0} from thread: {1}", n + n, Thread.CurrentThread.ManagedThreadId));

        }

        private static void BasicForEach()
        {
            var list = Enumerable.Range(1, 10).ToList();

            Parallel.ForEach(list, item => Console.WriteLine(item));

            Console.WriteLine("");
            Console.WriteLine("Squares");
            Parallel.ForEach(list, item => Console.WriteLine(Convert.ToDouble(Math.Pow(item, 2))));
        }

        private static void TaskParallelisation()
        {
            ConstructTaskWithStartFromLambda();
            ConstructTaskWithRun();
            ConstructTaskWithFactory();
            ConstructSpecifyingCreationOptions();

            TaskContinuation1();

            DetachedChildTask();

            //Console.WriteLine("Enter to continue"); 
            //Console.ReadKey();

            AttachedChildTask();
            WaitingOnTasks();
            //CancellationOfTasks();

            ExceptionHandling1();
            ExceptionHandling2();

            Thread.Sleep(1000);
        }

        private static void ExceptionHandling2()
        {
            Task<int> task = Task.Factory.StartNew(() =>
            {
                var x = 0;
                return 7 / x;
            });

            task.ContinueWith(t =>
            {
                switch (task.Status)
                {
                    case TaskStatus.RanToCompletion:
                        {
                            Console.WriteLine(task.Result);
                        }
                        break;
                    case TaskStatus.Faulted:
                        {
                            if (task.Exception != null)
                            {
                                Console.WriteLine(task.Exception.InnerException.Message);

                            }
                            else
                                Console.WriteLine("Didn't expect to get here");
                        }
                        break;
                }
            });
        }

        private static void ExceptionHandling1()
        {
            int x = 0;
            Task<int> task = Task.Factory.StartNew(() => 7 / x);
            try
            {
                int result = task.Result;
            }
            catch (AggregateException aex)
            {
                Console.WriteLine(aex.InnerException.Message);
            }
        }

        private static void CancellationOfTasks()
        {
            Console.WriteLine("Press any key to start. Press 'c' to cancel.");
            Console.ReadKey();

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            // Store references to the tasks so that we can wait on them and 
            // observe their status after cancellation. 
            Task[] tasks = new Task[10];

            // Request cancellation of a single task when the token source is canceled. 
            // Pass the token to the user delegate, and also to the task so it can 
            // handle the exception correctly. 
            tasks[0] = Task.Factory.StartNew(() => DoSomeWork(1, token), token);

            // Request cancellation of a task and its children. Note the token is passed 
            // to (1) the user delegate and (2) as the second argument to StartNew, so 
            // that the task instance can correctly handle the OperationCanceledException. 
            tasks[1] = Task.Factory.StartNew(() =>
            {
                // Create some cancelable child tasks. 
                for (int i = 2; i < 10; i++)
                {
                    // For each child task, pass the same token 
                    // to each user delegate and to StartNew. 
                    tasks[i] = Task.Factory.StartNew(iteration =>
                                DoSomeWork((int)iteration, token), i, token);
                }
                // Passing the same token again to do work on the parent task. 
                // All will be signaled by the call to tokenSource.Cancel below. 
                DoSomeWork(2, token);
            }, token);

            // Give the tasks a second to start. 
            Thread.Sleep(1000);

            // Request cancellation from the UI thread. 
            if (Console.ReadKey().KeyChar == 'c')
            {
                tokenSource.Cancel();
                Console.WriteLine("\nTask cancellation requested.");

                // Optional: Observe the change in the Status property on the task. 
                // It is not necessary to wait on tasks that have canceled. However, 
                // if you do wait, you must enclose the call in a try-catch block to 
                // catch the OperationCanceledExceptions that are thrown. If you do 
                // not wait, no OCE is thrown if the token that was passed to the 
                // StartNew method is the same token that requested the cancellation.

                #region Optional_WaitOnTasksToComplete
                try
                {
                    Task.WaitAll(tasks);
                }
                catch (AggregateException e)
                {
                    // For demonstration purposes, show the OCE message. 
                    foreach (var v in e.InnerExceptions)
                        Console.WriteLine("msg: " + v.Message);
                }

                // Prove that the tasks are now all in a canceled state. 
                for (int i = 0; i < tasks.Length; i++)
                    Console.WriteLine("task[{0}] status is now {1}", i, tasks[i].Status);
                #endregion
            }

            // Keep the console window open while the 
            // task completes its output. 
            Console.ReadLine();
        }

        static void DoSomeWork(int taskNum, CancellationToken ct)
        {
            // Was cancellation already requested? 
            if (ct.IsCancellationRequested)
            {
                Console.WriteLine("We were cancelled before we got started.");
                Console.WriteLine("Press Enter to quit.");
                ct.ThrowIfCancellationRequested();
            }
            int maxIterations = 1000;

            // NOTE!!! A benign "OperationCanceledException was unhandled 
            // by user code" error might be raised here. Press F5 to continue. Or, 
            //  to avoid the error, uncheck the "Enable Just My Code" 
            // option under Tools > Options > Debugging. 
            for (int i = 0; i < maxIterations; i++)
            {
                // Do a bit of work. Not too much. 
                var sw = new SpinWait();
                for (int j = 0; j < 3000; j++) sw.SpinOnce();
                Console.WriteLine("...{0} ", taskNum);
                if (ct.IsCancellationRequested)
                {
                    Console.WriteLine("bye from {0}.", taskNum);
                    Console.WriteLine("\nPress Enter to quit.");

                    ct.ThrowIfCancellationRequested();
                }
            }
        }

        private static void WaitingOnTasks()
        {
            Task[] tasks = new Task[2] 
                               { 
                                   Task.Factory.StartNew(() => Console.WriteLine("Task One")), 
                                   Task.Factory.StartNew(() => Console.WriteLine("Task Two")) 
                               };

            Task.WaitAll(tasks); //block 
        }

        private static void AttachedChildTask()
        {
            Console.WriteLine("Parent task beginning");

            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Child task beginning");
                var child = Task.Factory.StartNew(() =>
                {
                    Thread.SpinWait(500000);
                    Console.WriteLine("Attached child completed");
                }, TaskCreationOptions.AttachedToParent);

            });
            parent.Wait();
            Console.WriteLine("Parent task completed");
        }

        private static void DetachedChildTask()
        {
            var outer = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Outer task beginning");

                var child = Task.Factory.StartNew(() =>
                {
                    Thread.SpinWait(500000);
                    Console.WriteLine("Detached task completed");
                });
            });

            outer.Wait();
            Console.WriteLine("Outer task completed");
        }

        private static void TaskContinuation1()
        {
            Task<byte[]> getData = new Task<byte[]>(() => new byte[] { 1, 2, 3 });
            Task<double[]> analyseData = getData.ContinueWith(s => Analyse(s.Result));
            Task<string> reportData = analyseData.ContinueWith(d => Summarise(d.Result));

            getData.Start();

            Console.WriteLine(reportData.Result);
        }

        private static string Summarise(double[] result)
        {
            return result.Select(x => x.ToString()).Aggregate((x, y) => x.ToString() + " " + y.ToString());
        }

        private static double[] Analyse(byte[] result)
        {
            var things = result.Select(x => x * 2.0);
            return things.ToArray();
        }

        private static void ConstructTaskWithRun()
        {
            //.net 4.5? 
            //var t = new Task(() => Console.WriteLine("Hello from run")); 
            //t.Run(); 
        }

        private static void ConstructTaskWithStartFromLambda()
        {
            //need the wait handle otherwise, app might exit before task finishes 
            var evt = new ManualResetEvent(false);

            var taskA = new Task(() =>
            {
                Console.WriteLine("Hello from Task A");
                evt.Set();
            });
            taskA.Start();

            Console.WriteLine("Hello from the calling thread");
            evt.WaitOne(1000);
        }

        private static void ConstructTaskWithFactory()
        {
            //preferred way if creating and starting of threads do not have to be separated. 
            var task = Task.Factory.StartNew(() => Console.WriteLine("Hello from Task from factory"));
            Console.WriteLine("Hello from joining thread");
        }

        private static void ConstructSpecifyingCreationOptions()
        {
            var task = Task.Factory.StartNew(() => Console.WriteLine("specifying some creation options"), TaskCreationOptions.LongRunning);

        }
    } 
}
