using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MyWinzone : MonoBehaviour {

    public GameObject winPanel;

	// Use this for initialization
	void Start () {
        winPanel = GameObject.Find("WinPanel");
        winPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winPanel.SetActive(true);

            if (other.GetComponent<RigidbodyFirstPersonController>().enabled)
                other.GetComponent<RigidbodyFirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
