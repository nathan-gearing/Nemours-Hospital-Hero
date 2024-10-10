using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float minRiseSpeed = .1f;
    public float maxRiseSpeed = 1f;
    public float minLifeTime = .2f;
    public float maxLifeTime = .5f;

    private Animator animator;
    private AudioSource audioSource;
    public AudioClip popSound;
    private float riseSpeed;
    private float lifeTime;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        riseSpeed = Random.Range(minRiseSpeed, maxRiseSpeed);
        lifeTime = Random.Range(minLifeTime, maxLifeTime);
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
        audioSource.PlayOneShot(popSound);
    }
    private void BubbleDestroy()
    {
        Destroy(gameObject);
    }
}
