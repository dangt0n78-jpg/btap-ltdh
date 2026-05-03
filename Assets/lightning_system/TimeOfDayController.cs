using UnityEngine;
using UnityEngine.UI;

public class TimeOfDayController : MonoBehaviour
{
    [Header("Sun")]
    public GameObject sunObject;

    [Header("Slider")]
    public Slider timeSlider;

    void Start()
    {
        
        if (timeSlider != null)
        {
            timeSlider.onValueChanged.RemoveAllListeners();
            timeSlider.onValueChanged.AddListener(UpdateTimeOfDay); 
        }
    }

    public void UpdateTimeOfDay(float timeInHours)
    {
        if (sunObject != null)
        {
            float sunRotationX = (timeInHours / 24f) * 360f - 90f;

            
            sunObject.transform.rotation = Quaternion.Euler(sunRotationX, -30f, 0f);

            
            Debug.Log("Sun is at: " + sunRotationX + " degrees");
        }
    }
}