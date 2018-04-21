/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ONEMap :
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
    // Instance
    public static ONEMap Instance
    {
        get { return m_instance; }
    }

    public int WorldToMapUnit
    {
        get
        {
            return m_WorldToMapUnit;
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
    int m_NbRow;
    [SerializeField]
    int m_NbColumn;
    [SerializeField]
    int m_WorldToMapUnit;
    [SerializeField]
    List<string> m_TagSelectedList;

    [Space(20)]
    [Header("Debug Stuff")]
    [SerializeField]
    bool showMap = false;


    /********  PROTECTED        ************************/

    List<List<List<GameObject>>> m_Map;
    /********  PRIVATE          ************************/
    private static ONEMap m_instance;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_instance = this;
        m_Map = new List<List<List<GameObject>>>();
        for (int i = 0; i < m_NbRow; i++)
        {
            List<List<GameObject>> row = new List<List<GameObject>>(); // Create an empty row
            for (int j = 0; j < m_NbColumn; j++)
            {
                row.Add(new List<GameObject>()); // Add an empty GameObject list to the row
            }
            m_Map.Add(row); // Add the row to the main vector
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(showMap)
        {
            showMapElements();
            showMap = false;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void updateMap()
    {
        //Clear the map
        m_Map = new List<List<List<GameObject>>>();
        for (int i = 0; i < m_NbRow; i++)
        {
            List<List<GameObject>> row = new List<List<GameObject>>(); // Create an empty row
            for (int j = 0; j < m_NbColumn; j++)
            {
                row.Add(new List<GameObject>()); // Add an empty GameObject list to the row
            }
            m_Map.Add(row); // Add the row to the main vector
        }
        //Automatic gameObjects detection
        foreach (string tag in m_TagSelectedList)
        {
            GameObject[] detectedObjectsList = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject detectedObject in detectedObjectsList)
            {
                //Put object on mMap
                automaticPlacementComputation(detectedObject);
            }
        }
    }

    public List<GameObject> getObjectAt(int p_X, int p_Y)
    {
        if (p_X >= 0 && p_X < m_Map.Count && p_Y >= 0 && p_Y < m_Map[p_X].Count)
        {
            return m_Map[p_X][p_Y]; //Empty if empty case
        }
        else
        {
            return null;
        }
    }

    public bool isOnMapCoordinates(int p_X, int p_Y)
    {
        return (p_X >= 0 && p_X < m_Map.Count && p_Y >= 0 && p_Y < m_Map[p_X].Count);
    }

    public Vector2 getMapCoordinates(Transform p_Transforme)
    {
        float x = p_Transforme.position.z;
        float y = p_Transforme.position.x;
        return new Vector2(Mathf.FloorToInt(Mathf.FloorToInt(x) / m_WorldToMapUnit), Mathf.FloorToInt(Mathf.FloorToInt(y) / m_WorldToMapUnit));
    }

    public Vector2 getWorldCoordinates(int p_X, int p_Y)
    {
        return new Vector2(p_X * m_WorldToMapUnit, p_Y * m_WorldToMapUnit);
    }

    /********  PROTECTED        ************************/

    protected void automaticPlacementComputation(GameObject p_Object)
    {
        float x = p_Object.transform.position.z;
        float y = p_Object.transform.position.x;
        int row = Mathf.FloorToInt(Mathf.FloorToInt(x) / m_WorldToMapUnit);
        int column = Mathf.FloorToInt(Mathf.FloorToInt(y) / m_WorldToMapUnit);

        //Safty check
        if (row >= 0 && row < m_Map.Count && column >= 0 && column < m_Map[row].Count)
        {
            m_Map[row][column].Add(p_Object);
        }
        else
        {
            Debug.Log("Caution : Object '" + p_Object + "' detected out of map. (Row = "+ row + " ; Column = " + column+")");
        }
    }

    protected void showMapElements()
    {
        for (int i = 0; i < m_Map.Count; i++)
        {
            for (int j = 0; j < m_Map[i].Count; j++)
            {
                //If not null case
                if(m_Map[i][j].Count > 0)
                {
                    foreach(GameObject gameObject in m_Map[i][j])
                    {
                        Debug.Log("Object : [" + i + "][" + j + "] is " + gameObject.name);
                    }
                }
            }
        }
    }

    /********  PRIVATE          ************************/

    #endregion
}
