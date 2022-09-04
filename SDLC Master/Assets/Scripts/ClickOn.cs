using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{
    [SerializeField] private Material Color1;
    [SerializeField] private Material Color2;
    private MeshRenderer myRend;
    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    public void ClickMe()
    {
        myRend.material = Color2;
    }
}
