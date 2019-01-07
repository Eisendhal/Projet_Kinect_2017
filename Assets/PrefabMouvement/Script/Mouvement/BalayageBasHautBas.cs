using UnityEngine;
using System.Collections;

public class BalayageBasHautBas : GestesManager {

    public float _handGap;
    public bool test = false;
	private Vector3 initPos0, initPos1;
    static private Activated thisOne = Activated.BalayageHB;

    // Use this for initialization
    void Start()
    {
        Geste = "Balayage des deux mains vers le haut";
		initPos0 = Bones [0].transform.position;
		initPos1 = Bones [1].transform.position;
    }

    // Update is called once per frame
    void Update() {
        if ((_startAction + ActionTime < Time.time && _startAction > 0) || Mathf.Abs (initPos0.x - Bones[0].transform.position.x) > Threshold || Mathf.Abs (initPos0.z - Bones[0].transform.position.z) > Threshold || Mathf.Abs(initPos1.x - Bones[1].transform.position.x) > Threshold || Mathf.Abs(initPos1.z - Bones[1].transform.position.z) > Threshold)
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
        if (Mathf.Abs(Bones[0].transform.position.y - Bones[1].transform.position.y) < _handGap) {
            test = true;
            if (_actualState == State.Nothing && GestesManager.finishedMvt == Activated.Nothing)
            {
                if (Bones[0].transform.position.y > BonesReference.transform.position.y + StartAction && Bones[0].transform.position.y < BonesReference.transform.position.y)
                {
                    _actualState = State.Start;
					initPos0 = Bones[0].transform.position;
					initPos1 = Bones[1].transform.position;
                }
            }
            else if (_actualState == State.Start && GestesManager.activeMvt == Activated.Nothing)
            {
                if (Bones[0].transform.position.y > BonesReference.transform.position.y + MinDistance)
                {
                    Text.text = "";
                    _actualState = State.ReachedX;
                    GestesManager.activeMvt = thisOne;
                }
            }
            else if (_actualState == State.ReachedX)
            {
                if (Bones[0].transform.position.y < BonesReference.transform.position.y + MinDistance)
                {
                    _actualState = State.BackX;
                }
            }
            else if (_actualState == State.BackX)
            {
                if (Bones[0].transform.position.y < BonesReference.transform.position.y + StartAction)
                {
                    _actualState = State.Finish;
                }
            }
            else if (_actualState == State.Finish)
            {
                _actualState = State.Nothing;
                _startAction = -1;
                Text.text = Geste;
                test = false;
                GestesManager.finishedMvt = thisOne;
            }
        }
    }
}

