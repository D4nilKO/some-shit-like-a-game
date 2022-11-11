﻿using System;
using UnityEngine;
using Random = System.Random;

public class NonRepeatingRandom : MonoBehaviour
{
    /// <summary>
    /// Used in Shuffle(T).
    /// </summary>
    static Random _random = new Random();
    
    /// <summary>
    /// Shuffle the array.
    /// </summary>
    /// <typeparam name="T">Array element type.</typeparam>
    /// <param name="array">Array to shuffle.</param>
    public static void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < (n - 1); i++)
        {
            // Use Next on random instance with an argument.
            // ... The argument is an exclusive bound.
            //     So we will not go past the end of the array.
            int r = i + _random.Next(n - i);
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }
    
    
    private static void Main()
    {
        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Shuffle(array);
        Console.WriteLine("SHUFFLE: {0}", string.Join(",", array));
        
        string[] array2 = { "bird", "frog", "cat" };
        Shuffle(array2);
        Console.WriteLine("SHUFFLE: {0}", string.Join(",", array2));
    }
}