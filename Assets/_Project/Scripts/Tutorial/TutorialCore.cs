using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCore : MonoBehaviour
{
    public RectTransform tutorialTextParentPrefab;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        RectTransform _tutorialTextP = Instantiate(tutorialTextParentPrefab,GameManager.Instance.canvas.transform);
        _tutorialTextP.offsetMin = Vector2.zero;
        _tutorialTextP.offsetMax = Vector2.zero;
        tutorialTextParentPrefab = _tutorialTextP;
    }

}
