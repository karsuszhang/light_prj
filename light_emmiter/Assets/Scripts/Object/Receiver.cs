using UnityEngine;
using System.Collections;

public class Receiver : BaseCDObj {
    [SerializeField]
    public float DestIntensity = 3;

    [SerializeField]
    public Color ReceiveColor = Color.white;

    [SerializeField]
    public float AbsorbRate = 0.2f;

    [SerializeField]
    public float DropRate = 0.02f;

    private float m_BaseIntensity;
    private float m_CurIntensity;
    private MeshRenderer m_Render = null;
    private Light m_Light = null;
    public Receiver() : base(ObjectType.Receiver)
    {
        
    }
	
    protected override void _Start()
    {
        base._Start();
        m_BaseIntensity = gameObject.GetComponentInChildren<Light>().intensity;
        m_CurIntensity = m_BaseIntensity;
        gameObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", ReceiveColor);
        gameObject.GetComponentInChildren<Light>().color = ReceiveColor;
        m_Render = gameObject.GetComponentInChildren<MeshRenderer>();
        m_Light = gameObject.GetComponentInChildren<Light>();
    }
	// Update is called once per frame
	void Update () {
        if (m_CurIntensity > m_BaseIntensity)
        {
            m_CurIntensity = Mathf.Max(m_BaseIntensity, m_CurIntensity - DropRate);
            m_Render.material.SetFloat("_Threshold", (m_CurIntensity - m_BaseIntensity) / DestIntensity);
            m_Light.intensity = m_CurIntensity;
        }
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
                if (lp.LightColor == ReceiveColor)
                {
                    m_CurIntensity += lp.LightIntensity * AbsorbRate;

                    float ratio = Mathf.Min(1f, (m_CurIntensity - m_BaseIntensity) / DestIntensity);

                    gameObject.GetComponentInChildren<Light>().intensity = m_CurIntensity;
                    gameObject.GetComponentInChildren<MeshRenderer>().material.SetFloat("_Threshold", ratio);
                }
                    
            }
        }
    }
}
