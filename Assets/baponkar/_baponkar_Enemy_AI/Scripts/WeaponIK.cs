using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
[System.Serializable]
public class HumanBone 
{
    public HumanBodyBones bone;
    public float weight = 1.0f;
}

public class WeaponIK : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 targetOffset;
    public Transform aimTransform;
  

    public int iterations = 10;
    [Range(0,1.0f)]
    public float weight = 1.0f;

    public HumanBone [] humanBones;
    Transform [] boneTransforms;
    Animator animator;

    public float angleLimit = 90f;
    public float distanceLimit = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boneTransforms = new Transform[humanBones.Length];

        for(int i = 0; i < boneTransforms.Length; i++)
        {
            boneTransforms[i] = animator.GetBoneTransform(humanBones[i].bone);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(targetTransform == null)
        {
            return;
        }

        if(aimTransform == null)
        {
            return;
        }
        Vector3 targetPosition = GetTargetPosition();
        for(int i =0; i < iterations; i++)
        {
            for(int j =0; j<boneTransforms.Length; j++)
            {
                float boneWeight = humanBones[j].weight;
                Transform bone = boneTransforms[j];
                AimTarget(bone, targetPosition, boneWeight * weight );
            }
            
        }
        
    }

    Vector3 GetTargetPosition()
    {

        Vector3 targetDirection = (targetTransform.position + targetOffset) - aimTransform.position;
        Vector3 aimDirection = aimTransform.forward;
        float blendOut = 0.0f;

        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if(targetAngle > angleLimit)
        {
            blendOut += (targetAngle - angleLimit) / 50.0f;
        }

        float targetDistance = targetDirection.magnitude;
        if(targetDistance < distanceLimit)
        {
            blendOut += distanceLimit - targetDistance;
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);



        return aimTransform.position + direction;
    }

    private void AimTarget(Transform bone, Vector3 targetPosition, float weight)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - bone.position;

        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendRotation * bone.rotation;
    }

    public void SetTargetTransform(Transform target)
    {
        targetTransform = target;
    }

    public void SetAimTransform(Transform aim)
    {
        aimTransform = aim;
    }
}
}