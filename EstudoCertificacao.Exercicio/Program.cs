using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EstudoCertificacao.Exercicio
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThrowException();
            //NotThrowException();
            //Console.WriteLine("\nCHECKED output value is: {0}", CheckedMethod());
            //Console.WriteLine("UNCHECKED output value is: {0}", UncheckedMethod());

            //Using_QueueList();

            //Using_TaskRun();

            //Using_StackPushPop();

            //var starter = new UseStart();
            //((IHome)starter).Start();
            //((IOffice)starter).Start();

            //Using_WeakReference();

            //Regex_With_ForEach();
            //Regex_With_Enumerator();

            FormatString();

            Console.WriteLine();
            Console.WriteLine("Fim de processamento!");
            Console.ReadLine();
        }

        #region [ Block Checked ]

        static int maxIntValue = 2147483647;

        static int CheckedMethod()
        {
            int z = 0;
            try
            {
                z = checked(maxIntValue + 10);
            }
            catch (System.OverflowException e)
            {
                Console.WriteLine("CHECKED and CAUGHT:  " + e.ToString());
            }

            return z;
        }

        static int UncheckedMethod()
        {
            int z = 0;
            try
            {
                z = maxIntValue + 10;
            }
            catch (System.OverflowException e)
            {
                Console.WriteLine("UNCHECKED and CAUGHT:  " + e.ToString());
            }

            return z;
        }

        #endregion [ Block Checked ]

        #region [ Queue List ]
        
        static void Using_QueueList()
        {
            Queue<string> numbers = new Queue<string>();
            numbers.Enqueue("one");
            numbers.Enqueue("two");
            numbers.Enqueue("three");
            numbers.Enqueue("four");
            numbers.Enqueue("five");

            foreach (string number in numbers)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("\nDequeuing '{0}'", numbers.Dequeue());
            Console.WriteLine("Peek at next item to dequeue: {0}", numbers.Peek());
            Console.WriteLine("Dequeuing '{0}'", numbers.Dequeue());

            Queue<string> queueCopy = new Queue<string>(numbers.ToArray());

            Console.WriteLine("\nContents of the first copy:");
            foreach (string number in queueCopy)
            {
                Console.WriteLine(number);
            }

            string[] array2 = new string[numbers.Count * 2];
            numbers.CopyTo(array2, numbers.Count);

            Queue<string> queueCopy2 = new Queue<string>(array2);

            Console.WriteLine("\nContents of the second copy, with duplicates and nulls:");
            foreach (string number in queueCopy2)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("\nqueueCopy.Contains(\"four\") = {0}", queueCopy.Contains("four"));

            Console.WriteLine("\nqueueCopy.Clear()");
            queueCopy.Clear();
            Console.WriteLine("\nqueueCopy.Count = {0}", queueCopy.Count);
        }

        #endregion [ Queue List ]

        #region [ Exceptions ]

        static void ThrowException()
        {
            try
            {
                var a1 = 1;
                var a2 = 0;

                var x = a1 / a2;
            }
            catch (Exception ex)
            {
                LoggerException(ex);
                throw;
            }
        }

        static void NotThrowException()
        {
            try
            {
                var a1 = 1;
                var a2 = 0;

                var x = a1 / a2;
            }
            catch (Exception ex)
            {
                LoggerException(ex);
                throw ex;
            }
        }

        static void LoggerException(Exception ex)
        {

        }

        #endregion [ Exceptions ]

        #region [ Yield ]

        static IEnumerable<Customer> GetCustomer()
        {
            yield return new Customer { Id = 1, Name = "Test 1" };
            yield return new Customer { Id = 2, Name = "Test 2" };
        }

        #endregion [ Yield ]

        #region [ Task.Run ]

        static void Using_TaskRun()
        {
            var parent = Task.Factory.StartNew(() => {
                Console.WriteLine("Parent task beginning.");
                for (int ctr = 0; ctr < 10; ctr++)
                {
                    int taskNo = ctr;
                    Task.Factory.StartNew((x) => {
                        Thread.SpinWait(5000000);
                        Console.WriteLine("Attached child #{0} completed.",
                                          x);
                    },
                                          taskNo, TaskCreationOptions.AttachedToParent);
                }
            });

            parent.Wait();
            Console.WriteLine("Parent task completed.");
        }
        
        private static Double DoComputation(Double start)
        {
            Double sum = 0;
            for (var value = start; value <= start + 10; value += .1)
                sum += value;

            return sum;
        }

        #endregion [ Task.Run ]

        #region [ Stack Push and Pop ]

        static void Using_StackPushPop()
        {
            // Creates and initializes a new Stack.
            Stack myStack = new Stack();
            myStack.Push("The");
            myStack.Push("quick");
            myStack.Push("brown");
            myStack.Push("fox");

            // Displays the Stack.
            Console.Write("Stack values:");
            PrintValues(myStack, '\t');

            // Removes another element from the Stack.
            Console.WriteLine("(Pop)\t\t{0}", myStack.Pop());

            // Displays the Stack.
            Console.Write("Stack values:");
            PrintValues(myStack, '\t');

            // Views the first element in the Stack but does not remove it.
            Console.WriteLine("(Peek)\t\t{0}", myStack.Peek());

            // Displays the Stack.
            Console.Write("Stack values:");
            PrintValues(myStack, '\t');
        }

        private static void PrintValues(IEnumerable myCollection, char mySeparator)
        {
            foreach (Object obj in myCollection)
                Console.Write("{0}{1}", mySeparator, obj);
            Console.WriteLine();
        }

        #endregion [ Stack Push and Pop ]

        #region [ WeakReference ]

        static WeakReference _referenciaFraca;

        static void Using_WeakReference()
        {
            // atribuir a WeakReference.
            _referenciaFraca = new WeakReference(new StringBuilder("teste.estudo"));
            // verifica se a referência fraca esta 'viva'
            if (_referenciaFraca.IsAlive)
            {
                Console.WriteLine((_referenciaFraca.Target as StringBuilder).ToString());
            }
            // Invoca o coletor de lixo: GC.Collect.
            // ... Se você comentar esta linha de código
            // ....a próxima condição será avaliada como true(dependendo do GC)
            GC.Collect();

            // Verifica se a referência esta ativa(viva)
            if (_referenciaFraca.IsAlive)
            {
                Console.WriteLine("IsAlive");
            }
            // Encerra.
            Console.WriteLine("[Concluído]");
            Console.Read();
        }

        #endregion [ WeakReference ]

        #region [ Regex ]

        static void Regex_With_Enumerator()
        {
            var url = @"http://www.siteteste.com.br;http://www.siteteste2.com.br;http://www.teste3";
            var result = new List<string>();

            const string pattern = @"http://(www\.)?([^\.]+)\.com";

            MatchCollection myMatches = Regex.Matches(url, pattern);

            //result = (List<string>)myMatches.GetEnumerator();
            result = (from System.Text.RegularExpressions.Match m in myMatches
                      select m.Value).ToList<string>();

            Console.WriteLine("With Enumerator");

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        static void Regex_With_ForEach()
        {
            var url = @"http://www.siteteste.com.br;http://www.siteteste2.com.br;http://www.teste3";
            var result = new List<string>();

            const string pattern = @"http://(www\.)?([^\.]+)\.com";

            MatchCollection myMatches = Regex.Matches(url, pattern);

            foreach (Match item in myMatches)
            {
                result.Add(item.Value);
            }

            Console.WriteLine("With ForEach");

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        #endregion [ Regex ]

        #region [ Format string coins ]

        /// <summary>
        /// Question 156 - Topic 2
        /// </summary>
        static void FormatString()
        {
            var name = "teste nome";
            var coins = 1;

            var result = String.Format("Player {0} collected {1} coins.", name, coins.ToString("###0")); 

            Console.WriteLine(result);
        }

        #endregion [ Format string coins ]

    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    class CustomData
    {
        public long CreationTime;
        public int Name;
        public int ThreadNum;
    }

    interface IHome
    {
        void Start();
    }
    interface IOffice
    {
        void Start();
    }

    class UseStart : IHome, IOffice
    {
        void IHome.Start()
        {
            Console.WriteLine("Start de IHome.");
        }
        void IOffice.Start()
        {
            Console.WriteLine("Start de IOffice.");
        }
    }
}