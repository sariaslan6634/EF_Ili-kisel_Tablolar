using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loading_Related_Data.Configiration;


public class Datas : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasData(new Employee[]
        {
                new() {Id = 1, RegionId = 1 , Name = "Gençay",Surname = "Yıldız",Salary = 1500 },
                new() {Id = 2, RegionId = 2 , Name = "Mahmut",Surname = "Salar",Salary = 1500 },
                new() {Id = 3, RegionId = 1 , Name = "Tuncay",Surname = "Kahraman",Salary = 1500 },
                new() {Id = 4, RegionId = 2 , Name = "Orhan",Surname = "Cakmak",Salary = 1500 }
        });
    }
}

public class OrderData : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasData(new Order[]
        {
            new(){Id = 1, EmployeeId = 1 ,OrderDate = DateTime.Now },
            new(){Id = 2, EmployeeId = 1 ,OrderDate = DateTime.Now },
            new(){Id = 3, EmployeeId = 2 ,OrderDate = DateTime.Now },
            new(){Id = 4, EmployeeId = 2 ,OrderDate = DateTime.Now },
            new(){Id = 5, EmployeeId = 3 ,OrderDate = DateTime.Now },
            new(){Id = 6, EmployeeId = 3 ,OrderDate = DateTime.Now },
            new(){Id = 7, EmployeeId = 3 ,OrderDate = DateTime.Now },
            new(){Id = 8, EmployeeId = 4 ,OrderDate = DateTime.Now },
            new(){Id = 9, EmployeeId = 4 ,OrderDate = DateTime.Now },
            new(){Id = 10, EmployeeId = 1 ,OrderDate = DateTime.Now },
            new(){Id = 11, EmployeeId = 2 ,OrderDate = DateTime.Now }
        });
    }
}
public class RegionData : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasData(new Region[]
        {
                new() {Id =1,Name = "Ankara" },
                new() {Id =2,Name = "Yozgat" }
        });
    }
}