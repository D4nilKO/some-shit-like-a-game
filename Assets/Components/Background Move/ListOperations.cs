using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.cyberforum.ru/csharp-beginners/thread1464588.html
static class ListOperations
{
    public static void Move<T>(this List<T> list, int i, int j)
    {
        var elem = list[i];
        list.RemoveAt(i);
        list.Insert(j, elem);
    }

    public static void Swap<T>(this List<T> list, int i, int j)
    {
        var elem1 = list[i];
        var elem2 = list[j];

        list[i] = elem2;
        list[j] = elem1;
    }

}
