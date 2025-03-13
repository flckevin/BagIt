
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level_Two_Tutorial : TutorialCore
{
    public GameObject handImg;
    public GameObject hand_G;
    public GameObject[] target;
    public int _defaultAmount;
    private Sequence _tutSeq;
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();

        

        //get hand image
        handImg = tutorialTextParentPrefab.transform.GetChild(1).gameObject;

        //add sequence to hand image
        _tutSeq = DOTween.Sequence()

                        .Append(handImg.transform.DOScale(new Vector3(1,1,handImg.transform.localScale.z),1f))
                        .Append(handImg.transform.DOScale(new Vector3(1.5f,1.5f,handImg.transform.localScale.z),1f))
                        .AppendCallback(() => {handImg.SetActive(false);})
                        .AppendCallback(() => {hand_G.SetActive(true);})

                        .Append(hand_G.transform.DOScale(new Vector3(0.8f,0.8f,hand_G.transform.localScale.z),1f))
                        .Append(hand_G.transform.DOScale(new Vector3(1,1,hand_G.transform.localScale.z),1f))

                        .Append(hand_G.transform.DOMoveX(target[1].transform.position.x,1f))

                        .Append(hand_G.transform.DOScale(new Vector3(0.8f,0.8f,hand_G.transform.localScale.z),1f))
                        .Append(hand_G.transform.DOScale(new Vector3(1,1,hand_G.transform.localScale.z),1f))

                        .Append(hand_G.transform.DOMoveX(target[0].transform.position.x,1f))
                        .AppendInterval(0.5f)
                        .AppendCallback(() => {handImg.SetActive(true);})
                        .AppendCallback(() => {hand_G.SetActive(false);})

                        .SetLoops(-1);

        
        ItemExtension.AddSwap(1,GameManager.Instance.gameMenu.swapAmountText);

        _defaultAmount = ItemData.SwapAmount;

        Debug.Log($"AMOUNT {_defaultAmount}");
    }

    void Update()
    {
        if(_defaultAmount > ItemData.SwapAmount)
        {
            _tutSeq.Kill();
            tutorialTextParentPrefab.gameObject.SetActive(false);
            hand_G.gameObject.SetActive(false);
            handImg.SetActive(false);
            this.enabled = false;
            
        }
    }

}
