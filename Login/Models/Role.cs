using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Login.Base;

namespace Login.Models
{
    [Table("tb_m_role")]
    public class Role : BaseModel
    {
        public string Name { get; set; }

        public Role() { }
    }
}