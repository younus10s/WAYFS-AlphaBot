public interface ICamera
{
    Task TakePicture();
}

public interface IMotor
{
    void SetPower(double dutyCycle);
    void Forward();
    void Backward();
    void Stop();
    void StopPwm();
}

public interface IMotionControl
{
    void Left(double power);
    void Right(double power);
    void Forward(double power);
    void Stop();
    void SetPowerLeft(double power);
    void SetPowerRight(double power);
    void CleanUp();
    void ActivateForward();
    void ActivateBackward();
}

public interface ITRSensor
{
    int[] ReadLine();
    void Calibrate(IMotionControl motionControl);
    double GetPosition();
}