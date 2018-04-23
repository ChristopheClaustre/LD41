﻿/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Projectile :
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

    public ONEGeneral.Direction Direction
    {
        get
        {
            return m_Direction;
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

    /********  PROTECTED        ************************/
    [SerializeField]
    ONEGeneral.Direction m_Direction;

    [SerializeField]
    int m_TotalLifetime = 5;    // Turn left before supression
    int m_LeftLifetime;

    [SerializeField]
    Vector2 m_ProjectileCoordinates;
    [SerializeField]
    GameObject m_ProjectileSpawn; //Can be let to null

    [SerializeField]
    private Color m_initialColor;
    [SerializeField]
    private Color m_almostDeadColor;

    private bool m_isFromPlayer = true;

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

    public void Init(ONEGeneral.Direction p_Direction, int p_LeftLifetime, bool p_isFromPlayer)
    {
        m_Direction = p_Direction;
        m_isFromPlayer = p_isFromPlayer;

        m_LeftLifetime = p_LeftLifetime;
    }

    public void Init(ONEGeneral.Direction p_Direction, bool p_isFromPlayer)
    {
        m_Direction = p_Direction;
        m_isFromPlayer = p_isFromPlayer;

        m_LeftLifetime = m_TotalLifetime;
    }

    public void PlayMyTurn()
    {
        m_ProjectileCoordinates = ONEMap.Instance.getMapCoordinates(this.transform);

        //Time up, bye bye :( But let a gift ;)
        if (m_LeftLifetime <= 0)
        {
            Explode();
            Destroy(gameObject);
        }

        //Still here ? So analyse next cell
        Vector2 deplacement = ONEGeneral.DirectionToVec2(m_Direction);
        Vector2 nextProjectileCoordinates = new Vector2(m_ProjectileCoordinates.x + deplacement.y, m_ProjectileCoordinates.y + deplacement.x);
        
        int nextX = Mathf.RoundToInt(nextProjectileCoordinates.x);
        int nextY = Mathf.RoundToInt(nextProjectileCoordinates.y);
        if (ONEMap.Instance.isOnMapCoordinates(nextX, nextY))
        {
            List<GameObject> nextCellObjectList = ONEMap.Instance.getObjectAt(nextX, nextY);
            foreach (GameObject nextCellObject in nextCellObjectList)
            {
                if (nextCellObject.GetComponent<ONEPlayer>()) // Player
                {
                    CollisionWithPlayer();
                    return;
                }
                else if (nextCellObject.CompareTag("Obstacle")) // Obstacle
                {
                    Explode();
                    Destroy(gameObject);
                    return;
                }
                if (nextCellObject.GetComponent<Enemy>() && m_isFromPlayer) // Enemy
                {
                    nextCellObject.GetComponent<Enemy>().Hit(1);
                    Destroy(gameObject);
                    return;
                }
            }
            transform.localPosition = new Vector2(transform.localPosition.x + (deplacement.x * ONEMap.Instance.WorldToMapUnit), transform.localPosition.y + (deplacement.y * ONEMap.Instance.WorldToMapUnit));
            m_LeftLifetime--;
        }
        else
        {
            Explode();
            Destroy(gameObject);
        }

        float delta = (float)m_LeftLifetime / m_TotalLifetime;
        var calculatedColor = Color.Lerp(m_initialColor, m_almostDeadColor, 1-delta);
        calculatedColor.a = 1;
        GetComponent<SpriteRenderer>().color = calculatedColor;
    }

    public void CollisionWithPlayer()
    {
        ONEPlayer.Instance.Hit(1);
        Destroy(gameObject);
    }

    /********  PROTECTED        ************************/

    protected void Explode()
    {
        if (m_ProjectileSpawn == null) return;

        GameObject created = Instantiate(m_ProjectileSpawn, transform.parent);
        created.transform.localPosition = transform.localPosition;
        ProjectileSpawn script = created.GetComponent<ProjectileSpawn>();
        script.Direction = m_Direction;
        script.IsFromPlayer = m_isFromPlayer;
        Debug.Assert(script != null);
    }

    /********  PRIVATE          ************************/

    #endregion
}
