namespace YetAnotherMessenger.MVVM.Models;

public class VideoMessage(byte[] video) : Message("Видео сообщение")
{
    public byte[] Video { get; set; } = video;
}