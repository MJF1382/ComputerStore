using ComputerStore.Database.Entities;

namespace ComputerStore.Models
{
    public class ArticleTagModel
    {
        public Guid ArticleId { get; set; }
        public Guid TagId { get; set; }

        public static implicit operator ArticleTagModel(ArticleTag articleTag)
        {
            return new ArticleTagModel()
            {
                ArticleId = articleTag.ArticleId,
                TagId = articleTag.TagId
            };
        }

        public static implicit operator ArticleTag(ArticleTagModel articleTagModel)
        {
            return new ArticleTag()
            {
                ArticleId = articleTagModel.ArticleId,
                TagId = articleTagModel.TagId
            };
        }
    }
}
