using System.Collections;
using UnityEngine;


public class shooting : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10;
    public float bulletDelay = 3f;
    //public float triggerPressed;
    public float raycastRange = 100f;
    public LayerMask enemyLayerMask;
    public LineRenderer Line_Jolie;
    public RaycastHit hit;
    public ParticleSystem muzzleFlash;
    // AudioSource m_shootingSound;


    public float recoilForce = 1f;
    public float recoilDuration = 0.2f;
    public float recoverySpeed = 1f;

    private Vector3 originalPosition;

    private float lastShotTime = -Mathf.Infinity;
    void Start()
    {
        //m_shootingSound = GetComponent<AudioSource>();

        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        //triggerPressed = gunShooting.action.ReadValue<float>();v
        //if (Input.GetMouseButtonDown(0)//&& //Time.time >= lastShotTime + bulletDelay)
        if (Input.GetButton("Fire1") && Time.time >= lastShotTime + bulletDelay)
        {
            Debug.Log("Fakka");
            Shoot();
            lastShotTime = Time.time;

        }
        Line_Jolie.SetPosition(0, bulletSpawnPoint.position);
        Line_Jolie.SetPosition(1, hit.point);
    }
    private void Shoot(){
        


        muzzleFlash.Play();
        //m_shootingSound.Play();
        if (Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out hit, raycastRange, enemyLayerMask))
        {
            Debug.Log("yo jolie"); 
            enemy enemy = hit.collider.GetComponent<enemy>();
            bossEnemy enemy2 = hit.collider.GetComponent<bossEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }
            if (enemy2 != null)
            {
                enemy2.TakeDamage(10);
            }

        //    // if (enemy != null)
        //    // {
        //         enemy.TakeDamage(50);
        //    // }
        //     if (enemy2 != null)
        //     {
        //         enemy2.TakeDamage(50);
        //     }
          lastShotTime = Time.time;
        }




        Fire();
    }




  

    public void Fire()
    {
        StartCoroutine(Recoil());
    }

    private IEnumerator Recoil()
    {
        float elapsedTime = 0f;

        while (elapsedTime < recoilDuration)
        {
            transform.localPosition -= Vector3.forward * recoilForce * Time.deltaTime;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        while (transform.localPosition != originalPosition)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, recoverySpeed * Time.deltaTime);
            yield return null;
        }
    }
}
  