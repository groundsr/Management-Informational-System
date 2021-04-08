using System;

namespace API.Model
{
    public enum Rank{ PrincipalChiefAgent ,ChiefAgent , DeputyChiefAgent ,PrincipalAgent , Agent }
    public class Policeman
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Rank Rank { get; set; }
        public int Age { get; set; }
        public int Seniority { get; set; }

    }
}