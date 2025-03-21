using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Oven : Table
{
    [SerializeField] GameObject button;
    [SerializeField] Slider slider;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] TextMeshProUGUI hotnessText;

    float hotValue = 0;


    new private void Awake()
    {
        base.Awake();

        slider.onValueChanged.AddListener((float value) => HeatUp(value));
        itemToDisable.Add(slider.gameObject);
        slider.gameObject.SetActive(false);
    }


    void HeatUp(float value)
    {
        hotValue = value;

        button.transform.rotation = Quaternion.Euler(0, 0, 50 + value);
        hotnessText.text = value.ToString("F0") + '�';

        var main = particleSystem.main;
        main.startSize = new ParticleSystem.MinMaxCurve(0.1f + value / 500, 0.15f + value / 500);

        var emission = particleSystem.emission;
        emission.rateOverTime = value / 10;

    }

    public float ReturnHotValue()
    {
        return hotValue;
    }
}
