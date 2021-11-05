using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetControl : MonoBehaviour
{
    [Header("Helmet Interactable")]
    public HelmetInteractable helmetInteractable;

    [Header("Helmet Move Settings")]
    public float rotateSpeed;
    public float horizontalLimit;
    public float verticalLimit;
    private Vector3 currentRotation;

    [Header("Laser Settings")]
    public LineRenderer lineRender;
    public Transform lineStartPoint;
    private Vector3 lineStartPosition;
    private int layerMask;

    [Header("Ignite Settings")]
    public Collider targetCollider;
    public GameObject fireEffect;
    public AudioSource fireAudio;
    public float targetFocusTime;
    private float onTargetTimer;
    private bool alreadyIgnited;

    void Start()
    {
        currentRotation = transform.localRotation.eulerAngles;
        layerMask = ~(1 << 9);

        lineStartPosition = lineStartPoint.position;
        lineRender.SetPosition(0, lineStartPoint.localPosition);

        alreadyIgnited = false;
    }

    void Update()
    {
        ControlHelmet();
        DrawLine();
        CheckTargetCollider();
    }

    void ControlHelmet()
    {
        float horizontalDelta = rotateSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        float verticalDelta = -rotateSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        currentRotation.x = Mathf.Clamp(currentRotation.x + verticalDelta, -verticalLimit, verticalLimit);
        currentRotation.y = Mathf.Clamp(currentRotation.y + horizontalDelta, -horizontalLimit, horizontalLimit);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    void DrawLine()
    {
        RaycastHit hit;
        if (Physics.Raycast(lineStartPosition, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            lineRender.SetPosition(1, transform.InverseTransformPoint(hit.point));
        }
    }

    void CheckTargetCollider()
    {
        if (targetCollider.gameObject.activeInHierarchy == false || alreadyIgnited == true)
        {
            return;
        }

        RaycastHit hit;
        if (targetCollider.Raycast(new Ray(lineStartPosition, transform.forward), out hit, Mathf.Infinity))
        {
            onTargetTimer += Time.deltaTime;
        }
        else
        {
            onTargetTimer = 0.0f;
        }

        if (onTargetTimer > targetFocusTime)
        {
            StartCoroutine(Ignite());
        }
    }

    IEnumerator Ignite()
    {
        alreadyIgnited = true;
        fireAudio.Play();

        yield return new WaitForSeconds(0.7f);

        fireEffect.SetActive(true);

        yield return new WaitForSeconds(2);

        lineRender.enabled = false;
        helmetInteractable.FinishInteracting();
    }
}
