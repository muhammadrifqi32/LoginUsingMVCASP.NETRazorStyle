using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Login.Base;
using Login.ViewModel;

namespace Login.Models
{
    [Table("tb_m_role")]
    public class Role : BaseModel
    {
        public string Name { get; set; }

        public Role() { }
        public Role(RoleVM roleVM)
        {
            this.id = roleVM.Id;
            this.Name = roleVM.Name;
        }
        public void Update(RoleVM roleVM)
        {
            this.id = roleVM.Id;
            this.Name = roleVM.Name;
        }

        public void Delete()
        {
        }
    }
}