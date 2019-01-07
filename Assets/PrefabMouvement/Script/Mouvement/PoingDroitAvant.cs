using UnityEngine;
using System.Collections;

public class PoingDroitAvant : GestesManager
{

    public float _heightGap;
    public float _sideGap;
	private Vector3 initPos;
    static private Activated thisOne = Activated.Poing;

    // Use this for initialization
    void Start () {
		initPos = Bones [0].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if ((_startAction + ActionTime < Time.time && _startAction > 0) || Mathf.Abs (initPos.x - Bones[0].transform.position.x) > Threshold || Mathf.Abs (initPos.y - Bones[0].transform.position.y) > Threshold)
        {
            _startAction = -1;
            _actualState = State.Nothing;
            if (GestesManager.activeMvt == thisOne)
            {
                GestesManager.activeMvt = Activated.Nothing;
            }
        }
        else if (_startAction > 0) { }

        if (GestesManager.activeMvt != Activated.Nothing && GestesManager.activeMvt != thisOne)
        {
            _startAction = -1;
            _actualState = State.Nothing;
        }
        if (GestesManager.finishedMvt == thisOne)
        {
            GestesManager.activeMvt = Activated.Nothing;
            GestesManager.finishedMvt = Activated.Nothing;
        }

        UpdateState();

        if (_actualState == State.Start && _startAction < 0) { _startAction = Time.time; }
    }

    protected override void UpdateState()
    {
        if (_actualState == State.Nothing && GestesManager.finishedMvt == Activated.Nothing)
        {
            if (Bones[0].transform.position.z < BonesReference.transform.position.z + StartAction && Mathf.Abs(Bones[0].transform.position.y - Bones[1].transform.position.y) < _heightGap && Mathf.Abs(Bones[0].transform.position.x - Bones[1].transform.position.x) < _sideGap)
            {
                _actualState = State.Start;
                initPos = Bones[0].transform.position;
            }
        }
        else if (_actualState == State.Start && GestesManager.activeMvt == Activated.Nothing)
        {
            if (Bones[1].transform.position.z > BonesReference.transform.position.z + MinDistance)
            {
                Text.text = "";
                _actualState = State.Finish;
                GestesManager.activeMvt = thisOne;
            }
        }
        else if (_actualState == State.Finish)
        {
            _actualState = State.Nothing;
            _startAction = -1;
            Text.text = Geste;
            GestesManager.finishedMvt = thisOne;
        }
    }
}
