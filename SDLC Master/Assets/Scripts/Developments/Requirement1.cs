using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Requirement1 : MonoBehaviour
{
    public Button[] buttons;
    public Outline[] outlines;
    public Sprite normalBlueSprite;
    public Sprite selectBlueSprite;
    public Sprite normalPinkSprite;
    public Sprite selectPinkSprite;
    private Project project;
    // Start is called before the first frame update
    void Start() {
    }
    void OnEnable()
    {
        project = ProjectManager.instance.currentProject;
        
        buttons = GetComponentsInChildren<Button>();
        outlines = GetComponentsInChildren<Outline>();

        buttons[0].onClick.AddListener(() => ButtonPress(0));
        buttons[1].onClick.AddListener(() => ButtonPress(1));
        buttons[2].onClick.AddListener(() => ButtonPress(2));
        buttons[3].onClick.AddListener(() => ButtonPress(3));
        buttons[4].onClick.AddListener(() => ButtonPress(4));
        buttons[5].onClick.AddListener(() => ButtonPress(5));

        buttons[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = project.requirement1[0].word;
        buttons[1].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = project.requirement1[1].word;
        buttons[2].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = project.requirement1[2].word;
        buttons[3].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = project.requirement1[3].word;
        buttons[4].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = project.requirement1[4].word;
        buttons[5].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = project.requirement1[5].word;

        project.requirement1Answer = new List<string>();

        buttons[6].gameObject.SetActive(true);
        buttons[7].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonPress(int index)
    {
        if(index % 2 == 0)
        {
            // outlines[index].enabled = true;
            // outlines[index+1].enabled = !outlines[index].enabled;

            buttons[index].gameObject.GetComponent<Image>().sprite = selectBlueSprite;
            buttons[index+1].gameObject.GetComponent<Image>().sprite = normalPinkSprite;

            buttons[index].gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(34,38,42);
            buttons[index+1].gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(183,183,183);
        }
        else if(index % 2 == 1)
        {
            // outlines[index].enabled = true;
            // outlines[index-1].enabled = !outlines[index].enabled;

            buttons[index].gameObject.GetComponent<Image>().sprite = selectPinkSprite;
            buttons[index-1].gameObject.GetComponent<Image>().sprite = normalBlueSprite;

            buttons[index].gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(183,183,183);
            buttons[index-1].gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(34,38,42);
        }
    }

    public void Confirm()
    {
        int point = 0;
        foreach(var button in buttons)
        {
            Sprite answerSprite = button.gameObject.GetComponent<Image>().sprite;
            if(answerSprite == selectBlueSprite || answerSprite == selectPinkSprite){
                string answer = button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
                project.requirement1Answer.Add(answer);

                foreach(var word in project.requirement1){
                    if(answer == word.word && word.isCorrect){
                        button.gameObject.GetComponent<Image>().color = Color.green;
                        point++;
                    }else if(answer == word.word){
                        button.gameObject.GetComponent<Image>().color = Color.red;
                    }
                }
            }
        }

        // int point = 0;
        // foreach(var answer in project.requirement1Answer){
        //     foreach(var word in project.requirement1){
        //         if(answer == word.word && word.isCorrect){
        //             point++;
        //         }
        //     }
        // }
        // int point = project.requirement1Answer.Count(answer => project.requirement1.Any(word => word.word == answer && word.isCorrect));
        Debug.Log(point);
        
    }
}
