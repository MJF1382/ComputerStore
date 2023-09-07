using ComputerStore.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class FeedbackModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "نام و نام خانوادگی خود را وارد کنید.")]
        [StringLength(100, ErrorMessage = "نام و نام خانوادگی شما باید حداکثر 100 کاراکتر باشد.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "شماره موبایل خود را وارد کنید.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "شماره موبایل شما باید 11 کاراکتر باشد.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "ایمیل خود را وارد کنید.")]
        [StringLength(100, ErrorMessage = "ایمیل شما باید حداکثر 100 کاراکتر باشد.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "ایمیل خود را بدرستی وارد کنید.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "عنوان پیام خود را وارد کنید.")]
        [StringLength(100, ErrorMessage = "عنوان پیام باید حداکثر 100 کاراکتر باشد.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "متن پیام خود را وارد کنید.")]
        [StringLength(1000, ErrorMessage = "متن پیام باید حداکثر 1000 کاراکتر باشد.")]
        public string Body { get; set; }

        public static implicit operator FeedbackModel(Feedback feedback)
        {
            return new FeedbackModel()
            {
                Body = feedback.Body,
                Email = feedback.Email,
                FullName = feedback.FullName,
                Id = feedback.Id,
                PhoneNumber = feedback.PhoneNumber,
                Subject = feedback.Subject
            };
        }

        public static implicit operator Feedback(FeedbackModel feedbackModel)
        {
            return new Feedback()
            {
                Body = feedbackModel.Body,
                Email = feedbackModel.Email,
                FullName = feedbackModel.FullName,
                Id = feedbackModel.Id,
                PhoneNumber = feedbackModel.PhoneNumber,
                Subject = feedbackModel.Subject
            };
        }
    }
}
