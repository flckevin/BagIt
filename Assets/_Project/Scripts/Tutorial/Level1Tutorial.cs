using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Level_One_Tutorial : TutorialCore
{
    public Transform targetTrans;
    public Transform hand;
    private Vector3 targetRootTrans;

    protected override void Start()
    {
        base.Start();

         targetRootTrans = targetTrans.position;

        if(hand != null)
        {
            DOTween.Sequence().Append( hand.DOScale(new Vector3(0.9f,0.9f,hand.transform.localScale.z),1f))
                            .Append(hand.DOScale(new Vector3(1,1,hand.transform.localScale.z),1f))
                            .OnComplete( () => hand.DOScale(new Vector3(0.9f,0.9f,hand.transform.localScale.z),1f))
                            .SetLoops(-1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(targetTrans.position != targetRootTrans)
        {
            hand.gameObject.SetActive(false);
            tutorialTextParentPrefab.gameObject.SetActive(false);
            this.enabled = false;
        }
        
    }
}
