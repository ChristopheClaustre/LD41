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

    List<List<GameObject>> m_Map;
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
        m_Map = new List<List<GameObject>>();
        //Construct map
        for (int i = 0; i < m_NbRow; i++)
        {
            List<GameObject> row = new List<GameObject>(); // Create an empty row
            for (int j = 0; j < m_NbColumn; j++)
            {
                row.Add(null); // Add an element (column) to the row
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
        m_Map = new List<List<GameObject>>();
        for (int i = 0; i < m_NbRow; i++)
        {
            List<GameObject> row = new List<GameObject>(); // Create an empty row
            for (int j = 0; j < m_NbColumn; j++)
            {
                row.Add(null); // Add an element (column) to the row
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

    public GameObject getObjectAt(int pX, int pY)
    {
        if (pX >= 0 && pX < m_Map.Count && pY >= 0 && pY < m_Map[pX].Count)
        {
            return m_Map[pX][pY]; //null if empty case
        }
        else
        {
            return null;
        }
    }

    public bool isOnMapCoordinates(int pX, int pY)
    {
        return (pX >= 0 && pX < m_Map.Count && pY >= 0 && pY < m_Map[pX].Count);
    }

    /********  PROTECTED        ************************/

    protected void automaticPlacementComputation(GameObject pObject)
    {
        float x = pObject.transform.position.z;
        float y = pObject.transform.position.x;
        int row = Mathf.FloorToInt(Mathf.FloorToInt(x) / m_WorldToMapUnit);
        int column = Mathf.FloorToInt(Mathf.FloorToInt(y) / m_WorldToMapUnit);

        //Safty check
        if (row >= 0 && row < m_Map.Count && column >= 0 && column < m_Map[row].Count)
        {
            m_Map[row][column] = pObject;
        }
        else
        {
            Debug.Log("Caution : Object '" + pObject + "' detected out of map. (Row = "+ row + " ; Column = " + column+")");
        }
    }

    protected void showMapElements()
    {
        for (int i = 0; i < m_Map.Count; i++)
        {
            for (int j = 0; j < m_Map[i].Count; j++)
            {
                //If not null case
                if(m_Map[i][j])
                {
                    Debug.Log("Object : [" + i + "][" + j + "] is " + m_Map[i][j].name);
                }
            }
        }
    }

    /********  PRIVATE          ************************/

    #endregion
}
