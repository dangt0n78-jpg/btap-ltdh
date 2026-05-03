using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class TimeOfDayController : MonoBehaviour
{
    [Header("Object")]
    public GameObject sunObject;
    public GameObject moonObject;
    public Slider timeSlider;

    [Header("Slider test")]
    [Range(0f, 24f)]
    public float timeOfDay = 12f;

    private Light sunLight;
    private Light moonLight;

    void Update()
    {
        if (Application.isPlaying && timeSlider != null) timeOfDay = timeSlider.value;
        else if (!Application.isPlaying && timeSlider != null) timeSlider.value = timeOfDay;

        if (sunLight == null && sunObject != null) sunLight = sunObject.GetComponent<Light>();
        if (moonLight == null && moonObject != null) moonLight = moonObject.GetComponent<Light>();

        if (sunObject != null)
        {
            float sunRotationX = (timeOfDay / 24f) * 360f - 90f;
            sunObject.transform.rotation = Quaternion.Euler(sunRotationX, -30f, 0f);

            if (sunLight != null)
                sunLight.enabled = (timeOfDay > 6f && timeOfDay < 18f);
        }

        if (moonObject != null)
        {
            float moonRotationX = (timeOfDay / 24f) * 360f - 90f + 180f;
            moonObject.transform.rotation = Quaternion.Euler(moonRotationX, -60f, 0f);

            if (moonLight != null)
                moonLight.enabled = (timeOfDay <= 6f || timeOfDay >= 18f);
        }
    }
}