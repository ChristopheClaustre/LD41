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

    public ONEGeneral.Direction Direction
    {
        get
        {
            return m_direction;
        }
        set
        {
            m_direction = value;
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

    protected ONEGeneral.Direction m_direction = ONEGeneral.Direction.eEE;


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
            ONEMap.Instance.addToMap(projectileGO);

            Projectile projectileScript = projectileGO.GetComponent<Projectile>();

            ONEGeneral.Direction direction = ComputeDirection(newProjectileGenereted, m_direction);
            projectileScript.Init(direction, m_isFromPlayer);
            //Play one turn to place projectile
            projectileScript.PlayMyTurn();
        }
        Destroy(gameObject);
    }

    /********  PROTECTED        ************************/
    protected ONEGeneral.Direction ComputeDirection(GeneretedProjectile newProjectileGenereted, ONEGeneral.Direction p_Direction)
    {
        int orientation = (DirectionToOrientation(newProjectileGenereted.m_Direction) + DirectionToOrientation(p_Direction)) % 8;
        return DirectionFromOrientation(orientation);
    }

    protected int DirectionToOrientation(ONEGeneral.Direction p_Direction)
    {
        switch(p_Direction)
        {
            case ONEGeneral.Direction.eEE: return 0;
            case ONEGeneral.Direction.eSE: return 1;
            case ONEGeneral.Direction.eSS: return 2;
            case ONEGeneral.Direction.eSW: return 3;
            case ONEGeneral.Direction.eWW: return 4;
            case ONEGeneral.Direction.eNW: return 5;
            case ONEGeneral.Direction.eNN: return 6;
            case ONEGeneral.Direction.eNE: return 7;
        }
        return 0;
    }

    protected ONEGeneral.Direction DirectionFromOrientation(int p_Orientation)
    {
        switch (p_Orientation)
        {
            case 0: return ONEGeneral.Direction.eEE;
            case 1: return ONEGeneral.Direction.eSE;
            case 2: return ONEGeneral.Direction.eSS;
            case 3: return ONEGeneral.Direction.eSW;
            case 4: return ONEGeneral.Direction.eWW;
            case 5: return ONEGeneral.Direction.eNW;
            case 6: return ONEGeneral.Direction.eNN;
            case 7: return ONEGeneral.Direction.eNE;
        }
        return ONEGeneral.Direction.eEE;
    }

    /********  PRIVATE          ************************/

    #endregion
}
