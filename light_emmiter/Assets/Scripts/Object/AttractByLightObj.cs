using UnityEngine;
using System.Collections;

public class AttractByLightObj : BaseCDObj {

    [SerializeField]
    public float AttractDistance = 4f;

    public AttractByLightObj() : base(ObjectType.AttractByLight)
    {
    }

    void LateUpdate()
    {
        Vector3 final_dir = Vector3.zero;
        BaseCDObj n = null;
        float min_dis = 99999f;
        foreach (BaseCDObj o in Game.Instance.AllObjs)
        {
            if (o.Type == ObjectType.LightPlus)
            {
                Vector3 dis = o.Pos - this.Pos;
                if (dis.magnitude <= AttractDistance)
                {
                    final_dir += (AttractDistance - dis.magnitude) * dis.normalized;
                    if (dis.magnitude < min_dis)
                    {
                        n = o;
                        min_dis = dis.magnitude;
                    }
                }
            }
        }

        if (n != null)
        {
            this.Pos += final_dir.normalized * (1f - final_dir.magnitude / AttractDistance) * Time.deltaTime * (n as LightPlus).Speed / 2f;
        }
    }
}
