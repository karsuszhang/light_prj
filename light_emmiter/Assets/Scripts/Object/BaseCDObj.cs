using UnityEngine;
using System.Collections;

public enum ObjectType
{
    None,
    LightPlus,
    Emmiter,
    Block,
    Reflector,
}

public class BaseCDObj : MonoBehaviour {

    protected BaseCDObj(ObjectType t)
    {
        Type = t;
        Released = false;
    }

    public ObjectType Type{ get; private set; }
    public bool Released { get; protected set; }
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
        _Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        Game.Instance.UnRegObject(this);
    }

    protected virtual void _Start()
    {
        Game.Instance.RegCDObj(this);
    }

    public virtual void CheckCD(BaseCDObj c)
    {

    }

    public virtual void Release()
    {
        Released = true;
        CommonUtil.ResourceMng.Instance.Release(this.gameObject);
    }
}
