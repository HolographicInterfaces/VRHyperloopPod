using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrbitCamera : MonoBehaviour {

    public float offSet = 2;
	
	// Update is called once per frame
	void Update () {
        Vector3 target = (Camera.main.transform.forward * offSet) + Camera.main.transform.position;
        this.transform.position = new Vector3(this.transform.position.x, target.y, this.transform.position.z);
    }
}
