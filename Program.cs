using System;
using EngineLib;

namespace practice13
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            //	Подписка на события заключается в присоединении обработчика события к event-объекту:
            MyNewCollection<Engine> c = new MyNewCollection<Engine>();
            c.Name = "Collection1";
            MyNewCollection<Engine> c1 = new MyNewCollection<Engine>();
            c1.Name = "Collection2";
            
            //один объект Journal подписать на события CollectionCountChanged и CollectionReferenceChanged из первой коллекции
            Journal joun1 = new Journal();
            c.CollectionCountChanged += new MyNewCollection<Engine>.CollectionHandler(joun1.CollectionCountChanged);
            c.CollectionReferenceChanged += new MyNewCollection<Engine>.CollectionHandler(joun1.CollectionReferenceChanged);
            
            Journal joun2 = new Journal();
            c.CollectionReferenceChanged += new MyNewCollection<Engine>.CollectionHandler(joun2.CollectionReferenceChanged);
            c1.CollectionReferenceChanged += new MyNewCollection<Engine>.CollectionHandler(joun2.CollectionReferenceChanged);
            
            c.Add(new Engine(1));
            c.Add(new Engine(1));
            c.Add(new Engine(1));
            c.Add(new Engine(1));
            c.Add(new Engine(1));
            c[0] = new Engine(1,1,1);
            c[4] = new Engine(2,2,2);
            
            Console.WriteLine("\n");
            MyNewCollection<Engine>.Show(c);
            Console.WriteLine($"\nколичество элементов в коллекции = {c.Lenght}");
            c.Remove(3);
            c.Remove(1);
            Console.WriteLine($"количество элементов в коллекции = {c.Lenght}");
            MyNewCollection<Engine>.Show(c);
            
            
            c1.Add(new Engine(1));
            c1.Add(new Engine(1));
            c1[0] = new Engine(1,1,1);
            c1[1] = new Engine(1,1,1);
            Console.WriteLine($"\nколичество элементов в коллекции = {c.Lenght}");
            MyNewCollection<Engine>.Show(c);
            c1.Remove(1);
            c1.Remove(0);
            Console.WriteLine($"\nколичество элементов в коллекции = {c.Lenght}");
            MyNewCollection<Engine>.Show(c);
            
            Console.WriteLine("\n--------------------------------------------------------------");
            Console.WriteLine("Journal 1");
            joun1.Show();
            
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Journal 2");
            joun2.Show();
            
        }
    }
}