using System;
using System.Collections.Generic;

namespace practice13
{
    public class JournalEntry
    {
        public string Name { get; }
        public string Changes { get; }
        public object Obj { get; }

        public JournalEntry(string name, string changes, object obj)
        {
            Name = name;
            Changes = changes;
            Obj = obj;
        }

        public override string ToString()
        {
            if (Obj!=null)
                return $"{Name} : {Changes} : {Obj.ToString()}";
            else
                return $"{Name} : {Changes} : {null}";
        }
    }
    
    
    public class Journal
    {
        private List<JournalEntry> journal = new List<JournalEntry>();
        public void CollectionCountChanged(object sourse, CollectionHandlerEventArgs e)
        {
            if (e.Obj!=null)
            {
                JournalEntry je = new JournalEntry(e.Name, e.Changes, e.Obj.ToString());
                this.Add(je);
            }
            else
            {
                JournalEntry je = new JournalEntry(e.Name, e.Changes, null);
                this.Add(je);
            }

        }
        public void CollectionReferenceChanged(object sourse, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.Name, e.Changes, e.Obj.ToString());
            this.Add(je);
        }

        public void Add(JournalEntry change)
        {
            journal.Add(change);
        }

        public void Show()
        {
            foreach (JournalEntry change in journal)
            {
                Console.WriteLine(change.ToString());
            }
        }

    }
}