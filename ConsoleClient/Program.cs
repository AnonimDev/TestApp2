using System;
using System.Threading;
using System.Text.Json;
using TestApp2.Models;
using System.Collections.Generic;

namespace ConsoleClient
{
    class Program
    {
        private const int COUNT_TIME = 500;

        private static Queue<BaseModel> queue = new Queue<BaseModel>();

        static void Main(string[] args)
        {
           
            Thread thread = new Thread(new ThreadStart(StartTimer));
            thread.Start();

            Thread.Sleep(1000);

            Worker();
            
            Console.ReadLine();
            #region thread
            //Thread thread = new Thread(new ThreadStart(DoWork));
            //thread.Start();

            //Thread thread2 = new Thread(new ParameterizedThreadStart(DoWork2));
            //thread2.Start(int.MaxValue);

            //int j = 0;
            //for (int i = 0; i < int.MaxValue; i++)
            //{
            //    j++;

            //    if (j % 100000 == 0)
            //    {
            //        Console.WriteLine("Основной поток");
            //    }
            //}
            #endregion
        }

        public static void StartTimer()
        {
            TimerCallback tm = new TimerCallback(GenerationObject);

            Timer timer = new Timer(tm, 0, 0, COUNT_TIME);
        }

        public static void GenerationObject(object obj)
        {
            BaseModel model = new BaseModel
            {
                GUID = Guid.NewGuid(),
                CreatedAt = DateTime.Now
            };
            queue.Enqueue(model);
            Console.WriteLine($"{JsonSerializer.Serialize<BaseModel>(model)}");
        }

        public static void Worker()
        {
            try
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    ClientApi client = new ClientApi();
                    BaseModel model = queue.Dequeue();
                    client.sent(model);
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: {0}", ex.Message);
                Thread.Sleep(1500);
                Worker();
            }
        }
    }
}
