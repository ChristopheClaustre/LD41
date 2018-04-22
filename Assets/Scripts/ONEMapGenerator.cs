/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ONEMapGenerator :
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

    [SerializeField] private List<GameObject> m_spawners;
    [SerializeField] private Transform m_spawnerParent;
    
    [SerializeField] private List<SpriteRenderer> m_toParamColumn;
    [SerializeField] private List<SpriteRenderer> m_toParamRow;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        var column = ONEMap.Instance.NbColumn;
        var row = ONEMap.Instance.NbRow;

        // param tiled spriteRenderer
        foreach(var sprite in m_toParamColumn)
        {
            sprite.size = new Vector2(column, sprite.size.y);
        }
        foreach (var sprite in m_toParamRow)
        {
            sprite.size = new Vector2(sprite.size.x, row);
        }

        // spawners
        foreach (var prefab in m_spawners)
        {
            var created = Instantiate(prefab, m_spawnerParent);
            created.transform.localPosition = new Vector3(Random.Range(0, column), Random.Range(0, row), 0);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
