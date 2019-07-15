using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Simple_WebChat.Models.Mdl;
namespace Simple_WebChat.Models.Ctx
{
    public class ChatDBContext:DbContext
    {
        public ChatDBContext()
        {
            Database.Connection.ConnectionString = "Data Source=MÜHENDIS;Initial Catalog=ChatDB;Integrated Security=True";
        }

        public DbSet<Mesajlar> Mesaj { get; set; }

        public DbSet<Users> User { get; set; }
        public object Users { get; internal set; }
    }
}