using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCreationCanvas : MonoBehaviour
{
    [SerializeField] List<KitchenElement> ingredients;

    [SerializeField] Sprite placeHolderSprite;

    [SerializeField] List<TMP_Dropdown> dropdownIngredients;
    List<KitchenElement> selectedIngredients;

    [SerializeField] List<TMP_InputField> inputFields;
    List<string> outputFields;

    [SerializeField] Button quitButton;
    [SerializeField] Button createButton;


    [SerializeField] RecipeBook recipeBook;
    [SerializeField] GameObject bookCanvas;
    Outlining support;


    [SerializeField] GameObject penScript;


    private void Start()
    {


        outputFields = new List<string>();
        for (int i = 0; i < inputFields.Count; i++)
        {
            outputFields.Add("");
        }

        selectedIngredients = new List<KitchenElement>();
        for (int i = 0; i < dropdownIngredients.Count; i++)
        {
            selectedIngredients.Add(null);
        }

        for (int i = 0; i < dropdownIngredients.Count; i++)
        {
            foreach (var item in ingredients)
            {
                dropdownIngredients[i].options.Add(new TMP_Dropdown.OptionData(item.GetName(), item.GetSprite()));
            }

            int index = i;
            dropdownIngredients[i].onValueChanged.AddListener((value) => SelectIngredient(index, value));
        }

        for (int i = 0; i < inputFields.Count; i++)
        {
            outputFields[i] = "";
            int index = i;
            inputFields[i].onValueChanged.AddListener((value) => UpdateInputField(index, value));
        }

    }



    private void OnEnable()
    {
        quitButton.onClick.AddListener(QuitCanvas);
        createButton.onClick.AddListener(CreateRecipe);
    }
    private void OnDisable()
    {
        quitButton.onClick.RemoveListener(QuitCanvas);
        createButton.onClick.RemoveListener(CreateRecipe);
    }





    void CreateRecipe()
    {
        foreach (var item in outputFields)
        {
            if (item == "")
            {
                Debug.Log("Missing fields");
                return;
            }
        }

        if (selectedIngredients[0] == null)
        {
            Debug.Log("Missing ingredients");
            return;
        }

        List<KitchenElement> ingredients = new List<KitchenElement>();
        foreach (var item in selectedIngredients)
        {
            if(item != null)
                ingredients.Add(item);
        }

        Recipe recipe = new Recipe(outputFields[0], ingredients, outputFields[1], placeHolderSprite);
        recipeBook.AddRecipe(recipe);

        QuitCanvas();
    }


    void UpdateInputField(int index, string value)
    {
        outputFields[index] = value;
    }

    void SelectIngredient(int index, int value)
    {
        if(value == 0)
        {
            selectedIngredients[index] = null;
            return;
        }

        selectedIngredients[index] = ingredients[value-1];
    }



    private void Reset()
    {
        foreach (var item in inputFields)
        {
            item.text = "";
        }

        foreach (var item in dropdownIngredients)
        {
            item.value = 0;
        }
    }



    public void SetSupport(Outlining _support)
    {
        support = _support;
    }

    void QuitCanvas()
    {
        Reset();

        bookCanvas.GetComponent<GraphicRaycaster>().enabled = true;
        support.IsInteractButtonEnabled(true);
        gameObject.SetActive(false);

        foreach(Transform child in penScript.transform)
        {
            child.GetComponent<MeshCollider>().enabled = true;
        }
    }
}
