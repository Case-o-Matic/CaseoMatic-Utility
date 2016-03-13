using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace Caseomatic.Utility
{
    [Synchronization]
    public class ConcurrentStack<T> : ContextBoundObject
    {
        private readonly Stack<T> stack;

        public bool IsEmpty
        {
            get { return stack.Count == 0; }
        }

        public ConcurrentStack()
        {
            stack = new Stack<T>();
        }

        public void Push(T item)
        {
            stack.Push(item);
        }

        public void PushRange(T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                stack.Push(items[i]);
            }
        }

        public T Pop()
        {
            return stack.Pop();
        }

        public T[] PopAll()
        {
            var items = stack.ToArray();
            stack.Clear();

            return items;
        }

        public void Clear()
        {
            stack.Clear();
        }
    }
}
