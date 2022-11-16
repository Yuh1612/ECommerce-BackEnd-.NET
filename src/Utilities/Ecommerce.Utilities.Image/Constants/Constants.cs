namespace Ecommerce.Utilities.Image.Constants
{
    public static class Messagge
    {
        public const string ErrorMaxFileSize = "Maximum allowed file size is {0} bytes";

        public const string ErrorFileExtension = "This {0} extension is not allowed";
    }

    public static class Url
    {
        public const string ImageUrl = "{0}/{1}/{2}";
    }

    public static class FileExtension
    {
        public static readonly string[] ImageExtensions = new string[]
        {
            ".jpg",
            ".png",
            ".gif",
            ".webp",
            ".tiff",
            ".psd",
            ".raw",
            ".bmp",
            ".heif",
            ".indd",
            ".svg",
            ".ai",
            ".eps",
            ".jfif"
        };
    }
}