using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public float speed;
    // public float currentVelocity = 0;

    public void SetupBar(int _max, float _speed)
    {
        slider.maxValue = _max;
        slider.value = 0;
        speed = _speed;
    }

    // private void Update() {
    //     Debug.Log(slider.value);
    // }

    public void UpdateBar()
    {
        // yield return new WaitForSeconds(5);
        // slider.value = Mathf.SmoothDamp(slider.value, slider.maxValue, ref currentVelocity, speed * Time.deltaTime);
        // slider.value = Mathf.Lerp(slider.value, slider.value + 1, 0.5f * Time.deltaTime);
        slider.value += speed * Time.deltaTime;
    }

    public bool IsCompleted()
    {
        if(slider.value >= slider.maxValue){
            transform.DOPunchScale(new Vector3 (0.2f, 0.2f, 0.2f), .25f);
            return true;
        }  
        else
            return false;
    }

}
