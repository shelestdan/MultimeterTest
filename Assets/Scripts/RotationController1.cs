using TMPro;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public Material OutlineMaterial;

    [SerializeField]
    private float rotationSpeed = 100f;

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

    [SerializeField]
    private GameObject dcVoltageTrigger;

    [SerializeField]
    private GameObject acVoltageTrigger;

    [SerializeField]
    private GameObject currentTrigger;

    [SerializeField]
    private GameObject resistanceTrigger;

    private bool isMouseOver = false;
    private DeviceModel deviceModel = new DeviceModel();
    private Material defaultMaterial;
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        defaultMaterial = objectRenderer.material;

        deviceModel.Resistance = 1000f;
        deviceModel.Power = 400f;
        deviceModel.VariableVoltage = 0.01f;
    }

    private void OnMouseOver()
    {
        objectRenderer.material = OutlineMaterial;
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        objectRenderer.material = defaultMaterial;
        isMouseOver = false;
    }

    private void Update()
    {
        if (isMouseOver && Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float rotation = Input.GetAxis("Mouse ScrollWheel") * rotationSpeed;
            transform.Rotate(0, 0, rotation, Space.Self);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMouseOver)
        {
            if (other.gameObject == dcVoltageTrigger)
            {
                float current = Mathf.Sqrt(deviceModel.Power / deviceModel.Resistance);
                float voltage = deviceModel.Power / current;
                dcVoltageText.text = $"V = {voltage:F2}";
                mainResultText.text = $"{voltage:F2}";
            }
            else if (other.gameObject == acVoltageTrigger)
            {
                acVoltageText.text = $"V~ = {deviceModel.VariableVoltage:F2}";
                mainResultText.text = $"{deviceModel.VariableVoltage:F2}";
            }
            else if (other.gameObject == currentTrigger)
            {
                float current = Mathf.Sqrt(deviceModel.Power / deviceModel.Resistance);
                mainResultText.text = $"{current:F2}";
                currentText.text = $"A = {current:F2}";
            }
            else if (other.gameObject == resistanceTrigger)
            {
                mainResultText.text = $"{deviceModel.Resistance:F2}";
                resistanceText.text = $"R = {deviceModel.Resistance:F2}";
            }
            else
            {
                Debug.Log("Entered the trigger of an unknown object.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        dcVoltageText.text = "V = 0";
        acVoltageText.text = "V~ = 0";
        currentText.text = "A = 0";
        resistanceText.text = "R = 0";
        mainResultText.text = "0";
    }
}
