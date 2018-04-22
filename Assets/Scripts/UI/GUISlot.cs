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
public class GUISlot :
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

    [SerializeField] private Image m_icone;
    [SerializeField] private Text m_text;

    [SerializeField, Range(0, 2)] private int m_index;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        PlayMyTurn();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void PlayMyTurn()
    {
        GetComponent<Image>().color = ONEPlayer.Instance.Hand == m_index ? Color.yellow : Color.white;

        if (ONEPlayer.Instance.Slots.Count <= m_index)
        {
            m_icone.gameObject.SetActive(false);
            m_text.gameObject.SetActive(false);
        }
        else
        {
            ONEPlayer.WeaponInSlot weapon = ONEPlayer.Instance.Slots[m_index];

            m_icone.gameObject.SetActive(true);
            m_icone.sprite = weapon.m_icone;
            m_icone.color = weapon.CurrentCooldown > 0 ? Color.gray : Color.white;

            m_text.gameObject.SetActive(weapon.CurrentCooldown > 0);
            m_text.text = "" + weapon.CurrentCooldown;
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
