using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ocean : MonoBehaviour
{

    public Player player;
    public float tideForce = 8.0f;

    public Texture2D tideMap;
    private float forceMapMin = 0;
    private float forceMapMax = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculateForcemapLimits()
    {
        // find the mind and maxes of the texture
        Color[] forceMapColors = tideMap.GetPixels(0, 0, tideMap.width, tideMap.height);
        float[] grayscaleValues = forceMapColors.Select(color => (color.r + color.g + color.b) / 3).ToArray();
        forceMapMin = grayscaleValues.Min();
        forceMapMax = grayscaleValues.Max();
    }

    private float GetHeightMapMagnitude()
    {
        // use the raster file. Just assume for testing purposes the vector is -vector3.forward. Grayscale = magnitude.
        // assumes raster is 10x10 pixels at the moment. In future, will need to divide swimmer area / raster resolution
        int x = Mathf.FloorToInt(player.transform.position.x) + 5;
        int y = Mathf.FloorToInt(player.transform.position.z) + 5;
        Debug.Log("player floored coordinates: " + x + ", " + y);

        Color textColour = tideMap.GetPixel(x * 10, y * 10);

        float magnitude = (textColour.r + textColour.g + textColour.b) / 3;
        Debug.Log(magnitude);
        return magnitude;
        /**
        Debug.Log(tideMap.GetPixel(200, 5));
        Debug.Log(tideMap.GetPixels().Distinct().ToArray().Length);
        Debug.Log(tideMap.GetPixels().Length);
    **/
    }

    /// <summary>
    /// Produces increased tideforce on player based on vector map
    /// </summary>
    /// <returns></returns>
    private Vector3 CalculateTideVector()
    {
        Vector3 baseTideForce = transform.forward * tideForce * -1;
        float currentMultiplier = GetHeightMapMagnitude() / forceMapMax;
        Vector3 mappedTideForce = baseTideForce + (baseTideForce * currentMultiplier);
        return mappedTideForce
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<Player>() == player)
        {
            Debug.Log("player in ocean");
            player.rBody.AddForce(transform.forward * -tideForce);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (player.transform.position.z > 0)
        {
            // won
            Debug.Log("safe!");
        }
        else
        {
            // lost
            Debug.Log("loss...");
        }
    }
}
