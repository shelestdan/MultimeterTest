using UnityEngine;
using TMPro;

public class RotationController : MonoBehaviour
{
    public Material OutlineMaterial;
    private Material DefaultMaterial;
    private Renderer ObjectRenderer;

    [SerializeField]
    private float rotationSpeed = 100f;

    public TextMeshPro Result;

    public TextMeshProUGUI Resistance_R;
    public TextMeshProUGUI Current_A;
    public TextMeshProUGUI Voltage_V;
    public TextMeshProUGUI Voltage_V_Direct;

    public GameObject Trigger_V;
    public GameObject Trigger_V_Direct;
    public GameObject Trigger_A;
    public GameObject Trigger_R;

    private bool isMouseOver = false;

    private DeviceModel deviceModel = new DeviceModel();

    void Start()
    {
        ObjectRenderer = GetComponent<Renderer>();
        DefaultMaterial = ObjectRenderer.material;

        deviceModel.Resistance = 1000f;
        deviceModel.Power = 400f;
        deviceModel.VariableVoltage = 0.01f;
    }

    void OnMouseOver()
    {
        ObjectRenderer.material = OutlineMaterial;
        isMouseOver = true;
    }

    void OnMouseExit()
    {
        ObjectRenderer.material = DefaultMaterial;
        isMouseOver = false;
    }

    void Update()
    {
        if (isMouseOver && Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float rotation = Input.GetAxis("Mouse ScrollWheel") * rotationSpeed;
            transform.Rotate(0, 0, rotation, Space.Self);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isMouseOver)
        {
            if (other.gameObject == Trigger_V)
            {
                Voltage_V.text = $"V = {deviceModel.VariableVoltage:F2}";
                Result.text = $"{deviceModel.VariableVoltage:F2}";
            }
            else if (other.gameObject == Trigger_V_Direct)
            {
                float voltage = Mathf.Sqrt(deviceModel.Power * deviceModel.Resistance);
                Result.text = $"{voltage:F2}";
                Voltage_V_Direct.text = $"V~ = {voltage:F2}";
            }
            else if (other.gameObject == Trigger_A)
            {
                float current = Mathf.Sqrt(deviceModel.Power / deviceModel.Resistance);
                Result.text = $"{current:F2}";
                Current_A.text = $"A = {current:F2}";
            }
            else if (other.gameObject == Trigger_R)
            {
                Result.text = $"{deviceModel.Resistance:F2}";
                Resistance_R.text = $"R = {deviceModel.Resistance:F2}";
            }
            else
            {
                Debug.Log("Entered the trigger of an unknown object.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Voltage_V.text = "V = 0";
        Voltage_V_Direct.text = "V~ = 0";
        Current_A.text = "A = 0";
        Resistance_R.text = "R = 0";
        Result.text = "0";
    }
}
