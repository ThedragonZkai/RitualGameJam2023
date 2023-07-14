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
    public Vector3 recoil;
    public float maxRecoil;
    public float recoilSpeed;
    // AudioSource m_shootingSound;

    private float lastShotTime = -Mathf.Infinity;
    void Start()
    {
        //m_shootingSound = GetComponent<AudioSource>();
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
            if (enemy != null)
            {
                enemy.TakeDamage(10);
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

    }
}   