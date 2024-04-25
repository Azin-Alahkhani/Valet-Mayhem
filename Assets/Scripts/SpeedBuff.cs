using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/SpeedBuff")]
public class SpeedBuff : PowerUp
{

    public float SpeedPlus = 10;

 

    public GameObject bubblePrefab;

    GameObject bubbleObj;
    public override void Apply(GameObject go)
    {
        if (go.GetComponent<NewCarMovement>())
        {
            
                go.transform.GetComponent<NewCarMovement>().speed += SpeedPlus;
                bubbleObj = Instantiate(bubblePrefab, go.transform);
                bubbleObj.GetComponent<MoveWithCar>().move = true;
                bubbleObj.GetComponent<MoveWithCar>().car = go;
            


        }

    }

   

    public override void Cancel(GameObject gameObject)
    {
        gameObject.GetComponent<NewCarMovement>().speed -= SpeedPlus;
        Destroy(bubbleObj);
    }
}
