using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    public class BST
    {
        private Node root;

        public BST()
        {
            root = null;
        }
        public void Insert(int value) // user should not have access to private member root for initial call
        {
            this.root = insert(this.root, value);
        }
        private Node insert(Node node, int value)
        {
            if (node == null)
            {
                node = new Node(value);
                return node; // since these aren't pointers, we have to return a node type to be assigned
            }

            else
            {
                if (value < node.Val) // insert left
                {
                    //recurse back through this function but now comparing values to the left node
                    node.Left = insert(node.Left, value); // since node.Left is not a pointer, we have to assign it = the return value from insert() in order for it to update
                }
                else if (value > node.Val)
                {
                    //recurse back through this function but now comparing values to the right node
                    node.Right = insert(node.Right, value); 
                }
                else
                {
                    //duplicate found. Do nothing and let user know
                    Console.WriteLine("  duplicate found: {0} not inserted.", value);
                }
                return node;
            }
        }

        public void InOrderTraversal() //user shouldn't have access to root attribute so this method is here to call on the actual traversal
        {
            inOrderTraversal(this.root);
        }
        private void inOrderTraversal(Node node)
        {
            if (node == null) return;
            inOrderTraversal(node.Left);
            Console.Write(" {0} ", node.Val);
            inOrderTraversal(node.Right);
        }

        public int CountNodes() // encapsulation: user shouldn't have access to root attribute
        {
            return countNodes(this.root);
        }
        private int countNodes(Node node)
        {
            if (node == null) return 0;
            else
            {
                return 1 + countNodes(node.Left) + countNodes(node.Right); // use recursion to traverse tree adding up node count
            }
        }

        public int NumLevels() // encapsulation
        {
            return numLevels(this.root);
        }
        private int numLevels(Node node) // same method that would be used for getting the height of a tree
            // looks at both sides and determines which is longer, giving us the height/level of the tree
        {
            if (node == null) return 0;
            return Math.Max(numLevels(node.Left), numLevels(node.Right)) + 1;
        }

        public double MinimumLevels() // encapsulation: user shouldn't have access to root attribute in order to call minimumLevels()
        {
            // returns double type due to Math methods working in double types. Will still result in a whole number result
            return minimumLevels(this.root);
        }
        private double minimumLevels(Node node) 
        {
            // a tree with n nodes will have a minimum of 1 + floor(log_2(n)) levels
            // found formula in old advanced data structures textbook
            int numNodes = CountNodes();
            double x = Math.Log2(numNodes);
            return Math.Floor(x) + 1;
        }
    }
}
