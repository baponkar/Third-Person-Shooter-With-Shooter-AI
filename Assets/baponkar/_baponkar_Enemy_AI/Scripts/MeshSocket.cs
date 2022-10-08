using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonShooter.Ai
{
public class MeshSocket : MonoBehaviour
{
    public MeshSockets.SocketId socketId;
    public Vector3 offset;
    public Vector3 rotation;
    public Transform attachPoint;

    public HumanBodyBones bone;


    void Start()
    {
        Animator animator = GetComponentInParent<Animator>();
        attachPoint = new GameObject("socket" + socketId).transform;
        attachPoint.SetParent(animator.GetBoneTransform(bone));
        attachPoint.localPosition = offset;
        attachPoint.localRotation = Quaternion.Euler(rotation);
    }


    void Update()
    {
        
    }

    public void Attach(Transform objectTransform)
    {
        objectTransform.SetParent(attachPoint, false);
    }
}
}
