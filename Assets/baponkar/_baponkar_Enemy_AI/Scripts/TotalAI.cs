using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ThirdPersonShooter.Ai
{
public class TotalAI : MonoBehaviour
{
    public Text liveText;
    public Text deadText;
    public Text totalText;
    Health [] healths;
    [SerializeField]int live;
    [SerializeField] int dead = 0;
    float timer = 0;
    float time = .5f;
    // Start is called before the first frame update
    void Start()
    {
        healths = GameObject.FindObjectsOfType<Health>();
        live = healths.Length;
        totalText.text = "Total AI : "+healths.Length.ToString();
        timer = time;
    }

    // Update is called once per frame
    void Update()
    {
        // timer -= Time.deltaTime;
        // if(timer < 0)
        // {
            for(int i=0;i<healths.Length;i++)
            {
                if(healths[i].isDead)
                {
                    live--;
                    dead++;
                }
            }
            // timer = time;
        //}
        liveText.text = "Live AI : " + live.ToString();
        deadText.text = "Dead AI : " + dead.ToString();
    }
}
}