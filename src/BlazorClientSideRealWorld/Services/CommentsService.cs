using System.Threading.Tasks;
using BlazorClientSideRealWorld.Models;

namespace BlazorClientSideRealWorld.Services
{
    public class CommentsService
    {
        private IApiService api;

        public CommentsService(IApiService _api)
        {
            api = _api;
        }

        public async Task<ApiResponse<CommentResponse>> AddAsync(string slug, CommentModel value)
        {
            var response = await api.PostAsync<CommentResponse>($"/articles/{slug}/comments", new
            {
                comment = value
            });
            return response;
        }

        public async Task<ApiResponse<CommentResponse>> AddAsync(ArticleModel article, CommentModel value)
        {
            return await AddAsync(article.Slug, value);
        }

        public async Task<ApiResponse<CommentsResponse>> GetAllAsync(string slug)
        {
            var response = await api.GetAsync<CommentsResponse>($"/articles/{slug}/comments");
            return response;
        }

        public async Task<bool> DeleteAsync(string slug, int commentId)
        {
            var response = await api.DeleteAsync<CommentModel>($"/articles/{slug}/comments/{commentId.ToString()}");
            return response?.HasSuccessStatusCode ?? false;
        }

        public async Task<bool> DeleteAsync(string slug, CommentModel comment)
        {
            return await DeleteAsync(slug, comment.Id);
        }

        public async Task<bool> DeleteAsync(ArticleModel article, CommentModel comment)
        {
            return await DeleteAsync(article.Slug, comment.Id);
        }

        public async Task<bool> DeleteAsync(ArticleModel article, int commentId)
        {
            return await DeleteAsync(article.Slug, commentId);
        }
    }

    public class CommentsResponse
    {
        public CommentModel[] Comments { get; set; }
    }

    public class CommentResponse
    {
        public CommentModel Comment { get; set; }
    }
}
