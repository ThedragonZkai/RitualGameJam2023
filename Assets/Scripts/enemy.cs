using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class enemy : MonoBehaviour
{
    private float currentHealth;
    private float startingHealth = 100f;
	public Slider bossBar;

	// Start is called before the first frame update
	void Start()
    {
		bossBar = GameObject.Find("BossBar").GetComponent<Slider>();
		currentHealth = startingHealth;
		if (bossBar != null) {
			bossBar.maxValue = startingHealth;
		}
		bossBar.gameObject.SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

		if (bossBar != null) {
			bossBar.value = currentHealth;
		}
    }

}
