/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ONEPlayer :
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

    public static ONEPlayer Instance
    {
        get
        {
            if (m_instance == null) m_instance = GameObject.FindGameObjectWithTag("Player").GetComponent<ONEPlayer>();
            return m_instance;
        }
    }

    public static ONEGeneral.Direction Direction
    {
        get
        {
            return Instance.m_direction;
        }
    }

    public static int MaxLifePoint
    {
        get
        {
            return Instance.m_maxLifePoint;
        }
    }

    public static int CurrentLifePoint
    {
        get
        {
            return Instance.m_currentLifePoint;
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

    /********  PRIVATE          ************************/

    private ONEGeneral.Direction m_direction = ONEGeneral.Direction.eEE;

    [SerializeField, Range(2, 20)] private int m_maxLifePoint = 5;
    [SerializeField, Range(0, 20)] private int m_currentLifePoint = 3;

    private static ONEPlayer m_instance = null;

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

    public void Move(ONEGeneral.Direction p_movement)
    {
        m_direction = p_movement;

        Vector2 deplacement = ONEGeneral.DirectionToVec2(p_movement) * ONEMap.Instance.WorldToMapUnit;
        Vector2 destination = new Vector2(transform.localPosition.x + deplacement.x, transform.localPosition.y + deplacement.y);

        List<GameObject> gos = ONEMap.Instance.getObjectAt(Mathf.RoundToInt(destination.y), Mathf.RoundToInt(destination.x));
        if (gos != null)
        {
            if (gos.Count > 0)
            {
                bool blocked = false;

                foreach(GameObject go in gos)
                {
                    if (go != null) {
                        Enemy enemy = go.GetComponent<Enemy>();
                        //Weapon weapon = go.GetComponent<Weapon>();

                        // Obstacle
                        if (go.CompareTag("Obstacle")) blocked = true;

                        // Enemy
                        else if (enemy != null)
                        {
                            enemy.Hit(1);
                            blocked = true;
                        }

                        // TODO: weapon
                        //else if (weapon != null)
                        //{
                        //    TakeWeapon(weapon);
                        //}
                    }
                }

                // move if not blocked
                if (! blocked) transform.localPosition = destination;
            }
            else
            {
                transform.localPosition = destination;
            }
        }
    }

    // Hit me master
    public void Hit(int p_damage)
    {
        m_currentLifePoint -= p_damage;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
