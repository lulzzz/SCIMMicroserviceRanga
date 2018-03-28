﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ScimMicroservice.DLL;
using ScimMicroservice.DLL.Models;
using System;

namespace ScimMicroservice.Migrations
{
    [DbContext(typeof(SCIMContext))]
    [Migration("20180327092254_M3")]
    partial class M3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ScimMicroservice.DLL.Models.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Primary");

                    b.Property<int>("Type");

                    b.Property<int?>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email");

                    b.Property<string>("LogonName");

                    b.Property<int>("MetaDateId");

                    b.Property<int?>("MetaId");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.HasKey("Id");

                    b.HasIndex("MetaId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.MailingAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<string>("Formatted");

                    b.Property<string>("Locality");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Region");

                    b.Property<string>("StreetAddress");

                    b.HasKey("Id");

                    b.ToTable("MailingAddress");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.Meta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Location");

                    b.Property<int>("ResourceType");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.ToTable("Meta");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.Name", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FamilyName");

                    b.Property<string>("Formatted");

                    b.Property<string>("GivenName");

                    b.Property<string>("HonorificPrefix");

                    b.Property<string>("HonorificSuffix");

                    b.Property<string>("MiddleName");

                    b.HasKey("Id");

                    b.ToTable("Name");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Type");

                    b.Property<int?>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PhoneNumber");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanNotChangePassword");

                    b.Property<bool>("Disabled");

                    b.Property<DateTime>("ExpiresOn");

                    b.Property<string>("ExternalId");

                    b.Property<bool>("Locked");

                    b.Property<int?>("MailingAddressId");

                    b.Property<int?>("MetaId");

                    b.Property<bool>("MustChangePasswordAtNextLogin");

                    b.Property<int?>("NameId");

                    b.Property<DateTime>("PasswordExpiresOn");

                    b.Property<bool>("PasswordNeverExpires");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("MailingAddressId");

                    b.HasIndex("MetaId");

                    b.HasIndex("NameId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.UserGroups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupId");

                    b.Property<int>("MetaDataId");

                    b.Property<int?>("MetaId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("MetaId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.Email", b =>
                {
                    b.HasOne("ScimMicroservice.DLL.Models.User")
                        .WithMany("Emails")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.Group", b =>
                {
                    b.HasOne("ScimMicroservice.DLL.Models.Meta", "Meta")
                        .WithMany()
                        .HasForeignKey("MetaId");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.PhoneNumber", b =>
                {
                    b.HasOne("ScimMicroservice.DLL.Models.User")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.User", b =>
                {
                    b.HasOne("ScimMicroservice.DLL.Models.MailingAddress", "MailingAddress")
                        .WithMany()
                        .HasForeignKey("MailingAddressId");

                    b.HasOne("ScimMicroservice.DLL.Models.Meta", "Meta")
                        .WithMany()
                        .HasForeignKey("MetaId");

                    b.HasOne("ScimMicroservice.DLL.Models.Name", "Name")
                        .WithMany()
                        .HasForeignKey("NameId");
                });

            modelBuilder.Entity("ScimMicroservice.DLL.Models.UserGroups", b =>
                {
                    b.HasOne("ScimMicroservice.DLL.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScimMicroservice.DLL.Models.Meta", "Meta")
                        .WithMany()
                        .HasForeignKey("MetaId");

                    b.HasOne("ScimMicroservice.DLL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
