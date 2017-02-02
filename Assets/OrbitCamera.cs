using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrbitCamera : MonoBehaviour {

    public float offSet = 2;
	
	// Update is called once per frame
	void Update () {
        this.transform.position = (Camera.main.transform.forward * offSet) + Camera.main.transform.position;
    }
}
