using ComputerStore.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class ArticleModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ارسال کننده را وارد کنید.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "دسته بندی را وارد کنید.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "عنوان مقاله را وارد کنید.")]
        [StringLength(100, ErrorMessage = "عنوان مقاله باید حداکثر 100 کاراکتر باشد.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "عنوان مقاله را وارد کنید.")]
        public string Body { get; set; }

        [Required(ErrorMessage = "محتوای مقاله را وارد کنید.")]
        public DateTime PublishDateTime { get; set; }

        public List<CommentModel> Comments { get; set; }

        public static implicit operator ArticleModel(Article article)
        {
            return new ArticleModel()
            {
                Id = article.Id,
                Body = article.Body,
                CategoryId = article.CategoryId,
                PublishDateTime = article.PublishDateTime,
                Title = article.Title,
                UserId = article.UserId,
                Comments = article.Comments != null ? article.Comments.Select<Comment, CommentModel>(comment => comment).ToList() : new List<CommentModel>()
            };
        }

        public static implicit operator Article(ArticleModel articleModel)
        {
            return new Article()
            {
                Id = articleModel.Id,
                Body = articleModel.Body,
                CategoryId = articleModel.CategoryId,
                PublishDateTime = articleModel.PublishDateTime,
                Title = articleModel.Title,
                UserId = articleModel.UserId
            };
        }
    }
}
