﻿using UnityEngine;
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
            Ray r = new Ray();
            r.origin = c.Pos;
            r.direction = c.Dir;
            FindNearestCD(r, cds, (c as LightPlus).Length, out final);

            if (final.collider != null)
            {
                (c as LightPlus).EndAt(final.point);
            }
        }
    }
}