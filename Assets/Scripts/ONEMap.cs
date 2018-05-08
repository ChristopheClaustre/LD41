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

    public interface IMyTransformIsALie
    {
        Vector3 Position { get; }
    }

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
        get
        {
            if (m_instance == null) m_instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<ONEMap>();
            return m_instance;
        }
    }

    public int NbRow
    {
        get
        {
            return m_NbRow;
        }
    }

    public int NbColumn
    {
        get
        {
            return m_NbColumn;
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

    [SerializeField] SpriteRenderer m_sizer;

    [SerializeField]
    int m_NbRow;
    [SerializeField]
    int m_NbColumn;
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

    private void Awake()
    {
        if (m_sizer != null)
        {
            m_NbColumn = Mathf.RoundToInt(m_sizer.size.x);
            m_NbRow = Mathf.RoundToInt(m_sizer.size.y);
        }
        else Debug.Log("Sizer not set ! Will use the setted NbColumn and NbRow");

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

    // Use this for initialization
    private void Start()
    {

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

    public void addToMap(GameObject p_GameObject)
    {
        automaticPlacementComputation(p_GameObject);
    }

    public List<GameObject> getObjectAt(int p_row, int p_column)
    {
        if (p_row >= 0 && p_row < m_Map.Count && p_column >= 0 && p_column < m_Map[p_row].Count)
        {
            return m_Map[p_row][p_column]; //Empty if empty case
        }
        else
        {
            return null;
        }
    }

    public bool isOnMapCoordinates(int p_row, int p_column)
    {
        return (p_row >= 0 && p_row < m_Map.Count && p_column >= 0 && p_column < m_Map[p_row].Count);
    }

    public Vector2 getMapCoordinates(Transform p_Transforme)
    {
        float x = p_Transforme.position.z;
        float y = p_Transforme.position.x;
        return new Vector2(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
    }

    /********  PROTECTED        ************************/

    protected void automaticPlacementComputation(GameObject p_Object)
    {
        IMyTransformIsALie script = p_Object.GetComponent<IMyTransformIsALie>();
        Vector2 position;
        if (script != null)
        {
            position.x = script.Position.y;
            position.y = script.Position.x;
        }
        else
        {
            position.x = p_Object.transform.position.z;
            position.y = p_Object.transform.position.x;
        }
        int row = Mathf.RoundToInt(position.x);
        int column = Mathf.RoundToInt(position.y);

        placement(p_Object, row, column);
    }

    protected void placement(GameObject p_Object, int p_row, int p_column)
    {
        //Safty check
        if (p_row >= 0 && p_row < m_Map.Count && p_column >= 0 && p_column < m_Map[p_row].Count)
        {
            m_Map[p_row][p_column].Add(p_Object);
        }
        else
        {
            Debug.Log("Caution : Object '" + p_Object + "' detected out of map. (Row = " + p_row + " ; Column = " + p_column + ")");
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
