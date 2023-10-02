using eTech.Entities;

namespace eTech.Services.Interfaces
{
    public interface IAddressService
    {
        public Task<List<Address>> GetAll();
        public Task<Address> GetById(int Id);
        public Task<Address> Add(Address address);
        public Task<Address> Update(Address address);
        public Task Delete(int id);
    }
}
