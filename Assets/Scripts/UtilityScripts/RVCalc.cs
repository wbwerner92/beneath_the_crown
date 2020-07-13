using UnityEngine;

public class RVCalc
{
    public static int GetVal(int min, int max)
    {
        if (max <= min)
        {
            Debug.LogError("Inavlid Roll - Min: " + min + ", Max: " + max);
        }

        return Random.Range(min, (max + 1));
    }

    /// <summary>
    /// Rolls a D20 with an added base value.
    /// Advantage value should be -1, 0, or 1.
    /// </summary>
    /// <param name="adv">Advantage value should be -1, 0, or 1</param>
    /// <returns></returns>
    public static int RollD20(int baseVal = 0, int advVal = 0)
    {
        bool adv = advVal == 1;
        bool dis = advVal == -1;

        if (adv && dis)
        {
            Debug.LogError("ERROR - Cannot have both Advantage & Disadvantage");
            return 0;
        }
        else if (adv || dis)
        {
            int roll1 = GetVal(1, 20);
            int roll2 = GetVal(1, 20);

            if (adv)
            {
                return Mathf.Max(roll1, roll2) + baseVal;
            }
            else
            {
                return Mathf.Min(roll1, roll2) + baseVal;
            }
        }
        else
        {
            return GetVal(1, 20) + baseVal;
        } 
    }
}
