using System;
using System.Threading;

namespace Lab1
{
    public struct ThreadData
    {
        public int Id;
        public bool IsFinished;
    }

    public class Program
    {
        public static void Main()
        {
            ThreadData threadData1 = new ThreadData { Id = 1, IsFinished = false };
            ThreadData threadData2 = new ThreadData { Id = 2, IsFinished = false };

            Thread thread1 = new Thread(() => PrintNumber(ref threadData1));
            Thread thread2 = new Thread(() => PrintNumber(ref threadData2));

            thread1.Start();
            thread2.Start();

            Thread.Sleep(3000);

            threadData1.IsFinished = true;
            threadData2.IsFinished = true;
            
            thread1.Join();
            thread2.Join();

            Console.WriteLine("Все потоки завершены.");
        }

        public static void PrintNumber(ref ThreadData data)
        {
            try
            {
                while (!data.IsFinished)
                {
                    Console.WriteLine($"Поток {data.Id}");
                    Thread.Sleep(500);
                }
                throw new ThreadInterruptedException();
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine($"Поток {data.Id} был прерван, произошла ошибка: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Поток {data.Id} произошла ошибка: {e.Message}");
            }
        }
    }
}
