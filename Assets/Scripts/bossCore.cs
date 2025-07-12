using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossCore : MonoBehaviour
{
    [Header("시계 초침/분침, 플레이어")]
    public GameObject minuteHand;
    public GameObject secondHand;

    public GameObject playerCharacter;

    private SpriteRenderer sp;
    
    [Header("기본 설정")]
    [SerializeField] private float handsThresHold = 10f;
    [SerializeField] private float circleSpeed = 1f;
    [SerializeField] private float triangleSpeed = 3f;


    [SerializeField] private float circleMin = 1f;
    [SerializeField] private float circleMax = 15f;

    [SerializeField] private float triangleMin = 1f;
    [SerializeField] private float triangleMax = 7f;


    [Header("공격 수단 프리팹")]
    public GameObject circleProjectile;
    public GameObject triangleProjectile;

    // 패턴 쿨타임
    private bool circleDebounce = false;
    private bool triangleDebounce = false;

    private List<Vector2> trackerSpawns = new List<Vector2>
    {
        new Vector2(15f,0),
        new Vector2(0,15f),
        new Vector2(-15f,0),
        new Vector2(0,-15f)
    };



    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        checkDamagable();

        if(!circleDebounce)
        StartCoroutine(fireProjectiles(Random.Range(circleMin,circleMax)));

        if(!triangleDebounce)
        StartCoroutine(fireTrackers(Random.Range(triangleMin,triangleMax)));
    }

    private IEnumerator fireProjectiles(float debounceTime)
    {   
        GameObject projectile = Instantiate(circleProjectile,new Vector2(0,0),Quaternion.identity);

        Rigidbody2D circlerb = projectile.GetComponent<Rigidbody2D>();

        Vector2 fireDirection = (playerCharacter.transform.position - transform.position).normalized;

        circlerb.AddForce(fireDirection * circleSpeed,ForceMode2D.Impulse);

        circleDebounce = true;
        yield return new WaitForSeconds(debounceTime);
        circleDebounce = false;
        yield return new WaitForSeconds(10f);

        Destroy(projectile);
    }

    private IEnumerator fireTrackers(float debounceTime)
    {   
        for(int i = 0; i < 4; i ++)
        {
             GameObject triangle = Instantiate(triangleProjectile,trackerSpawns[i],Quaternion.identity);
             trackHandler trackscript = triangle.GetComponent<trackHandler>();
             trackscript.playerCharacter = playerCharacter;
             trackscript.moveSpeed = triangleSpeed;
        }


        triangleDebounce = true;
        yield return new WaitForSeconds(debounceTime);
        triangleDebounce = false;
    }

    private bool checkDamagable()
    {
        float secHandAngle = secondHand.transform.localEulerAngles.z;
        float minHandAngle = minuteHand.transform.localEulerAngles.z;

        secHandAngle = NormalizedAngle(secHandAngle);
        minHandAngle = NormalizedAngle(minHandAngle);


        float angleDifference = Mathf.Abs(secHandAngle - minHandAngle);

        if (angleDifference > 180)
        angleDifference = 360 - angleDifference;

        if(angleDifference <= handsThresHold)
        {
            Debug.Log("IN RANGE");
            return true;
        }
        else
        return false;
    }

    float NormalizedAngle(float angle)
    {
        angle = angle % 360;
        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }
}
