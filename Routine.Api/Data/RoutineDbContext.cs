using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Data
{
    public class RoutineDbContext : DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>()
                .Property(x => x.Intruction).HasMaxLength(500);

            modelBuilder.Entity<Employee>()
                .Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .Property(x => x.LastName).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>().HasData(
                new Company()
                {
                    Id = Guid.Parse("4500EB58-25AE-4D0C-B3CF-1E1DF5070212"),
                    Name = "Microsoft",
                    Intruction = "YoYo Company",
                    Country="2China",
                    Industry="Qibolin",
                    Product="PS4",
                },
                new Company()
                {
                    Id = Guid.Parse("7EF3C682-6163-4074-85DD-4AB0798FEC86"),
                    Name = "Google",
                    Intruction = "Niubi de Company",
                    Country = "sChinaNo.1",
                    Industry = "Qibolin1",
                    Product = "PS41",
                },
                new Company()
                {
                    Id = Guid.Parse("141CF996-29A8-4119-A291-D8115EA5C758"),
                    Name = "Yahoo",
                    Intruction = "Not good Company",
                    Country = "dChina2",
                    Industry = "aQibolin2",
                    Product = "hPS42",
                },
                new Company()
                {
                    Id = Guid.Parse("4500EB58-25AE-4D0C-B3CF-1E1DF5070211"),
                    Name = "Microsoft1",
                    Intruction = "YoYo Company",
                    Country = "Japan.1",
                    Industry = "Capcom",
                    Product = "QQ111",
                },
                new Company()
                {
                    Id = Guid.Parse("7EF3C682-6163-4074-85DD-4AB0798FEC81"),
                    Name = "Google1",
                    Intruction = "Niubi de Company",
                    Country = "Japan.21",
                    Industry = "Capcom4",
                    Product = "QQ1114",
                },
                new Company()
                {
                    Id = Guid.Parse("141CF996-29A8-4119-A291-D8115EA5C751"),
                    Name = "Yahoo1",
                    Intruction = "Not good Company",
                    Country = "2Japan.21",
                    Industry = "SCapcom4",
                    Product = "AQQ1114",
                },
                new Company()
                {
                    Id = Guid.Parse("4500EB58-25AE-4D0C-B3C2-1E1DF5070212"),
                    Name = "Microsoft2",
                    Intruction = "YoYo Company",
                    Country = "S2Japan.21",
                    Industry = "HSCapcom4",
                    Product = "QAQQ1114",
                },
                new Company()
                {
                    Id = Guid.Parse("7EF3C682-6163-4024-85DD-4AB0798FEC86"),
                    Name = "Google2",
                    Intruction = "Niubi de Company",
                    Country = "I2Japan.21",
                    Industry = "OSCapcom4",
                    Product = "PAQQ1114",
                },
                new Company()
                {
                    Id = Guid.Parse("141CF996-29A8-4129-A291-D8115EA5C758"),
                    Name = "Yahoo2",
                    Intruction = "Not good Company",
                    Country = "AA2Japan.21",
                    Industry = "THFHSCapcom4",
                    Product = "AWDAAQQ1114",
                },
                new Company()
                {
                    Id = Guid.Parse("4500EB58-23AE-4D0C-B3CF-1E1DF5070212"),
                    Name = "Microsoft3",
                    Intruction = "YoYo Company",
                    Country = "America.21",
                    Industry = "kltSCapcom4",
                    Product = "adawdAQQ1114",
                },
                new Company()
                {
                    Id = Guid.Parse("7EF3C682-6133-4074-85DD-4AB0798FEC86"),
                    Name = "Google3",
                    Intruction = "Niubi de Company",
                    Country = "America.21",
                    Industry = "R.Star",
                    Product = "LOL",
                },
                new Company()
                {
                    Id = Guid.Parse("141CF996-23A8-4119-A291-D8115EA5C758"),
                    Name = "Yahoo3",
                    Intruction = "Not good Company",
                    Country = "Russian.21",
                    Industry = "JinZhengEn",
                    Product = "Zhangchengze",
                });
            modelBuilder.Entity<Employee>().HasData(
                        new Employee
                                      {
                                          Id= Guid.Parse("6EC4C01C-0901-4BE0-ADEA-A18B5AE5F3A5"),
                                          CompanyId = Guid.Parse("4500EB58-25AE-4D0C-B3CF-1E1DF5070212"),
                                          DateOfBirth = new DateTime(1986, 01, 25),
                                          EmployeeNo = "A8L",
                                          FirstName = "Lebron",
                                          LastName = "James",
                                          Gender = Gender.男
                                      },
                        new Employee
                        {
                            Id = Guid.Parse("91C383A5-F359-4573-A456-8BF8DD54827D"),
                            CompanyId = Guid.Parse("4500EB58-25AE-4D0C-B3CF-1E1DF5070212"),
                            DateOfBirth = new DateTime(1990, 10, 1),
                            EmployeeNo = "No01",
                            FirstName = "赛",
                            LastName = "田",
                            Gender = Gender.男
                        },
                        new Employee
                        {
                            Id = Guid.Parse("B9489C18-613F-4FD6-ABEA-C10A53A0BE56"),
                            CompanyId = Guid.Parse("4500EB58-25AE-4D0C-B3CF-1E1DF5070212"),
                            DateOfBirth = new DateTime(1976, 03, 21),
                            EmployeeNo = "G18L",
                            FirstName = "Macheal",
                            LastName = "Jordan",
                            Gender = Gender.男
                        },
                        new Employee
                        {
                            Id = Guid.Parse("4CD3EBEA-F2D6-4595-935C-CF0E7683A6B3"),
                            CompanyId = Guid.Parse("4500EB58-25AE-4D0C-B3CF-1E1DF5070212"),
                            DateOfBirth = new DateTime(1989, 12, 25),
                            EmployeeNo = "GLA300",
                            FirstName = "Taylor",
                            LastName = "Swift",
                            Gender = Gender.女
                        },
                        new Employee
                                               {
                            Id = Guid.Parse("4E432F7B-778F-4524-9853-4C36F3BD79F8"),
                            CompanyId = Guid.Parse("7EF3C682-6163-4074-85DD-4AB0798FEC86"),
                                                   DateOfBirth = new DateTime(1993, 06, 13),
                                                   EmployeeNo = "C369",
                                                   FirstName = "Rihanna",
                                                   LastName = "Lol",
                                                   Gender = Gender.女
                                               },
                        new Employee
                        {
                            Id = Guid.Parse("30E4CA6B-AFEF-4DF9-B0D9-442037BB7E1B"),
                            CompanyId = Guid.Parse("7EF3C682-6163-4074-85DD-4AB0798FEC86"),
                            DateOfBirth = new DateTime(1990, 10, 1),
                            EmployeeNo = "No011",
                            FirstName = "赛1",
                            LastName = "田1",
                            Gender = Gender.男
                        },
                        new Employee
                        {
                            Id = Guid.Parse("88D5DCF1-9113-4B0E-A274-489C6CCB75E7"),
                            CompanyId = Guid.Parse("7EF3C682-6163-4074-85DD-4AB0798FEC86"),
                            DateOfBirth = new DateTime(1976, 03, 21),
                            EmployeeNo = "G18L1",
                            FirstName = "Macheal1",
                            LastName = "Jordan1",
                            Gender = Gender.男
                        },
                        new Employee
                        {
                            Id = Guid.Parse("48E060D5-69BB-4EF0-8057-B336660BF949"),
                            CompanyId = Guid.Parse("7EF3C682-6163-4074-85DD-4AB0798FEC86"),
                            DateOfBirth = new DateTime(1989, 12, 25),
                            EmployeeNo = "GLA3001",
                            FirstName = "Taylor1",
                            LastName = "Swift1",
                            Gender = Gender.女
                        },
                        new Employee
                                                {
                                                    Id = Guid.Parse("2CA376C1-AE8B-4CA2-A83B-FA3DEBB2E267"),
                                                    CompanyId = Guid.Parse("141CF996-29A8-4119-A291-D8115EA5C758"),
                                                    DateOfBirth = new DateTime(1986, 01, 25),
                                                    EmployeeNo = "A8L2",
                                                    FirstName = "Lebron2",
                                                    LastName = "James2",
                                                    Gender = Gender.女
                                                },
                        new Employee
                        {
                            Id = Guid.Parse("68B1E56A-4248-4F11-90FB-D6D8AF271896"),
                            CompanyId = Guid.Parse("141CF996-29A8-4119-A291-D8115EA5C758"),
                            DateOfBirth = new DateTime(1990, 10, 1),
                            EmployeeNo = "No012",
                            FirstName = "赛2",
                            LastName = "田2",
                            Gender = Gender.男
                        },
                        new Employee
                        {
                            Id = Guid.Parse("5DA80C4E-1624-49EA-9C8E-6F6D8210965F"),
                            CompanyId = Guid.Parse("141CF996-29A8-4119-A291-D8115EA5C758"),
                            DateOfBirth = new DateTime(1976, 03, 21),
                            EmployeeNo = "G18L2",
                            FirstName = "Macheal2",
                            LastName = "Jordan2",
                            Gender = Gender.男
                        },
                        new Employee
                        {
                            Id = Guid.Parse("27711C50-FF3F-440C-A19E-E686B0323228"),
                            CompanyId = Guid.Parse("141CF996-29A8-4119-A291-D8115EA5C758"),
                            DateOfBirth = new DateTime(1989, 12, 25),
                            EmployeeNo = "GLA3002",
                            FirstName = "Taylor2",
                            LastName = "Swift2",
                            Gender = Gender.女
                        },
                        new Employee
                        {
                            Id = Guid.Parse("6EC4C01C-0901-4BE0-ADEA-A18B5AE5F3A1"),
                            CompanyId = Guid.Parse("4500EB58-25AE-4D0C-B3CF-1E1DF5070211"),
                            DateOfBirth = new DateTime(1986, 01, 25),
                            EmployeeNo = "A8L1",
                            FirstName = "Lebron1",
                            LastName = "James1",
                            Gender = Gender.男
                        },
                        new Employee
                        {
                            Id = Guid.Parse("91C383A5-F359-4573-A456-8BF8DD548271"),
                            CompanyId = Guid.Parse("4500EB58-25AE-4D0C-B3CF-1E1DF5070211"),
                            DateOfBirth = new DateTime(1990, 10, 1),
                            EmployeeNo = "No011",
                            FirstName = "赛1",
                            LastName = "田1",
                            Gender = Gender.男
                        },
                        new Employee
                        {
                            Id = Guid.Parse("B9489C18-613F-4FD6-ABEA-C10A53A0BE51"),
                            CompanyId = Guid.Parse("4500EB58-25AE-4D0C-B3CF-1E1DF5070211"),
                            DateOfBirth = new DateTime(1976, 03, 21),
                            EmployeeNo = "G18L1",
                            FirstName = "Macheal1",
                            LastName = "Jordan1",
                            Gender = Gender.男
                        },
                        new Employee
                        {
                            Id = Guid.Parse("4CD3EBEA-F2D6-4595-935C-CF0E7683A6B1"),
                            CompanyId = Guid.Parse("4500EB58-25AE-4D0C-B3CF-1E1DF5070211"),
                            DateOfBirth = new DateTime(1989, 12, 25),
                            EmployeeNo = "GLA3001",
                            FirstName = "Taylor1",
                            LastName = "Swift1",
                            Gender = Gender.女
                        }
                );
        }
    }
}
