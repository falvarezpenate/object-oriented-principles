using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    public class Node
    {
        private int val;
        private Node left;
        private Node right;

        public Node(int newVal)
        {
            this.val = newVal;
            this.left = null;
            this.right = null;
        }
        
        // we don't want the user to have access to the private data members above, so we encapsulate them using properties
        public int Val // PROPERTY
        {
            get => val;
            set => val = value;
        }

        public Node Left // PROPERTY
        {
            get => left;
            set => left = value;
        }

        public Node Right // PROPERTY
        {
            get => right;
            set => right = value;
        }
    }
}
