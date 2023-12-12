public class CameraControl : ICameraController
{
    private const int HorizontalChannel = 0; // Assuming channel 0 for horizontal movement
    private const int VerticalChannel = 1;   // Assuming channel 1 for vertical movement

    private PCA9685 pwmController;
    private int horizontalPulse;
    private int verticalPulse;
    private const int PulseStep = 5;
    private const int MinPulse = 500;
    private const int MaxPulse = 2500;

    public CameraControl()
    {
        pwmController = new PCA9685(0x40);
        pwmController.SetPWMFreq(50);

        // Initialize with default central positions
        horizontalPulse = 1500;
        verticalPulse = 1500;
        pwmController.SetServoPulse(HorizontalChannel, horizontalPulse);
        pwmController.SetServoPulse(VerticalChannel, verticalPulse);
    }

    public void PanLeft()
    {
        horizontalPulse += PulseStep;
        if (horizontalPulse > MaxPulse) horizontalPulse = MaxPulse;
        pwmController.SetServoPulse(HorizontalChannel, horizontalPulse);
    }

    public void PanRight()
    {
        horizontalPulse -= PulseStep;
        if (horizontalPulse < MinPulse) horizontalPulse = MinPulse;
        pwmController.SetServoPulse(HorizontalChannel, horizontalPulse);
    }

    public void TiltUp()
    {
        verticalPulse -= PulseStep;
        if (verticalPulse < MinPulse) verticalPulse = MinPulse;
        pwmController.SetServoPulse(VerticalChannel, verticalPulse);
    }

    public void TiltDown()
    {
        verticalPulse += PulseStep;
        if (verticalPulse > MaxPulse) verticalPulse = MaxPulse;
        pwmController.SetServoPulse(VerticalChannel, verticalPulse);
    }
}
