using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SetNewLocation : MonoBehaviour {

    public Transform posIndicator;
    public Transform crowd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            SetTargetLocation();
        }




    }

    private void SetTargetLocation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            posIndicator.position = hit.point;

        foreach (Transform child in crowd)
            child.GetComponent<AICharacterControl>().SetTarget(posIndicator);
    }
}
