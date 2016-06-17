using UnityEngine;
using System.Collections;

public class Receiver : BaseCDObj {

    [SerializeField]
    public int DestCount = 1;


    private int m_CurCount = 0;
    public Receiver() : base(ObjectType.Receiver)
    {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void CheckCD(BaseCDObj c)
    {
        if (c.Type == ObjectType.LightPlus)
        {
            Collider[] cds = gameObject.GetComponentsInChildren<Collider>();
            RaycastHit final = new RaycastHit();;
            Ray r = new Ray();
            r.origin = c.Pos;
            r.direction = c.Dir;
            FindNearestCD(r, cds, (c as LightPlus).Length, out final);

            if (final.collider != null)
            {
                (c as LightPlus).Release();
                m_CurCount++;
                if (m_CurCount >= DestCount)
                    Release();
            }
        }
    }
}
