using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StackController : MonoBehaviour
{
    List<Transform> stackList = new List<Transform>();
    [SerializeField] Transform sphere;
    [SerializeField] float _jumpDuration = 0.35f;
    [SerializeField] float upModifierY = 1.26f;
    [SerializeField] float coDurationUp, coDurationDown;

    void Start()
    {
        stackList.Add(sphere);
    }
  IEnumerator CoJump(Collider other,int upOrDown,float duration)
    {
        int stackCount = stackList.Count;
    
        for (int i = stackCount-1; i >=0; i--)
        {
            WaitForSeconds wait = new WaitForSeconds(duration);
           
            stackList[i].DOLocalJump(new Vector3(0, upModifierY*upOrDown, 0f), 1, 1, _jumpDuration).SetRelative().SetEase(Ease.InSine);
           
            yield return wait;
        
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="greenCube")
        {
            StartCoroutine(CoJump(other,1,coDurationUp));
            other.transform.SetParent(transform);
            stackList.Add(other.transform);
            other.transform.DOLocalMove(Vector3.zero, _jumpDuration);
            other.enabled = false;
        }

        if (other.tag == "obstacle")
        {
          
            if (stackList.Count>0)
            {

                stackList[stackList.Count - 1].SetParent(transform.parent);
                stackList.RemoveAt(stackList.Count - 1);

                if (stackList.Count==0)
                {
                    transform.GetComponent<CharacterController>().Speed = 0; //GameOver
                }
                StartCoroutine(WaitForFall(other));
             
            }
            other.enabled = false;
        }
    }
    IEnumerator WaitForFall(Collider other)
    {
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(CoJump(other, -1,coDurationDown));
    }

   
}
