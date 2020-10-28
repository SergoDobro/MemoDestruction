using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimlessProject_09_MemoDestruction
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<LinkedList<decimal>>> linkedLists = new List<List<LinkedList<decimal>>>();

            Int64 iterationsCount = 0;
            try
            {
                List<Task> tasks = new List<Task>();
                long a = 0;
                for (int i = 0; i < 8; i++)
                {
                    tasks.Add(
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {

                            while (true)
                            {
                                a++;
                                for (int iiii = linkedLists.Count; iiii < linkedLists.Count/2; iiii++)
                                {

                                    for (int ii = linkedLists[iiii].Count; i < 100; i++)
                                    {
                                        for (int iii = 0; i < 10; i++)
                                        {
                                            linkedLists[iiii][ii].AddCellToEnd(new LinkedListCell<decimal>(decimal.MaxValue));
                                        }

                                    }
                                    lock ("1")
                                    {
                                        linkedLists[iiii].Add(new LinkedList<decimal>());
                                    }
                                }
                                lock ("")
                                {

                                    linkedLists.Add(new List<LinkedList<decimal>>());
                                }
                                //Console.WriteLine($"IterationsCount is {iterationsCount}");
                                //linkedList.AddCellToEnd(new LinkedListCell<long>(a));
                                iterationsCount++;
                                }
                            }
                            catch (OutOfMemoryException)
                            {
                                Console.WriteLine($"IterationsCount is {iterationsCount}");
                                throw;
                            }
                        }, TaskCreationOptions.LongRunning)
                    );
                    List<LinkedList<decimal>> l = new List<LinkedList<decimal>>();
                    l.Add(new LinkedList<decimal>());
                    linkedLists.Add(l);

                    //foreach (var linkedList in linkedLists)
                    //{

                    //    // создал четрые таски с параметрами запуска LongRunning 
                    //    // чтобы они запустились каждая в своём потоке и максимально 
                    //    // эффективно загрузили мой 4-х ядерный процессор
                    //    //new System.Threading.Thread((x) => {
                    //    //    while (true)
                    //    //    {
                    //    //        a++;
                    //    //        linkedList.AddCellToEnd(new LinkedListCell<long>(a));
                    //    //        iterationsCount++;
                    //    //    }
                    //    //}).Start();
                    //    //Console.WriteLine($"Iteration: "+a);
                    //}
                }
                Console.ReadLine();
            }
            catch (OutOfMemoryException)
            {
                // жду пока у моего компа не закончится память, точнее пока не закончится память, которую ОС может выделить под мой процесс
                // Обычно в таком случае приложуха выбрасывает OutOfMemoryException, которую я и перехватываю строчкой выше
                // записываю в консоль количество добавленных ячеек
                Console.WriteLine($"IterationsCount is {iterationsCount}");
                throw;
            }
            Console.ReadLine();
        }
        public class LinkedListCell<T>
        {
            public T Data { get; set; }
            public LinkedListCell<T> Next { get; set; }

            public LinkedListCell(T data)
            {
                Data = data;
            }
        }

        public class LinkedList<T>
        {
            // головная ячейка списка
            public LinkedListCell<T> TopCell { get; private set; }

            public void AddCellToStart(LinkedListCell<T> cell)
            {
                // если список пустой, то просто заполняем первую ячейку
                if (TopCell == null)
                {
                    TopCell = cell;
                    return;
                }

                // кладём в новую ячейку предыдущую головную ячейку списка
                cell.Next = TopCell;

                // заменяем голову списка на новую
                TopCell = cell;
            }

            public void AddCellToEnd(LinkedListCell<T> lastCell)
            {
                // проверяем, что у нас вообще есть головная ячейка и если нет, то заполняем её
                if (TopCell == null)
                {
                    TopCell = lastCell;
                    return;
                }

                // бежим по ячейкам до самого конца списка. тут чем больше у нас элементов, тем дольше и сложнее бежать
                var currentCell = TopCell;
                while (currentCell.Next != null)
                {
                    currentCell = currentCell.Next;
                }

                currentCell.Next = lastCell;
            }

            public int Count()
            {
                int result = 0;
                var currentCell = TopCell;
                while (currentCell != null)
                {
                    result += 1;
                    currentCell = currentCell.Next;
                }

                return result;
            }
        }

    }
}
