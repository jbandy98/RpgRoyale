using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatCell : MonoBehaviour
{
    public int locX;
    public int locY;
    public Image sprite;
    public CombatUnit combatUnit;    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (combatUnit != null || sprite != null)
        {
            if (combatUnit.isHero)
            {
                sprite.sprite = (Sprite)Resources.Load("knight.gif");
            } else
            {
                sprite.sprite = (Sprite)Resources.Load("goblin.gif");
            }
        } else
        {
            sprite.sprite = (Sprite)Resources.Load("none.png");
        }
    }
}
