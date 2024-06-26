using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransformTransitionUseDotween : MonoBehaviour
{
    [SerializeField] GameObject[] children;

    GameObject dotweenSettingObject;

    Vector3 rotation;
    int duration;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < children.Length; i++) 
        {
            Vector3 move = children[i].transform.position + new Vector3(0, 2, 0);

            int random = Random.Range(0, 10);

            if (random >= 0 && random <= 3)
            {
                rotation = new Vector3(0, 15, 0);
                duration = 2;
            }
            else if (random >= 4 && random <= 7)
            {
                rotation = new Vector3(15, 0, 0);
                duration = 3;
            }
            else
            {
                rotation = new Vector3(0, 0, 15);
                duration = 4;
            }

            children[i].transform.DORotate(rotation, duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            children[i].transform.DOMove(move, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        dotweenSettingObject = GameObject.Find("[DOTween]");
        dotweenSettingObject.tag = "Setting";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
