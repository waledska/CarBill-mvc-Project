using Microsoft.Extensions.Hosting;

namespace CarBill.Services
{
    public class transferPhotoToPathWithStoreService : ITransferPhotosToPathWithStoreService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public transferPhotoToPathWithStoreService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        // for uploading more than one image
        public List<string> GetPhotosPath(List<IFormFile> model)
        {
            var resultPaths = new List<string>();

            if (model == null || !model.Any())
            {
                resultPaths.Add("error, List<IFormFile> model can't be empty");
                return resultPaths;
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var maxFileSizeInBytes = 10 * 1024 * 1024; // 10 MB
            var imagesFolderName = "images"; // Folder name

            foreach (var file in model)
            {
                if (file == null || file.Length == 0)
                {
                    resultPaths.Add("error, IFormFile model can't be empty");
                    return resultPaths;
                }

                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    resultPaths.Add("error, file format should be only { \".jpg\", \".jpeg\", \".png\", \".gif\" }");
                    return resultPaths;
                }

                if (file.Length > maxFileSizeInBytes)
                {
                    resultPaths.Add("error, image size can't be bigger than 10MB");
                    return resultPaths;
                }

                string uniquePhotoName = Guid.NewGuid() + fileExtension;
                // Construct the full path using WebRootPath
                var fullPath = Path.Combine(_hostEnvironment.WebRootPath, imagesFolderName, uniquePhotoName);

                try
                {
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    resultPaths.Add(fullPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving file: {ex.Message}");
                    resultPaths.Add("error, something went wrong!");
                }
            }

            return resultPaths;
        }

        // for uploading one video
        public string SaveVideoFile(IFormFile videoFile)
        {
            if (videoFile == null || videoFile.Length == 0)
            {
                return "error, IFormFile videoFile can't be empty";
            }

            var allowedExtensions = new[] { ".mp4", ".avi", ".mov", ".wmv", ".mkv" };
            var maxFileSizeInBytes = 100 * 1024 * 1024; // 100 MB
            var videosFolderName = "videos"; // Folder name

            var fileExtension = Path.GetExtension(videoFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return "error, file format should be only { \".mp4\", \".avi\", \".mov\", \".wmv\", \".mkv\" }";
            }

            if (videoFile.Length > maxFileSizeInBytes)
            {
                return "error, video size can't be bigger than 100MB";
            }

            string uniqueVideoName = Guid.NewGuid() + fileExtension;
            // Construct the full path using WebRootPath
            var fullPath = Path.Combine(_hostEnvironment.WebRootPath, videosFolderName, uniqueVideoName);

            try
            {
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    videoFile.CopyTo(fileStream);
                }

                return fullPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
                return "error, something went wrong!";
            }
        }


        // delete un needed images 
        public bool DeleteFile(string path)
        {
            // Check if file exists with its full path    
            if (File.Exists(path))
            {
                // If file found, delete it    
                File.Delete(path);
                Console.WriteLine("File deleted.");
                return true;
            }
            else
            {
                Console.WriteLine("File not found");
                return false;
            }
        }


    }
}
