using AutoMapper;
using TestApplication.Entities;
using TestApplication.Entities.Dto;
using TestApplication.Repository;

namespace TestApplication.Dal
{
    public class PersonDal : IPersonDal
    {
        private readonly IRepository _repository;
        public PersonDal(IRepository repository)
        {
            _repository = repository;
        }
        public async Task CreateNewPersonAsync(PersonRequestModel personModel)
        {
            var person = GetPersonByModel(personModel);
            person.Guid = Guid.NewGuid();
            person.CreateDate = DateTime.Now;

            await _repository.CreateNewPersonAsync(person, CancellationToken.None);
        }

        private Person GetPersonByModel(PersonRequestModel personModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonRequestModel, Person>());
            var mapper = config.CreateMapper();
            return mapper.Map<Person>(personModel);
        }        

    }
}
