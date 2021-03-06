﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inspired by Peerplays snow track system. I simply use the same 

public class DrawWithMouse : MonoBehaviour
{

    public Camera Cam;
    public Shader DrawShader;

    public float DrawSize = 75f;





    [SerializeField]
    private RenderTexture m_SplatMap;
    private Material m_WallMaterial;
    public Material m_DrawMaterial;
    private RaycastHit m_Hit;


    // TODO Integrate this into our controller 
    public EraseDraw eraser;


    private bool IsErasing = false;



    // Start is called before the first frame update
    void Start()
    {
        if (!m_DrawMaterial)
        {
            m_DrawMaterial = new Material(DrawShader);
            m_DrawMaterial.SetVector("_Color", Color.red);
            m_DrawMaterial.SetFloat("_BrushSize", DrawSize);
        }

        m_WallMaterial = GetComponent<MeshRenderer>().material;

       

        m_SplatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        m_WallMaterial.SetTexture("_Splat", m_SplatMap);

        if(eraser == null)
        {
            eraser = GetComponent<EraseDraw>();
        }
    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKey(KeyCode.L))
        {
            m_DrawMaterial.SetVector("_Color", Color.red);
        }
        if (Input.GetKey(KeyCode.M))
        {
            m_DrawMaterial.SetVector("_Color", Color.blue);
        }
        if (Input.GetKey(KeyCode.K))
        {
            m_DrawMaterial.SetVector("_Color", Color.green);
        }
        if(Input.GetKey(KeyCode.Backspace))
        {
            IsErasing = !IsErasing;
        }

        m_DrawMaterial.SetFloat("_BrushSize", DrawSize);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(Physics.Raycast(Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out m_Hit))
            {

                if (IsErasing == false)
                {
                    if (m_Hit.collider.gameObject == gameObject)
                    {


                        m_DrawMaterial.SetVector("_Coordinate", new Vector4(m_Hit.textureCoord.x, m_Hit.textureCoord.y, 0, 0));



                        RenderTexture temp = RenderTexture.GetTemporary(m_SplatMap.width, m_SplatMap.height, 0, RenderTextureFormat.ARGBFloat);

                        Graphics.Blit(m_SplatMap, temp);
                        Graphics.Blit(temp, m_SplatMap, m_DrawMaterial);
                        RenderTexture.ReleaseTemporary(temp);
                    }
                }
                else
                {
                    eraser.Erase(m_Hit);
                }
            }
        }

        


    }


    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 256, 256), m_SplatMap, ScaleMode.ScaleToFit, false, 1);
    }
}
