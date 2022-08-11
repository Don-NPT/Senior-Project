using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintColiision : MonoBehaviour
{
    private Material material;
    private Color collisionColor = Color.red;
    private Color defaultColor = Color.green;

    private bool isCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        material.color = collisionColor;
        isCollided = true;
    }

    private void OnTriggerExit(Collider other) {
        material.color = defaultColor;
        isCollided = false;
    }

    public bool isCollide(){
        return isCollided;
    }
}
