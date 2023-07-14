using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class bossEnemy : MonoBehaviour
{
    private float currentHealth;
    private float startingHealth = 100f;
    public float moveSpeed = 100f;
    public float attackDistance = 4f;
    private Rigidbody rb;
	public Slider bossBar;
    public GameObject player;
    public float attackSpeed = 10f;
    public GameObject ratAttackPrefab;
    public int ratAttackDamage = 10;
    public float attackDelay = 5f;

    private bool canShoot = true;
	// Start is called before the first frame update
	void Start()
    {
        currentHealth = startingHealth;
		if (bossBar != null) {
			bossBar.maxValue = startingHealth;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= attackDistance){
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else{
            Shoot();
        }
        

        
        if(player != null){


            Debug.Log("among");
        }

    }
    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
			SendMessageUpwards("BossDead");
		}

		if (bossBar != null) {
			bossBar.value = currentHealth;
		}
    }
private void Shoot()
{
    if (canShoot)
    {
        GameObject ratAttack = Instantiate(ratAttackPrefab, transform.position, Quaternion.identity);
        ratAttack.transform.position += Vector3.up * 3;
        Rigidbody ratAttackRigidbody = ratAttack.GetComponent<Rigidbody>();

        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction += Vector3.down * 1f;
        ratAttackRigidbody.AddForce(direction * attackSpeed, ForceMode.VelocityChange);
        Physics.IgnoreCollision(ratAttack.GetComponent<Collider>(), GetComponent<Collider>());

        canShoot = false;
        Invoke("ResetShoot", attackDelay);
    }
}

    private void ResetShoot()
    {
        canShoot = true;
    }

}
