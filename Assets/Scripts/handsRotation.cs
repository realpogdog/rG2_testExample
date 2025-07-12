using UnityEngine;

public class handsRotation : MonoBehaviour
{
    public GameObject minuteHand;
    public GameObject secondHand;

    public float minuteHandRotation = 10f;
    public float secondHandRotation = 20f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        minuteHand.transform.Rotate(0,0,-minuteHandRotation * Time.deltaTime);
        secondHand.transform.Rotate(0,0,-secondHandRotation * Time.deltaTime);
    }
}
