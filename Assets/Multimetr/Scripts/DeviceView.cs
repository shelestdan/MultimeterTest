using TMPro;
using UnityEngine;

public class DeviceView : MonoBehaviour
{
    [SerializeField]
    private Material outlineMaterial;

    [SerializeField]
    private TextMeshPro mainResultText;

    [SerializeField]
    private TextMeshProUGUI resistanceText;

    [SerializeField]
    private TextMeshProUGUI currentText;

    [SerializeField]
    private TextMeshProUGUI dcVoltageText;

    [SerializeField]
    private TextMeshProUGUI acVoltageText;

    private Material _defaultMaterial;
    private Renderer objectRenderer;

    public void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        _defaultMaterial = objectRenderer.material;
    }

    public void OnMouseOver()
    {
        objectRenderer.material = outlineMaterial;
    }

    public void OnMouseExit()
    {
        objectRenderer.material = _defaultMaterial;
    }

    public void UpdateDcVoltageText(float dcVoltage)
    {
        dcVoltageText.text = $"V = {dcVoltage:F2}";
    }

    public void UpdateAcVoltageText(float acVoltage)
    {
        acVoltageText.text = $"V~ = {acVoltage:F2}";
    }

    public void UpdateCurrentText(float current)
    {
        currentText.text = $"A = {current:F2}";
    }

    public void UpdateResistanceText(float resistance)
    {
        resistanceText.text = $"R = {resistance:F2}";
    }

    public void UpdateMainResultText(float mainResult)
    {
        mainResultText.text = $"{mainResult:F2}";
    }

    public void ResetTexts()
    {
        dcVoltageText.text = "V = 0";
        acVoltageText.text = "V~ = 0";
        currentText.text = "A = 0";
        resistanceText.text = "R = 0";
        mainResultText.text = "0";
    }
}
