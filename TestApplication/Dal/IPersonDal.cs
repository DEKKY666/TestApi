using TestApplication.Entities.Dto;

namespace TestApplication.Dal
{
    public interface IPersonDal
    {
        Task CreateNewPersonAsync(PersonRequestModel personModel);
    }
}
