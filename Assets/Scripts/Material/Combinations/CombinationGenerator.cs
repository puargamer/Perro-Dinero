using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// mostly reference for later
public class CombinationGenerator : MonoBehaviour
{
    private int totalMaterials = System.Enum.GetValues(typeof(MaterialType)).Length;

    public CombinationType GetCombination(MaterialType first, MaterialType second)
    {
        int indexFirst = (int)first;
        int indexSecond = (int)second;

        if (indexFirst > indexSecond)
        {
            (indexFirst, indexSecond) = (indexSecond, indexFirst);
        }

        /// if x > y then
        /// combIndex = total types * y + x - 
        /// ((y + 1) * y) / 2 // eliminate all duplicate combinations before the yth material
        /// LMAO THIS DOESNT EVEN WORK AS INTENDED

        int combinationIndex = (totalMaterials * indexFirst) + indexSecond - ((indexFirst + 1) * indexFirst) / 2;

        return (CombinationType)combinationIndex;
    }
}
