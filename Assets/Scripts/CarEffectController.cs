//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CarEffectController : MonoBehaviour
//{
//    public bool isTurning;
//    public bool tireMarksFlag;

//    public TrailRenderer[] tireMarks;
//    //public ParticleSystem[] smoke;
    

//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void FixedUpdate()
//    {
//        CheckIfTurning();

        
//    }
//    void CheckIfTurning()
//    {
//        if(isTurning)
//        {
//            StartEmitter();
//        } else
//        {
//            StopEmitter();
//        }

//    }
//    void StartEmitter()
//    {
//        if (tireMarksFlag) return;

//        foreach(TrailRenderer tr in  tireMarks)
//        {
//            tr.emitting = true;
//        }
//        tireMarksFlag = true;


//    }
//    void StopEmitter()
//    {
//        if (!tireMarksFlag) return;

//        foreach (TrailRenderer tr in tireMarks)
//        {
//            tr.emitting = false;
//        }
//        tireMarksFlag = false;
//    }
//}
