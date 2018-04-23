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

    [System.Serializable]
    public class WeaponInSlot
    {
        public GameObject m_projectileSpawn;
        public Sprite m_icone;
        public Sprite m_weaponOnChar;
        public int m_cooldown = 3;

        private int m_currentCooldown = 0;

        public int CurrentCooldown
        {
            get
            {
                return m_currentCooldown;
            }
        }

        public void Shoot(Transform p_parent, Vector3 p_position, ONEGeneral.Direction p_direction)
        {
            if (m_projectileSpawn == null || m_currentCooldown != 0) { Debug.Log("Can't shoot"); return; }

            GameObject created = Instantiate(m_projectileSpawn, p_parent);
            created.transform.localPosition = p_position;
            ProjectileSpawn script = created.GetComponent<ProjectileSpawn>();
            script.IsFromPlayer = true;
            script.Direction = p_direction;
            Debug.Assert(script != null);
            
            m_currentCooldown = m_cooldown + 1;

            ONESoundDesign.PlayerShoot();
        }

        public void Cooldown()
        {
            if (m_currentCooldown > 0) m_currentCooldown--;
        }
    }

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

    public int Hand
    {
        get
        {
            return m_hand;
        }
    }

    public List<WeaponInSlot> Slots
    {
        get
        {
            return m_slots;
        }
    }

    public int Progression
    {
        get
        {
            return m_progression;
        }
    }

    public int ColumnLimit
    {
        get
        {
            return m_columnLimit;
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

    [SerializeField]private List<WeaponInSlot> m_slots = new List<WeaponInSlot>();
    [SerializeField, Range(0, 2)] private int m_hand = 0;

    [SerializeField] private int m_visibility = 18;
    [SerializeField] private int m_offset = 3;
    private int m_progression = 0;
    private int m_columnLimit = 0;

    [SerializeField] private bool m_makeMeUnloadable;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        if (m_makeMeUnloadable)
            DontDestroyOnLoad(gameObject.transform.parent.gameObject);
    }

    // Update is called once per frame
    private void Update()
    {

    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void NewStage()
    {
        m_progression = Mathf.RoundToInt(transform.localPosition.x);
        m_columnLimit = 0;
    }

    public void Move(ONEGeneral.Direction p_movement)
    {
        m_direction = p_movement;

        Vector2 deplacement = ONEGeneral.DirectionToVec2(p_movement) * ONEMap.Instance.WorldToMapUnit;
        Vector2 destination = new Vector2(transform.localPosition.x + deplacement.x, transform.localPosition.y + deplacement.y);

        List<GameObject> gos = ONEMap.Instance.getObjectAt(Mathf.RoundToInt(destination.y), Mathf.RoundToInt(destination.x));
        if (gos != null && destination.x >= m_columnLimit)
        {
            bool blocked = false;

            foreach(GameObject go in gos)
            {
                if (go != null) {
                    Enemy enemy = go.GetComponent<Enemy>();
                    Weapon weapon = go.GetComponent<Weapon>();
                    Projectile projectile = go.GetComponent<Projectile>();

                    // Obstacle
                    if (go.CompareTag("Obstacle")) blocked = true;

                    // Enemy
                    else if (enemy != null)
                    {
                        enemy.Hit(1);
                        blocked = true;
                    }

                    // Weapon
                    else if (weapon != null)
                    {
                        TakeWeapon(weapon.WeaponInSlot);
                        Destroy(go);
                    }

                    // Projectile
                    else if (projectile != null && ONEGeneral.OppositeDirection(projectile.Direction, m_direction))
                    {
                        projectile.CollisionWithPlayer();
                    }
                }
            }

            // move if not blocked
            if (! blocked) transform.localPosition = destination;
        }

        m_progression = System.Math.Max(m_progression, Mathf.RoundToInt(transform.localPosition.x));
        m_columnLimit = System.Math.Min(m_progression - m_offset, ONEMap.Instance.NbColumn+1 - m_visibility);

        WeaponsCooldown();
    }

    // Hit me master
    public void Hit(int p_damage)
    {
        m_currentLifePoint -= p_damage;
        ONESoundDesign.PlayerHurt();
    }

    public void ChangeWeapon(int index)
    {
        Debug.Assert(index >= 0 && index <= 2);
        m_hand = index;

        WeaponsCooldown();
    }

    public void Shoot()
    {
        if (m_hand < m_slots.Count)
        {
            WeaponInSlot selected = m_slots[m_hand];
            selected.Shoot(transform.parent, transform.localPosition, m_direction);
        }
        
        WeaponsCooldown();
    }

    public void Wait()
    {
        WeaponsCooldown();
    }

    public void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void TakeWeapon(WeaponInSlot weapon)
    {
        if (m_slots.Count < 3)
            m_slots.Add(weapon);
        else
            m_slots[m_hand] = weapon;
    }

    private void WeaponsCooldown()
    {
        foreach (WeaponInSlot weapon in m_slots)
        {
            if (weapon != null) weapon.Cooldown();
        }
    }

    #endregion
}
