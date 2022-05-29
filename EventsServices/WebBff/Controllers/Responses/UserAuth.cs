using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBff.Controllers.Responses
{
    public enum UserType
    {
        Provider,
        EventHost
    }

    public class UserAuth 
    {
        public string Id { get; set; }

        public string Email { get; init; }
        public string Username { get; init; }
        

        public string Password { get; init; }

        public UserType Type { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public UserAuth() { }
        //public override string ToString()
        //{
        //    return "Username: " + Username.ToString() +
        //        "\nPassword: " + Password.ToString() +
        //        "\nId: " + Id.ToString() +
        //        "\nType: " + Type.ToString() +"\n";
        //}
    }
}
