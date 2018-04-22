/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Projectile :
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
    int m_RemainingLifetime;

    /********  PROTECTED        ************************/
    [SerializeField]
    ONEGeneral.Direction m_Direction;
    [SerializeField]
    int m_LeftLifetime;    // Turn left before new activation 
    [SerializeField]
    Vector2 m_ProjectileCoordinates;
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
        
    }

    // Update is called once per frame
    private void Update()
    {

    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void Init(ONEGeneral.Direction p_Direction, int p_LeftLifetime)
    {
        m_Direction = p_Direction;
        m_LeftLifetime = p_LeftLifetime;
    }

    public void PlayMyTurn()
    {
        m_ProjectileCoordinates = ONEMap.Instance.getMapCoordinates(this.transform);

        //Time up, bye bye :(
        if(m_LeftLifetime <= 0)
        {
            Destroy(gameObject);
        }
 
        //Check if player is on current cell
        List<GameObject> currentCellObjectList = ONEMap.Instance.getObjectAt(Mathf.RoundToInt(m_ProjectileCoordinates.x), Mathf.RoundToInt(m_ProjectileCoordinates.y));
        foreach (GameObject currentCellObject in currentCellObjectList)
        {
            if (currentCellObject.GetComponent<ONEPlayer>())  // Player
            {
                //TODO Call player hit
                Destroy(gameObject);
            }
        }

        //Still here ? So analyse next cell
        Vector2 deplacement = ONEGeneral.DirectionToVec2(m_Direction);
        Vector2 nextProjectileCoordinates = new Vector2(m_ProjectileCoordinates.x + deplacement.y, m_ProjectileCoordinates.y + deplacement.x);
        
        int nextX = Mathf.RoundToInt(nextProjectileCoordinates.x);
        int nextY = Mathf.RoundToInt(nextProjectileCoordinates.y);
        if (ONEMap.Instance.isOnMapCoordinates(nextX, nextY))
        {
            List<GameObject> nextCellObjectList = ONEMap.Instance.getObjectAt(nextX, nextY);
            foreach (GameObject nextCellObject in nextCellObjectList)
            {
                if (nextCellObject.GetComponent<ONEPlayer>())  // Player
                {
                    ONEPlayer.Instance.Hit(1);
                    Destroy(gameObject);
                }
                //else if (nextCellObject.GetComponent<Obstacle>()) //
                //{
                //    Destroy(this);
                //}
            }
            transform.localPosition = new Vector2(transform.localPosition.x + (deplacement.x * ONEMap.Instance.WorldToMapUnit), transform.localPosition.y + (deplacement.y * ONEMap.Instance.WorldToMapUnit));
            m_LeftLifetime--;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
