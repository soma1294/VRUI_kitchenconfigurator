using UnityEngine;
using System.Collections;


//[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(OVRGrabbable))]
public class Snappable : MonoBehaviour
{
    public bool inHand = false;
    public bool parentInHand = false;
    public GameObject snapColliders;
    public GameObject previewBoxPrefab;
    public Vector3Int snapEnabled = Vector3Int.zero;

    public SnapBehaviour snapObject;
    public Vector3 snapPosition;
    public Quaternion snapRotation;

    public KitchenElement kitchenFurniture;

    private OVRGrabbable ovrGrababble;
    public OVRGrabber hoverGrabber = null;

    //private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);
    private GameObject previewBox;

    private void Start()
    {
        //Changed
        inHand = false;
        kitchenFurniture = this.GetComponent<KitchenElement>();
        snapPosition = this.transform.position;
        snapRotation = this.transform.rotation;
        ovrGrababble = GetComponent<OVRGrabbable>();
    }

    private void Update()
    {
        /*
        if (isBeeingHovered)
        {
            hoverGrabber = ovrGrababble.hoveredBy;
            HandHoverUpdate(hoverGrabber);
        }
        */
        if (inHand)
        {
            if (this.previewBox == null)
            {
                this.previewBox = Instantiate(previewBoxPrefab);
            }
            if (snapEnabled.magnitude > 0)
            {
                previewBox.SetActive(true);
            }
            else
            {
                previewBox.SetActive(false);
            }
            if (snapEnabled.x == 0)
            {
                snapPosition.x = this.transform.position.x;
            }
            if (snapEnabled.y == 0)
            {
                snapPosition.y = this.transform.position.y;
            }
            if (snapEnabled.z == 0)
            {
                snapPosition.z = this.transform.position.z;
            }
            previewBox.transform.localScale = this.transform.localScale * 0.99f;
            previewBox.transform.position = snapPosition;
            previewBox.transform.rotation = snapRotation;
        }
        else
        {
            if (this.previewBox != null)
            {
                Destroy(this.previewBox);
            }
        }
    }

    public void HandHoverUpdate(OVRGrabber grabber)
    {
        bool grabButtonDown = false;
        bool grabberIsLeftHand = grabber.gameObject.CompareTag("Left Hand");
        bool grabberIsRightHand = grabber.gameObject.CompareTag("Right Hand");
        if (grabberIsLeftHand)
        {
            grabButtonDown = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) > 0.1f;
        } else if (grabberIsRightHand)
        {
            grabButtonDown = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0.1f;
        }
        if (grabButtonDown)
        {
            if (grabber.grabbedObject != gameObject)
            {
                //this.transform.parent = null;
                // Call this to continue receiving HandHoverUpdate messages,
                // and prevent the hand from hovering over anything else
                //hand.HoverLock(GetComponent<Interactable>());

                // Attach this object to the hand
                //hand.AttachObject(gameObject, startingGrabType, attachmentFlags);

                inHand = true;
                Snappable[] childs = this.GetComponentsInChildren<Snappable>();
                for (int i = 0; i < childs.Length; i++)
                {
                    childs[i].parentInHand = true;
                    childs[i].snapColliders.SetActive(false);
                }
                parentInHand = false;
            }
        }
        if ((grabberIsRightHand && OVRInput.GetUp(OVRInput.RawButton.RHandTrigger)) || (grabberIsLeftHand && OVRInput.GetUp(OVRInput.RawButton.LHandTrigger)))
        {
            // Detach this object from the hand
            //hand.DetachObject(gameObject);

            // Call this to undo HoverLock
            //hand.HoverUnlock(GetComponent<Interactable>());

            this.transform.position = snapPosition;
            if (snapEnabled == Vector3Int.zero)
            {
                snapRotation.eulerAngles = new Vector3(0, 90 * Mathf.RoundToInt(this.transform.rotation.eulerAngles.y / 90f), 0);
            }
            this.transform.rotation = snapRotation;

            this.transform.parent = snapObject?.children?.transform;



            inHand = false;
            Snappable[] childs = this.GetComponentsInChildren<Snappable>();
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i].parentInHand = false;
                childs[i].snapColliders.SetActive(true);
            }
            KitchenElement[] elements = FindObjectsOfType<KitchenElement>();
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i].redraw = true;
            }
        }
    }

    private void OnDisable()
    {
        if (this.previewBox != null)
        {
            Destroy(this.previewBox);
        }
    }
}
