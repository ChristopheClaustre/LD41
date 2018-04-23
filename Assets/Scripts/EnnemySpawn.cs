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
        public GameObject m_Enemy;
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
    [SerializeField]
    bool m_Loop = false;

    /********  PROTECTED        ************************/

    bool m_Activated;
    int m_Index;
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
        m_Activated = false;
        m_CoolDownTick = 0;
        m_Index = m_SpawnList.Count-1;
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
            //Create enemy
            if(m_Index > 0 )
            {
                if(m_SpawnList[m_Index].m_Enemy != null)
                {
                    GameObject newEnemy = Instantiate(m_SpawnList[m_Index].m_Enemy, transform.parent);
                    //Place it
                    int xOffset = Mathf.FloorToInt(ONEMap.Instance.WorldToMapUnit * m_SpawnList[m_Index].m_Pos.x);
                    int yOffset = Mathf.FloorToInt(ONEMap.Instance.WorldToMapUnit * m_SpawnList[m_Index].m_Pos.y);
                    newEnemy.transform.localPosition = new Vector2(transform.localPosition.x + xOffset, transform.localPosition.y + yOffset);
                }
                // Delete ennemy on list (and position)
                m_Index--;
            }
            if(m_Index == 0)
            {
                if(m_Loop)
                {
                    m_Index = m_SpawnList.Count-1;
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            m_CoolDownTick = m_SpawnTick;
        }
        else
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
