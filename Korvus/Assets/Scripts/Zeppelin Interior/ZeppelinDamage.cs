using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class ZeppelinDamage : MonoBehaviour
{
    private float Timer;
    static public float MinDamageTimer = 10;
    static public float MaxDamageTimer = 15;

    [SerializeField] GameObject DamageLocationParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Timer = 0;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Timer += 1;
        if (Timer > MinDamageTimer || Timer < MaxDamageTimer)
        {
            if (UnityEngine.Random.Range(0, 10) == 5)
            {
                Timer = 0;
                // DoDamage();
            }
        }
        else if (Timer > MaxDamageTimer)
        {
            Timer = 0;
            DoDamage();
        }
    }

    void DoDamage()
    {
        int count = DamageLocationParent.transform.childCount;

        int index = UnityEngine.Random.Range(0, count);
        Transform spawner = DamageLocationParent.transform.GetChild(index);
        spawner.gameObject.SetActive(true);
    }
}
