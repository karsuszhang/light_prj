using UnityEngine;
using System.Collections;

public class LightPlus : BaseCDObj {

    enum RunningState
    {
        Flying,
        Ending,
        Starting,
    }

    public LightPlus() : base(ObjectType.LightPlus)
    {
    }

	public float Speed = 0f;

    public float Length
    {
        get
        {
            return this.gameObject.transform.localScale.z;
        }
        set
        {
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, value);
        }
    }

    private RunningState m_CurState = RunningState.Flying;
    private Vector3 m_EndPos;
    private Vector3 m_StartPos;
    private float m_DestLength;

    public static LightPlus GenLightPlus()
    {
        GameObject lpo = CommonUtil.ResourceMng.Instance.GetResource("Object/LightPlus", CommonUtil.ResourceType.Model) as GameObject;
        LightPlus lo = lpo.GetComponent<LightPlus>();
        return lo;
    }

	// Update is called once per frame
	void Update () {
        if (m_CurState != RunningState.Starting)
        {
            if (Speed > 0.1f)
                gameObject.transform.position += Time.deltaTime * Speed * gameObject.transform.forward;
        }
        else
        {
            Length += (Time.deltaTime * Speed * gameObject.transform.forward).magnitude;
            if (Length >= m_DestLength)
            {
                Length = m_DestLength;
                m_CurState = RunningState.Flying;
            }
        }

        if (m_CurState == RunningState.Flying || m_CurState == RunningState.Starting)
        {
            Game.Instance.CheckCD(this);
            if (m_CurState == RunningState.Starting)
            {
                DoStarting();
            }
        }
        else if (m_CurState == RunningState.Ending)
        {
            DoEnding();
        }

        CheckBoard();
	}

    public void EndAt(Vector3 end_pos)
    {
        m_EndPos = end_pos;
        m_CurState = RunningState.Ending;
    }

    public void StartAt(Vector3 start_pos, float dest_len)
    {
        Pos = start_pos;
        m_StartPos = start_pos;
        m_CurState = RunningState.Starting;
        m_DestLength = dest_len;

        Length = 0f;
    }

    private void CheckBoard()
    {
        if (Mathf.Abs(Pos.x) > 10f || Mathf.Abs(Pos.z) > 40f)
            Release();
    }

    void DoEnding()
    {
        //CommonUtil.CommonLogger.Log(Vector3.Dot(Dir, (m_EndPos - Pos)).ToString());
        if (Vector3.Dot(Dir, (m_EndPos - Pos)) < 0)
        {
            Release();
            return;
        }

        this.Length = Mathf.Max(0f, (m_EndPos - Pos).magnitude);

        if (Length < Vector3.kEpsilon)
            Release();
    }

    void DoStarting()
    {
    }
}
