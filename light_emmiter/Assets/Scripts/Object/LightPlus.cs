using UnityEngine;
using System.Collections;

public class LightPlus : BaseCDObj {

    enum RunningState
    {
        Flying,
        Ending,
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

	// Update is called once per frame
	void Update () {
		if (Speed > 0.1f)
			gameObject.transform.position += Time.deltaTime * Speed * gameObject.transform.forward;

        if (m_CurState == RunningState.Flying)
        {
            Game.Instance.CheckCD(this);
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
}
