using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEnrollment.Repository
{
    public interface IDataAccess
    {
        List<string> Get();
    }

    public class DataAccess : IDataAccess
    {
        //private readonly IUserProvider _userProvider;
        //public UserRepository(IUserProvider userProvider)
        //{
        //    _userProvider = userProvider;
        //}
        public List<string> Get()
        {
            //var user = _userProvider.UserName;
            return new List<string> { "User 1", "User 2", "User 3" };
        }
    }
}