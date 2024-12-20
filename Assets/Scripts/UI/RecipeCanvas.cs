using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCanvas : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> texts;
    [SerializeField] Button quitButton;
    [SerializeField] GameObject bookCanvas;

    Outlining support;
    GameObject penScript;


    private void OnEnable()
    {
        quitButton.onClick.AddListener(CloseRecipe);
    }
    private void OnDisable()
    {
        quitButton.onClick.RemoveListener(CloseRecipe);
    }



    public void PrintOnPage(Recipe recipe, Outlining _support, GameObject _penScript)
    {
        support = _support;
        penScript = _penScript;

        texts[0].text = recipe.GetRecipeTitle();
        texts[1].text = recipe.GetRecipeIngredients();
        texts[2].text = recipe.GetRecipeInstructions();
    }

    void CloseRecipe()
    {
        bookCanvas.GetComponent<GraphicRaycaster>().enabled = true;
        support.IsInteractButtonEnabled(true);
        gameObject.SetActive(false);

        foreach (Transform child in penScript.transform)
        {
            child.GetComponent<MeshCollider>().enabled = true;
        }
    }
}
