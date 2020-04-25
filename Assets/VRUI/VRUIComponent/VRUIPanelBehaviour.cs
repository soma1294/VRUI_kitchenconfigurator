﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class VRUIPanelBehaviour : MonoBehaviour
{
    [SerializeField]
    [Min(0)]
    [HideInInspector]
    private float panelSizeX;
    [SerializeField]
    [Min(0)]
    [HideInInspector]
    private float panelSizeY;

    private Mesh mesh;
    private Mesh meshFront;
    private Mesh meshBack;

    private VRUIPositioner vruiPositioner;

    private Vector3 lastScale;

    private Vector2 lastPanelSize;

    private void OnEnable()
    {
        //FirstDrawPanel();
        lastScale = transform.localScale;
#if UNITY_EDITOR
        Undo.undoRedoPerformed -= UndoPanel;
        Undo.undoRedoPerformed += UndoPanel;
#endif
    }

    private void Start()
    {
        FirstDrawPanel();
    }

    private void UndoPanel()
    {
        if (lastPanelSize.x != PanelSizeX || lastPanelSize.y != PanelSizeY)
        {
            RedrawPanel();
        }
    }

    private void Update()
    {
        transform.localScale = lastScale;
    }

    void Reset()
    {
        if (!Application.isPlaying)
        {
            vruiPositioner = null;
            PanelSizeX = 1;
            PanelSizeY = 1;
            RedrawPanel();
        }
        //On undo, we want to Redraw the panel to make sure it has the correct size.
        //Undo.undoRedoPerformed -= RedrawPanel;
        //Undo.undoRedoPerformed += RedrawPanel;
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        //Remove our fuction from the delegate when this Monobehaviour is destroyed
        Undo.undoRedoPerformed -= UndoPanel;
#endif
    }

    protected void FirstDrawPanel()
    {
        //Only draw the mesh the first time, when no mesh exists
        /*
        if (Application.isPlaying)
        {
            if (GetComponent<MeshFilter>().mesh != null)
                return;
        } else
        {
            if (GetComponent<MeshFilter>().sharedMesh != null)
                return;
        }*/
        lastPanelSize = new Vector2(PanelSizeX, PanelSizeY);
        //TODO: Check if it still leaks when this if is not executing
        if (Application.isPlaying)
        {
            Destroy(meshFront);
            Destroy(meshBack);
        }
        else
        {
            DestroyImmediate(meshFront);
            DestroyImmediate(meshBack);
        }

        //Create the four edge vertices for the panel mesh
        Vector3[] boundaryVertices = new Vector3[]
        {
            transform.rotation * new Vector3(- PanelSizeX/2, PanelSizeY/2) + transform.position,
            transform.rotation * new Vector3(- PanelSizeX/2, - PanelSizeY/2) + transform.position,
            transform.rotation * new Vector3(PanelSizeX/2, - PanelSizeY/2) + transform.position,
            transform.rotation * new Vector3(PanelSizeX/2, PanelSizeY/2) + transform.position
        };
        //Set the triangles
        int[] trianglesFront = new int[]
        {
            0, 1, 2,
            0, 2, 3
        };
        int[] trianglesBack = new int[]
        {
            3, 2, 1,
            3, 1, 0
        };

        meshFront = new Mesh();
        meshBack = new Mesh();

        //Create one side of the mesh
        meshFront.Clear();

        meshFront.vertices = boundaryVertices;
        meshFront.triangles = trianglesFront;
        meshFront.RecalculateNormals();

        //Create the other side of the mesh
        meshBack.Clear();

        meshBack.vertices = boundaryVertices;
        meshBack.triangles = trianglesBack;
        meshBack.RecalculateNormals();

        //Save all relevant data before we set the position to the worlds origin point
        //This is important for the combination of the meshes
        Vector3 scale = transform.localScale;
        Quaternion rot = transform.rotation;
        Vector3 pos = transform.position;
        Matrix4x4 myTransform = transform.worldToLocalMatrix;

        //Set the transform to the origin of the world
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;

        //Combine the two meshes into one
        CombineInstance[] instances = new CombineInstance[2];
        instances[0].mesh = meshFront;
        instances[0].transform = myTransform * transform.localToWorldMatrix;
        instances[1].mesh = meshBack;
        instances[1].transform = myTransform * transform.localToWorldMatrix;

        Mesh combinedMesh = new Mesh();
        combinedMesh.name = "Panel Mesh";
        combinedMesh.hideFlags = HideFlags.HideAndDontSave;
        combinedMesh.CombineMeshes(instances, true);
        GetComponent<MeshFilter>().mesh = combinedMesh;

        //Revert back to the original values
        transform.localScale = scale;
        transform.rotation = rot;
        transform.position = pos;
    }

    public void RedrawPanel()
    {
        lastPanelSize = new Vector2(PanelSizeX, PanelSizeY);
        //TODO: Check if it still leaks when this if is not executing
        if (Application.isPlaying)
        {
            Destroy(meshFront);
            Destroy(meshBack);
        }
        else
        {
            DestroyImmediate(meshFront);
            DestroyImmediate(meshBack);
        }
        
        //Create the four edge vertices for the panel mesh
        Vector3[] boundaryVertices = new Vector3[]
        {
            transform.rotation * new Vector3(- PanelSizeX/2, PanelSizeY/2) + transform.position,
            transform.rotation * new Vector3(- PanelSizeX/2, - PanelSizeY/2) + transform.position,
            transform.rotation * new Vector3(PanelSizeX/2, - PanelSizeY/2) + transform.position,
            transform.rotation * new Vector3(PanelSizeX/2, PanelSizeY/2) + transform.position
        };
        //Set the triangles
        int[] trianglesFront = new int[]
        {
            0, 1, 2,
            0, 2, 3
        };
        int[] trianglesBack = new int[]
        {
            3, 2, 1,
            3, 1, 0
        };

        meshFront = new Mesh();
        meshBack = new Mesh();

        //Create one side of the mesh
        meshFront.Clear();

        meshFront.vertices = boundaryVertices;
        meshFront.triangles = trianglesFront;
        meshFront.RecalculateNormals();

        //Create the other side of the mesh
        meshBack.Clear();

        meshBack.vertices = boundaryVertices;
        meshBack.triangles = trianglesBack;
        meshBack.RecalculateNormals();

        //Save all relevant data before we set the position to the worlds origin point
        //This is important for the combination of the meshes
        //Vector3 scale = transform.localScale;
        Transform parent = transform.parent;
        Vector3 scale = transform.lossyScale;
        Quaternion rot = transform.rotation;
        Vector3 pos = transform.position;
        Matrix4x4 myTransform = transform.worldToLocalMatrix;

        //Set the transform to the origin of the world
        //transform.localScale = Vector3.one;
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
        
        //Combine the two meshes into one
        CombineInstance[] instances = new CombineInstance[2];
        instances[0].mesh = meshFront;
        instances[0].transform = myTransform * transform.localToWorldMatrix;
        instances[1].mesh = meshBack;
        instances[1].transform = myTransform * transform.localToWorldMatrix;

        Mesh combinedMesh = new Mesh();
        combinedMesh.name = "Panel Mesh";
        combinedMesh.hideFlags = HideFlags.HideAndDontSave;
        combinedMesh.CombineMeshes(instances, true);
        GetComponent<MeshFilter>().mesh = combinedMesh;

        //Revert back to the original values
        transform.localScale = scale;
        transform.rotation = rot;
        transform.position = pos;
        transform.SetParent(parent);

        //The anchors now need to be repositioned TODO: Beste Methode?
        foreach (Transform child in transform)
        {
            VRUIPositioner positioner = child.gameObject.GetComponent<VRUIPositioner>();
            if (positioner)
            {
                positioner.SetupAnchor();
                //positioner.SetRotationRelativeToParent();
                positioner.SetPositionRelativeToAnchor();
            }
        }
        vruiPositioner = gameObject.GetComponent<VRUIPositioner>();
        if (vruiPositioner && vruiPositioner.HasParentVRUIPositioner())
            vruiPositioner.SetupAnchor();
    }

    public float PanelSizeX
    {
        get { return panelSizeX; }
        set
        {
            if (value > 0)
                panelSizeX = value;
            else
                panelSizeX = 0f;
        }
    }

    public float PanelSizeY
    {
        get { return panelSizeY; }
        set
        {
            if (value > 0)
                panelSizeY = value;
            else
                panelSizeY = 0f;
        }
    }

    public Bounds PanelBounds
    {
        get { return new Bounds(transform.position, new Vector3(PanelSizeX, PanelSizeY)); }
    }
}