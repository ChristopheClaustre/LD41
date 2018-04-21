/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class GUIProgression :
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
        
    }

    // Update is called once per frame
    private void Update()
    {
        int current = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player").transform.localPosition.x);
        int max = ONEMap.Instance.NbColumn;

        Slider slider = GetComponent<Slider>();
        slider.maxValue = max - 1;
        slider.value = current;
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
