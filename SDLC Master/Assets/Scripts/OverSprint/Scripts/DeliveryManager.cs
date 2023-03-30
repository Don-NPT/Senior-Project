using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    public List<string> newRecipeNames = new List<string>();

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipesAmount;
    // public List<List<KitchenObjectSO>> sprintList;
    public List<SprintTask> sprintList;
    private int sprintIndex;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
        sprintList = new List<SprintTask>();
        sprintIndex = 1;
    }

    private void Update()
    {
        // if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax)
        // {
        //     spawnNewRecipe();
        // }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        // for (int i = 0; i < waitingRecipeSOList.Count; i++)
        // {
        //     RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

        //     if (waitingRecipeSO.kitchenObjectList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
        //     {
        //         // Matching number of ingredients found
        //         bool plateContentsMatchesRecipe = true;
        //         foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectList)
        //         {
        //             // Check if each recipe ingredient is present in plate
        //             bool ingredientFound = false;
        //             foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        //             {
        //                 if (plateKitchenObjectSO == recipeKitchenObjectSO)
        //                 {
        //                     ingredientFound = true;
        //                     break;
        //                 }
        //             }
        //             if (!ingredientFound)
        //             {
        //                 plateContentsMatchesRecipe = false;
        //             }
        //         }
        //         if (plateContentsMatchesRecipe)
        //         {
        //             // Matching recipe found
        //             successfulRecipesAmount++;
        //             waitingRecipeSOList.RemoveAt(i);
        //             OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        //             OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
        //             spawnNewRecipe(); // Spawn a new recipe after a successful delivery
        //             return;
        //         }
        //     }
        // }

        // No matching recipe found, add plate contents as a new recipe
        // RecipeSO newRecipeSO = ScriptableObject.CreateInstance<RecipeSO>();
        // newRecipeSO.kitchenObjectList = new List<KitchenObjectSO>(plateKitchenObject.GetKitchenObjectSOList());
        // newRecipeSO.name = "Task for sprint " + (recipeListSO.recipeSOList.Count + 1);
        // newRecipeNames.Add(newRecipeSO.name);
        // recipeListSO.recipeSOList.Add(newRecipeSO);
        
        // Create a new asset for the new recipe
        // string recipeAssetPath = "Assets/OverSprint/ScriptableObjects/KitchenObjectSO/RecipeSO/";
        // AssetDatabase.CreateAsset(newRecipeSO, recipeAssetPath + "NewRecipe" + (recipeListSO.recipeSOList.Count) + ".asset");
        // AssetDatabase.SaveAssets();

        // waitingRecipeSOList.Add(newRecipeSO);
        // Debug.Log("New recipe created: " + string.Join(", ", newRecipeSO.kitchenObjectList));
        // Debug.Log("New Recipe Names List: " + string.Join(", ", newRecipeNames));
        // OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        // spawnNewRecipe(); // Spawn a new recipe after a new recipe is added
        // OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        if(sprintList.Count < 15){
            sprintList.Add(new SprintTask(plateKitchenObject.GetKitchenObjectSOList(), "Sprint " + sprintIndex));
            sprintIndex++;
        }
        
        GameObject[] counters = GameObject.FindGameObjectsWithTag("ContainerCounter");
        int sumEmpty = 0;
        foreach(var counter in counters){
            if(counter.GetComponent<ContainerCounter>().numTasks == 0){
                sumEmpty++;
            }
        }
        if(sumEmpty == 4 && GameObject.FindGameObjectsWithTag("Task").Length == 0){
            KitchenGameManager.Instance.SetGameOver();
        }
    }



    public List<RecipeSO> GetWaitingRecipeSOList(){
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount() {
        return successfulRecipesAmount;
    }
    
    private void spawnNewRecipe()
    {
        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
        
        // Check if waitingRecipeSO is already in waitingRecipeSOList
        if (waitingRecipeSOList.Contains(waitingRecipeSO))
        {
            Debug.Log("Recipe already exists in waiting list: " + waitingRecipeSO.name);
            return;
        }

        waitingRecipeSOList.Add(waitingRecipeSO);

        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }
    public void ClearNewRecipeList()
    {
        // Loop through the list in reverse order so that we can safely remove items
        for (int i = recipeListSO.recipeSOList.Count - 1; i > 0; i--)
        {
            // Check if the recipe is a NewRecipe
            if (waitingRecipeSOList.Contains(recipeListSO.recipeSOList[i]))
            {
                // Remove the NewRecipe from the recipe list
                recipeListSO.recipeSOList.RemoveAt(i);
            }
        }

        // Clear the waitingRecipeSOList
        waitingRecipeSOList.Clear();
    }

}
