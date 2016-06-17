using UnityEngine;
using System.Collections;

public class Emmiter : BaseCDObj {

    public Color LightColor = Color.white;

    public Emmiter()
        : base(ObjectType.Emmiter)
    {
    }

	private int m_PressedFingerID = 0;

	private float m_TimePress = 0f;

	private bool m_IsPressing = false;
	
	// Update is called once per frame
	void Update () {
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
        LightPlus lo = LightPlus.GenLightPlus();
        lo.Dir = this.Dir;
        lo.Pos = this.Pos;
        lo.Speed = 5f;
        lo.SetColor(LightColor);
		m_IsPressing = false;
	}
}
