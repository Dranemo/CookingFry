using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FbxToPNGConverter : MonoBehaviour
{
    List<GameObject> prefabs = new();
    string folderPath = "Assets/Toony Kitchen Ingredients Free/Prefabs/Ingredients"; // Chemin du dossier contenant les prefabs � convertir


    private void Start()
    {
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            Debug.LogError($"Le dossier sp�cifi� n'existe pas : {folderPath}");
            return;
        }



        // R�cup�rer tous les fichiers .prefab du dossier sp�cifi�
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (prefab != null)
            {
                prefabs.Add(prefab);
            }
        }


        Debug.Log($"Nombre de prefabs trouv�s : {prefabs.Count}");


        StartCoroutine(GeneratePreviews());
    }


    IEnumerator GeneratePreviews()
    {
        foreach (GameObject prefab in prefabs)
        {
            Texture2D preview = AssetPreview.GetAssetPreview(prefab);

            while (preview == null)
            {
                yield return new WaitForSeconds(0.1f);
                preview = AssetPreview.GetAssetPreview(prefab);
            }

            if (preview != null)
            {
                // Rendre l'arri�re-plan transparent
                preview = MakeBackgroundTransparent(preview);
                // Sauvegarder l'aper�u sous forme d'image
                byte[] bytes = preview.EncodeToPNG();
                string filePath = $"Assets/SpritePNGs/Ingredients/{prefab.name}.png";
                System.IO.File.WriteAllBytes(filePath, bytes);
                Debug.Log($"Preview g�n�r�e et sauvegard�e : {filePath}");
            }
            else
            {
                Debug.LogWarning("Impossible d'obtenir un aper�u pour ce prefab.");
            }
        }
    }










    static Texture2D MakeBackgroundTransparent(Texture2D original)
    {
        Color backgroundColor = new Color(82 / 255f, 82 / 255f, 82 / 255f, 1f); // Couleur de fond par d�faut (gris Unity)
        float tolerance = 0.05f; // Tol�rance pour d�tecter la couleur de fond

        Texture2D transparentTexture = new Texture2D(original.width, original.height, TextureFormat.RGBA32, false);
        for (int y = 0; y < original.height; y++)
        {
            for (int x = 0; x < original.width; x++)
            {
                Color pixel = original.GetPixel(x, y);

                if (IsColorSimilar(pixel, backgroundColor, tolerance))
                {
                    pixel.a = 0;
                }

                transparentTexture.SetPixel(x, y, pixel);
            }
        }

        transparentTexture.Apply();
        return transparentTexture;
    }

    static bool IsColorSimilar(Color c1, Color c2, float tolerance)
    {
        return Mathf.Abs(c1.r - c2.r) < tolerance &&
               Mathf.Abs(c1.g - c2.g) < tolerance &&
               Mathf.Abs(c1.b - c2.b) < tolerance;
    }

}
