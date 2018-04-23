/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ProjectileSpawn :
    MonoBehaviour
    , ONETurnBased.ITurnBasedThing
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/
    [System.Serializable]
    public class GeneretedProjectile
    {
        public ONEGeneral.Direction m_Direction;
        public GameObject m_Projectile;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/
    public bool IsFromPlayer
    {
        get
        {
            return m_isFromPlayer;
        }
        set
        {
            m_isFromPlayer = value;
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
    [SerializeField]
    List<GeneretedProjectile> m_ProjectilesSpawnList;


    protected bool m_isFromPlayer = false;

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

    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void PlayMyTurn()
    {
        //Creat projectile
        foreach(GeneretedProjectile newProjectileGenereted in m_ProjectilesSpawnList)
        {
            GameObject projectileGO = Instantiate(newProjectileGenereted.m_Projectile, transform.parent);

            projectileGO.transform.localPosition = transform.localPosition;

            Projectile projectileScript = projectileGO.GetComponent<Projectile>();
            projectileScript.Init(newProjectileGenereted.m_Direction, m_isFromPlayer);
            //Play one turn to place projectile
            projectileScript.PlayMyTurn();
        }
        Destroy(gameObject);
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
