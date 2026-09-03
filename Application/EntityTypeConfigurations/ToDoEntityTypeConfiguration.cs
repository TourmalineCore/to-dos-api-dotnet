using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ToDoEntityTypeConfiguration : IEntityTypeConfiguration<ToDo>
{
  public void Configure(EntityTypeBuilder<ToDo> builder)
  {
    builder.HasIndex(x => x.Id).IsUnique();

    builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
  }
}
