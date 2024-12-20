using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] List<Recipe> recipes;


    [SerializeField] TextMeshProUGUI recipePage1Title;
    [SerializeField] Image recipePage1Image;

    [SerializeField] TextMeshProUGUI recipePage2Title;
    [SerializeField] Image recipePage2Image;




    [SerializeField] Button previousButton;
    [SerializeField] Button nextButton;

    [SerializeField] GameObject recipeCanvas;
    [SerializeField] GameObject bookCanvas;

    [SerializeField] Outlining Support;
    [SerializeField] GameObject pen;

    private int currentPage = 0;




    private void Start()
    {
        previousButton.onClick.AddListener(PreviousPage);
        nextButton.onClick.AddListener(NextPage);

        recipePage1Title.transform.GetComponentInParent<Button>().onClick.AddListener(() => ShowRecipe(currentPage));
        recipePage2Title.transform.GetComponentInParent<Button>().onClick.AddListener(() => ShowRecipe(currentPage + 1));

        previousButton.gameObject.SetActive(false);

        if (recipes.Count > 0)
        {
            SetRecipe(recipePage1Title, recipePage1Image, 0);
            SetRecipe(recipePage2Title, recipePage2Image, 1);
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
            SetRecipe(recipePage1Title, recipePage1Image, currentPage);
            SetRecipe(recipePage2Title, recipePage2Image, currentPage + 1);
        }
    }

    void PreviousPage()
    {
        currentPage -= 2;

        if (currentPage >= 0)
        {
            SetRecipe(recipePage1Title, recipePage1Image, currentPage);
            SetRecipe(recipePage2Title, recipePage2Image, currentPage + 1);
        }
    }


    void ShowRecipe(int page)
    {
        if (page < recipes.Count)
        {
            bookCanvas.GetComponent<GraphicRaycaster>().enabled = false;
            Support.IsInteractButtonEnabled(false);

            foreach (Transform child in pen.transform)
            {
                child.GetComponent<MeshCollider>().enabled = false;
            }

            recipeCanvas.SetActive(true);   
            recipeCanvas.GetComponent<RecipeCanvas>().PrintOnPage(recipes[page], Support, pen);
        }
    }



    void SetRecipe(TextMeshProUGUI recipePageTitle, Image sprite, int page)
    {

        if(page < 0 || page >= recipes.Count)
        {
            recipePageTitle.text = "";
            sprite.enabled = false;
            return;
        }

        recipePageTitle.text = recipes[page].GetRecipeTitle();

        if(!sprite.enabled)
            sprite.enabled = true;
        sprite.sprite = recipes[page].GetSprite();
    }

    public void AddRecipe(Recipe recipe)
    {
        recipes.Add(recipe);

        currentPage -= 2;
        NextPage();
    }
}
