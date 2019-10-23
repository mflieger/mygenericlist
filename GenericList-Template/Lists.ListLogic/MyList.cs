using System;
using System.Collections;

namespace Lists.ListLogic
{
    /// <summary>
    /// Die Liste verwaltet beliebige Elemente und implementiert
    /// das IList-Interface und damit auch ICollection und IEnumerable
    /// </summary>
    public class MyList : IList
    {
        Node _head;

        #region IList Members

        /// <summary>
        /// Ein neues Objekt wird in die Liste am Ende
        /// eingefügt. Etwaige Typinkompatiblitäten beim Vergleich werden
        /// nicht behandelt und lösen eine Exception aus.
        /// </summary>
        /// <param name="value">Einzufügender Datensatz</param>
        /// <returns>Index des Werts in der Liste</returns>
        public int Add(object value)
        {
            Node insertNode = new Node(value);
            if (_head == null)
            {
                _head = insertNode;
                return 0;
            }
            Node searchNode = _head;
            int index = 1;
            while (searchNode.Next != null)
            {
                searchNode = searchNode.Next;
                index++;
            }
            searchNode.Next = insertNode;
            return index;
        }

        /// <summary>
        /// Die Liste wird geleert. Die Elemente werden einfach ausgekettet.
        /// Der GC räumt dann schon auf.
        /// </summary>
        public void Clear()
        {
            _head = null;
        }

        /// <summary>
        /// Gibt es den gesuchten DataObject zumindest ein mal?
        /// </summary>
        /// <param name="value">gesuchter DataObject</param>
        /// <returns></returns>
        public bool Contains(object value)
        {
            return IndexOf(value) >= 0;
        }

        /// <summary>
        /// Der DataObject wird gesucht und dessen Index wird zurückgegeben.
        /// </summary>
        /// <param name="value">gesuchter DataObject</param>
        /// <returns>Index oder -1, falls der DataObject nicht in der Liste ist</returns>
        public int IndexOf(object value)
        {
            Node searchNode = _head;
            int index = 0;
            while (searchNode != null && !searchNode.DataObject.Equals(value))
            {
                searchNode = searchNode.Next;
                index++;
            }
            if (searchNode == null)
            {
                return -1;
            }
            return index;
        }

        /// <summary>
        /// DataObject an bestimmter Position in Liste einfügen.
        /// Es ist auch erlaubt, einen DataObject hinter dem letzten Element
        /// (index == count) einzufügen.
        /// </summary>
        /// <param name="index">Einfügeposition</param>
        /// <param name="value">Einzufügender DataObject</param>
        public void Insert(int index, object value)
        {
            if (index > Count || index < 0)
            {
                return;
            }
            Node newNode = new Node(value);
            if (index == 0)
            {
                newNode.Next = _head;
                _head = newNode;
                return;
            }
            Node searchNode = _head;
            for (int i = 1; i < index; i++)
            {
                searchNode = searchNode.Next;
            }
            newNode.Next = searchNode.Next;
            searchNode.Next = newNode;
        }

        /// <summary>
        /// Wird nicht verwendet ==> Immer false
        /// </summary>
        public bool IsFixedSize
        {
            get { return false; }
        }

        /// <summary>
        /// Wird nicht verwendet ==> Immer false
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Ein DataObject wird aus der Liste entfernt, wenn es ihn 
        /// zumindest ein mal gibt.
        /// </summary>
        /// <param name="value">zu entfernender DataObject</param>
        public void Remove(object value)
        {
            int index = IndexOf(value);
            if (index >= 0)
            {
                RemoveAt(index);
            }
        }

        /// <summary>
        /// Der DataObject an der Position Index wird entfernt.
        /// Gibt es die Position nicht, geschieht nichts.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0)
            {
                return;
            }
            if (index == 0)
            {
                _head = _head.Next;
                return;
            }
            Node searchNode = _head;
            for (int i = 1; i < index; i++)
            {
                searchNode = searchNode.Next;
            }
            searchNode.Next = searchNode.Next.Next;
        }

        /// <summary>
        /// Indexer zum Einfügen und Lesen von Werten an
        /// gesuchten Positionen.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    return null;
                }
                Node searchNode = _head;
                for (int i = 0; i < index; i++)
                {
                    searchNode = searchNode.Next;
                }
                return searchNode.DataObject;
            }
            set
            {
                Insert(index, value);
            }
        }

        #endregion

        #region ICollection Members

        /// <summary>
        /// Werte in ein bereits angelegtes Array kopieren.
        /// Ist das übergebene Array zu klein, oder stimmt der
        /// Startindex nicht, geschieht nichts.
        /// </summary>
        /// <param name="array">Zielarray, existiert bereits</param>
        /// <param name="index">Startindex</param>
        public void CopyTo(Array array, int index)
        {
            if (array.Length < Count - index)
            {
                return;
            }
            Node searchNode = _head;
            for (int i = 0; i < index; i++)
            {
                searchNode = searchNode.Next;
            }
            int targetIndex = 0;
            while (searchNode != null)
            {
                array.SetValue(searchNode.DataObject, targetIndex);
                searchNode = searchNode.Next;
                targetIndex++;
            }
        }

        /// <summary>
        /// Die Anzahl von Elementen in der Liste wird immer 
        /// explizit gezählt und ist nicht redundant gespeichert.
        /// </summary>
        public int Count
        {
            get
            {
                int counter = 0;
                Node searchNode = _head;
                while (searchNode != null)
                {
                    searchNode = searchNode.Next;
                    counter++;
                }
                return counter;
            }
        }

        /// <summary>
        /// Multithreading wird nicht verwendet
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// Multithreading wird nicht verwendet
        /// </summary>
        public object SyncRoot   
        {
            get { return null; }
        }

        #endregion

        public IEnumerator GetEnumerator()
        {
            return new MyListEnumerator(_head);
        }
    }
}
