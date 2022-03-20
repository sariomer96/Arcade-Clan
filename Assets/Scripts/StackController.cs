using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StackController : MonoBehaviour
{
    List<Transform> stackList = new List<Transform>();
    [SerializeField] Transform sphere;
    [SerializeField] float upModifierY = 1.26f;
     
    void Start()
    {
        stackList.Add(sphere);
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="greenCube")
        {

          
            foreach (var item in stackList)
            {
                item.DOLocalJump(new Vector3(0, upModifierY, 0f), 1, 1, 0.35f).SetRelative().SetEase(Ease.InSine);
            }
           
            other.transform.SetParent(transform);
            stackList.Add(other.transform);
        
            other.transform.DOLocalMove(Vector3.zero, 0.35f);

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
                StartCoroutine(wait());
                other.enabled = false;
            }
          
       
         

        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.3f);
        foreach (var item in stackList)
        {
            item.DOLocalJump(new Vector3(0, -upModifierY, 0f), 1, 1, 0.32f).SetRelative().SetEase(Ease.OutSine);
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
