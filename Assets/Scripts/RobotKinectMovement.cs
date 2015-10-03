using System;
using Windows.Kinect;
using UnityEngine;

public class RobotKinectMovement : MonoBehaviour
{
    protected Body[] Bodies;
    protected KinectSensor KinectSensor;

    public GameObject Head;
    public GameObject Body;
    public GameObject UpperLeftArm;
    public GameObject LowerLeftArm;
    public GameObject UpperRightArm;
    public GameObject LowerRightArm;
    public GameObject UpperLeftLeg;
    public GameObject LowerLeftLeg;
    public GameObject UpperRightLeg;
    public GameObject LowerRightLeg;

    private void Start()
    {
        // Setup the Kinect Sensor
        KinectSensor = KinectSensor.GetDefault();
        KinectSensor.Open();
        var reader = KinectSensor.BodyFrameSource.OpenReader();

        // Create enough storage for however many the Kinect can track
        Bodies = new Body[KinectSensor.BodyFrameSource.BodyCount];

        // Update robot positions every time we get new Kinect data
        reader.FrameArrived += UpdateRobotPositions;
    }

    private void UpdateRobotPositions(object sender, BodyFrameArrivedEventArgs e)
    {
        using (var frame = e.FrameReference.AcquireFrame())
        {
            if (frame != null)
            {
                //Debug.Log("Hi not null frame");
                // Frame is usable
                frame.GetAndRefreshBodyData(Bodies);
                foreach (var body in Bodies)
                {
                    if (body.IsTracked)
                    {
                        // We're tracking this body
                        CameraSpacePoint hipRight = body.Joints[JointType.HipRight].Position;
                        CameraSpacePoint kneeRight = body.Joints[JointType.KneeRight].Position;
                        CameraSpacePoint ankleRight = body.Joints[JointType.AnkleRight].Position;
                        CameraSpacePoint hipLeft = body.Joints[JointType.HipLeft].Position;
                        CameraSpacePoint kneeLeft = body.Joints[JointType.KneeLeft].Position;
                        CameraSpacePoint ankleLeft = body.Joints[JointType.AnkleLeft].Position;
                        CameraSpacePoint shoulderLeft = body.Joints[JointType.ShoulderLeft].Position;
                        CameraSpacePoint elbowLeft = body.Joints[JointType.ElbowLeft].Position;
                        CameraSpacePoint wristLeft = body.Joints[JointType.WristLeft].Position;
                        CameraSpacePoint shoulderRight = body.Joints[JointType.ShoulderRight].Position;
                        CameraSpacePoint elbowRight = body.Joints[JointType.ElbowRight].Position;
                        CameraSpacePoint wristRight = body.Joints[JointType.WristRight].Position;
                        CameraSpacePoint neck = body.Joints[JointType.Neck].Position;
                        CameraSpacePoint head = body.Joints[JointType.Head].Position;
                        CameraSpacePoint spineBase = body.Joints[JointType.SpineBase].Position;
                        CameraSpacePoint spineShoulder = body.Joints[JointType.SpineShoulder].Position;

                        // Upper Right Leg - Right Hip -> Right Knee
                        UpperRightLeg.transform.localEulerAngles = new Vector3(0, 0,
                            -270 + CalculationsHelper.CalculateAngleDifference(hipRight, kneeRight));
                        // Lower Right Leg = Right Knee -> Right Ankle
                        LowerRightLeg.transform.localEulerAngles = new Vector3(0, 0,
                            -270 + CalculationsHelper.CalculateAngleDifference(kneeRight, ankleRight));
                        // Upper Left Leg = Left Hip -> Left Knee
                        UpperLeftLeg.transform.localEulerAngles = new Vector3(0, 0,
                            -270 + CalculationsHelper.CalculateAngleDifference(hipLeft, kneeLeft));
                        // Lower Left Leg = Left Knee -> Left Ankle
                        LowerLeftLeg.transform.localEulerAngles = new Vector3(0, 0,
                            -270 + CalculationsHelper.CalculateAngleDifference(kneeLeft, ankleLeft));
                        // Upper Right Arm = Right Shoulder -> Right Elbow
                        UpperRightArm.transform.localEulerAngles = new Vector3(0, 0,
                            CalculationsHelper.CalculateAngleDifference(shoulderRight, elbowRight));
                        // Lower Right Arm = Right Elbow -> Right Wrist
                        LowerRightArm.transform.localEulerAngles = new Vector3(0, 0,
                            CalculationsHelper.CalculateAngleDifference(elbowRight, wristRight));
                        // Upper Left Arm = Left Shoulder -> Left Elbow
                        UpperLeftArm.transform.localEulerAngles = new Vector3(0, 0,
                            -180 + CalculationsHelper.CalculateAngleDifference(shoulderLeft, elbowLeft));
                        // Lower Left Arm = Left Elbow -> Left Wrist
                        LowerLeftArm.transform.localEulerAngles = new Vector3(0, 0,
                            -180 + CalculationsHelper.CalculateAngleDifference(elbowLeft, wristLeft));

                        Head.transform.localEulerAngles = new Vector3(0, 0,
                           -90 + CalculationsHelper.CalculateAngleDifference(neck, head));

                        Body.transform.localEulerAngles = new Vector3(0, 0,
                           -90 + CalculationsHelper.CalculateAngleDifference(spineBase, spineShoulder));

                        

                        // even if there's more bodies in the scene, we found one, which is good enough.
                        return;
                    }
                }
            }
        }
    }

    public void OnMouseDown()
    {
        Debug.LogFormat("Kinect Status: {0}", KinectSensor.IsOpen);

        if (KinectSensor.IsOpen)
        {
            KinectSensor.Close();
        }
        else
        {
            KinectSensor.Open();
        }
    }
}