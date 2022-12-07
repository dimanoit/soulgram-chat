using Soulgram.Chat.Domain.Enums;
using Soulgram.Chat.Domain.Models;

namespace Soulgram.Chat.Services.Converters;

public static class FileConvertor
{
    public static AttachmentType ToAttachmentType(this string contentType)
    {
        var markerWithoutFileExtension = TrimToOnlyContentType(contentType);

        switch (markerWithoutFileExtension)
        {
            case KnownContentTypes.Image: return AttachmentType.Image;
            case KnownContentTypes.Text: return AttachmentType.Text;
            case KnownContentTypes.Video: return AttachmentType.Video;
        }

        throw new ArgumentOutOfRangeException(contentType, $"Not supported content type {contentType}");
    }

    private static string TrimToOnlyContentType(string contentType)
    {
        // Trim like "text/csv" -> "text" 
        return contentType.Substring(
            0,
            contentType.IndexOf("/", StringComparison.Ordinal));
    }
}