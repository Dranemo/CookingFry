using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] List<Recipe> recipes;

    [Header("Title, ingredients, instructions")]
    [SerializeField] List<TextMeshProUGUI> recipePage1;
    [SerializeField] List<TextMeshProUGUI> recipePage2;

    [SerializeField] Button previousButton;
    [SerializeField] Button nextButton;

    [SerializeField] GameObject recipeCanvas;
    [SerializeField] GameObject bookCanvas;

    private int currentPage = 0;




    private void Start()
    {
        previousButton.onClick.AddListener(PreviousPage);
        nextButton.onClick.AddListener(NextPage);

        recipePage1[0].transform.GetComponentInParent<Button>().onClick.AddListener(() => ShowRecipe(currentPage));
        recipePage2[0].transform.GetComponentInParent<Button>().onClick.AddListener(() => ShowRecipe(currentPage + 1));

        previousButton.gameObject.SetActive(false);

        if (recipes.Count > 0)
        {
            SetRecipe(recipePage1, 0);
            SetRecipe(recipePage2, 1);
        }
    }



    private void Update()
    {
        if (currentPage < 2 && previousButton.gameObject.activeSelf)
            previousButton.gameObject.SetActive(false);
        else if(currentPage >= 2 && !previousButton.gameObject.activeSelf)
            previousButton.gameObject.SetActive(true);


        if (currentPage >= recipes.Count - 2 && nextButton.gameObject.activeSelf)
            nextButton.gameObject.SetActive(false);
        else if (currentPage < recipes.Count -2 && !nextButton.gameObject.activeSelf)
            nextButton.gameObject.SetActive(true);
    }


    void NextPage()
    {
        currentPage += 2;

        if (currentPage < recipes.Count)
        {
            SetRecipe(recipePage1, currentPage);
            SetRecipe(recipePage2, currentPage + 1);
        }
    }

    void PreviousPage()
    {
        currentPage -= 2;

        if (currentPage >= 0)
        {
            SetRecipe(recipePage1, currentPage);
            SetRecipe(recipePage2, currentPage + 1);
        }
    }


    void ShowRecipe(int page)
    {
        if (page < recipes.Count)
        {
            bookCanvas.GetComponent<GraphicRaycaster>().enabled = false;
            recipeCanvas.SetActive(true);   
            recipeCanvas.GetComponent<RecipeCanvas>().PrintOnPage(recipes[page]);
        }
    }



    void SetRecipe(List<TextMeshProUGUI> recipePage, int page)
    {

        if(page < 0 || page >= recipes.Count)
        {
            recipePage[0].text = "";
            recipePage[1].text = "";
            recipePage[2].text = "";
            return;
        }

        recipePage[0].text = recipes[page].GetRecipeTitle();
        recipePage[1].text = recipes[page].GetRecipeIngredients();
        recipePage[2].text = recipes[page].GetRecipeInstructions();
    }
}
