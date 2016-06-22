using UnityEngine;
using System.Collections;

public class GameHelper {
    static System.Random s_Randomer;

    public static int Random(int min, int max)
    {
        if (s_Randomer == null)
            s_Randomer = new System.Random(Time.frameCount);

        return s_Randomer.Next(min, max);
    }

    public static float Random(float min, float max)
    {
        if (s_Randomer == null)
            s_Randomer = new System.Random(Time.frameCount);

        int p = Random(0, 100);

        return min + (float)p / 100f * (max - min);
    }
}
