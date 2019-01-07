using UnityEngine;
using System.Collections;

public class Pendule : GestesManager
{

    private enum front { left, right, nothing }
    private front _front;
    private Vector3 initPos0, initPos1;
    static private Activated thisOne = Activated.Pendule;

    // Use this for initialization
    void Start()
    {
        _front = front.nothing;
        initPos0 = Bones[0].transform.position;
        initPos1 = Bones[1].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((_startAction + ActionTime < Time.time && _startAction > 0) || Mathf.Abs(initPos0.z - Bones[0].transform.position.z) > Threshold || Mathf.Abs(initPos1.z - Bones[1].transform.position.z) > Threshold)
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
            SceneScheduler.Instance.LoadScene();
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
            if (Bones[0].transform.position.x > BonesReference.transform.position.x + StartAction)
            {
                _front = front.left;
                _actualState = State.Start;
                initPos0 = Bones[0].transform.position;
                initPos1 = Bones[1].transform.position;
            }
            else if (Bones[1].transform.position.x < BonesReference.transform.position.x + StartAction)
            {
                _front = front.right;
                _actualState = State.Start;
                initPos0 = Bones[0].transform.position;
                initPos1 = Bones[1].transform.position;
            }
        }
        else if (_actualState == State.Start && GestesManager.activeMvt == Activated.Nothing)
        {
            switch (_front)
            {
                case front.left:
                    if (Bones[1].transform.position.x < BonesReference.transform.position.x + StartAction)
                    {
                        Text.text = "";
                        _actualState = State.ReachedX;
                        GestesManager.activeMvt = thisOne;

                    }
                    break;
                case front.right:
                    if (Bones[0].transform.position.x > BonesReference.transform.position.x + StartAction)
                    {
                        Text.text = "";
                        _actualState = State.ReachedX;
                        GestesManager.activeMvt = thisOne;
                    }
                    break;
            }
        }
        else if (_actualState == State.ReachedX)
        {
            switch (_front)
            {
                case front.left:
                    if (Bones[0].transform.position.x > BonesReference.transform.position.x + StartAction)
                    {
                        Text.text = "";
                        _actualState = State.Finish;
                    }
                    break;
                case front.right:
                    if (Bones[1].transform.position.x < BonesReference.transform.position.x + StartAction)
                    {
                        Text.text = "";
                        _actualState = State.Finish;
                    }
                    break;
            }
        }
        else if (_actualState == State.Finish)
        {
            _actualState = State.Nothing;
            _startAction = -1;
            Text.text = Geste;
            _front = front.nothing;
            GestesManager.finishedMvt = thisOne;
        }
    }
}
