

using NobleMuffins.LimbHacker.Guts;
using UnityEngine;

public class SliceTest : MonoBehaviour {


	// Use this for initialization
	void Start () {
        LimbHackerAgent.instance.SeverByJoint(gameObject, "LeftArm", 0,null);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
