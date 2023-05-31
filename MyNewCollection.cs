using System;
using System.Collections.Generic;
using System.Linq;
using MyCollection;

namespace practice13

{
    public class MyNewCollection<T> : MyCollection<T>, IList<T>
    {
        public string Name { get; set; } = "ColName";
        
        public int Lenght//текущее количество элементов в колекции
        {
            get
            {
                int i = 0;
                Point<T> temp = this.last;
                while (temp != null)
                {
                    temp = temp.prev;
                    i++;
                }
                return i;
            }
        }

        public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);//делегат
        //происходит при добавлении нового элемента или при удалении элемента из //коллекции
        public event CollectionHandler CollectionCountChanged;
        //объекту коллекции присваивается новое значение       
        public event CollectionHandler CollectionReferenceChanged; 

        
        
         
         //обработчик события CollectionCountChanged
        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args) 
        {
            if (CollectionCountChanged != null)
               CollectionCountChanged(source, args);
        }
        //обработчик события OnCollectionReferenceChanged
        public virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)        
       {
            if (CollectionReferenceChanged != null)
                CollectionReferenceChanged(source, args);
       }

         
        
        public  bool Remove(int index)
        {
            // интерфейс
            //попытка удаления первого вхождения значения в стек
            //случаи нахождения элемента
            
            Point<T> temp = this.last;
            Point<T> tlast = this.last;
            
            if (temp==null)
                return false;
            else if (index == 0)//первый элемент
            {
                
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "delete", this[index]));
                this.last = this.last.prev;
                return true;
            }
            else if (index == Count - 1)//последний элемент
            {
                int j = 0;
                while (j < index-2)//спускаемся по стеку
                {
                    temp = temp.prev;
                    j++;
                }
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "delete", this[index]));
                temp.prev = null;//удаляем ссылку на последний элемент
                this.last = tlast;
                return true;
            }
            else if (index > 0 && index < Count - 1)//в середине
            {
                int j = 0;
                while (j < index-1)//спускаемся по стеку
                {
                    temp = temp.prev;
                    j++;
                }
                
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "delete", this[index]));
                temp.prev = temp.prev.prev;//заменяем ссылку на последний элемент
                this.last = tlast;
                return true;
            }
            return false;//не нашли элемент
        }
        
        public override void Add(T p)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "add", p));
            base.Add(p);
        }

        public void AddDefault()
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "adddefault", default(T)));
            base.Add(default(T));
        }

        int IList<T>.IndexOf(T value)
        {
            return 1;
        }

        void IList<T>.Insert(int index, T value) { }
        void IList<T>.RemoveAt(int i) { }
        
        public new  T this[int index]
        {
            //доступ к значениям по []
            get
            {
                if (index < 0 || index > this.Count)
                    throw new ArgumentOutOfRangeException();
                Point<T> current = this.last;
                for (int i = 0; i < index; i++)
                    current = current.prev;
                //OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(this.Name, "changed", current.data.ToString()));
                return current.data;
            }
            set
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException();
                Point<T> current = this.last;
                for (int i = 0; i < index; i++)
                {
                    current = current.prev;
                }
                current.data = value;
                OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(this.Name, "changed", current.data));
            }
        }
        
        
    }
}

/*
Часть 2
1.  В коллекцию MyNewCollection (лаб. раб. 12) добавить 3 метода расширения, реализующие следующие запросы:
a)	На выборку данных по условию.
b)	Агрегирование данных (среднее, максимум/минимум, сумма и пр.).
c)	Сортировка коллекции (по убыванию/по возрастанию).

Дополнительное задание:
a)	Группировка данных.

Методы расширения должны в качестве параметров передавать делегаты, с помощью которых задаются правила выполнения соответствующих запросов.

Например, получение счетчика выполняется следующим образом: 

public static int Count(this MyCollection<Person> collection, Func<Person, bool> predicate)
        {
            int count = collection.Where(predicate).Count();          
            return count;
        }
 */

























/*

    *1.	Создать иерархию классов. 
     Для каждого класса реализовать конструктор без параметров, с параметрами, свойства для доступа к полям объектов,
      метод для автоматического формирования объектов. Перегрузить метод ToString() 
      для формирования строки со значениями всех полей класса (библиотека из  лаб. 10).

    *•2.	Определить класс MyNewCollection<T> производный от класса MyCollection<T>. 
    Класс MyСollection<T> взять из лабораторной работы №12 (лучше, как библиотеку). 
    В классе должны быть реализованы в классе методы для заполнения коллекции (элементы коллекции формируются автоматически), 
    добавления элементов коллекции, удаления элементов коллекции, очистки коллекции, реализован итератор для доступа к элементам коллекции, 
    реализовано свойство Length (только для чтения), содержащее текущее количество элементов коллекции. 

    *3.	В класс MyNewCollection добавить события, которые извещают об изменениях в коллекции. Коллекция изменяется:
    *•	 при удалении/добавлении элементов
    *•	при изменении одной из входящих в коллекцию ссылок, например, когда одной из ссылок присваивается новое значение. 

    В этом случае в соответствующих методах или свойствах класса генерируются события.  

    4.	 В класс MyNewCollection добавить 
    *•	открытое автореализуемое свойство типа string с названием коллекции; 
    *•	метод void Add(T obj) для добавления элемента в коллекцию;
    *•	метод void AddDefault() для добавления случайного элемента в коллекцию;
•метод bool Remove (int j) для удаления элемента с номером j; если в списке нет элемента с номером j, метод возвращает значение false; 
•индексатор (с методами get и set) с целочисленным индексом для доступа к элементу с заданным номером.

    *5.	 Для событий, извещающих об изменениях в коллекции, определяется делегат CollectionHandler:
    *         void CollectionHandler (object source, CollectionHandlerEventArgs args); 

    *6.	Для передачи информации о событии определить класс CollectionHandlerEventArgs, производный от класса System.EventArgs, который содержит 
    *•	открытое автореализуемое свойство типа string с информацией о типе изменений в коллекции; 
    *•	открытое автореализуемое свойство для ссылки на объект, с которым связаны изменения; 
    *•	конструкторы для инициализации класса; 

    7.	В класс MyNewCollection добавить два события типа CollectionHandler.
    •	 CollectionCountChanged, которое происходит при добавлении нового элемента в коллекцию или при удалении элемента из коллекции;
          через объект CollectionHandlerEventArgs cобытие передает строку с информацией о том, 
          что в коллекцию был добавлен новый элемент или из нее был удален элемент, ссылку на добавленный или удаленный элемент; 
    *•	 CollectionReferenceChanged, которое происходит, когда одной из ссылок, входящих в коллекцию, присваивается новое значение; 
        через объект CollectionHandlerEventArgs событие передает строку с информацией о том,
         что был заменен элемент в коллекции, и ссылку на новый элемент. 

    *8.	Событие CollectionCountChanged бросают следующие методы класса MyNewCollection 
    *•	AddDefaults(); // добавить случайный объект
    *•	Add (Т obj ) ; //добавить объект
•Remove (int index) //удалить элемент по индексу. 

    9.	Событие CollectionReferenceChanged бросает метод set индексатора, определенного в классе MyNewCollection. 

    *10.	Информация об изменениях коллекции записывается в класс Journal, который хранит информацию в списке объектов типа JournalEntry.
    *     Каждый объект типа JournalEntry содержит информацию об отдельном изменении, которое произошло в коллекции. JournalEntry содержит: 
    *•	открытое автореализуемое свойство типа string с названием коллекции, в которой произошло событие; 
    *•	открытое автореализуемое свойство типа string с информацией о типе изменений в коллекции; 
    *•	открытое автореализуемое свойство типа string c данными объекта, с которым связаны изменения в коллекции; 
    *•	конструктор для инициализации полей класса; 
    *•	перегруженную версию метода string ToString(). 

    *11.	Написать демонстрационную программу, в которой:
    *•	создать две коллекции MyNewCollection.
    *•	Создать два объекта типа Journal, один объект Journal подписать на события CollectionCountChanged и CollectionReferenceChanged
      из первой коллекции, другой объект Journal подписать на события CollectionReferenceChanged из обеих коллекций. 


    *12.	Внести изменения в коллекции MyNewCollection 
    *•	добавить элементы в коллекции; 
    *•	удалить некоторые элементы из коллекций; 
    •	присвоить некоторым элементам коллекций новые значения. 

    *13.	Вывести данные обоих объектов Journal. 

 */