using UnityEngine;
using System.Collections;

public class KeepFace : MonoBehaviour {

    int maxIterations = 30;
	// Update is called once per frame
	void Update () {
        float dot = Vector3.Dot(Camera.main.transform.position - this.transform.position, this.transform.forward);
        if (dot > -.35f)
        {
            int count = 0;
            while (dot > -.35)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, transform.position - Camera.main.transform.position, .01f, 0.0F));
                if (count++ < maxIterations)
                    break;
            }
        }
    }
}
