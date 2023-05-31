using System;

namespace practice13
{
    public class CollectionHandlerEventArgs : System.EventArgs
    {
        public string Name { get; }
        public string Changes { get; }
        public object Obj { get; }
        
        
        public CollectionHandlerEventArgs()
        {
            Name = String.Empty;
            Changes = String.Empty;
            Obj = null;
        }
        
        public CollectionHandlerEventArgs(string name, string changes, object obj)
        {
            Name = name;
            Changes = changes;
            Obj = obj;
        }
        
    }
}