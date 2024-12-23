﻿using ClearDrive.shared.Models;
using ClearDrive.shared.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ClearDrive.backend.Context
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            /*List<ProblemTicket> ticketList = new List<ProblemTicket> 
            {
                new ProblemTicket()
                {
                    Id = Guid.NewGuid(),
                    Description = "valami valmai",
                    Status = StatusType.Denied,
                    Problem = ProblemType.NaturalDisaster
                },

                new ProblemTicket()
                {
                    Id = Guid.NewGuid(),
                    Description = "valami valmai2",
                    Status = StatusType.Denied,
                    Problem = ProblemType.NaturalDisaster
                },
            };

            List<User> userList = new List<User>
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = "Alma",
                    Password = "password",
                    Email = "Banan@dasd.com",
                    TelNumber = "3123213"
                    
                },

                new User()
                {
                    Id= Guid.NewGuid(),
                    UserName = "Alma2",
                    Password = "password",
                    Email = "Banan@dasd.com",
                    TelNumber = "3123213"

                },
            };

            List<Administrator> adminList = new List<Administrator>
            {
                new Administrator()
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Password = "password",
                    Email = "vasd.das@.com"
                },

                new Administrator()
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin2",
                    Password = "password",
                    Email = "vasd.das@.com"
                },
            };*/

            List<Position> positionList = new List<Position>
            {
                new Position()
                {
                    Id = Guid.NewGuid(),
                    Latitude = 46.252243,
                    Longitude=20.147692,
                    StatusType = StatusType.ToDO,
                    Priority = 1
                },

                new Position()
                {
                    Id = Guid.NewGuid(),
                    Latitude = 46.253949,
                    Longitude=20.149033,
                    StatusType = StatusType.InProgress,
                    Priority = 1
                },
                new Position()
                {
                    Id = Guid.NewGuid(),
                    Latitude = 46.251701, 
                    Longitude = 20.150803,
                    StatusType = StatusType.Done,
                    Priority = 1
                }
            };

            //modelBuilder.Entity<ProblemTicket>().HasData(ticketList);
            //modelBuilder.Entity<User>().HasData(userList);
            //odelBuilder.Entity<Administrator>().HasData(adminList);
            modelBuilder.Entity<Position>().HasData(positionList);
        }
    }
}
