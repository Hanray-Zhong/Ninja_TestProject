using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int[] array_1 = new int[2];
    public List<int[]> list_1 = new List<int[]>();
    public List<int[]>[] array_2 = new List<int[]>[2];
    public int[,] array_3 = new int[9, 9];
    private void Update() {
        Func();
    }

    void Func() {
        Random.Range(1,2);
        list_1.Add(array_1);
        array_2[0] = list_1;
        array_2[1] = null;

        array_3[0, 1] = 1;
    }
}
