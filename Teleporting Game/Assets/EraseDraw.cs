using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseDraw : MonoBehaviour
{

    public Shader EraseShader;
    private Material _EraserMat;
    private MeshRenderer _meshRender;
    // Start is called before the first frame update
    void Start()
    {
        _meshRender = GetComponent<MeshRenderer>();
        _EraserMat = new Material(EraseShader);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Erase(RaycastHit hit)
    {


        _EraserMat.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));

        RenderTexture surface = (RenderTexture)_meshRender.material.GetTexture("_Splat");
        RenderTexture temp = RenderTexture.GetTemporary(surface.width, surface.height, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(surface, temp, _EraserMat);
        Graphics.Blit(temp, surface);

        _meshRender.material.SetTexture("_Splat", surface);
        RenderTexture.ReleaseTemporary(temp);
    }
}
