/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ONETurnBased :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public interface ITurnBasedThing
    {
        void PlayMyTurn();
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

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    [SerializeField] private List<ITurnBasedThing> m_turnBasedThings = new List<ITurnBasedThing>();

    [SerializeField] private bool m_isAxisInUse = false;

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
        if (Input.GetAxis("NW____") == 0 && Input.GetAxis("__NN__") == 0 && Input.GetAxis("____NE") == 0 &&
            Input.GetAxis("WW____") == 0 && Input.GetAxis("_STAY_") == 0 && Input.GetAxis("____EE") == 0 &&
            Input.GetAxis("SW____") == 0 && Input.GetAxis("__SS__") == 0 && Input.GetAxis("____SE") == 0 &&
            Input.GetAxis("SLOT#1") == 0 && Input.GetAxis("SLOT#2") == 0 && Input.GetAxis("SLOT#3") == 0 && Input.GetAxis("USE") == 0)
        {
            m_isAxisInUse = false;
        }
        else if (!m_isAxisInUse)
        {
            m_isAxisInUse = true;

            Player script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            if (Input.GetAxis("NW____") != 0)
                script.Move(ONEGeneral.Direction.eNW);
            else if (Input.GetAxis("__NN__") != 0)
                script.Move(ONEGeneral.Direction.eNN);
            else if (Input.GetAxis("____NE") != 0)
                script.Move(ONEGeneral.Direction.eNE);
            else if (Input.GetAxis("WW____") != 0)
                script.Move(ONEGeneral.Direction.eWW);
            else if (Input.GetAxis("_STAY_") != 0) { }
            else if (Input.GetAxis("____EE") != 0)
                script.Move(ONEGeneral.Direction.eEE);
            else if (Input.GetAxis("SW____") != 0)
                script.Move(ONEGeneral.Direction.eSW);
            else if (Input.GetAxis("__SS__") != 0)
                script.Move(ONEGeneral.Direction.eSS);
            else if (Input.GetAxis("____SE") != 0)
                script.Move(ONEGeneral.Direction.eSE);
            else if (Input.GetAxis("SLOT#1") != 0)
                Debug.Log("SLOT#1"); // TODO
            else if (Input.GetAxis("SLOT#2") != 0)
                Debug.Log("SLOT#2"); // TODO
            else if (Input.GetAxis("SLOT#3") != 0)
                Debug.Log("SLOT#3"); // TODO
            else if (Input.GetAxis("USE") != 0)
                Debug.Log("USE"); // TODO

            PlayOneTurn();
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    // Play one turn just after the player has played
    public void PlayOneTurn()
    {
        m_turnBasedThings.Clear();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("TurnBased"))
        {
            ITurnBasedThing script = go.GetComponent<ITurnBasedThing>();
            m_turnBasedThings.Add(script);

            script.PlayMyTurn();
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
