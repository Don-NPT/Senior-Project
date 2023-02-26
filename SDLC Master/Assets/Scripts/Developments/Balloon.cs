using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class Balloon : MonoBehaviour, IPointerDownHandler 
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.up * UnityEngine.Random.Range(1.5f, 3);
    }

    public void OnPointerDown (PointerEventData eventData) 
     {
        if(ProjectManager.instance.currentProjects[0].balloons[index].isCorrect)
        {
            Debug.Log("Point up");
        }else{
            Debug.Log("Point down");
        }

        transform.DOPunchScale (new Vector3 (0.2f, 0.2f, 0.2f), .25f);
        GetComponent<Material>().DOColor(Color.green, 1);
        Destroy(gameObject, 0.25f);
     }
}