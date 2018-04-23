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
    [SerializeField] private Vector2 m_playerSpawn;

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

        // spawners
        foreach (var prefab in m_spawners)
        {
            var created = Instantiate(prefab, m_spawnerParent);
            created.transform.localPosition = new Vector3(Random.Range(1, column-1), Random.Range(1, row-1), 0);
        }

        Debug.Assert(ONEMap.Instance.isOnMapCoordinates(Mathf.RoundToInt(m_playerSpawn.y), Mathf.RoundToInt(m_playerSpawn.x)));
        ONEPlayer.Instance.gameObject.transform.localPosition = m_playerSpawn;
        ONEPlayer.Instance.NewStage();
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
