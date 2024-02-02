using UnityEngine;

public class PointLightController : MonoBehaviour
{
    public Light pointLight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            pointLight.enabled = !pointLight.enabled;
        }
    }
}
