using Iot.Device.Buzzer;
using Iot.Device.Buzzer.Samples;

public class Jukebox
{
    public Jukebox()
    {
        IList<MelodyElement> melody = new List<MelodyElement>()
        {
            new NoteElement(Note.C, Octave.Fourth, Duration.Quarter),
        };

        using var player = new MelodyPlayer(new Buzzer(4));

        Task.WaitAll(
            Task.Run(() => player.Play(melody, 100)));
    }
}
