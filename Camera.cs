using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Text;

class Camera{
    private VideoCapture capture;
    private Mat frame;


    public Camera(){
        capture = new VideoCapture(0, VideoCapture.API.DShow);
        if (!capture.IsOpened)
            Console.WriteLine("Error: Unable to open camera.");

        frame = new Mat();
    }


    public void RunCamera(){
        while (true)
            {
                capture.Read(frame); // Read a frame from the camera

                if (frame.IsEmpty)
                {
                    Console.WriteLine("Error: Frame is empty.");
                    break;
                }

                // Access raw pixel data (BGR format)
                //byte[] data = new byte[frame.Width * frame.Height * frame.NumberOfChannels];
                //frame.CopyTo(data);

                // You can process or print the raw data here
                //Console.WriteLine("Raw data: " + string.Join(", ", data));


                // Convert the frame to grayscale
                CvInvoke.CvtColor(frame, frame, ColorConversion.Bgr2Gray);

                // Convert the grayscale frame to ASCII art
                string asciiArt = ConvertToAsciiArt(frame, 80, 40);

                // Print the ASCII art to the console
                Console.Clear();
                Console.WriteLine(asciiArt);

                // Optionally, add a delay to control the frame rate
                // System.Threading.Thread.Sleep(100); // Add a 100ms delay
            }
    }


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

}