using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatCell : MonoBehaviour
{
    public int locX;
    public int locY;
    Image image;
    public CombatDataUnit dataUnit;
    public float updateTime = 0.5f;
    public float timeElapsed = 0.0f;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > updateTime)
        {
            timeElapsed = 0.0f;
            if (dataUnit != null && image != null)
            {
                Debug.Log("Data unit: " + dataUnit.ToString());
                Debug.Log("data is hero: " + dataUnit.isHero);
                if (dataUnit.isHero)
                {
                    image.sprite = Resources.Load<Sprite>("knight");
                }
                else
                {
                    image.sprite = Resources.Load<Sprite>("goblin");
                }
            }
            else
            {
                image.sprite = Resources.Load<Sprite>("none");
            }
        }
    }
}
