namespace YetAnotherMessenger.MVVM.Models;

public class VideoMessage(byte[] video) : Message
{
    public byte[] Video { get; set; } = video;
}