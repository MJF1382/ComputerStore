using ComputerStore.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class SatisfactionModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ارسال کننده را وارد کنید.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "امتیاز خود را وارد کنید.")]
        public int Score { get; set; }

        [Required(ErrorMessage = "متن نظر خود را وارد کنید.")]
        [StringLength(300, ErrorMessage = "متن نظر شما باید حداکثر 300 کاراکتر باشد.")]
        public string Body { get; set; }

        public DateTime PublishDate { get; set; }

        public static implicit operator SatisfactionModel(Satisfaction satisfaction)
        {
            return new SatisfactionModel()
            {
                Id = satisfaction.Id,
                UserId = satisfaction.UserId,
                Score = satisfaction.Score,
                Body = satisfaction.Body,
                PublishDate = satisfaction.PublishDate
            };
        }

        public static implicit operator Satisfaction(SatisfactionModel satisfactionModel)
        {
            return new Satisfaction()
            {
                Id = satisfactionModel.Id,
                UserId = satisfactionModel.UserId,
                Score = satisfactionModel.Score,
                Body = satisfactionModel.Body,
                PublishDate = satisfactionModel.PublishDate
            };
        }
    }
}
