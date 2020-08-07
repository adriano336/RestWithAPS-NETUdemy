﻿using RestWithAPS_NETUdemy.Model.Base;

namespace RestWithAPS_NETUdemy.Model
{
    public class Person : BaseEntity
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
    }
}
