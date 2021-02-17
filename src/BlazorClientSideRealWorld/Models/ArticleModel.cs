using System;

namespace BlazorClientSideRealWorld.Models
{
    public class ArticleModel
    {
        public ArticleModel()
        {
            Author = new AuthorModel();
        }

        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public string[] TagList { get; set; }
        public string Description { get; set; }
        public AuthorModel Author { get; set; }
        public int FavoritesCount { get; set; }
        public bool Favorited { get; set; }
    }

    public class AuthorModel
    {
        public string Username { get; set; }

        private string image;
        public string Image
        {
            get
            {
                if (string.IsNullOrWhiteSpace(image)) return string.Empty;

                Uri uri;

                if (Uri.TryCreate(image, UriKind.Absolute, out uri))
                    return "//" + uri.Host + uri.PathAndQuery;
                else if (Uri.TryCreate(image, UriKind.Relative, out uri))
                {
                    string result = uri.OriginalString;
                    if (result.Substring(0, 1) == "/")
                        return result;
                    else
                        return "//" + result;
                }
                else
                    return string.Empty;
            }
            set
            {
                image = value;
            }
        }

        public string Bio { get; set; }

        public bool Following { get; set; }
    }
}
