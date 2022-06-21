using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Service;
using CommonService.Contracts;

namespace WebEnrollment.Repository
{
    public interface IBusiness
    {
        void Save();
    }

    public class Business : IBusiness
    {
        private readonly IDataAccess _dataAccess;
        public Business(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void Save()
        {
            var context = new EnrollmentEntities();
            var b = context.Students.First();
            var _student = new Student()
            {
                StudentNumber = DateTime.Now.Year.ToString() + DateTime.Now.Second.ToString(),
                FirstName = "Raymond",
                LastName = "Cruz",
                Email = "tomlawyer26@yahoo.com"

            };
            context.Students.Add(_student);
            context.SaveChanges();
        }
    }
}