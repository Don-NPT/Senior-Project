using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class Balloon : MonoBehaviour, IPointerDownHandler 
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.up * 0.5f;
    }

    public void OnPointerDown (PointerEventData eventData) 
     {
        transform.DOPunchScale (new Vector3 (0.2f, 0.2f, 0.2f), .25f);
        GetComponent<Material>().DOColor(Color.green, 1);
        Destroy(gameObject, 0.25f);
     }
}
