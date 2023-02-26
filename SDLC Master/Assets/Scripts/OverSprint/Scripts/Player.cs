using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //กำหนดความเร็วในการเคลื่อนที่
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    //
    private void Update(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;
        //ให้หันหัวตามทิศที่เดิน โดยที ratateSpeed เป็นความเร็วหัน
        float rotateSpeed = 10f;
        transform.forward =  Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        Debug.Log(Time.deltaTime);
    }

    public bool IsWalking(){
        return isWalking;

    }

}
