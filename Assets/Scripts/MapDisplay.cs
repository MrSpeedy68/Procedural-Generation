using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;

    public void DrawTexture(Texture2D texture)
    {
        textureRender.sharedMaterial.mainTexture = texture; //sharedmaterial is used to show the texture in editor as .material is instatiated at runtime
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height); //set size of the plane to the same size as the map
    }
}
