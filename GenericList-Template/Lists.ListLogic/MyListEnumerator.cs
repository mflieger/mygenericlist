using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lists.ListLogic
{
    internal class MyListEnumerator : IEnumerator
    {
        private Node _head;
        private Node _actualNode;
        private bool _isReset;
        public MyListEnumerator(Node head)
        {
            _head = head;
            Reset();
        }

        public bool MoveNext()
        {
            if (_isReset)
            {
                _actualNode = _head;
                _isReset = false;
            }
            else
            {
                _actualNode = _actualNode.Next;
            }
            return _actualNode != null;
        }

        public void Reset()
        {
            _actualNode = null;
            _isReset = true;
        }

        public object Current { get { return _actualNode.DataObject; } }
    }
}