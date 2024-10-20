using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationGenerator : MonoBehaviour
{
    private int totalMaterials = System.Enum.GetValues(typeof(MaterialType)).Length;

    public CombinationTypes GetCombination(MaterialType first, MaterialType second)
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

        int combinationIndex = (totalMaterials * indexFirst) + indexSecond - ((indexFirst + 1) * indexFirst) / 2;

        return (CombinationTypes)combinationIndex;
    }
}
