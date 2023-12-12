using System.Device.I2c;

public class PCA9685 : IPCA9685
{
    private const byte MODE1 = 0x00;
    private const byte PRESCALE = 0xFE;
    private const byte LED0_ON_L = 0x06;
    private const byte LED0_OFF_L = 0x08;

    private I2cDevice device;

    public PCA9685(int address = 0x40)
    {
        var settings = new I2cConnectionSettings(1, address); // Bus ID 1 is usually used on Raspberry Pi
        device = I2cDevice.Create(settings);

        Console.WriteLine("Resetting PCA9685");
        Write(MODE1, 0x00);
    }

    private void Write(byte reg, byte value)
    {
        device.Write(new byte[] { reg, value });
        Console.WriteLine($"I2C: Write 0x{value:X2} to register 0x{reg:X2}");
    }

    private byte Read(byte reg)
    {
        byte[] writeBuffer = new byte[] { reg };
        byte[] readBuffer = new byte[1];
        device.WriteRead(writeBuffer, readBuffer);
        byte result = readBuffer[0];
        Console.WriteLine($"I2C: Device 0x{device.ConnectionSettings.DeviceAddress:X2} returned 0x{result:X2} from reg 0x{reg:X2}");
        return result;
    }

    public void SetPWMFreq(int freq)
    {
        double prescaleval = 25000000.0; // 25MHz
        prescaleval /= 4096.0; // 12-bit
        prescaleval /= freq;
        prescaleval -= 1.0;
        byte prescale = (byte)Math.Floor(prescaleval + 0.5);

        Console.WriteLine($"Setting PWM frequency to {freq} Hz");
        Console.WriteLine($"Estimated pre-scale: {prescaleval}");
        Console.WriteLine($"Final pre-scale: {prescale}");

        byte oldmode = Read(MODE1);
        byte newmode = (byte)((oldmode & 0x7F) | 0x10); // sleep
        Write(MODE1, newmode); // go to sleep
        Write(PRESCALE, prescale);
        Write(MODE1, oldmode);
        Thread.Sleep(5);
        Write(MODE1, (byte)(oldmode | 0x80));
    }

    public void SetServoPulse(int channel, int pulse)
    {
        int on = 0; // Always starts at 0
        int off = pulse * 4096 / 20000; // Convert pulse length to off time
        SetPWM(channel, on, off);
    }

    private void SetPWM(int channel, int on, int off)
    {
        Write((byte)(LED0_ON_L + 4 * channel), (byte)(on & 0xFF));
        Write((byte)(LED0_ON_L + 4 * channel + 1), (byte)(on >> 8));
        Write((byte)(LED0_OFF_L + 4 * channel), (byte)(off & 0xFF));
        Write((byte)(LED0_OFF_L + 4 * channel + 1), (byte)(off >> 8));
        Console.WriteLine($"channel: {channel}  LED_ON: {on} LED_OFF: {off}");
    }
}
