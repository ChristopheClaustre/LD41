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
public class GUILifePoint :
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

    [SerializeField] private List<GameObject> m_childrens;

    [SerializeField] private Color m_lost;
    [SerializeField] private Color m_remaining;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_childrens = new List<GameObject>();

        foreach (Transform child in transform)
        {
            m_childrens.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        for(int i = 0; i < m_childrens.Count ; ++i)
        {
            m_childrens[i].GetComponent<Image>().color = (i >= player.CurrentLifePoint) ? m_lost : m_remaining;
            m_childrens[i].SetActive(i < player.MaxLifePoint);
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
