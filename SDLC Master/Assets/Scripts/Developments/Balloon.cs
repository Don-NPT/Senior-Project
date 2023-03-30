using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class Balloon : MonoBehaviour, IPointerDownHandler 
{
    public int index;
    public bool isDev;
    private Project project;
    // Start is called before the first frame update
    void Start()
    {
        project = ProjectManager.instance.currentProject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.up * UnityEngine.Random.Range(1.5f, 3);
    }

    public void OnPointerDown (PointerEventData eventData) 
     {
        int pointCorrect = BalloonBoom.instance.pointCorrect;
        int pointWrong = BalloonBoom.instance.pointWrong;
        int calculateQuality = BalloonBoom.instance.calculateQuality;
        if(ProjectManager.instance.currentProject.balloons[index].isCorrect)
        {
            if(isDev){
                Debug.Log("Point up");
                calculateQuality = (int)Mathf.Round(((float)StaffManager.instance.getSumStaffStat("Programmer")/((float)(project.scale * 15))) * pointCorrect);
                ProjectManager.instance.currentProject.balloonPoint += calculateQuality;
                WaterFallManager.instance.qualityEachPhase[2] += calculateQuality;
                FindObjectOfType<AudioManager>().Play("Purchase");
            }else{
                Debug.Log("Point down");
                calculateQuality = (int)Mathf.Round(((float)StaffManager.instance.getSumStaffStat("Programmer")/((float)(project.scale * 15))) * pointWrong);
                ProjectManager.instance.currentProject.balloon2Point += calculateQuality;
                WaterFallManager.instance.qualityEachPhase[3] += calculateQuality;
                FindObjectOfType<AudioManager>().Play("Warning");
            }
            
        }else{
            if(isDev){
                Debug.Log("Point down");
                calculateQuality = (int)Mathf.Round(((float)StaffManager.instance.getSumStaffStat("Programmer")/((float)(project.scale * 15))) * pointWrong);
                ProjectManager.instance.currentProject.balloonPoint += pointWrong;
                WaterFallManager.instance.qualityEachPhase[2] += pointWrong;
                FindObjectOfType<AudioManager>().Play("Warning");
            }else{
                Debug.Log("Point up");
                calculateQuality = (int)Mathf.Round(((float)StaffManager.instance.getSumStaffStat("Programmer")/((float)(project.scale * 15))) * pointCorrect);
                ProjectManager.instance.currentProject.balloon2Point += pointCorrect;
                WaterFallManager.instance.qualityEachPhase[3] += pointCorrect;
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
