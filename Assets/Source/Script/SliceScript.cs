using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using EzySlice;

public class SliceScript : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;

    public LayerMask SlicaebleLayer;

    private float cutForce = 1000f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, SlicaebleLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);

        }



    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = VelocityEstimator.GetVelocityEstimate();
        Vector3 PlaneNormal = Vector3.Cross(velocity, endSlicePoint.position - startSlicePoint.position).normalized;

        SliceHull hull = target.Slice(endSlicePoint, PlaneNormal);

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target);
            SetupSlicedComponenet(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target);
            SetupSlicedComponenet(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSlicedComponenet(GameObject obj)
    {
        Rigidbody rb = obj.AddComponent<Rigidbody>();
        MeshCollider collider = obj.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddExplosionForce(cutForce,obj.transform.position,1.0f);
    }*/
}
