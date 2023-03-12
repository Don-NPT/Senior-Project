using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class Balloon : MonoBehaviour, IPointerDownHandler 
{
    public int index;
    public bool isDev;
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
        if(ProjectManager.instance.currentProject.balloons[index].isCorrect)
        {
            if(isDev){
                Debug.Log("Point up");
                ProjectManager.instance.currentProject.balloonPoint += 5;
                WaterFallManager.instance.qualityEachPhase[2] += 5;
                FindObjectOfType<AudioManager>().Play("Purchase");
            }else{
                Debug.Log("Point down");
                ProjectManager.instance.currentProject.balloon2Point -= 5;
                WaterFallManager.instance.qualityEachPhase[3] -= 5;
                FindObjectOfType<AudioManager>().Play("Warning");
            }
            
        }else{
            if(isDev){
                Debug.Log("Point down");
                ProjectManager.instance.currentProject.balloonPoint -= 5;
                WaterFallManager.instance.qualityEachPhase[2] -= 5;
                FindObjectOfType<AudioManager>().Play("Warning");
            }else{
                Debug.Log("Point up");
                ProjectManager.instance.currentProject.balloon2Point += 5;
                WaterFallManager.instance.qualityEachPhase[3] += 5;
                FindObjectOfType<AudioManager>().Play("Purchase");
            }
        }
        if(isDev)
            ProjectManager.instance.currentProject.balloonAnswer.Add(ProjectManager.instance.currentProject.balloons[index].word);
        else
            ProjectManager.instance.currentProject.balloon2Answer.Add(ProjectManager.instance.currentProject.balloons[index].word);

        transform.DOPunchScale (new Vector3 (0.2f, 0.2f, 0.2f), .25f);
        GetComponent<Material>().DOColor(Color.green, 1);
        Destroy(gameObject, 0.25f);
     }
}
