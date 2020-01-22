using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshCollider : MonoBehaviour
{
    void Start()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        collider = GetComponent<MeshCollider>();
    }

    private float time = 0;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (this.gameObject.GetComponent<Renderer>().isVisible)
        {
            if (time >= 5f)
            {
                time = 0;
                UpdateCollider();
            }
        }
    }

    SkinnedMeshRenderer meshRenderer;
    MeshCollider collider;

    public void UpdateCollider()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        collider.sharedMesh = null;
        collider.sharedMesh = colliderMesh;
    }
}

