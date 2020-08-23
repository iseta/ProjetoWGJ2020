using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{

    public event System.Action<Block> OnBlockPressed;
    public event System.Action OnFinishedMoving;
    public Vector2Int coord;

    Vector2Int startingCoord;
    MeshRenderer selfMesh;
    bool isCorrect;

    public void Init (Vector2Int startingCoord, Texture2D image)
    {
        selfMesh = GetComponent<MeshRenderer>();
        this.startingCoord = startingCoord;
        coord = startingCoord;

        selfMesh.material = Resources.Load<Material>("Block");
        selfMesh.material.mainTexture = image;
        IsAtStartingCoord();
    }

    public void MoveToPosition(Vector2 target, float duration)
    {
        StartCoroutine(AnimateMove(target, duration));
    }

    void OnMouseDown()
    {
        if (!IsPointerOverUIObject())
        {
            OnBlockPressed?.Invoke(this);
        }
    }

    IEnumerator AnimateMove(Vector2 target, float duration)
    {
        Vector2 initialPos = transform.position;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime / duration;
            transform.position = Vector2.Lerp (initialPos, target, percent);
            yield return null;
        }
        IsAtStartingCoord();
        OnFinishedMoving?.Invoke();
    }

    IEnumerator AnimateGrayscale (float duration, bool isGrayScale)
    {
        float time = 0;
        while (duration > time)
        {
            float durationFrame = Time.deltaTime;
            float ratio = time / duration;
            float grayAmount = isGrayScale
                ? ratio
                : 1 - ratio;
            SetGrayscale(grayAmount);
            time += durationFrame;
            yield return null;
        }
        SetGrayscale(isGrayScale? 1:0);
    }

    private void SetGrayscale (float amount = 1)
    {
        selfMesh.material.SetFloat("_GrayscaleAmount", amount);
    }


    public bool IsAtStartingCoord()
    {
        if (coord == startingCoord && gameObject.activeSelf)
        {
            StartCoroutine(AnimateGrayscale(.2f, false));
            isCorrect = true;
        }
        else
        {
            if (isCorrect && gameObject.activeSelf)
            {
                StartCoroutine(AnimateGrayscale(.2f, true));
                isCorrect = false;
            }
        }
        return coord == startingCoord;

    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}