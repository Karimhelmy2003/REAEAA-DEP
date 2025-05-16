using Microsoft.EntityFrameworkCore;
using REAEAA_DEPI_API.Models;

namespace REAEAA_DEPI_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Patient> Patients_REAEAA_DEPI { get; set; }
        public DbSet<Doctor> Doctors_REAEAA_DEPI { get; set; }
        public DbSet<Admin> Admins_REAEAA_DEPI { get; set; }
        public DbSet<Appointment> Appointments_REAEAA_DEPI { get; set; }
        public DbSet<Department> Departments_REAEAA_DEPI { get; set; }
        public DbSet<MedicalHistory> MedicalHistories_REAEAA_DEPI { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Appointments configuration
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Admin)
                .WithMany(ad => ad.Appointments)
                .HasForeignKey(a => a.AdminID)
                .OnDelete(DeleteBehavior.NoAction);

            // MedicalHistory configuration
            modelBuilder.Entity<MedicalHistory>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.MedicalHistories)
                .HasForeignKey(m => m.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MedicalHistory>()
                .HasOne(m => m.Doctor)
                .WithMany()
                .HasForeignKey(m => m.DoctorID)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Prevent multiple cascade paths
        }
    }
}
