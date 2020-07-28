using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0); //Grabs the first dimension 0 which is x
        int height = noiseMap.GetLength(1); //Grabs the second dimension 1 which is y

        Texture2D texture = new Texture2D(width, height); //Makes a new 2D texture of height and width

        //Set color of all the pixels by generating an array of colors and setting them to the pixels
        Color[] colourMap = new Color[width * height];
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]); //Set pixel color between black and white % range is the same as noiseMap
            }
        }
        texture.SetPixels(colourMap); //Applys the colours
        texture.Apply();

        textureRender.sharedMaterial.mainTexture = texture; //sharedmaterial is used to show the texture in editor as .material is instatiated at runtime
        textureRender.transform.localScale = new Vector3(width, 1, height); //set size of the plane to the same size as the map
    }
}
