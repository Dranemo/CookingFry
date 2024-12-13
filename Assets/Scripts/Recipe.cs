using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipes/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    [SerializeField] string recipeName;
    [SerializeField] List<string> ingredients;
    [SerializeField] string instructions;
    [SerializeField] Vector3 itemInWorld;



    public string GetRecipe()
    {
        string stringRecipe;

        stringRecipe = $"<b><u>{recipeName}<u></b";

        stringRecipe += $"<b>Ingredients:</b>\n";
        foreach (var ingredient in ingredients)
        {
            stringRecipe += $"{ingredient}\n";
        }
        stringRecipe += "\n";

        stringRecipe += $"<b>Instructions:</b>\n{instructions}\n\n";

        return stringRecipe;
    }



    public string GetRecipeTitle()
    {
        return $"<b><u>{recipeName}<u></b>";
    }
    public string GetRecipeIngredients()
    {
        string stringRecipe = "";

        stringRecipe += $"<b>Ingredients:</b>\n";
        foreach (var ingredient in ingredients)
        {
            stringRecipe += $"- {ingredient}\n";
        }

        return stringRecipe;
    }

    public string GetRecipeInstructions()
    {
        return $"<b>Instructions:</b>\n{instructions}"; ;
    }









    public Vector3 GetItemInWorld()
    {
        return itemInWorld;
    }




}
