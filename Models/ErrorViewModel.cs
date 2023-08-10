namespace Task_2.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public int? numId { get; set;}

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
public class ImageInfo
{
    public string? Ans { get; set; }
    public string? Link { get; set; } = "";

    public int? id { get; set;}
}
