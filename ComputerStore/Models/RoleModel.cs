using ComputerStore.Database.Entities;

namespace ComputerStore.Models
{
    public class RoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static implicit operator RoleModel(AppRole role)
        {
            return new RoleModel()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static implicit operator AppRole(RoleModel roleModel)
        {
            return new AppRole()
            {
                Id = roleModel.Id,
                Name = roleModel.Name
            };
        }
    }
}
