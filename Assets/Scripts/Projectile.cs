/***************************************************/
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
    , ONEMap.IMyTransformIsALie
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

    public Vector3 Position
    {
        get { return m_destination; }
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
    ONEGeneral.Direction m_Direction;

    [SerializeField]
    int m_TotalLifetime = 5;    // Turn left before supression
    int m_LeftLifetime;
    
    [SerializeField]
    GameObject m_ProjectileSpawn; //Can be let to null

    [SerializeField]
    private Color m_initialColor;
    [SerializeField]
    private Color m_almostDeadColor;

    private bool m_isFromPlayer = true;

    private Vector3 m_destination;

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
        transform.localPosition = Vector3.Lerp(transform.localPosition, m_destination, Time.deltaTime * ONEGeneral.VelocityAnimation);
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void Init(ONEGeneral.Direction p_Direction, int p_LeftLifetime, bool p_isFromPlayer)
    {
        m_destination = transform.localPosition;

        m_Direction = p_Direction;
        m_isFromPlayer = p_isFromPlayer;

        m_LeftLifetime = p_LeftLifetime;
    }

    public void Init(ONEGeneral.Direction p_Direction, bool p_isFromPlayer)
    {
        Init(p_Direction, m_TotalLifetime, p_isFromPlayer);
    }

    public void PlayMyTurn()
    {
        //Time up, bye bye :( But let a gift ;)
        if (m_LeftLifetime <= 0)
        {
            Explode();
            Destroy(gameObject);
            return;
        }

        //Still here ? So analyse next cell
        Vector2 deplacement = ONEGeneral.DirectionToVec2(m_Direction);
        Vector2 destination = new Vector2(Mathf.RoundToInt(m_destination.x + deplacement.x), Mathf.RoundToInt(m_destination.y + deplacement.y));
        
        int column = (int)destination.x;
        int row = (int)destination.y;
        if (ONEMap.Instance.isOnMapCoordinates(row, column))
        {
            List<GameObject> nextCellObjectList = ONEMap.Instance.getObjectAt(row, column);
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
            m_destination = destination;
            m_LeftLifetime--;
        }
        else
        {
            Explode();
            Destroy(gameObject);
            return;
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
        created.transform.localPosition = m_destination;
        ProjectileSpawn script = created.GetComponent<ProjectileSpawn>();
        script.Direction = m_Direction;
        script.IsFromPlayer = m_isFromPlayer;
        Debug.Assert(script != null);
    }

    /********  PRIVATE          ************************/

    #endregion
}
