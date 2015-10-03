using System;
using Windows.Kinect;

public class CalculationsHelper {
    public static float CalculateAngleDifference(CameraSpacePoint a, CameraSpacePoint b)
    {
        float xDiff = b.X - a.X;
        float yDiff = b.Y - a.Y;
        return (float) (Math.Atan2(yDiff, xDiff)*(180/Math.PI));
    }
}
