using JwtStore.Core.Context.AccountContext.Entities;
using JwtStore.Core.Context.SharedContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Core.Contexts.AccountContext.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; } =string.Empty;
        public List<User> Users { get; set; } = new(); 
    }
}
