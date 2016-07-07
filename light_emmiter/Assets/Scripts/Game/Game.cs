using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public static Game Instance = null;

    public List<BaseCDObj> AllObjs
    {
        get
        {
            return m_CDObjs;
        }
    }
    private List<BaseCDObj> m_CDObjs = new List<BaseCDObj>();

	// Use this for initialization
	void Awake () {
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

    public void UnRegObject(BaseCDObj o)
    {
        //CommonUtil.CommonLogger.Log("Object UnReg " + o.gameObject.name);
        m_CDObjs.Remove(o);
    }

    public void CheckCD(BaseCDObj o, List<BaseCDObj> uncolliders = null)
    {
        foreach (BaseCDObj obj in m_CDObjs)
        {
            if (uncolliders != null && uncolliders.Contains(obj))
                continue;
            
            if (!o.Released && o != obj)
            {
                obj.CheckCD(o);
            }
        }
    }

    public void LevelComplete()
    {
        GameObject button = CommonUtil.UIManager.Instance.AddUI("UI/BackButton");
        button.GetComponent<UIButton>().onClick.Add(new EventDelegate(this.Back2Main));
    }

    public void Back2Main()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
