using System;
using UnityEngine;

public class CollectionInteractUI : MonoBehaviour
{
    [SerializeField] private Transform lookAtTarget;
    [SerializeField] private Transform followTarget;
     [SerializeField] private Vector3 offset;
    private Vector3 targetPos;


    private void Start()
    {
        lookAtTarget = GameObject.FindWithTag("Player").transform.Find("FollowCamera");
    }

    void Update()
    {
        transform.position = followTarget.position + offset;
        
        targetPos = transform.position - lookAtTarget.position;
        transform.rotation =  Quaternion.LookRotation(targetPos);
    }
}