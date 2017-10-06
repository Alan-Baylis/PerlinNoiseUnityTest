using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour {

    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawTexture(Texture2D texture)
    {
        textureRender.sharedMaterial.mainTexture = texture; //this is visible outside of run time
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);

    }

    public void DrawMesh(MeshData meshdata, Texture2D texture)
    {
        meshFilter.sharedMesh = meshdata.CreateMesh(); //generate mesh outside of game mode
        meshRenderer.sharedMaterial.mainTexture = texture;
    }

}
