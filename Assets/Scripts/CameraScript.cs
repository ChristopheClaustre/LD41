/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class CameraScript :
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

    /********  PROTECTED        ************************/

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
        float size = ((-3.7894f * ((float)Screen.width / (float)Screen.height)) + 11.7368f);
        Camera.main.orthographicSize = Mathf.Ceil(Mathf.Round(size * 1000.0f) / 1000.0f);
        PlayMyTurn();
    }

    // Update is called once per frame
    private void Update()
    {
        float size = ((-3.7894f * ((float)Screen.width / (float)Screen.height)) + 11.7368f);
        Camera.main.orthographicSize = Mathf.Ceil(Mathf.Round(size * 1000.0f) / 1000.0f);
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void PlayMyTurn()
    {
        Vector3 cameraPosition = gameObject.transform.position;
        var demiLongeurCamera = Camera.main.orthographicSize * Camera.main.aspect;

        gameObject.transform.position = new Vector3(ONEPlayer.Instance.ColumnLimit + demiLongeurCamera, cameraPosition.y, cameraPosition.z);
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
