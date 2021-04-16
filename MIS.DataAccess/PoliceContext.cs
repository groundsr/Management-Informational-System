using MSI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MIS.Model;

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
        //remove since it is duplicate
        public DbSet<PolicemanMeeting> PolicemanMeetings { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<CriminalRecordPoliceman> CriminalRecordPolicemen { get; set; }
        public DbSet<MeetingRequestPoliceman> MeetingRequestPolicemen { get; set; }
        public DbSet<MeetingPoliceman> MeetingPolicemen { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
