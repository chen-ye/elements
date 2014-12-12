using UnityEngine;
using UnityEngine.UI;



public class syncStatusManager : MonoBehaviour
{
    public MyoGestureController leftArm = null;
    public MyoGestureController rightArm = null;
    public MyoGestureController leftLeg = null;
    public MyoGestureController rightLeg = null;

    public Image leftArmIndicator = null;
    public Image rightArmIndicator = null;
    public Image leftLegIndicator = null;
    public Image rightLegIndicator = null;

    public Sprite syncedSprite = null;
    public Sprite pairedSprite = null;
    public Sprite unpairedSprite = null;

    // Use this for initialization
    private void Start()
    {
    }

    private void OnEnable()
    {
        leftArm.OnMyoStatus += setLeftArmStatus;
        rightArm.OnMyoStatus += setRightArmStatus;
        leftLeg.OnMyoStatus += setLeftLegStatus;
        rightLeg.OnMyoStatus += setRightLegStatus;
    }

    private void OnDisable()
    {
        leftArm.OnMyoStatus -= setLeftArmStatus;
        rightArm.OnMyoStatus -= setRightArmStatus;
        leftLeg.OnMyoStatus -= setLeftLegStatus;
        rightLeg.OnMyoStatus -= setRightLegStatus;
    }

    private void setLeftArmStatus(SyncStatus status)
    {
        setStatus(leftArmIndicator, status);
    }

    private void setRightArmStatus(SyncStatus status)
    {
        setStatus(rightArmIndicator, status);
    }

    private void setLeftLegStatus(SyncStatus status)
    {
        setStatus(leftLegIndicator, status);
    }

    private void setRightLegStatus(SyncStatus status)
    {
        setStatus(rightLegIndicator, status);
    }

    private void setStatus(Image indicator, SyncStatus status)
    {
        switch (status) {
            case SyncStatus.Paired:
                indicator.sprite = pairedSprite;
                break;
            case SyncStatus.Unpaired:
                indicator.sprite = unpairedSprite;
                break;
            case SyncStatus.Synced:
                indicator.sprite = syncedSprite;
                break;
        }
    }
}