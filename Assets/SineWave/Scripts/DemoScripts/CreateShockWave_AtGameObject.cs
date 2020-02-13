using UnityEngine;
using System.Collections;

public class CreateShockWave_AtGameObject : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        InvokeRepeating("CreateShockWave",0.5f,3f);
	}


    void CreateShockWave()
    {
//        SineWave.Get().ReverseIt(gameObject,0.0000001f,5f, 0.125f);
    }
	
}
