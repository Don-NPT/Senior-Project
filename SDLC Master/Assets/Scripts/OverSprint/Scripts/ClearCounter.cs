using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //ไม่มี
            if(player.HasKitchenObject()){
                //ถืออยู่
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else{
                //ไม่ได้ถืออยู่
            }
        }else{
            //มี
            if(player.HasKitchenObject()){
                //ถืออยู่
            }else{
                //ไม่ได้ถืออยู่
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }


}
