using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TreniniDotNet.Infrastructure.Persistence.Images
{
    public sealed class ImagesRepository
    {
        private ApplicationDbContext DbContext { get; }

        public ImagesRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task SaveImageAsync(Image image)
        {
            await DbContext.Images.AddAsync(image);
            await DbContext.SaveChangesAsync();
        }

        public Task<Image?> GetImageByFilenameAsync(string filename) =>
#pragma warning disable 8619
            DbContext.Images.FirstOrDefaultAsync(it => it.Filename == filename);
#pragma warning restore 8619
    }
}
