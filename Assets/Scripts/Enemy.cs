﻿/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Enemy :
    MonoBehaviour
    , ONETurnBased.ITurnBasedThing
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
        public ONEGeneral.Direction[] m_multiples;
    }

    // IngredientDrawer
    [CustomPropertyDrawer(typeof(Action), true)]
    public class ActionDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label)
                + EditorGUIUtility.standardVerticalSpacing
                + GetChildHeight(property, label);
        }

        private float GetChildHeight(SerializedProperty property, GUIContent label)
        {
            if ((Action.Kind)property.FindPropertyRelative("m_kind").intValue != Action.Kind.eShoots)
                return 0; 

            var child = property.FindPropertyRelative("m_multiples");
            var incr = base.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing;

            return child.arraySize * incr;
        }

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Action.Kind kind = (Action.Kind)property.FindPropertyRelative("m_kind").intValue;

            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Calculate rects
            var halfWidth = position.width/2;
            var leftRect = new Rect(position.x, position.y, halfWidth, base.GetPropertyHeight(property, label));
            var rightRect = new Rect(position.x + halfWidth, position.y, halfWidth, base.GetPropertyHeight(property, label));

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(leftRect, property.FindPropertyRelative("m_kind"), GUIContent.none);

            if (kind == Action.Kind.eMove)
                EditorGUI.PropertyField(rightRect, property.FindPropertyRelative("m_direction"), GUIContent.none);
            if (kind == Action.Kind.eShoots)
            {
                var multiples = property.FindPropertyRelative("m_multiples");

                multiples.arraySize = EditorGUI.IntField(rightRect, multiples.arraySize);

                var arrayRect = new Rect(position.x+10, position.y, position.width-10, position.height);
                foreach (SerializedProperty p in multiples)
                {
                    arrayRect.y += base.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.PropertyField(arrayRect, p, GUIContent.none, true);
                }
            }

            EditorGUI.EndProperty();
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

    public int LifePoint
    {
        get
        {
            return m_lifePoint;
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

    [SerializeField, Range(0, 10)] private int m_lifePoint = 2;

    [SerializeField] private GameObject m_loot;

    [SerializeField] private GameObject m_projectile;

    [SerializeField] private Action[] m_pattern;
    private int m_patternIndex = 0;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        if (m_pattern.Length == 0) { Debug.Log("No pattern"); return; }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void PlayMyTurn()
    {
        if (m_pattern.Length == 0) return;
        
        Action currentAction = m_pattern[m_patternIndex];
        doAction(currentAction);

        // tour suivant
        m_patternIndex = (m_patternIndex + 1) % m_pattern.Length;
    }
    
    public void Hit(int p_damage)
    {
        m_lifePoint -= p_damage;

        if (m_lifePoint <= 0)
        {
            if (m_loot)
            {
                GameObject created = Instantiate(m_loot, transform.parent);
                created.transform.localPosition = transform.localPosition;
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
                foreach (ONEGeneral.Direction direction in p_action.m_multiples) Shoot(direction);
                break;
            case Action.Kind.eWait:
                // Nothing
                break;
        }
    }

    void Move(ONEGeneral.Direction p_direction)
    {
        Vector2 deplacement = ONEGeneral.DirectionToVec2(p_direction) * ONEMap.Instance.WorldToMapUnit;
        Vector2 destination = new Vector2(transform.localPosition.x + deplacement.x, transform.localPosition.y + deplacement.y);

        List<GameObject> gos = ONEMap.Instance.getObjectAt(Mathf.RoundToInt(destination.y), Mathf.RoundToInt(destination.x));
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
            if (!blocked) transform.localPosition = destination;
        }
    }

    void Shoot(ONEGeneral.Direction p_direction)
    {
        if (m_projectile == null) return;

        GameObject created = Instantiate(m_projectile, transform.parent);

        created.transform.localPosition = transform.localPosition;

        Projectile script = created.GetComponent<Projectile>();
        Debug.Assert(script != null);
        script.Init(p_direction, 10, false);
        script.PlayMyTurn();
    }

    #endregion
}
