using System;
using System.Collections.Generic;
using UfsConnectBook.Models.Entities;

namespace UfsConnectBook.Data
{
    public interface IRepositoryWrapper
    {
     
        IRepositoryBase<Booking> Booking { get; }
        IRepositoryBase<Facility> Facility { get; }
        IRepositoryBase<Review> Review { get; }

        void Save(); 
    }

    public interface IRepositoryBase<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Edit(T entity);
        void Cancel(T entity);
        void Add(FeedBack feedBack);
        
    }
}
