using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[System.Serializable]
public class LevelImage
{
    public string levelName;
    public GameObject levelObj;
    public int sceneIndex;
    
}


public class LevelScroll : MonoBehaviour
{

    public ScrollRect scrollRect;
    public RectTransform container;
    public float scrollStep;
    public float scrollSpeed;
    public TextMeshProUGUI levelNameText;

    bool useAxis;
    bool scroll;

    public LevelImage[] levels;

    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
    }


    void UserInput()
    {

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (!useAxis)
            {

                index++;
                if (index >= levels.Length)
                {
                    index = 0;
                }
                useAxis = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (!useAxis)
            {
                if (index <= 0)
                {
                    index = levels.Length;
                }
                index--;
                useAxis = true;
            }
        }

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            useAxis = false;
        }
    }

    void Scroll()
    {
        SnapTo(levels[index].levelObj.GetComponent<RectTransform>());
    }


    // Update is called once per frame
    void Update()
    {
        UserInput();
        Scroll();
        UpdateText();
        

        //scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, scrollRect.transform.InverseTransformPoint(levels[index].transform.position).x, Time.deltaTime * scrollSpeed);
    }

    void UpdateText()
    {
        levelNameText.text = levels[index].levelName;
    }

    public void ScrollRight()
    {
        Debug.Log("right");
        index++;
        if (index >= levels.Length)
        {
            index = 0;
        }
    }

    public void ScrollLeft()
    {
        if (index <= 0)
        {
            index = levels.Length;
        }
        index--;
    }

    public void LoadLevel()
    {
        AudioManager.Instance.ChangeMusic(index);

        SceneManager.LoadScene(levels[index].sceneIndex);
    }
    void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        Vector2 pos = (Vector2)scrollRect.transform.InverseTransformPoint(container.position)
                - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

        container.localPosition = Vector2.Lerp(container.localPosition, pos, Time.deltaTime * scrollSpeed);

    }

}
