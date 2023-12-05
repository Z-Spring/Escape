using TMPro;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public bool HasEatFood { get; set; }

    [SerializeField] private Transform interactUI;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TextMeshProUGUI tipText;
    [SerializeField] private float interactDistance = 1.5f;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material transparentMaterial;

    private bool hasAddedTipMessage;

    private void Start()
    {
        tipText = interactUI.Find("TipText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        CheckForPlayerInteraction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snake"))
        {
            HasEatFood = true;
            gameObject.SetActive(false);
        }
    }

    private void CheckForPlayerInteraction()
    {
        if (IsPlayerNearby() && !PlayerController.Instance.HasCollection())
        {
            ShowInteractUI();
            PrintInteractionTip();
            CheckForHoldInput();
        }
        else
        {
            HideInteractUI();
        }
    }

    private bool IsPlayerNearby()
    {
        return Physics.OverlapSphere(transform.position, interactDistance, LayerMask.GetMask("Player")).Length > 0;
    }

    private void ShowInteractUI()
    {
        interactUI.gameObject.SetActive(true);
    }

    private void HideInteractUI()
    {
        interactUI.gameObject.SetActive(false);
    }

    private void PrintInteractionTip()
    {
        if (!hasAddedTipMessage)
        {
            PrintTipMessage.Instance.SetPrintMessageInfos("press E to hold it, press F to throw it.", tipText);
            hasAddedTipMessage = true;
        }
    }

    private void CheckForHoldInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleCollection();
        }
    }

    private void HandleCollection()
    {
        PlayerController.Instance.SetCollection(transform);
        ChangeCollectionMaterial(transparentMaterial);
        AttachToHoldPoint();
        HideInteractUI();
    }

    private void ChangeCollectionMaterial(Material targetMaterial)
    {
        Material[] materials = meshRenderer.materials;
        materials[1] = targetMaterial;
        meshRenderer.materials = materials;
    }

    private void AttachToHoldPoint()
    {
        Transform holdCollectionPoint = PlayerController.Instance.holdCollectionPoint;
        transform.SetParent(holdCollectionPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rb.isKinematic = true;
    }
}