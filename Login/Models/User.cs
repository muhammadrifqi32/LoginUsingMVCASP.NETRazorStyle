using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Login.Base;
using Login.ViewModel;

namespace Login.Models
{
    [Table("tb_m_user")]
    public class User : BaseModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public User()
        {

        }

        public User(UserVM userVM)
        {
            this.id = userVM.Id;
            this.Email = userVM.Email;
            this.Username = userVM.Username;
            this.Password = userVM.Password;
        }
        public void Update(UserVM userVM)
        {
            this.id = userVM.Id;
            this.Email = userVM.Email;
            this.Username = userVM.Username;
            this.Password = userVM.Password;
        }

        public void Delete()
        {
        }
    }
}