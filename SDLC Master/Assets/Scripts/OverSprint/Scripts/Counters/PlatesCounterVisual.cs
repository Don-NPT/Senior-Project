using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefeb;


    private List<GameObject> plateVisualGameObjectList;

    private void Awake(){
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start(){
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlatedSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlatedRemoved;
    }

    private void PlatesCounter_OnPlatedRemoved(object sender, System.EventArgs e){
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlatedSpawned(object sender, System.EventArgs e){
        Transform plateVisualTransform = Instantiate(plateVisualPrefeb, counterTopPoint);

        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

}
