namespace StudentAdminPortal.API.Repositories
{
    public class LocalStorageImageRepository : IImageRepository
    {
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\images",fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);

                return GetServerRelativePath(fileName);
            }
        }

        private string GetServerRelativePath(string fileName)
        {
            return Path.Combine(@"Resources\images", fileName);
        }
    }
}
