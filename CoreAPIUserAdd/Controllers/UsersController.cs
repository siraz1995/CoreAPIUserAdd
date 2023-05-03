using CoreAPIUserAdd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPIUserAdd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _userContext;
        public UsersController(UserContext userContext)
        {
            this._userContext = userContext;
        }
        [HttpGet]
        [Route("GetUsers")]
        public List<Users> GetUsers()
        {
           return _userContext.Users.ToList();
        }
        [HttpGet]
        [Route("GetUser")]
        public Users GetUser(int id)
        {
            return _userContext.Users.Where(x=>x.Id==id).FirstOrDefault();
        }
        [HttpPost]
        [Route("AddUser")]
        public string AddUser(Users users)
        {
            string response=string.Empty;
            _userContext.Users.Add(users);
            _userContext.SaveChanges();
            return "User Added";
        }
        [HttpPut]
        [Route("UpdateUser")]
        public string UpdateUser(Users users)
        {
            _userContext.Entry(users).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _userContext.SaveChanges();
            return "Update User";
        }
        [HttpDelete]
        [Route("DeleteUser")]
        public string DeleteUser(int id)
        {
            Users user = _userContext.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user != null)
            {
                _userContext.Users.Remove(user);
                _userContext.SaveChanges();
                return "User Deleted";
            }
            else
            {
                return "User not found";
            }
          
        }
    }
}
