using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class BlinkChar : MonoBehaviour
{

    public bool Blink = true;
    [Tooltip("Minimum seconds between blinks")]
    public float MinTime = 1f;
    [Tooltip("Maximum seconds between blinks")]
    public float MaxTime = 3f;
    [Tooltip("Bigger the number, faster the blink")]
    public float blinkSpeed = 3f;
    public int BlinkBlendShapeIndex = 0;

    bool isBlinking = true;
    float BlinkWeight = 0f;

    SkinnedMeshRenderer thisMesh;

    void Start()
    {
        thisMesh = this.GetComponent<SkinnedMeshRenderer>();
        if (thisMesh.sharedMesh.blendShapeCount == 0) 
        {
            Blink = false;
            Debug.LogWarning("this Mesh doesn't seem to have any BlendShapes");
        }
        
    }

    void Update()
    {
        if (Blink)
        {
            if (isBlinking)
            {
                if(BlinkWeight < 99f) 
                { 
                    thisMesh.SetBlendShapeWeight(BlinkBlendShapeIndex, BlinkWeight);
                    BlinkWeight += blinkSpeed;
                } else
                {
                    BlinkWeight = 0;
                    thisMesh.SetBlendShapeWeight(BlinkBlendShapeIndex, BlinkWeight);
                    isBlinking = false;

                    float RandomRange = Random.Range(MinTime, MaxTime);
                    StartCoroutine(DoWait(RandomRange));
                }
            }
        }
    }

    IEnumerator DoWait(float theRandomRange)
    {
        yield return new WaitForSeconds(theRandomRange);
        isBlinking = true;
    }

}
