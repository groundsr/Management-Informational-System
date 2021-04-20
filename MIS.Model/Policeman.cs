using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace MSI.Model
{
    public enum Rank{ PrincipalChiefAgent ,ChiefAgent , DeputyChiefAgent ,PrincipalAgent , Agent }
    public class Policeman
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Rank Rank { get; set; }
        public int Age { get; set; }
        public int Seniority { get; set; }
        public string Email { get; set; }
        public virtual List<Policeman> Subordinates { get; set; } = new List<Policeman>();
        public Policeman(string email , int age , Rank rank , string name , Guid id)
        {
            Email = email;
            Age = age;
            Rank = rank;
            Name = name;
            Id = id;
        }
        public Policeman()
        {

        }
         
    }
}