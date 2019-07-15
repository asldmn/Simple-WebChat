using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple_WebChat.Models.Mdl
{
    public class Mesajlar
    {
        public int MesajlarId { get; set; }
        public string Mesaj { get; set; }
        public int UserId { get; set; }
        public virtual Users User { get; set; }
    }
}