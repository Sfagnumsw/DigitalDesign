using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _123
{
    class MyList<T> : IEnumerable<T>
    {
        Node<T> first; // первый элемент
        Node<T> last; // последний элемент
        int countElems; // количество элементов в дэке
        public T First { get { return first.Data; } }
        public T Last { get { return last.Data; } }

        public void AddLast(T elems) 
        {
            Node<T> node = new Node<T>(elems); // элемент который надо добавить в конец дэка

            if(first == null) { first = node; } // если дэк пуст, то этот элемент становится первым

            else // иначе вставляем элемент в конец
            {
                last.Next = node;
                node.Previous = last;
            }
            last = node; // теперь последним будет считаться наш элемент
            countElems++;
        }

        public void AddFirst(T elems)
        {
            Node<T> node = new Node<T>(elems); // элемент который надо добавить в начало дэка
            Node<T> head = first; // промежуточный элемент 
            node.Next = head;
            first = node;

            if(countElems == 0) // если дэк пуст , то наш элемент является и первым и последним
            {
                last = first;
            }

            else
            {
                head.Previous = node; // ставим второму элементу в дэке ссылку на первый элемент
            }
            countElems++;
        }

        public T RemoveLast()
        {
            if(countElems == 0) { throw new ArgumentOutOfRangeException(); } // если список пуст - исключение
            T output = last.Data;

            if(countElems == 1) // если в списке был 1 элемент - список становится пустым
            {
                first = last = null;
            }

            else
            {
                last.Previous = last;
                last.Next = null;
            }
            countElems--;
            return output;
        }

        public T RemoveFirst()
        {
            if(countElems == 0) { throw new ArgumentOutOfRangeException(); } // если список пуст - исключение
            T output = first.Data;

            if(countElems == 1)
            {
                first = last = null;
            }

            else
            {
                first.Next = first;
                first.Previous = null;
            }
            countElems--;
            return output;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = first;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }
}

//node
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _123
{
    class Node<T> //каждый элемент в дэке 
    {
        public T Data { get; set; }
        public Node(T elems)
        {
            Data = elems;
        }
        public Node<T> Previous { get; set; } // предыдущий элемент
        public Node<T> Next { get; set; } // слудующий
    }
}
