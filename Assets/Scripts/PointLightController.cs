using UnityEngine;
using System.Collections;

public class PointLightController : MonoBehaviour
{
    public Light pointLight;

    void Start()
    {
        StartCoroutine(CheckForKeyPress());
    }

    IEnumerator CheckForKeyPress()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                pointLight.enabled = !pointLight.enabled;
            }
            yield return null;
        }
    }
}
