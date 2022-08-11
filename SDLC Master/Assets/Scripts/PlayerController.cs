using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector3 position;
    public float speed = 0.05f;

    public Image healthBar;
    public float healthBarYOOffset = 1f;

    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            position.z -= speed;
            transform.position = position;
        }
        if(Input.GetKey(KeyCode.S))
        {
            position.z += speed;
            transform.position = position;
        }
        if(Input.GetKey(KeyCode.A))
        {
            position.x += speed;
            transform.position = position;
        }
        if(Input.GetKey(KeyCode.D))
        {
            position.x -= speed;
            transform.position = position;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.name == "Player")
                {
                    healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                    healthBar.gameObject.SetActive(true);
                }
            }
        }
    }
}
