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

        public CategoryModel? Category { get; set; }
        public UserModel? User { get; set; }
        public List<ArticleTagModel>? ArticleTags { get; set; }
        public List<CommentModel>? Comments { get; set; }

        public static implicit operator ArticleModel(Article article)
        {
            ArticleModel articleModel = new ArticleModel()
            {
                Id = article.Id,
                Body = article.Body,
                CategoryId = article.CategoryId,
                PublishDateTime = article.PublishDateTime,
                Title = article.Title,
                UserId = article.UserId
            };

            if (article.Category != null)
            {
                articleModel.Category = article.Category;
            }
            if (article.User != null)
            {
                articleModel.User = article.User;
            }
            if (article.ArticleTags != null)
            {
                articleModel.ArticleTags = article.ArticleTags.Select<ArticleTag, ArticleTagModel>(articleTag => articleTag).ToList();
            }
            if (article.Category != null)
            {
                articleModel.Comments = article.Comments.Select<Comment, CommentModel>(comment => comment).ToList();
            }

            return articleModel;
        }

        public static implicit operator Article(ArticleModel articleModel)
        {
            Article article =  new Article()
            {
                Id = articleModel.Id,
                Body = articleModel.Body,
                CategoryId = articleModel.CategoryId,
                PublishDateTime = articleModel.PublishDateTime,
                Title = articleModel.Title,
                UserId = articleModel.UserId
            };

            if (articleModel.Category != null)
            {
                article.Category = articleModel.Category;
            }
            if (articleModel.User != null)
            {
                article.User = articleModel.User;
            }
            if (articleModel.ArticleTags != null)
            {
                article.ArticleTags = articleModel.ArticleTags.Select<ArticleTagModel, ArticleTag>(articleTag => articleTag).ToList();
            }
            if (articleModel.Category != null)
            {
                article.Comments = articleModel.Comments.Select<CommentModel, Comment>(comment => comment).ToList();
            }

            return article;
        }
    }
}
