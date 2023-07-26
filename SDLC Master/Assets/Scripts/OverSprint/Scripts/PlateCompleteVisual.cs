using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {

    [Serializable]
    public struct KitchenObjectSO_GameObject {

        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;

    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;
    private int index;


    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList) {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
        index=0;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        if(index < kitchenObjectSOGameObjectList.Count){
            kitchenObjectSOGameObjectList[index].gameObject.SetActive(true);
            index++;
        }
    }

}