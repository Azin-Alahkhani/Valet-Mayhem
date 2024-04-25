using System;
using UnityEngine;

public class CarObject 
{
    public int playerIndex;
    public int carIndex;
    public Color carColor;   
    public string carName;   
    public GameObject carObject;
    public bool isParked=false;
    public float parkingScore;
    public int life = 100;
    
  
  
    public CarObject(int index, Color color, string name,GameObject carObj)
    {
        carIndex = index;
        carColor = color;
        carName = name;
        carObject = carObj; 
      
         }
        public void SetIsParked(bool isparked)
        {
            isParked=isparked;
        }
        public void SetParkingScore(float score)
        {
            parkingScore = score;
        }
   

    
}