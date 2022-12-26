using BuberBreakfast.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BuberBreakfast.Persistence.Configurations;

public class BreakfastConfigurations : IEntityTypeConfiguration<Breakfast>
{
    public void Configure(EntityTypeBuilder<Breakfast> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();



        builder.Property(a => a.Name)
            .HasMaxLength(Breakfast.MaxNameLenght);
        
        builder.Property(a => a.Description)
            .HasMaxLength(Breakfast.MaxDescriptionLenght);

        builder.Property(a => a.Savory)
            .HasConversion(
            a => string.Join(",", a),
            a => a.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        builder.Property(a => a.Sweet)
           .HasConversion(
           a => string.Join(",", a),
           a => a.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
           );
    }
}

//public Guid Id { get; private set; }
//public string Name { get; private set; }
//public string Description { get; private set; }
//public DateTime StartDateTime { get; private set; }
//public DateTime EndDateTime { get; private set; }
//public DateTime LastModifiedDateTime { get; private set; }
//public List<string> Savory { get; private set; }
//public List<string> Sweet { get; private set; }