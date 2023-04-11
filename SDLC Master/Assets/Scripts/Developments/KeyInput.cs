using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class KeyInput : MonoBehaviour
{
    public int maxTime;
    public int pointCorrect;
    public int pointWrong;
    public Transform inputUI;
    public Transform charUI;
    public TextMeshProUGUI hint;
    public GameObject inputBlockPrefab;
    public GameObject charBlockPrefab;
    public Slider slider;
    GameObject[] inputBlocks;
    GameObject[] charBlocks;
    private Project project;
    private Coroutine timer;
    private Tween sliderTween;
    private float calculateQuality;

    public int index;

    void Start()
    {
        
    }

    private void OnEnable() {
        project = ProjectManager.instance.currentProject;
        project.keyInputPass = new List<bool>();
        project.designPoint = 0;

        index = 0;
        SetupKeyInput();
    }

    void SetupKeyInput()
    {
        DestroyAllBlock();

        string vocab = project.keyInput[index].word;
        hint.text = project.keyInput[index].hint;
        char[] additionalChar = project.keyInput[index].additionalChar;

        inputBlocks = new GameObject[vocab.Length];
        charBlocks = new GameObject[vocab.Length + additionalChar.Length];

        //input block
        char[] randomChars = GetrandomCharacters(project.keyInput[index].showNum, vocab);
        for(int i=0; i<vocab.Length; i++)
        {
            inputBlocks[i] = (GameObject)Instantiate(inputBlockPrefab);
            inputBlocks[i].transform.SetParent(inputUI);
            inputBlocks[i].transform.localScale = new Vector3(1, 1, 1);

            inputBlocks[i].GetComponentInChildren<TextMeshProUGUI>().text = vocab[i].ToString();
            inputBlocks[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;

            foreach (char randomChar in randomChars)
            {
                if(randomChar == vocab[i])
                {
                    inputBlocks[i].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                }
            }
        }
        
        // all character block
        for(int i=0; i<vocab.Length; i++)
        {
            charBlocks[i] = (GameObject)Instantiate(charBlockPrefab);
            charBlocks[i].transform.SetParent(charUI);
            charBlocks[i].transform.localScale = new Vector3(1, 1, 1);

            charBlocks[i].GetComponentInChildren<TextMeshProUGUI>().text = vocab[i].ToString();
        }
        for(int i=0; i<additionalChar.Length; i++)
        {
            charBlocks[i] = (GameObject)Instantiate(charBlockPrefab);
            charBlocks[i].transform.SetParent(charUI);
            charBlocks[i].transform.localScale = new Vector3(1, 1, 1);

            charBlocks[i].GetComponentInChildren<TextMeshProUGUI>().text = additionalChar[i].ToString();
        }
        RandomizeChildren();
        ResetCharAlpha();
        slider.maxValue = maxTime;
        slider.value = maxTime;
        timer = StartCoroutine(StartTimer(maxTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    string keyName = key.ToString();
                    if (project.keyInput[index].word.Contains(keyName))
                    {
                        foreach(var block in inputBlocks)
                        {
                            if(block.GetComponentInChildren<TextMeshProUGUI>().text == keyName)
                            {
                                block.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                                LowerCharAlpha(keyName);
                            }
                        }
                    }else{
                        AudioManager.instance.Play("Warning");
                        if (sliderTween != null) sliderTween.Kill();
                        if(timer != null) StopCoroutine(timer);
                        slider.value -= pointWrong;
                        Debug.Log("เวลาลด"+slider.value);
                        timer = StartCoroutine(StartTimer((int)slider.value));
                    }
                }
            }
            if(CheckInputBlockPass())
            {
                NextKeyInput();
            }
        }
    }

    private void FixedUpdate() {
        if(slider.value <= 0){
            NextKeyInput();
        }
    }

    bool CheckInputBlockPass()
    {
        int pass = 0;
        foreach(var inputBlock in inputBlocks)
        {
            if(inputBlock.GetComponentInChildren<TextMeshProUGUI>().enabled == true)
            {
                pass++;
            }
        }
        if(pass >= inputBlocks.Length) {
            project.keyInputPass.Add(true);
            calculateQuality = (int)Mathf.Round(((float)StaffManager.instance.getSumStaffStat("Designer")/((float)(project.scale * 15))) * pointCorrect);
            calculateQuality = calculateQuality * SkillManager.instance.GetQualityBonus();
            WaterFallManager.instance.qualityEachPhase[1] += calculateQuality;
            project.designPoint += calculateQuality;
            AudioManager.instance.Play("Purchase");
            return true;
        }
        else return false;
    }

    void NextKeyInput()
    {
        if (sliderTween != null) sliderTween.Kill();
        if(timer != null) StopCoroutine(timer);
        index++;
        ProjectHUD.instance.WaterfallHUDUpdate();
        if(index < project.keyInput.Length)
        {
            SetupKeyInput();
        }else{
            gameObject.SetActive(false);
            WaterFallManager.instance.NextPhase();
        }
    }

    char[] GetrandomCharacters(int num, string text)
    {
        int n = text.Length;
        char[] randomCharacters = new char[num];

        for (int i = 0; i < num; i++)
        {
            int randomIndex = Random.Range(0, n - i);
            for (int j = 0; j < i; j++)
            {
                if (randomCharacters[j] <= text[randomIndex])
                {
                    randomIndex++;
                }
            }
            randomCharacters[i] = text[randomIndex];
        }

        return randomCharacters;
    }

    private void RandomizeChildren()
    {
        for (int i = 0; i < charBlocks.Length; i++)
        {
            int randomIndex = Random.Range(0, charBlocks.Length-1);
            string temp = charBlocks[i].GetComponentInChildren<TextMeshProUGUI>().text;
            charBlocks[i].GetComponentInChildren<TextMeshProUGUI>().text = charBlocks[randomIndex].GetComponentInChildren<TextMeshProUGUI>().text;
            charBlocks[randomIndex].GetComponentInChildren<TextMeshProUGUI>().text = temp;
        }
    }

    void LowerCharAlpha(string character){
        foreach(var charBlock in charBlocks){
            if(charBlock.GetComponentInChildren<TextMeshProUGUI>().text == character && charBlock.activeSelf){
                // charBlock.SetActive(false);
                Color color = charBlock.GetComponent<Image>().color;
                color.a = 0.3f;
                charBlock.GetComponent<Image>().color = color;
            }
        }
    }

    void ResetCharAlpha(){
        foreach(var charBlock in charBlocks){
            // charBlock.SetActive(false);
            Color color = charBlock.GetComponent<Image>().color;
            color.a = 1f;
            charBlock.GetComponent<Image>().color = color;
        }
    }

    IEnumerator StartTimer(int time)
    {
        sliderTween = slider.DOValue(0, time);
        yield return new WaitForSeconds(time);
    }

    public void DestroyAllBlock()
    {
        for (int i = inputUI.childCount - 1; i >= 0; i--)
        {
            GameObject child = inputUI.GetChild(i).gameObject;
            Destroy(child);
        }
        for (int i = charUI.childCount - 1; i >= 0; i--)
        {
            GameObject child = charUI.GetChild(i).gameObject;
            Destroy(child);
        }
    }

}
