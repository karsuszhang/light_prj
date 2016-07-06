using UnityEngine;
using System.Collections;

public class Receiver : BaseCDObj {

    [SerializeField]
    public int DestCount = 1;

    [SerializeField]
    public float DestIntensity = 3;

    private Color m_Color = Color.white;
    private int m_CurCount = 0;
    private float m_BaseIntensity;
    public Receiver() : base(ObjectType.Receiver)
    {
        
    }
	
    protected override void _Start()
    {
        base._Start();
        m_BaseIntensity = gameObject.GetComponentInChildren<Light>().intensity;
    }
	// Update is called once per frame
	void Update () {
	
	}

    public override void CheckCD(BaseCDObj c)
    {
        if (c.Type == ObjectType.LightPlus)
        {
            Collider[] cds = gameObject.GetComponentsInChildren<Collider>();
            RaycastHit final = new RaycastHit();
            Ray r = new Ray();
            r.origin = c.Pos;
            r.direction = c.Dir;
            FindNearestCD(r, cds, (c as LightPlus).Length, out final);

            if (final.collider != null)
            {
                (c as LightPlus).EndAt(final.point, this);
                m_CurCount++;
                if (m_CurCount > DestCount)
                {
                    m_CurCount = DestCount;
                    Game.Instance.LevelComplete();
                }

                float ratio = (float)m_CurCount / DestCount;

                Color org_color = (c as LightPlus).LightColor;
                Color co = /*GameHelper.LerpColorByMainColor(m_Color, org_color, ratio)*/HSBColor.Lerp(m_Color, org_color, ratio);
                gameObject.GetComponentInChildren<Light>().color = org_color;
                gameObject.GetComponentInChildren<Light>().intensity = m_BaseIntensity + ratio * (DestIntensity - m_BaseIntensity);
                gameObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", co);
                //if (m_CurCount >= DestCount)
                //    Release();
            }
        }
    }
}
