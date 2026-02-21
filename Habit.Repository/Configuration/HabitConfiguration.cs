using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReHabit.Habit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Habit.Repository.Configuration
{
    internal class HabitConfiguration : IEntityTypeConfiguration<HabitModel>
    {
        public void Configure(EntityTypeBuilder<HabitModel> builder)
        {
           builder.HasKey(h => h.Id);

           builder.Property(h => h.HabitName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.Description)
                .IsRequired(true)
                .HasMaxLength(255);
        }
    }
}
