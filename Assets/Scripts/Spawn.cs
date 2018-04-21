/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Spawn :
    MonoBehaviour
    , ONETurnBased.ITurnBasedThing
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
    Queue<Ennemy> m_SpawnQueue;
    int m_SpawnTick;    // Turn between two activations
    float m_TriggerRange;

    /********  PROTECTED        ************************/

    bool m_Activated;
    int m_CoolDownTick;    // Turn left before new activation 
    /********  PRIVATE          ************************/

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_SpawnQueue = new Queue<Ennemy>();
        m_Activated = false;
        m_CoolDownTick = 0;
    }

    // Update is called once per frame
    private void Update()
    {

    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void PlayMyTurn()
    {
        if (isSpawnActivated() && m_CoolDownTick == 0)
        {
            Instantiate(m_SpawnQueue.Dequeue(), this.transform);
            m_CoolDownTick = m_SpawnTick + 1;
        }
        m_CoolDownTick--;
    }

    /********  PROTECTED        ************************/

    protected bool isSpawnActivated()
    {
        Transform playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        if(playerTransform && !m_Activated)
        {
            if ((Mathf.Abs(this.transform.position.x - playerTransform.position.x) + Mathf.Abs(this.transform.position.z - playerTransform.position.z)) > m_TriggerRange)
            {
                m_Activated = true;
            }
        }
        return m_Activated;
    }

    /********  PRIVATE          ************************/

    #endregion
}
