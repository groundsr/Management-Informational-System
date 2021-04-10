using MSI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.DataAccess
{
    public class PoliceContext : DbContext
    {
        public PoliceContext(DbContextOptions<PoliceContext> options) : base(options)
        {

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CriminalRecord> CriminalRecords { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingRequest> MeetingRequests { get; set; }
        public DbSet<Policeman> Policemen { get; set; }
        public DbSet<PoliceSection> PoliceSections { get; set; }
        public DbSet<PolicemanMeeting> PolicemanMeetings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
