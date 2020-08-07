using RestWithAPS_NETUdemy.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAPS_NETUdemy.Model
{
    [Table("Books")]
    public class Book : BaseEntity
    {   
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }
    }
}
