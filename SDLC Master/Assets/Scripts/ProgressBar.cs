using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public float speed;
    public Color color;
    // public float currentVelocity = 0;

    public void SetupBar(int _max, float _speed)
    {
        slider.maxValue = _max;
        slider.value = 0;
        speed = _speed;
    }

    public void SetupBarWithColor(int _max, float _speed, Project.Phases phase)
    {
        slider.maxValue = _max;
        slider.value = 0;
        speed = _speed;
        slider.GetComponentsInChildren<Image>()[1].color = getColorbyPhase(phase);
    }

    public void UpdateBar()
    {
        slider.value += speed * Time.deltaTime;
    }

    public bool IsCompleted()
    {
        if(slider.value >= slider.maxValue - 1){
            transform.DOPunchScale(new Vector3 (0.2f, 0.2f, 0.2f), .25f);
            Destroy(gameObject, 2f);
            return true;
        }  
        else
            return false;
    }

    Color32 getColorbyPhase(Project.Phases phase)
    {
        switch(phase)
        {
            case Project.Phases.ANALYSIS:
                return new Color32(253, 166, 87, 255);
            case Project.Phases.DESIGN:
                return new Color32(245, 253, 87, 255);
            case Project.Phases.CODING:
                return new Color32(87, 253, 101, 255);
            case Project.Phases.TESTING:
                return new Color32(87, 173, 253, 255);
            case Project.Phases.DEPLOYMENT:
                return new Color32(174, 136, 255, 255);
        }
        return new Color32(87, 253, 132, 255);
    }

}
