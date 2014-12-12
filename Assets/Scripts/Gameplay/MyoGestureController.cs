using System.Collections.Generic;
using UnityEngine;
using Myo = Thalmic.Myo.Myo;
using Pose = Thalmic.Myo.Pose;
using LockingPolicy = Thalmic.Myo.LockingPolicy;

public enum SyncStatus
{
    Synced,
    Paired,
    Unpaired
}

public class MyoGestureController : MonoBehaviour
{
    public GameObject myo = null;
    public Limb limb;
    public GestureController gestureController;

    public delegate void MyoGesture();

    public event MyoGesture OnMyoGesture;

    public delegate void MyoStatus(SyncStatus status);

    public event MyoStatus OnMyoStatus;

    private ThalmicMyo thalmicMyo;
    private Pose _lastPose = Pose.Unknown;
    private bool _lastSync = false;
    private bool _lastPaired = false;

    private bool myoFound = false;

    private Dictionary<Pose, Gesture> poseToGesture = new Dictionary<Pose, Gesture>()
    {
        {Pose.FingersSpread, Gesture.FingersSpread},
        {Pose.Fist, Gesture.Fist},
        {Pose.DoubleTap, Gesture.DoubleTap},
        {Pose.WaveIn, Gesture.WaveIn},
        {Pose.WaveOut, Gesture.WaveOut},
        {Pose.Rest, Gesture.None},
        {Pose.Unknown, Gesture.None}
    };

    private void OnEnable()
    {
        thalmicMyo = myo.GetComponent<ThalmicMyo>();
        foreach (KeyValuePair<Pose, Gesture> kvp in poseToGesture)
        {
            if (kvp.Value != Gesture.None)
            {
                gestureController.Gestures[limb][kvp.Value] = false;
            }
        }
        OnMyoGesture += ControllerPoseUpdate;
    }

    private void OnDisable()
    {
        OnMyoGesture -= ControllerPoseUpdate;
        if (thalmicMyo.internalMyo != null)
        {
            thalmicMyo.internalMyo.PoseChange -= internalMyo_PoseChange;
        }
    }

    private void Update()
    {
        //Disable this code for now since there's some issues with threading
        //if (thalmicMyo.internalMyo != null)
        //{
        //    if (!myoFound)
        //    {
        //        print("Huzzah, found a proper Myo!");
        //        thalmicMyo.internalMyo.PoseChange += internalMyo_PoseChange;
        //        myoFound = true;
        //    }
        //}
        //else
        //{
        //    internalMyo_PoseChange(null, null);
        //    myoFound = false;
        //}

        internalMyo_PoseChange(null, null);

        if((thalmicMyo.armSynced != _lastSync) || (thalmicMyo.isPaired != _lastPaired))
        {
            _lastSync = thalmicMyo.armSynced;
            _lastPaired = thalmicMyo.isPaired;

            if (OnMyoStatus != null)
            {
                if (_lastSync)
                {
                    OnMyoStatus(SyncStatus.Synced);
                }
                else if (_lastPaired)
                {
                    OnMyoStatus(SyncStatus.Paired);
                }
                else
                {
                    OnMyoStatus(SyncStatus.Unpaired);
                }
            }
        }
    }

    private void internalMyo_PoseChange(object sender, Thalmic.Myo.MyoEventArgs e)
    {
        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;

            if (OnMyoGesture != null)
            {
                OnMyoGesture();
            }
        }
    }

    private void ControllerPoseUpdate()
    {
        /* Thalmic Gestures are mutually exclusive.  So run through the poseToGesture dictionary and set everything which isn't currently active to false */
        foreach (KeyValuePair<Pose, Gesture> kvp in poseToGesture)
        {
            if (kvp.Value != Gesture.None)
            {
                gestureController.Gestures[limb][kvp.Value] = (kvp.Key == thalmicMyo.pose) ? true : false;
            }
        }

        //print (thalmicMyo.name + " gesture: " + poseToGesture[thalmicMyo.pose] + " - " + gestureController.Gestures[limb][poseToGesture[thalmicMyo.pose]]);

        /* Now call the LL Gesture event */
        gestureController.fireLLGesture();
    }
}