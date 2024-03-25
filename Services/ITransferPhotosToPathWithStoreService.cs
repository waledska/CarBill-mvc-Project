namespace CarBill.Services
{
    public interface ITransferPhotosToPathWithStoreService
    {
        List<string> GetPhotosPath(List<IFormFile> model);
        string SaveVideoFile(IFormFile videoFile);
        bool DeleteFile(string path);
    }
}
