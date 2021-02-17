using System;

namespace BlazorClientSideRealWorld.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public AuthorModel Author { get; set; }

    }
}
