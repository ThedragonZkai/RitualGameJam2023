using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

	public TMP_Text taskText;
	public int stage;
	public int candlesCollected;
	public int candlesToCollect;
	public GameObject gun;
	public float distanceToCheck;
	public GameObject[] candles;
	public GameObject player;
	public GameObject boss;
	// Start is called before the first frame update
	void Start()
    {
		gun.SetActive(false);
		candlesToCollect = candles.Length;
	}

    // Update is called once per frame
    void Update()
    {
		if (stage == 1) {
			taskText.text = "Investigate the light";
		}
        if (stage == 2) {
			taskText.text = "Bring candles to pentagram (" + candlesCollected + "/" + candlesToCollect + ")";
		}
		else if (stage == 3) {
			taskText.text = "Kill Monday";
			gun.SetActive(true);
		}




		if (stage == 1) {
			if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) < distanceToCheck) {
				stage = 2;
			}
		}
		if (stage == 2) {
			candlesCollected=0;
		for (int i = 0; i < candles.Length; i++)
		{
			if (Vector3.Distance(this.gameObject.transform.position, candles[i].transform.position) < distanceToCheck)
			{
				candlesCollected++;
			}
		}
		if (candlesCollected > candlesToCollect-1) {
				stage = 3;
				Instantiate(boss);
			}
		}
		if (stage == 3) {
			
		}

    }


}
