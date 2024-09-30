using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float minRiseSpeed = .1f;
    public float maxRiseSpeed = 1f;
    public float lifeTime = 2f;

    private Animator animator;
    private float riseSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        riseSpeed = Random.Range(minRiseSpeed, maxRiseSpeed);
        StartCoroutine(BubbleRise());
    }

    IEnumerator BubbleRise()
    {
        float elaspedTime = 0f;

        while (elaspedTime < lifeTime )
        {
            transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        animator.SetTrigger("Pop");
        yield return null;
    }

    public void BubblePop()
    {
        Destroy(gameObject);
    }
}
