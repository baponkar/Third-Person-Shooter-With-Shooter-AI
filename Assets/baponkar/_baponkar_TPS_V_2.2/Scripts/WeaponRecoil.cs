using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonShooter.Ai;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public CharacterAiming characterAiming;
    [HideInInspector] public Cinemachine.CinemachineImpulseSource cameraShake;
    public float verticalRecoil;
    public float horizontalRecoil;
    public float duration;
    public float recoilModifier = 1.0f;

    float time;
    int index;

    public Vector2 [] recoilPattern;

    public Animator rigController;
    int recoilLayerIndex = 1;

    AiAgent agent;
    bool getRigController;
   

    void Awake()
    {
        cameraShake = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!getRigController)
        {
            agent = GetComponentInParent<AiAgent>();
            if(agent != null)
            {
                rigController = agent.rigController;
                recoilLayerIndex = rigController.GetLayerIndex("Weapon_Recoil");
                getRigController = true;
            }
        }

        
        if(time > 0 && transform.tag == "Player")
        {
            characterAiming.yAxis.Value -= (((verticalRecoil/10f) * Time.deltaTime) / duration) * recoilModifier;
            characterAiming.xAxis.Value += (((horizontalRecoil/10f) * Time.deltaTime) / duration) * recoilModifier;
            time -= Time.deltaTime;
        }
     
    }
    public void Reset()
    {
        index = 0;
    }

    int GetNextIndex(int index)
    {
        return (index + 1) % recoilPattern.Length;
    }

    public void GenerateRecoil(string weaponName)
    {
        time = duration;
        horizontalRecoil = recoilPattern[index].x;
        verticalRecoil = recoilPattern[index].y;

        index = GetNextIndex(index);
        if(rigController)
        {
            //rigController?.Play("weapon_recoil_" + weaponName, recoilLayerIndex, 0.0f);
        }
        
    }

    public void GenerateCameraShake()
    {
        cameraShake.GenerateImpulse(Camera.main.transform.forward);
    }
}
