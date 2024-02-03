using UnityEngine;

public class DeviceController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f;

    [SerializeField]
    private GameObject dcVoltageTrigger;

    [SerializeField]
    private GameObject acVoltageTrigger;

    [SerializeField]
    private GameObject currentTrigger;

    [SerializeField]
    private GameObject resistanceTrigger;

    private bool isMouseOver = false;
    private DeviceModel deviceModel;
    private DeviceView deviceView;

    private void Start()
    {
        deviceModel = new DeviceModel
        {
            Resistance = 1000f,
            Power = 400f,
            VariableVoltage = 0.01f
        };

        deviceView = GetComponent<DeviceView>();
    }

    private void OnMouseOver()
    {
        deviceView.OnMouseOver();
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        deviceView.OnMouseExit();
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
            float current = Mathf.Sqrt(deviceModel.Power / deviceModel.Resistance);
            float voltage = deviceModel.Power / current;

            if (other.gameObject == dcVoltageTrigger)
            {
                deviceView.UpdateDcVoltageText(voltage);
                deviceView.UpdateMainResultText(voltage);
            }
            else if (other.gameObject == acVoltageTrigger)
            {
                deviceView.UpdateAcVoltageText(deviceModel.VariableVoltage);
                deviceView.UpdateMainResultText(deviceModel.VariableVoltage);
            }
            else if (other.gameObject == currentTrigger)
            {
                deviceView.UpdateCurrentText(current);
                deviceView.UpdateMainResultText(current);
            }
            else if (other.gameObject == resistanceTrigger)
            {
                deviceView.UpdateResistanceText(deviceModel.Resistance);
                deviceView.UpdateMainResultText(deviceModel.Resistance);
            }
            else
            {
                Debug.Log("Entered the trigger of an unknown object.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        deviceView.ResetTexts();
    }
}
