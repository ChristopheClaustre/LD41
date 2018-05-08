/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ONEGeneral :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public enum Direction
    {
        eNW, eNN, eNE,
        eWW,      eEE,
        eSW, eSS, eSE
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public static float VelocityAnimation
    {
        get { return m_instance.m_velocityAnimimation; }
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

    [SerializeField] private float m_velocityAnimimation = 4;

    private static ONEGeneral m_instance = null;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        if (ONEPlayer.CurrentLifePoint <= 0)
        {
            ONEPlayer.Instance.Destroy();
            SceneManager.LoadScene("Defeat");
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public static Vector2 DirectionToVec2(Direction p_movement)
    {
        Vector2 destination = Vector2.zero;
        switch (p_movement)
        {
            case ONEGeneral.Direction.eNW:
                destination = new Vector2(-1, 1);
                break;
            case ONEGeneral.Direction.eNN:
                destination = new Vector2(0, 1);
                break;
            case ONEGeneral.Direction.eNE:
                destination = new Vector2(1, 1);
                break;
            case ONEGeneral.Direction.eWW:
                destination = new Vector2(-1, 0);
                break;
            case ONEGeneral.Direction.eEE:
                destination = new Vector2(1, 0);
                break;
            case ONEGeneral.Direction.eSW:
                destination = new Vector2(-1, -1);
                break;
            case ONEGeneral.Direction.eSS:
                destination = new Vector2(0, -1);
                break;
            case ONEGeneral.Direction.eSE:
                destination = new Vector2(1, -1);
                break;
            default:
                break;
        }

        return destination;
    }

    public static bool OppositeDirection(Direction p_direction, Direction p_direction2)
    {
        return DirectionToVec2(p_direction) + DirectionToVec2(p_direction2) == Vector2.zero;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
