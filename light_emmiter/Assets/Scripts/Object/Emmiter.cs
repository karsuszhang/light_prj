using UnityEngine;
using System.Collections;

public class Emmiter : BaseCDObj {

    public Color LightColor = Color.white;

    [SerializeField]
    public float MaxIntensityTime = 3f;

    [SerializeField]
    public float MaxLightSpeed = 10f;

    [SerializeField]
    public float MinLightSpeed = 2f;

    [SerializeField]
    public float MaxLightIntensity = 2f;

    private float m_TimeCount = 0f;
    private Color m_BaseColor;
    public Emmiter()
        : base(ObjectType.Emmiter)
    {
        
    }

	private int m_PressedFingerID = 0;

	private bool m_IsPressing = false;
	
    protected override void _Start()
    {
        base._Start();
        m_BaseColor = this.gameObject.GetComponentInChildren<MeshRenderer>().material.GetColor("_EmissionColor");
    }
	// Update is called once per frame
	void Update () {
        if (m_IsPressing)
        {
            m_TimeCount += Time.deltaTime;
            if (m_TimeCount >= MaxIntensityTime)
                m_TimeCount = MaxIntensityTime;

            Color c = HSBColor.Lerp(m_BaseColor, LightColor, m_TimeCount / MaxIntensityTime);
            gameObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", c);
        }
		CheckKeyBoard ();
		CheckTouch ();
	}

	void CheckKeyBoard()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			m_IsPressing = true;
		}

		if (Input.GetKeyUp (KeyCode.Space))
			ReleaseLight ();
	}

	void CheckTouch()
	{
		foreach (Touch t in Input.touches) 
		{
            if (t.phase == TouchPhase.Began)
            {
                if (m_PressedFingerID == 0)
                {
                    m_PressedFingerID = t.fingerId;
                    m_IsPressing = true;
                    break;
                }
            }
            else if (t.phase == TouchPhase.Canceled || t.phase == TouchPhase.Ended)
            {
                if (m_PressedFingerID == t.fingerId)
                {
                    m_PressedFingerID = 0;
                    ReleaseLight();
                    break;
                }
            }
		}
	}

	void ReleaseLight()
	{
        float ratio = m_TimeCount / MaxIntensityTime;
        ReleaseLight(ratio);
	}

    public void ReleaseLight(float ratio)
    {
        Color c = HSBColor.Lerp(m_BaseColor, LightColor, ratio);

        LightPlus lo = LightPlus.GenLightPlus();
        lo.Dir = this.Dir;
        lo.Pos = this.Pos;
        lo.Speed = MinLightSpeed + ratio * (MaxLightSpeed - MinLightSpeed);
        lo.SetColor(c, ratio * MaxLightIntensity);
        m_IsPressing = false;

        m_TimeCount = 0f;
        gameObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", m_BaseColor);
    }

    public void SetColorLerp(float ratio)
    {
        Color c = HSBColor.Lerp(m_BaseColor, LightColor, ratio);
        gameObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", c);
    }
}
