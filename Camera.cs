using MMALSharp;
using MMALSharp.Common;
using MMALSharp.Handlers;

public class Camera{


    public Camera(){
        
    }

    public async Task TakePicture(){
        MMALCamera cam = MMALCamera.Instance;

        try
        {
            // Configure the camera settings
            cam.ConfigureCameraSettings();

            // Create an image capture handler
            using (var imgCaptureHandler = new ImageStreamCaptureHandler("../", "jpg"))
            {
                //Thread.Sleep(1000);
                // Capture an image
                Console.WriteLine("Ready");
                await cam.TakePicture(imgCaptureHandler, MMALEncoding.JPEG, MMALEncoding.I420);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Cleanup resources
            cam.Cleanup();
        }
    }

    /*
    private string ConvertToAsciiArt(Mat frame, int width, int height)
    {
        StringBuilder asciiArt = new StringBuilder();

        // Resize the frame to the specified width and height
        CvInvoke.Resize(frame, frame, new System.Drawing.Size(width, height));

        // Access pixel data as a byte array
        byte[] data = new byte[frame.Width * frame.Height];
        frame.CopyTo(data);

        int dataIndex = 0;

        for (int y = 0; y < frame.Height; y++)
        {
            for (int x = 0; x < frame.Width; x++)
            {
                int intensity = data[dataIndex++];
                char asciiChar = GetAsciiChar(intensity);
                asciiArt.Append(asciiChar);
            }
            asciiArt.AppendLine();
        }

        return asciiArt.ToString();
    }

    private char GetAsciiChar(int intensity)
    {
        // Define a mapping of intensity values to ASCII characters
        char[] asciiChars = { ' ', '.', ':', '-', '=', '+', '*', '#', '%', '8', '@' };

        // Normalize intensity to fit within the range of ASCII characters
        int index = (intensity * (asciiChars.Length - 1)) / 255;

        return asciiChars[index];
    }
    */

}