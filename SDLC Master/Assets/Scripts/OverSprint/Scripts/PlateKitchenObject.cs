using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO){
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO)){
            //ไม่ใช่ของที่จะมาใส่จาน
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO)){
            //มี ประเภทนี้แล้ว
            return false;
        }else{
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
    }

}
