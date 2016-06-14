using UnityEngine;
using System.Collections;

public class BaseCDObj : MonoBehaviour {

    public Vector3 Dir
    {
        get
        {
            return this.gameObject.transform.forward;
        }
        set
        {
            this.gameObject.transform.forward = value;
        }
    }

    public Vector3 Pos
    {
        get
        {
            return this.gameObject.transform.position;
        }
        set
        {
            this.gameObject.transform.position = value;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
