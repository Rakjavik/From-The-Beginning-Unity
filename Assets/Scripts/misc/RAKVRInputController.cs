using UnityEngine;

public class RAKVRInputController : MonoBehaviour {

    public float speed;

    private SteamVR_TrackedObject device;

	// Use this for initialization
	void Start () {
        device = GetComponent<SteamVR_TrackedObject>();
        
	}

    // Update is called once per frame
    void Update()
    {
        

    }
}
