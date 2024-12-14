using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Animator crosshairAnim;

    // Ammo, Firerate, Shottype
    public int maxAmmo;
    public float shootDelay;
    public float reloadTime;
    public int damage;

    PlayerInputs inputs;

    bool fire = true;
    int ammo;

    private void Awake()
    {
        ammo = maxAmmo;
        inputs = new PlayerInputs();
    }

    private void Update()
    {
        if (inputs.Default.Aim.ReadValue<float>() > 0) { GetComponent<Animator>().SetBool("aim", true); crosshairAnim.SetBool("aim", true); }
        else { GetComponent<Animator>().SetBool("aim", false); crosshairAnim.SetBool("aim", false); }

        if (inputs.Default.Reload.ReadValue<float>() > 0 && ammo > 0)
        {
            fire = false;
            ammo = 0;
        }

        if (ammo > 0)
        {
            if (fire)
            {
                if (inputs.Default.Fire.ReadValue<float>() > 0)
                {
                    ammo--;
                    fire = false;

                    int layerMask = 1 << 7;

                    layerMask = ~layerMask;

                    RaycastHit hit;
                    Transform camera = GetComponentInParent<Transform>();

                    if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, 250, layerMask))
                    {
                        if (hit.collider.gameObject.tag == "Enemy") { hit.collider.gameObject.GetComponent<Enemy>().hp -= damage; }
                    }

                    Instantiate(bulletPrefab, bulletSpawn);

                    StartCoroutine(ShotDelay());
                }
            }
            /*else
            {
                if (inputs.Default.Fire.ReadValue<float>() == 0) { fire = true; }
            }*/
        }
        else
        {
            if (!fire) { StartCoroutine(Reload()); }
        }
    }

    private IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        fire = true;
    }

    private IEnumerator Reload()
    {
        fire = true;
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
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
