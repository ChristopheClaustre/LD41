/***************************************************/
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
    public void Move(ONEGeneral.Direction pMovement)
    {
        switch (pMovement)
        {
            case ONEGeneral.Direction.eNW:
                transform.Translate(new Vector2(-1, 1));
                break;
            case ONEGeneral.Direction.eNN:
                transform.Translate(new Vector2(0, 1));
                break;
            case ONEGeneral.Direction.eNE:
                transform.Translate(new Vector2(1, 1));
                break;
            case ONEGeneral.Direction.eWW:
                transform.Translate(new Vector2(-1, 0));
                break;
            case ONEGeneral.Direction.eEE:
                transform.Translate(new Vector2(1, 0));
                break;
            case ONEGeneral.Direction.eSW:
                transform.Translate(new Vector2(-1, -1));
                break;
            case ONEGeneral.Direction.eSS:
                transform.Translate(new Vector2(0, -1));
                break;
            case ONEGeneral.Direction.eSE:
                transform.Translate(new Vector2(1, -1));
                break;
            default:
                break;
        }

        m_direction = pMovement;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
