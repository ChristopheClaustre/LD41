/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class EnnemySpawn :
    MonoBehaviour
    , ONETurnBased.ITurnBasedThing
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/
    [System.Serializable]
    public class EnemyPositionSpawn
    {
        public Vector2 m_Pos;
        public Enemy m_Enemy;
    }

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
    [SerializeField]
    List<EnemyPositionSpawn> m_SpawnList;
    [SerializeField]
    int m_SpawnTick;    // Turn between two activations
    [SerializeField]
    float m_TriggerRange;

    /********  PROTECTED        ************************/

    bool m_Activated;
    int m_CoolDownTick;    // Turn left before new activation 
    //int m_SpawnListIndex;

    Queue<Enemy> m_SpawnQueue;
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
        if (isSpawnActivated() && m_CoolDownTick <= 0)
        {
            //Creat enemy
            if(m_SpawnList.Count > 0)
            {
                Enemy newEnemy = Instantiate(m_SpawnList[m_SpawnList.Count - 1].m_Enemy, this.transform);
                //Place it 
                int xOffset = Mathf.FloorToInt(ONEMap.Instance.WorldToMapUnit * m_SpawnList[m_SpawnList.Count-1].m_Pos.x);
                int yOffset = Mathf.FloorToInt(ONEMap.Instance.WorldToMapUnit * m_SpawnList[m_SpawnList.Count-1].m_Pos.y);
                newEnemy.transform.localPosition = new Vector2(newEnemy.transform.localPosition.x + xOffset, newEnemy.transform.localPosition.y + yOffset);
                // Delete ennemy on list (and position)
                m_SpawnList.RemoveAt(m_SpawnList.Count - 1);
            }

            m_CoolDownTick = m_SpawnTick + 1;
        }
        if (m_CoolDownTick > 0)
        {
            m_CoolDownTick--;
        }
    }

    /********  PROTECTED        ************************/

    protected bool isSpawnActivated()
    {
        Transform playerTransform = ONEPlayer.Instance.transform;
        if(playerTransform && !m_Activated)
        {
            if (Mathf.Abs(this.transform.position.x - playerTransform.position.x) < m_TriggerRange)
            {
                m_Activated = true;
            }
        }
        return m_Activated;
    }

    /********  PRIVATE          ************************/

    #endregion
}
