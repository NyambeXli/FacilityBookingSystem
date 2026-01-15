using UfsConnectBook.Models.Entities;

namespace UfsConnectBook.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        public IRepositoryBase<Booking> Booking => throw new NotImplementedException();

        public IRepositoryBase<Facility> Facility => throw new NotImplementedException();

        public IRepositoryBase<Review> Review => throw new NotImplementedException();

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
