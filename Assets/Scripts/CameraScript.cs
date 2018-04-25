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
        // taille de camera
        float size = ((-3.7894f * ((float)Screen.width / (float)Screen.height)) + 11.7368f);
        Camera.main.orthographicSize = Mathf.Ceil(Mathf.Round(size * 1000.0f) / 1000.0f);

        // position de camera
        Vector3 cameraPosition = gameObject.transform.position;
        var demiLongeurCamera = Camera.main.orthographicSize * Camera.main.aspect;
        gameObject.transform.position = new Vector3(ONEPlayer.Instance.ColumnLimit + demiLongeurCamera, cameraPosition.y, cameraPosition.z);
    }

    // Update is called once per frame
    private void Update()
    {
        // taille de camera
        float size = ((-3.7894f * ((float)Screen.width / (float)Screen.height)) + 11.7368f);
        Camera.main.orthographicSize = Mathf.Ceil(Mathf.Round(size * 1000.0f) / 1000.0f);

        // position de camera
        Vector3 cameraPosition = gameObject.transform.position;
        var demiLongeurCamera = Camera.main.orthographicSize * Camera.main.aspect;
        gameObject.transform.position =
            new Vector3( Mathf.Lerp(cameraPosition.x, ONEPlayer.Instance.ColumnLimit + demiLongeurCamera, Time.deltaTime * 2), cameraPosition.y, cameraPosition.z);
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
