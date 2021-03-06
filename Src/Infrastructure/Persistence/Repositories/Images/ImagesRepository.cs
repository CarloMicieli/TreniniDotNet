using System.Threading.Tasks;
using Dapper;
using NodaTime;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Images
{
    public sealed class ImagesRepository
    {
        private readonly IConnectionProvider _dbContext;
        private readonly IClock _clock;

        public ImagesRepository(IConnectionProvider dbContext, IClock clock)
        {
            _dbContext = dbContext;
            _clock = clock;
        }

        public async Task SaveImageAsync(Image image)
        {
            await using var connection = _dbContext.Create();
            await connection.OpenAsync();

            await connection.ExecuteAsync(InsertImageCommand, new
            {
                filename = image.Filename,
                content_type = image.ContentType,
                content = image.Content,
                created = _clock.GetCurrentInstant().ToDateTimeUtc()
            });
        }

        public async Task<Image?> GetImageByFilenameAsync(string filename)
        {
            await using var connection = _dbContext.Create();
            await connection.OpenAsync();

            return await connection.QueryFirstAsync<Image?>(SelectImageQuery, new
            {
                filename
            });
        }

        private const string InsertImageCommand = @"
            INSERT INTO images(filename, content_type, content, created) 
            VALUES(@filename, @content_type, @content, @created);";

        private const string SelectImageQuery = "SELECT filename, content_type AS ContentType, content FROM images WHERE filename = @filename";
    }
}
