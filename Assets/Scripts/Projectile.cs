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
    int m_RemainingLifetime;

    /********  PROTECTED        ************************/
    [SerializeField]
    ONEGeneral.Direction mDirection;
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

    public void PlayMyTurn()
    {
        m_ProjectileCoordinates = ONEMap.Instance.getMapCoordinates(this.transform);
        //Analyse next cell
        
        //TODO : Compute real next values
        int nextX = (int) m_ProjectileCoordinates.x; //TODO Not the right value  
        int nextY = (int) m_ProjectileCoordinates.y;//TODO Not the right value  
        if (ONEMap.Instance.isOnMapCoordinates(nextX, nextY))
        {
            List<GameObject> nextCellObjectList = ONEMap.Instance.getObjectAt(nextX, nextY);
            foreach (GameObject nextCellObject in nextCellObjectList)
            {
                Debug.Log("TODO");
                if (nextCellObject.GetType() == typeof(Ennemy))
                {
                    //Do some stuff
                }
                else if (nextCellObject.GetType() == typeof(Player))
                {
                    //Do other stuff
                }
                else
                {
                    //F**k, do what you want
                }
            }
        }
        else
        {
            Destroy(this);
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
