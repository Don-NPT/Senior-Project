using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
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
        float calculateQuality = BalloonBoom.instance.calculateQuality;
        if(ProjectManager.instance.currentProject.balloons[index].isCorrect)
        {
            if(isDev){
                Debug.Log("Point up");
                calculateQuality = ((float)StaffManager.instance.getSumStaffStat("Programmer")/((float)(project.scale * 15))) * pointCorrect;
                calculateQuality = calculateQuality * SkillManager.instance.GetQualityBonus();
                ProjectManager.instance.currentProject.balloonPoint += calculateQuality;
                WaterFallManager.instance.qualityEachPhase[2] += calculateQuality;
                FindObjectOfType<AudioManager>().Play("Purchase");
                GetComponent<Image>().DOColor(Color.green, 0.2f);
            }else{
                Debug.Log("Point down");
                calculateQuality = ((float)StaffManager.instance.getSumStaffStat("Tester")/((float)(project.scale * 15))) * pointWrong;
                calculateQuality = calculateQuality * SkillManager.instance.GetQualityBonus();
                ProjectManager.instance.currentProject.balloon2Point += calculateQuality;
                WaterFallManager.instance.qualityEachPhase[3] += calculateQuality;
                FindObjectOfType<AudioManager>().Play("Warning");
                GetComponent<Image>().DOColor(Color.red, 0.2f);
            }
            
        }else{
            if(isDev){
                Debug.Log("Point down");
                calculateQuality = ((float)StaffManager.instance.getSumStaffStat("Programmer")/((float)(project.scale * 15))) * pointWrong;
                calculateQuality = calculateQuality * SkillManager.instance.GetQualityBonus();
                ProjectManager.instance.currentProject.balloonPoint += calculateQuality;
                WaterFallManager.instance.qualityEachPhase[2] += calculateQuality;
                FindObjectOfType<AudioManager>().Play("Warning");
                GetComponent<Image>().DOColor(Color.red, 0.2f);
            }else{
                Debug.Log("Point up");
                calculateQuality = ((float)StaffManager.instance.getSumStaffStat("Tester")/((float)(project.scale * 15))) * pointCorrect;
                calculateQuality = calculateQuality * SkillManager.instance.GetQualityBonus();
                ProjectManager.instance.currentProject.balloon2Point += calculateQuality;
                WaterFallManager.instance.qualityEachPhase[3] += calculateQuality;
                FindObjectOfType<AudioManager>().Play("Purchase");
                GetComponent<Image>().DOColor(Color.green, 0.2f);
            }
        }
        if(isDev)
            ProjectManager.instance.currentProject.balloonAnswer.Add(ProjectManager.instance.currentProject.balloons[index].word);
        else
            ProjectManager.instance.currentProject.balloon2Answer.Add(ProjectManager.instance.currentProject.balloons[index].word);

        transform.DOPunchScale (new Vector3 (0.2f, 0.2f, 0.2f), .3f);
        Destroy(gameObject, 0.3f);
     }
}
