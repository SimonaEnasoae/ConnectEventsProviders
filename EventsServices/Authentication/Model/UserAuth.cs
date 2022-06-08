using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Models
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

    }
}
