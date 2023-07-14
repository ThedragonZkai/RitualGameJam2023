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
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (stage == 1) {
			taskText.text = "Investigate the light";
		}
        if (stage == 2) {
			taskText.text = "Bring candles to pentagon (" + candlesCollected + "/" + candlesToCollect + ")";
		}
		else if (stage == 3) {
			taskText.text = "Kill Monday";
			gun.SetActive(true);
		}
    }
}
