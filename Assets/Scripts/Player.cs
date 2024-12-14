using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float sens;
    public float jumpForce;
    public int hp;

    PlayerInputs inputs;
    Rigidbody rb;

    bool onGround;

    private void Awake()
    {
        inputs = new PlayerInputs();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector2 move = inputs.Default.Move.ReadValue<Vector2>();
        rb.velocity = transform.right * move.x * speed + transform.forward * move.y * speed + transform.up * rb.velocity.y;

        float x = inputs.Default.Look.ReadValue<Vector2>().x;
        transform.Rotate(Vector3.up * x * sens * Time.deltaTime);
        
        if (onGround)
        {
            float jump = inputs.Default.Jump.ReadValue<float>();
            if (jump > 0)
            {
                rb.AddForce(Vector3.up * jumpForce);
                onGround = false;
            }
        }

        if (hp <= 0)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") { onGround = true; }
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
