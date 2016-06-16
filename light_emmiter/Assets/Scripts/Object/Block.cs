using UnityEngine;
using System.Collections;

public class Block : BaseCDObj {

    public Block() : base(ObjectType.Block)
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
            float len = 999999f;
            foreach(Collider cd in cds)
            {
                Ray r = new Ray();
                r.origin = c.Pos;
                r.direction = c.Dir;
                RaycastHit info;
                if (cd.Raycast(r, out info, (c as LightPlus).Length))
                {
                    if (info.distance < len)
                        final = info;
                }
            }

            if (final.collider != null)
            {
                (c as LightPlus).EndAt(final.point);
            }
        }
    }
}
