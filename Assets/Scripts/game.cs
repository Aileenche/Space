using UnityEngine;
using System.Collections;

public class game : MonoBehaviour {
	// Use this for initialization

	void Start () {
        gameObject.AddComponent("Dataharvester");
        gameObject.AddComponent("MainMenu");
        gameObject.AddComponent("ServerConnector");
        gameObject.AddComponent("ErrorPopup");
    }
	
	// Update is called once per frame
	void Update () {
	}
}
