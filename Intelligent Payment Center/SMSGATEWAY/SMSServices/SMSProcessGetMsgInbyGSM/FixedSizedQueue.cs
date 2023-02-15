using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSProcessGetMsgInbyGSM
{
    public class FixedSizedQueue<T>
    {
        public Queue<T> items = new Queue<T>();

        public int Limit { get; set; }
        public void Enqueue(T obj)
        {
            items.Enqueue(obj);
            lock (this)
            {
                while (items.Count > Limit)
                    items.Dequeue();
            }
        }
    }
}
