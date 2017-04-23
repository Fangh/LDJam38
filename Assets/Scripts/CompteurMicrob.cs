using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompteurMicrob : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		GetComponent<Text> ().text = ("Microbes : " + MicrobCount.Instance.NBMicrob);
	}
}
