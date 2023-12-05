using MMALSharp;
using MMALSharp.Common;
using MMALSharp.Handlers;

public class Camera
{
    public async Task TakePicture()
    {
        MMALCamera cam = MMALCamera.Instance;

        try
        {
            cam.ConfigureCameraSettings();

            using var imgCaptureHandler = new ImageStreamCaptureHandler("../", "jpg");

            await cam.TakePicture(imgCaptureHandler, MMALEncoding.JPEG, MMALEncoding.I420);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            cam.Cleanup();
        }
    }
}