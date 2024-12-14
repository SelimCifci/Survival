using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float sens;

    PlayerInputs inputs;

    private void Awake()
    {
        inputs = new PlayerInputs();
    }

    private void Update()
    {
        float y = inputs.Default.Look.ReadValue<Vector2>().y;
        transform.Rotate(Vector3.right * y * -sens * Time.deltaTime);
        // Todo: Clamp Rotation // transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, -90f, 90f), 0, 0);
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}
