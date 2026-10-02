using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    public class Program
    {
        static void Main(string[] args)
        {
            BST tree = new BST();
            string[] inputArr;

            inputArr = getUserInput();

            populateBST(tree, inputArr);
            Console.WriteLine(); // enter new line
            
            traverseTree(tree);
            Console.WriteLine(); // enter new line

            countNodes(tree);
            Console.WriteLine(); // enter new line

            numLevels(tree);
            Console.WriteLine(); // enter new line

            minimumLevels(tree);
            Console.WriteLine(); // enter new line

            Console.WriteLine("done.");
            Console.WriteLine(); // enter new line
        }

        static string[] getUserInput() 
        {
            // assumes that the user enters a correctly formatted input string that meets
            // requirements of being integers separated by spaces in the range 0,100

            string input;
            Console.WriteLine("Enter a collection of numbers in the range [0, 100] separated by spaces: \n");
            input = Console.ReadLine();

            // Console.WriteLine(input);

            // split input at each " " string into an array of multiple strings
            string[] inputArr = input.Split(" ");
            // Console.WriteLine(stringArr[3]);

            return inputArr;
        }

        static void populateBST(BST tree, string[] values)
        {
            // populates BST from array, needs to convert string values in array to int type
            // assumes array is not empty
            Console.WriteLine("\nnow populating tree... ");
            int temp;
       
            for (int i = 0; i < values.Length; i++)
            {
                temp = Convert.ToInt32(values[i]);
                Console.WriteLine(" inserting {0}", temp);
                tree.Insert(temp);
            }
        }

        static void traverseTree(BST tree)
        {
            //calls on InOrderTraversal method.
            Console.WriteLine("now traversing tree in order...");
            tree.InOrderTraversal();
            Console.WriteLine();
        }

        static void countNodes(BST tree)
        {
            // calls on CountNodes() method
            Console.WriteLine("counting nodes in tree...");
            int numNodes = tree.CountNodes();
            Console.WriteLine(" number of nodes in tree: {0}", numNodes);
        }

        static void numLevels(BST tree)
        {
            //calls on the NumLevels() method from BST
            Console.WriteLine("counting the number of levels in tree...");
            int numLevels = tree.NumLevels();
            Console.WriteLine(" number of levels: {0}", numLevels);
        }
        static void minimumLevels(BST tree)
        {
            // calls on CountNodes() and MinimumLevels() methods for tree
            Console.WriteLine("calculating minimum number of levels required for this tree using 1 + floor(log_2(number of nodes))...");
            int numNodes = tree.CountNodes();
            double levels = tree.MinimumLevels();
            Console.WriteLine(" minimum number of levels for a tree containing {0} nodes: {1}", numNodes, levels);
        }
    }
}


