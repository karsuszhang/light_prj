using UnityEngine;
using System.Collections;

public class LightPlus : BaseCDObj {

	public float Speed = 0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Speed > 0.1f)
			gameObject.transform.position += Time.deltaTime * Speed * gameObject.transform.forward;
	}
}
