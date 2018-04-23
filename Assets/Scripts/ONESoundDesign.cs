/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ONESoundDesign :
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

    private static ONESoundDesign m_instance;

    [SerializeField] private AudioSource m_playerHurt;
    [SerializeField] private AudioSource m_enemyHurt;
    [SerializeField] private AudioSource m_enemyShoot;
    [SerializeField] private AudioSource m_playerShoot;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    private void Awake()
    {
        m_instance = this;
    }

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

    public static void PlayerHurt()
    {
        if (m_instance) Play(m_instance.m_playerHurt);
    }

    public static void EnemyHurt()
    {
        if (m_instance) Play(m_instance.m_enemyHurt);
    }

    public static void EnemyShoot()
    {
        if (m_instance) Play(m_instance.m_enemyShoot);
    }

    public static void PlayerShoot()
    {
        if (m_instance) Play(m_instance.m_playerShoot);
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private static void Play(AudioSource source)
    {
        if (source.isPlaying) return;

        source.Play();
    }

    #endregion
}
