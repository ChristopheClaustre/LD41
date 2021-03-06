﻿/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Enemy :
    MonoBehaviour
    , ONETurnBased.ITurnBasedThing
    , ONEMap.IMyTransformIsALie
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    [System.Serializable]
    public class Action
    {
        public enum Kind
        {
            eMove,
            eShoots,
            eWait
        }

        public Kind m_kind;
        public ONEGeneral.Direction m_direction;
        public GameObject m_projectileSpawn;
    }

//    [CustomPropertyDrawer(typeof(Action), true)]
//    public class ActionDrawer : PropertyDrawer
//    {
//        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//        {
//            Action.Kind kind = (Action.Kind)property.FindPropertyRelative("m_kind").intValue;
//            float spacing = base.GetPropertyHeight(property, label)+ EditorGUIUtility.standardVerticalSpacing;
//            if (kind == Action.Kind.eShoots)
//            {
//                spacing += base.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing;
//            }
//            return spacing;
//        }

//        // Draw the property inside the given rect
//        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//        {
//            Action.Kind kind = (Action.Kind)property.FindPropertyRelative("m_kind").intValue;

//            // Using BeginProperty / EndProperty on the parent property means that
//            // prefab override logic works on the entire property.
//            EditorGUI.BeginProperty(position, label, property);

//            // Draw label
//            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//            // Calculate rects
//            var halfWidth = position.width/2;
//            var leftRect = new Rect(position.x, position.y, halfWidth, base.GetPropertyHeight(property, label));
//            var rightRect = new Rect(position.x + halfWidth, position.y, halfWidth, base.GetPropertyHeight(property, label));

//            // Draw fields - passs GUIContent.none to each so they are drawn without labels
//            EditorGUI.PropertyField(leftRect, property.FindPropertyRelative("m_kind"), GUIContent.none);

//            if (kind == Action.Kind.eMove)
//                EditorGUI.PropertyField(rightRect, property.FindPropertyRelative("m_direction"), GUIContent.none);
//            if (kind == Action.Kind.eShoots)
//            {
//                EditorGUI.PropertyField(rightRect, property.FindPropertyRelative("m_direction"), GUIContent.none);
//                leftRect.y += base.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing;
//                EditorGUI.PropertyField(leftRect, property.FindPropertyRelative("m_projectileSpawn"), GUIContent.none);
                
//            }

//            EditorGUI.EndProperty();
//        }
//    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

#endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public int LifePoint
    {
        get
        {
            return m_lifePoint;
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

    /********  PRIVATE          ************************/

    [SerializeField, Range(0, 10)] private int m_lifePoint = 2;
    [SerializeField, Range(0, 10)] private int m_currentLifePoint = 2;

    [SerializeField] private GameObject m_loot;

    [SerializeField] private Action[] m_pattern;
    [SerializeField] private int m_patternIndex = 0;

    [SerializeField] private Vector3 m_destination;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    public void Start()
    {
        if (m_pattern.Length == 0) { Debug.Log("No pattern"); }

        m_destination = transform.localPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, m_destination, Time.deltaTime * ONEGeneral.VelocityAnimation);
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void PlayMyTurn()
    {
        if (m_destination.x < ONEPlayer.Instance.ColumnLimit) { Destroy(gameObject); return; }

        var calculatedColor = Color.white * ((float)m_currentLifePoint / m_lifePoint);
        calculatedColor.a = 1;
        if (m_pattern.Length > 0)
        {
            var nextAction = m_pattern[(m_patternIndex + 1) % m_pattern.Length];
            calculatedColor.g *= (nextAction.m_kind == Action.Kind.eShoots) ? 0.5f : 1;
            calculatedColor.b *= (nextAction.m_kind == Action.Kind.eShoots) ? 0.5f : 1;
        }
        GetComponent<SpriteRenderer>().color = calculatedColor;

        if (m_pattern.Length > 0)
        {
            Action currentAction = m_pattern[m_patternIndex];
            doAction(currentAction);

            // tour suivant
            m_patternIndex = (m_patternIndex + 1) % m_pattern.Length;
        }
    }
    
    public void Hit(int p_damage)
    {
        m_currentLifePoint -= p_damage;

        ONESoundDesign.EnemyHurt();

        if (m_currentLifePoint <= 0)
        {
            if (m_loot)
            {
                GameObject created = Instantiate(m_loot, transform.parent);
                created.transform.localPosition = m_destination;
            }

            Debug.Log(name + " diededed ! x(");
            Destroy(gameObject);
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    void doAction(Action p_action)
    {
        switch (p_action.m_kind)
        {
            case Action.Kind.eMove:
                Move(p_action.m_direction);
                break;
            case Action.Kind.eShoots:
                Shoot(p_action.m_direction, p_action.m_projectileSpawn);
                break;
            case Action.Kind.eWait:
                // Nothing
                break;
        }
    }

    void Move(ONEGeneral.Direction p_direction)
    {
        Vector2 deplacement = ONEGeneral.DirectionToVec2(p_direction);
        Vector2 destination = new Vector2(Mathf.RoundToInt(m_destination.x + deplacement.x), Mathf.RoundToInt(m_destination.y + deplacement.y));

        List<GameObject> gos = ONEMap.Instance.getObjectAt((int)destination.y, (int)destination.x);
        if (gos != null)
        {
            bool blocked = false;

            foreach (GameObject go in gos)
            {
                if (go != null)
                {
                    Enemy enemy = go.GetComponent<Enemy>();
                    ONEPlayer player = go.GetComponent<ONEPlayer>();

                    // Obstacle
                    if (go.CompareTag("Obstacle")) blocked = true;

                    // Player
                    else if (player != null)
                    {
                        player.Hit(1);
                        blocked = true;
                    }

                    // Enemy
                    else if (enemy != null)
                    {
                        blocked = true;
                    }
                }
            }

            // move if not blocked
            if (!blocked) m_destination = destination;
        }
    }

    void Shoot(ONEGeneral.Direction p_direction, GameObject p_projectileSpawn)
    {
        if (p_projectileSpawn == null) return;

        GameObject created = Instantiate(p_projectileSpawn, transform.parent);
        created.transform.localPosition = m_destination;
        ProjectileSpawn script = created.GetComponent<ProjectileSpawn>();
        Debug.Assert(script != null);
        script.Direction = p_direction;
        script.PlayMyTurn();

        ONESoundDesign.EnemyShoot();
    }

#endregion
}
