using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipes/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    [SerializeField] string recipeName;
    [SerializeField] List<KitchenElement> ingredients;
    [SerializeField] string instructions;
    [SerializeField] Sprite sprite;



    public string GetRecipe()
    {
        string stringRecipe;

        stringRecipe = $"<b><u>{recipeName}<u></b";

        stringRecipe += $"<b>Ingredients:</b>\n";
        foreach (KitchenElement ingredient in ingredients)
        {
            stringRecipe += $"{ingredient.GetName()}\n";
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
        foreach (KitchenElement ingredient in ingredients)
        {
            stringRecipe += $"- {ingredient.GetName()}\n";
        }

        return stringRecipe;
    }

    public string GetRecipeInstructions()
    {
        return $"<b>Instructions:</b>\n{instructions}"; ;
    }









    public Sprite GetSprite()
    {
        return sprite;
    }




}
