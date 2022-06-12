using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEnrollment.Repository
{
    public interface IBusiness
    {
        List<string> GetUsers();

        void Save();
    }

    public class Business : IBusiness
    {
        private readonly IDataAccess _dataAccess;
        public Business(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public List<string> GetUsers()
        {
            return _dataAccess.Get();
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