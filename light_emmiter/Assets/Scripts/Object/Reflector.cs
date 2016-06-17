using UnityEngine;
using System.Collections;

public class Reflector : BaseCDObj 
{
    [SerializeField]
    public bool DoubleSideReflect = false;

    public Reflector() : base(ObjectType.Reflector)
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
                if (!DoubleSideReflect)
                {
                    if (Vector3.Dot(final.normal, this.Dir) < 0)
                    {
                        (c as LightPlus).EndAt(final.point);
                        return;
                    }
                   
                }
                LightPlus old = (c as LightPlus);
                old.EndAt(final.point);
                Vector3 org_dir = old.Dir;
                float angle = Mathf.Acos(Vector3.Dot(final.normal, org_dir));
                //CommonUtil.CommonLogger.Log("org cos " + angle + " deg " + Mathf.Rad2Deg * angle);
                float d = Vector3.Cross(org_dir, final.normal).y;
                //CommonUtil.CommonLogger.Log("cross y " + d);

                LightPlus rl = LightPlus.GenLightPlus();
                rl.StartAt(final.point, old.Length);
                rl.Dir = final.normal;
                rl.Speed = old.Speed;
                rl.SetColor(old.LightColor);

                rl.gameObject.transform.RotateAround(rl.Pos, Vector3.up, (180f - Mathf.Rad2Deg * angle) * (d < 0 ? 1f : -1f));
            }
        }
    }
}
