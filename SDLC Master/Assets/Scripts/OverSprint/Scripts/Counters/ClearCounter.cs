using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //Empty
            if(player.HasKitchenObject()){
                //hold
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else{
                //don't hold
            }
        }else{
            //มี
            if(player.HasKitchenObject()){
                //hold
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //hold task plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                    GetKitchenObject().DestroySelf();
                    }
                }else{
                    //don't hold task plate but hold something...may be ghost....
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        //counter have plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }else{
                //don't hold anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }


}
