using HW2.DistinctIntegers;
using System.Runtime.CompilerServices;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace HW2
{
    public partial class Form1 : Form
    {   
        public Form1()
        {
            InitializeComponent();
            RunDistinctIntegers();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void AddTextToTextbox(string text) // adds text to the textbox we created
        {
            textBox1.AppendText(text);
            textBox1.AppendText(Environment.NewLine); // new line to insert text onto
        }
        private void RunDistinctIntegers() // this is your method 
        {   
            string result = new string("");

            int hashSetResult, storComplexResult, sortedResult;

            //explanation arrays to insert to textbox
            string hashSetExplanation = "Explanation: Create a hash set variable that holds ints " +
                "and insert all of the items found in the array. Hash sets don't allow duplicates, so " +
                "displaying the Count attribute gives the number of distinct items.";
            string hashSetTime = "Time complexity: O(n), where n is the length of the array. We have to traverse the entire " +
                "array 1 by 1, inserting each item into a hash set.";


            string storComplexExplanation = "Start at index i = 0 and j = 1. Increase j until a duplicate is found. If a duplicate " +
                "is found, increase the duplicate counter set i to the next ith index and j to i + 1. If not, go to the next ith " +
                "index and set j to i + 1. Subtract length of array " +
                "by number of duplicates, resulting in the number of distinct items.";
            string storComplexTime = "Time complexity: undetermined for avg case since it depends on the order of the duplicates. Since j " +
                "stops at the first encounter of a duplicate, it all depends on how close each duplicate is to one another. The farther " +
                "away a duplicate is, the more times we have to iterate through the second loop before breaking; therefore, worst time " +
                "complexity is when the list has no duplicates, resulting in O(n!).";


            string sortedExplanation = "Start at index i = 0 and j = 1. Increase j until the ith index and the jth index " +
                "values don't match. Increase the number of distinct items found by 1, set i = j and j = i + 1. Loop keeps going " +
                "until i or j reach the end of the array. Return number of distinct items + 1 in order to account for the initial " +
                "item that didn't get counted.";
            string sortedTime = "Time complexity: O(n-1) because the loop stops when i or j reach the end of the list since it's ordered. " +
                "and j will always reach it first since it starts at index 1 instead of 0, resulting in one less item in the list " +
                "that we have to traverse through.";

            //Call on method to generate a random 10_000 len array ranging with random nums from 0 - 20_000
            int[] arr = GenerateRandomList();

            //call on the method for hashset and append result to a string to write to textbox
            hashSetResult = DistinctIntergers.HashSetMethod(arr);
            result = "1. Hash set method: " + hashSetResult.ToString();
            AddTextToTextbox(result);
            AddTextToTextbox(hashSetExplanation);
            AddTextToTextbox(hashSetTime);
            textBox1.AppendText(Environment.NewLine); // enter a new line into the box for neatness

            //call on the method for O(1) and append result to a string to write to textbox
            storComplexResult = DistinctIntergers.StorageComplexityMethod(arr);
            result = "2. Storage Complexity O(1) method: " + storComplexResult.ToString();
            AddTextToTextbox(result);
            AddTextToTextbox(storComplexExplanation);
            AddTextToTextbox(storComplexTime);
            textBox1.AppendText(Environment.NewLine); // enter a new line into the box for neatness

            //call on the method for sorted and append result to a string to write to textbox
            sortedResult = DistinctIntergers.SortedMethod(arr);
            result = "3. Sorted Array Method: " + sortedResult.ToString();
            AddTextToTextbox(result);
            AddTextToTextbox(sortedExplanation);
            AddTextToTextbox(sortedTime);
            textBox1.AppendText(Environment.NewLine); // enter a new line into the box for neatness
        }

        private static int[] GenerateRandomList()
        // Creates a random number array and returns it using the Random class
        // Range is [0,20_000]
        // Duplicates allowed
        // Length of array is 10_000
        {
            int[] randomArr = new int[10_000];
            Random randNum = new Random();
            for (int i = 0; i < 10_000; i++)
            {
                randomArr[i] = randNum.Next(0, 20_000); // min and max defined as 0 and 20_000 respectively
            }
            return randomArr; 
        }

        
    }
}