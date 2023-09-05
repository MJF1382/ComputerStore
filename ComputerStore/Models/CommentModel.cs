using ComputerStore.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ارسال کننده را وارد کنید.")]
        public Guid UserId { get; set; }

        public Guid? ProductId { get; set; }

        public Guid? ArticleId { get; set; }

        [Required(ErrorMessage = "امتیاز خود را وارد کنید.")]
        public float Score { get; set; }

        [Required(ErrorMessage = "نوع کامنت را وارد کنید.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "متن کامنت را وارد کنید.")]
        [StringLength(1000, ErrorMessage = "تن کامنت باید حداکثر 1000 کاراکتر باشد.")]
        public string Body { get; set; }

        public DateTime PublishDateTime { get; set; }

        public static implicit operator CommentModel(Comment comment)
        {
            return new CommentModel()
            {
                Id = comment.Id,
                ArticleId = comment.ArticleId,
                UserId = comment.UserId,
                PublishDateTime = comment.PublishDateTime,
                Body = comment.Body,
                ProductId = comment.ProductId,
                Score = comment.Score,
                Type = comment.Type
            };
        }

        public static implicit operator Comment(CommentModel commentModel)
        {
            return new Comment()
            {
                Id = commentModel.Id,
                ArticleId = commentModel.ArticleId,
                UserId = commentModel.UserId,
                PublishDateTime = commentModel.PublishDateTime,
                Body = commentModel.Body,
                ProductId = commentModel.ProductId,
                Score = commentModel.Score,
                Type = commentModel.Type
            };
        }
    }
}
