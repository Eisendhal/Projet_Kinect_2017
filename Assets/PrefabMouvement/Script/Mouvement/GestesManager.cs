using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class GestesManager : MonoBehaviour {

    public Transform[] Bones;
    public Transform BonesReference;
    public float ActionTime;

    public float Threshold;   // variation suivant y
    public float StartAction;  // point a partir duquel on verifie le mouvement
    public float MinDistance;  //distance minimal a parcours pour valider le mouvement;

    public Text Text;

    [SerializeField] protected string Geste;
    [SerializeField] protected int BonesNumber;

    protected float _startAction;

    protected enum State { Nothing, Start, ReachedX, BackX, Finish }
    [SerializeField] protected State _actualState;

    protected enum Activated { Nothing, BalayageDD, BalayageDG, BalayageHB, Course, Poing, Pendule, Jambe}
    static protected Activated activeMvt = Activated.Nothing;

    static protected Activated finishedMvt = Activated.Nothing;


    // Use this for initialization
    void Start () {
        _startAction = -1;
        _actualState = State.Nothing;

        if (Bones.Length != BonesNumber) { Debug.LogError("Pas de \"Bones\" selectionné pour le script" + this.GetType().Name); }
    }
	
	// Update is called once per frame
	void Update () { 
    }

    protected abstract void UpdateState();
}
