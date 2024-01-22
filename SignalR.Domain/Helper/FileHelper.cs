
namespace MeetingManagement.Domain.Helper
{
    public static class FileHelper
    {
        //public const string GoogleCalenderAttachment = "GoogleCalenderAttachments";
        public static string Upload(IFormFile file, string? folderName = null)
        {
            var wwwRootDirectory = Path.Combine(pLDirectory(), "wwwroot");

            var fileName = $"{GenerateUniqueFileName()}-{Path.GetFileName(file.FileName)}";

            string? filePath;

            if (!string.IsNullOrEmpty(folderName))
                filePath = Path.Combine(wwwRootDirectory, folderName, fileName);
            else
                filePath = Path.Combine(wwwRootDirectory, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }
        public static (string,int) UploadBase64(string base64File,string name, FileExtansions fileExtansions, string? folderName = null)
        {
            var wwwRootDirectory = Path.Combine(pLDirectory(), "wwwroot");

            var fileName = $"{GenerateUniqueFileName()}-{name}.{fileExtansions}";

            string? filePath;

            if (!string.IsNullOrEmpty(folderName))
                filePath = Path.Combine(wwwRootDirectory, folderName, fileName);
            else
                filePath = Path.Combine(wwwRootDirectory, fileName);

            byte[] fileBytes = Convert.FromBase64String(base64File);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            fileStream.Write(fileBytes, 0, fileBytes.Length);

            return (fileName,fileBytes.Length);
        }
        public static void Delete(string fileName, string? folderName = null)
        {
            var wwwRootDirectory = Path.Combine(pLDirectory(), "wwwroot");

            string? filePath;

            if (!string.IsNullOrEmpty(folderName))
                filePath = Path.Combine(wwwRootDirectory, folderName, fileName);
            else
                filePath = Path.Combine(wwwRootDirectory, fileName);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }

        private static string pLDirectory()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var binIndex = baseDirectory.IndexOf("\\bin\\", StringComparison.OrdinalIgnoreCase);

            string pLDirectory = "";

            if (binIndex > 0)
                pLDirectory = baseDirectory.Substring(0, binIndex);

            return pLDirectory;
        }
        private static string GenerateUniqueFileName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
