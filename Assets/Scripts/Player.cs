﻿/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Player :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public ONEGeneral.Direction Direction
    {
        get
        {
            return m_direction;
        }
    }

    /********  PROTECTED        ************************/

    #endregion
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private ONEGeneral.Direction m_direction = ONEGeneral.Direction.eEE;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    // eNW, eN, eNE,
    // eW,      eE,
    // eSW, eS, eSE
    public void Move(ONEGeneral.Direction p_movement)
    {
        m_direction = p_movement;

        Vector2 deplacement = ONEGeneral.DirectionToVec2(p_movement) * ONEMap.Instance.WorldToMapUnit;
        Vector2 destination = new Vector2(transform.localPosition.x + deplacement.x, transform.localPosition.y + deplacement.y);

        List<GameObject> gos = ONEMap.Instance.getObjectAt(Mathf.RoundToInt(destination.y), Mathf.RoundToInt(destination.x));
        if (gos != null)
        {
            if (gos.Count > 0)
            {
                bool blocked = false;

                foreach(GameObject go in gos)
                {
                    if (go != null) {
                        Obstacle obstacle = go.GetComponent<Obstacle>();
                        Enemy enemy = go.GetComponent<Enemy>();
                        //Weapon weapon = go.GetComponent<Weapon>();

                        // Obstacle
                        if (obstacle != null) blocked = true;

                        // Enemy
                        else if (enemy != null)
                        {
                            enemy.Hit(1);
                            blocked = true;
                        }

                        // TODO: weapon
                        //else if (weapon != null)
                        //{
                        //    TakeWeapon(weapon);
                        //}
                    }
                }

                // move if not blocked
                if (! blocked) transform.localPosition = destination;
            }
            else
            {
                transform.localPosition = destination;
            }
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
