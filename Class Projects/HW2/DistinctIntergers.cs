using System;
using System.Globalization;

namespace HW2.DistinctIntegers
{
    public class DistinctIntergers
    {
        public static int HashSetMethod(int[] arr)
            // Creates a HashSet type variable that holds ints
            // inserts each item in array to that hashset 
            // hashsets can't contain duplicates, so the .Count attribute returns unique values
        {
            HashSet<int> hashSet = new HashSet<int>();
            //Iterate through every item in arr and add it to hash set
            foreach (int item in arr)
            {
                hashSet.Add(item); // doesn't allow duplicates
            }
            return hashSet.Count; // returns the count of hash set items (no duplicates)
        }

        public static int StorageComplexityMethod(int[] arr)
        {
            // Takes a different approach than expected
            // Instead of counting unique pairs
            // Traverse list until jth index matches ith index
            // Increase duplicate count and break;
            // Go to next ith index and repeat
            // O(n!) time comlexity
            int numDuplicates = 0;
            for (int i = 0; i < arr.Length; i++) 
            { 
                for (int j = i + 1; j < arr.Length; j++) // j starts at the index following i 
                {
                    if (arr[i] == arr[j]) // duplicate found
                    {
                        numDuplicates++; // inc count
                        break; // go to next value to check
                    }
                }
            }

            return arr.Length - numDuplicates; // length of the list - num duplicates = num unique values
            
        }

        public static int SortedMethod(int[] arr)
        // sorts an array and traverses through the items counting unique values ignoring duplicates.
        {   
            if(arr.Length == 0)
            {
                return 0;
            }
            int numDistinctInts = 1;

            // Sorts input array 
            Array.Sort(arr);

            // Starts with i on first index and j on second index
            int i = 0;
            int j = 1;
          
            while (i < arr.Length && j < arr.Length)
            {
                if (arr[i] == arr[j])
                {
                    j++; // move j to the next index that doesn't match the index that i is pointing at. In short, go to next unique value.
                }
                else
                {
                    i = j; // set i to the index of j. In short, move i to the next value that is unique
                    j = i + 1; // set j to the index following the statement above, could be duplicate or a new value
                    numDistinctInts++; // since new value was found, increase number of distinct integers found
                }
            }
            return numDistinctInts; // return result
        }
    }
}
