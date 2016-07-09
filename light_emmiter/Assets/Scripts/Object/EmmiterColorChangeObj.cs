using UnityEngine;
using System.Collections;

public class EmmiterColorChangeObj : BaseCDObj {
    [SerializeField]
    public Color ChangeColor = Color.white;

    public EmmiterColorChangeObj() : base(ObjectType.EmmiterColorChanger)
    {
    }

    public override void CheckCD(BaseCDObj c)
    {
        if (c.Type == ObjectType.LightPlus)
        {
            RaycastHit final = FindCollideWithLightPlus(c as LightPlus);

            if (final.collider != null)
            {
                LightPlus lp = (c as LightPlus);
                lp.EndAt(final.point, this);
                this.Release();

                if (lp.LightEmmiter != null)
                {
                    lp.LightEmmiter.SetBaseColor(ChangeColor);
                }
            }
        }
    }
}
