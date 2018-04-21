/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Map :
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

    [SerializeField]
    int mNbRow;
    [SerializeField]
    int mNbColumn;
    [SerializeField]
    int wordToMapUnit;
    [SerializeField]
    List<string> TagSelectedList;

    [Space(20)]
    [Header("Debug Stuff")]
    [SerializeField]
    bool showMap = false;


    /********  PROTECTED        ************************/

    List<List<GameObject>> mMap;
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
        mMap = new List<List<GameObject>>();
        //Construct map
        for (int i = 0; i < mNbRow; i++)
        {
            List<GameObject> row = new List<GameObject>(); // Create an empty row
            for (int j = 0; j < mNbColumn; j++)
            {
                row.Add(null); // Add an element (column) to the row
            }
            mMap.Add(row); // Add the row to the main vector
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Automatic gameObjects detection
        foreach (string tag in TagSelectedList)
        {
            GameObject[] detectedObjectsList = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject detectedObject in detectedObjectsList)
            {
                //Put object on mMap
                automaticPlacementComputation(detectedObject);
            }
        }

        if(showMap)
        {
            showMapElements();
            showMap = false;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public GameObject getObjectAt(int pX, int pY)
    {
        if (pX >= 0 && pX < mMap.Count && pY >= 0 && pY < mMap[pX].Count)
        {
            return mMap[pX][pY]; //null if empty case
        }
        else
        {
            return null;
        }
    }

    public bool isOnMapCoordinates(int pX, int pY)
    {
        return (pX >= 0 && pX < mMap.Count && pY >= 0 && pY < mMap[pX].Count);
    }

    /********  PROTECTED        ************************/

    protected void automaticPlacementComputation(GameObject pObject)
    {
        float x = pObject.transform.position.x;
        float y = pObject.transform.position.z;
        int row = Mathf.FloorToInt(Mathf.FloorToInt(x) / wordToMapUnit);
        int column = Mathf.FloorToInt(Mathf.FloorToInt(y) / wordToMapUnit);

        //Safty check
        if (row >= 0 && row < mMap.Count && column >= 0 && column < mMap[row].Count)
        {
            mMap[row][column] = pObject;
        }
        else
        {
            Debug.Log("Caution : Object '" + pObject + "' detected out of map. (Row = "+ row + " ; Column = " + column+")");
        }
    }

    protected void showMapElements()
    {
        for (int i = 0; i < mMap.Count; i++)
        {
            for (int j = 0; j < mMap[i].Count; j++)
            {
                //If not null case
                if(mMap[i][j])
                {
                    Debug.Log("Object : [" + i + "][" + j + "] is " + mMap[i][j].name);
                }
            }
        }
    }

    /********  PRIVATE          ************************/

    #endregion
}
