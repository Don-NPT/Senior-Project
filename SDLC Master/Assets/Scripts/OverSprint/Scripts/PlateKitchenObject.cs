using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlateKitchenObject : KitchenObject
{
    private int kitchenhold = 0;
    private int kitchenholdMax =15;
    public Slider progressBar;

    public event EventHandler <OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }


    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
        progressBar.maxValue = kitchenholdMax;
        progressBar.value = 0;
    }

    private void FixedUpdate() {
        progressBar.value = kitchenhold;
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO){
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO)){
            //ไม่ใช่ของที่จะมาใส่จาน
            return false;
        }
        // if(kitchenObjectSOList.Contains(kitchenObjectSO)){
        //     //มี ประเภทนี้แล้ว
        //     return false;
        // }
        else{
            if((kitchenhold + kitchenObjectSO.size) <= kitchenholdMax){
            kitchenhold += kitchenObjectSO.size;
            
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{
                kitchenObjectSO = kitchenObjectSO
            });
            Debug.Log(kitchenObjectSO.objectName);
            return true;
            }
            else{
                Debug.Log("เกิน limit sprint");
                progressBar.transform.DOShakeScale(0.2f, 1, 10, 90);
                return false;
            }
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }

}
