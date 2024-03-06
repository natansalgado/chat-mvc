using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mvc.Models;

namespace chatmvc.Context.Maps
{
    public class MessageMap : IEntityTypeConfiguration<MessageModel>
    {
        public void Configure(EntityTypeBuilder<MessageModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}