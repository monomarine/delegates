namespace ConsoleApp1
{

    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor[] colors = { ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Green };
            Random rand = new Random();
            Test test = new Test() { Name = "первый объект"};
            Test test2 = new Test() { Name ="второй объект" };

            test.TestEvent += delegate (Object? obj, int count)
            {
                Console.WriteLine($"объект {obj} сгенировал число {count}");
            };
            test.TestEvent += (Object? obj, int count) => Console.ForegroundColor = colors[rand.Next(0, 3)];

            test2.TestEvent += delegate (Object? obj, int count)
            {
                Console.WriteLine($"объект {obj} сгенировал число {count}");
                if( obj is  Test test ) { test.I -= 10; }
            };
            test2.TestEvent += (Object? obj, int count) => Console.ForegroundColor = colors[rand.Next(0, 3)];

            test.Work();
            test2.Work();
            
        }
    }

    public class Test
    {
        //public delegate void EventHandler(object? sender, EventArgs e);
        public event EventHandler<int> TestEvent;
        public int I { get; set; } = 0;
        public string Name { get; set; } = "";
        public void Work()
        {
            
            while (I < 100)
            {
                I++;
                if (I % 10 == 0 )
                    TestEvent?.Invoke(this, I);
                Thread.Sleep(10);
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

}