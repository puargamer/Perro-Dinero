using System.Collections.Generic;
using UnityEngine;

public class MaterialRecipe
{
    public MaterialType MaterialOne { get; }
    public MaterialType MaterialTwo { get; }
    public CombinationType Result { get; }
    public string FishColor { get; }

    public MaterialRecipe(MaterialType materialOne, MaterialType materialTwo, CombinationType result, string fColor)
    {
        MaterialOne = materialOne;
        MaterialTwo = materialTwo;
        Result = result;
        FishColor = fColor;
    }
}

public static class RecipeBook
{
    private static List<MaterialRecipe> recipes;

    static RecipeBook()// holds all of the recipes to be used in the crafting thus far
    {
        recipes = new List<MaterialRecipe>
        {
            new MaterialRecipe(MaterialType.Cobweb, MaterialType.Twig, CombinationType.BaitA, "Brown Lure"), // brown lookin idiot
            new MaterialRecipe(MaterialType.Feather, MaterialType.Cobweb, CombinationType.BaitB, "Red Lure"), // red clam
            new MaterialRecipe(MaterialType.Flower, MaterialType.Stones, CombinationType.BaitC, "Orange Lure"), // shrimp
            new MaterialRecipe(MaterialType.Flower, MaterialType.Twig, CombinationType.BaitD, "Yellow Lure"), // yellow marionette
            new MaterialRecipe(MaterialType.Stones, MaterialType.Feather, CombinationType.BaitE, "Blue Lure"), // blue wooper
            new MaterialRecipe(MaterialType.Cobweb, MaterialType.Stones, CombinationType.BaitF, "Green Lure"), // christmas tree
            new MaterialRecipe(MaterialType.Feather, MaterialType.Flower, CombinationType.BaitG, "Purple Lure"), // cockroach
        };
    }

    // check if valid recipe
    public static CombinationType UseRecipe(MaterialType first, MaterialType second)
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
    public static List<MaterialRecipe> GetAllRecipes()
    {
        return new List<MaterialRecipe>(recipes);
    }
    public static List<string> GetFormattedValidRecipes()
    {
        List<string> validRecipes = new List<string>();

        foreach (var recipe in recipes)
        {
            string formattedRecipe = $"{recipe.MaterialOne} + {recipe.MaterialTwo} = {recipe.FishColor}";
            validRecipes.Add(formattedRecipe);
        }

        return validRecipes;
    }
}

/// example usage:
///  public RecipeManager recipeManager;
///  recipeManager.GetCombination(firstMaterial, secondMaterial);