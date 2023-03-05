using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class KeyInput : MonoBehaviour
{
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

    int index;

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
        // RandomizeChildren();
        slider.maxValue = 15;
        slider.value = 15;
        timer = StartCoroutine(StartTimer(15));
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
                            }
                        }
                    }else{
                        AudioManager.instance.Play("Warning");
                        if (sliderTween != null) sliderTween.Kill();
                        if(timer != null) StopCoroutine(timer);
                        slider.value -= 2;
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
            WaterFallManager.instance.qualityEachPhase[1] += 5;
            project.designPoint += 5;
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
        if(index < project.keyInput.Length)
        {
            SetupKeyInput();
        }else{
            gameObject.SetActive(false);
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

    public void RandomizeChildren()
    {
        int n = charBlocks.Length;
        for (int i = 0; i < n; i++)
        {
            int randomIndex = Random.Range(0, n);
            GameObject temp = charBlocks[i];
            charBlocks[i] = charBlocks[randomIndex];
            charBlocks[randomIndex] = temp;
        }

        for (int i = 0; i < n; i++)
        {
            charBlocks[i].transform.SetParent(charUI);
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
