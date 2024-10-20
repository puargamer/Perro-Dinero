using System.Collections.Generic;
using UnityEngine;

public class MaterialRecipe
{
    public MaterialType MaterialOne { get; }
    public MaterialType MaterialTwo { get; }
    public CombinationType Result { get; }

    public MaterialRecipe(MaterialType materialOne, MaterialType materialTwo, CombinationType result)
    {
        MaterialOne = materialOne;
        MaterialTwo = materialTwo;
        Result = result;
    }
}

public class RecipeBook : MonoBehaviour
{
    private List<MaterialRecipe> recipes;

    void Start() // holds all of the recipes to be used in the crafting thus far
    {
        recipes = new List<MaterialRecipe>
        {
            new MaterialRecipe(MaterialType.Cobweb, MaterialType.Twig, CombinationType.BaitA), // brown lookin idiot
            new MaterialRecipe(MaterialType.Feather, MaterialType.Cobweb, CombinationType.BaitB), // red clam
            new MaterialRecipe(MaterialType.Flower, MaterialType.Stones, CombinationType.BaitC), // shrimp
            new MaterialRecipe(MaterialType.Flower, MaterialType.Twig, CombinationType.BaitD), // yellow marionette
            new MaterialRecipe(MaterialType.Stones, MaterialType.Feather, CombinationType.BaitE), // blue wooper
            new MaterialRecipe(MaterialType.Cobweb, MaterialType.Stones, CombinationType.BaitF), // christmas tree
            new MaterialRecipe(MaterialType.Feather, MaterialType.Flower, CombinationType.BaitG), // cockroach
        };
    }

    // check if valid recipe
    public CombinationType UseRecipe(MaterialType first, MaterialType second)
    {
        foreach (var recipe in recipes)
        {
            if ((recipe.MaterialOne == first && recipe.MaterialTwo == second) ||
                (recipe.MaterialOne == second && recipe.MaterialTwo == first))
            {
                return recipe.Result;
            }
        }
        Debug.Log("invalid recipe, defaulting to BaitG");
        return CombinationType.BaitG;  // default value
    }
}

/// example usage:
///  public RecipeManager recipeManager;
///  recipeManager.GetCombination(firstMaterial, secondMaterial);