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
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //ถือจานยุค้าบ
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                    GetKitchenObject().DestroySelf();
                    }
                }else{
                    //ผู้เล่นไม่ได้ถือจาน แต่ถือบางอย่างอยู่ กุ๊กกุ๊ก กู๊ววว
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        //counter มีจานวางอยู่
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }else{
                //ไม่ได้ถืออะไรอยู่เยย
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }


}
