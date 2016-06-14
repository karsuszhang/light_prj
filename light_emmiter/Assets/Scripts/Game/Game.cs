using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public static Game Instance = null;

    private List<BaseCDObj> m_CDObjs = new List<BaseCDObj>();

	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RegCDObj(BaseCDObj o)
    {
        if (m_CDObjs.Contains(o))
        {
            CommonUtil.CommonLogger.LogError("Obj Reg Twice: " + o.gameObject.name);
            return;
        }

        m_CDObjs.Add(o);
    }
}
