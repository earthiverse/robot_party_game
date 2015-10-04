using Windows.Kinect;
using JetBrains.Annotations;
using UnityEngine;

    public class RobotKinectMovement : MonoBehaviour
    {
        protected Body[] Bodies;

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

        public KinectSensor KinectSensor;
        public BodyFrameReader Reader;

        [UsedImplicitly]
        private void Start()
        {
            // Setup the Kinect Sensor
            KinectSensor = KinectSensor.GetDefault();
            if (KinectSensor != null)
            {
                Debug.Log("Got Sensor!");
                KinectSensor.Open();
                Debug.Log("Opened sensor");
                Reader = KinectSensor.BodyFrameSource.OpenReader();
                Debug.Log("Opened Frame Reader");

                if (!KinectSensor.IsOpen)
                {
                    Debug.Log("Opening Sensor (again...)");
                    KinectSensor.Open();
                }

                // Create enough storage for however many the Kinect can track
                Bodies = new Body[KinectSensor.BodyFrameSource.BodyCount];
            }
        }

        void Update()
        {
            using (BodyFrame frame = Reader.AcquireLatestFrame())
            {
                if (frame != null)
                {
                    //Debug.Log("Hi not null frame");
                    // Frame is usable
                    frame.GetAndRefreshBodyData(Bodies);
                    frame.Dispose();

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
                            // Head = Neck -> Head
                            Head.transform.localEulerAngles = new Vector3(0, 0,
                                -90 + CalculationsHelper.CalculateAngleDifference(neck, head));
                            // Body = Spine Base -> Spine Shoulder
                            Body.transform.localEulerAngles = new Vector3(0, 0,
                                -90 + CalculationsHelper.CalculateAngleDifference(spineBase, spineShoulder));

                            // even if there's more bodies in the scene, we found one, which is good enough.
                            return;
                        }
                    }
                }
            }
            
        }
    }