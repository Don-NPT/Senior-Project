using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnplayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject()){
            //ไม่ได้ถืออะไรอยู่
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefeb);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnplayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

}