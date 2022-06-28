/*using Bogus;
using Core.Domain.Common;
using Core.Domain.Properties;
using Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Core.Data;

public class SeedData
{
    internal static Faker<User> UserData()
    {
        var count = new Random().Next(1, 10);
        var users = new Faker<User>()
            .RuleFor(u => u.Id, f => f.IndexFaker)
            .RuleFor(u => u.Active, f => f.Random.Bool())
            .RuleFor(u => u.Deleted, f => f.Random.Bool())
            .RuleFor(u => u.Facebook, f => f.Internet.Url())
            .RuleFor(u => u.Instagram, f => f.Internet.Url())
            .RuleFor(u => u.Linkedin, f => f.Internet.Url())
            .RuleFor(u => u.Password, f => f.Person.Random.AlphaNumeric(8))
            .RuleFor(u => u.Picture, f => f.Image.PicsumUrl())
            .RuleFor(u => u.Properties, f => PropertiesData().Generate(count))
            .RuleFor(u => u.Twitter, f => f.Internet.Url())
            .RuleFor(u => u.AdminComment, f => f.Lorem.Sentence(50))
            .RuleFor(u => u.FirstName, f => f.Person.FirstName)
            .RuleFor(u => u.IsAdmin, f => f.Random.Bool(0))
            .RuleFor(u => u.IsAgent, f => f.Random.Bool())
            .RuleFor(u => u.IsCompany, f => f.Random.Bool())
            .RuleFor(u => u.LastName, f => f.Person.LastName)
            .RuleFor(u => u.PasswordSalt, f => f.Hashids.Encode(6768))
            .RuleFor(u => u.UserAddresses, f => AddressesData().Generate(count))
            .RuleFor(u => u.UserEmail, f => f.Internet.Email())
            .RuleFor(u => u.CreatedOnUtc, f => f.Date.Past())
            .RuleFor(u => u.FailedLoginAttempts, f => f.Random.Int(10))
            .RuleFor(u => u.PasswordFormatId, f => PasswordFormat.Clear)
            .RuleFor(u => u.LastActivityDateUtc, f => f.Date.Soon())
            .RuleFor(u => u.LastLoginDateUtc, f => f.Date.Recent());

        return users;
    }

    internal static Faker<Property> PropertiesData()
    {
        var count = new Random().Next(1, 10);
        var properties = new Faker<Property>()
            .RuleFor(p => p.Id, f => f.IndexFaker)
            .RuleFor(p => p.Address, f => AddressesData().Generate())
            .RuleFor(p => p.User, f => UserData().Generate())
            .RuleFor(p => p.AddressId, f => AddressesData().Generate().Id)
            .RuleFor(p => p.PropertyDescription, f => f.Lorem.Paragraph())
            .RuleFor(p => p.PropertyImages, f => PropertyImagesData().Generate(count))
            .RuleFor(p => p.PropertyName, f => f.Commerce.ProductName())
            .RuleFor(p => p.PropertyStatus, f => PropertyStatus.Active)
            .RuleFor(p => p.PropertyType, f => PropertyTypesData().Generate())
            .RuleFor(p => p.SellerId, f => UserData().Generate().Id)
            .RuleFor(p => p.PropertyTypeId, f => PropertyTypesData().Generate().Id);

        return properties;
    }

    internal static Faker<PropertyImage> PropertyImagesData()
    {
        var propertyImages = new Faker<PropertyImage>()
            .RuleFor(pi => pi.Id, f => f.IndexFaker)
            .RuleFor(pi => pi.ImageDescription, f => f.Lorem.Sentence())
            .RuleFor(pi => pi.ImageName, f => f.Lorem.Text())
            .RuleFor(pi => pi.PropertyId, f => PropertiesData().Generate().Id);

        return propertyImages;
    }

    internal static Faker<PropertyType> PropertyTypesData()
    {
        var count = new Random().Next(1, 10);
        var propertyTypes = new Faker<PropertyType>()
            .RuleFor(pt => pt.Id, f => f.IndexFaker)
            .RuleFor(pt => pt.Properties, f => PropertiesData().Generate(count))
            .RuleFor(pt => pt.AgentId, f => UserData().Generate().Id)
            .RuleFor(pt => pt.PropertyTypeName, f => f.Lorem.Text());

        return propertyTypes;
    }


    internal static Faker<Address> AddressesData()
    {
        var addresses = new Faker<Address>()
            .RuleFor(a => a.Id, f => f.IndexFaker)
            .RuleFor(a => a.City, f => f.Person.Address.City)
            .RuleFor(a => a.Country, f => f.Person.Address.Street)
            .RuleFor(a => a.State, f => f.Person.Address.State)
            .RuleFor(a => a.Property, f => PropertiesData().Generate())
            .RuleFor(a => a.User, f => UserData().Generate())
            .RuleFor(a => a.FaxNumber, f => f.Phone.PhoneNumber())
            .RuleFor(a => a.LocalGovernment, f => f.Lorem.Word())
            .RuleFor(a => a.OfficeId, f => UserData().Generate().Id)
            .RuleFor(a => a.PhoneNumber, f => f.Person.Phone)
            .RuleFor(a => a.PropertyId, f => PropertiesData().Generate().Id)
            .RuleFor(a => a.CreatedOnUtc, f => f.Date.Past())
            .RuleFor(a => a.ZipPostalCode, f => f.Person.Address.ZipCode);

        return addresses;
    }

    internal static Faker<Appointment> AppointmentData()
    {
        var appointments = new Faker<Appointment>()
            .RuleFor(a => a.Id, f => f.IndexFaker)
            .RuleFor(a => a.Admin, f => UserData())
            .RuleFor(a => a.Buyer, f => UserData())
            .RuleFor(a => a.Property, f => PropertiesData())
            .RuleFor(a => a.Seller, f => UserData().Generate())
            .RuleFor(a => a.AdminId, f => UserData().Generate().Id)
            .RuleFor(a => a.AppointmentDate, f => f.Date.Soon())
            .RuleFor(a => a.AppointmentDescription, f => f.Lorem.Paragraph())
            .RuleFor(a => a.AppointmentStatus, f => AppointmentData().Generate().AppointmentStatus)
            .RuleFor(a => a.BuyerId, f => UserData().Generate().Id)
            .RuleFor(a => a.PropertyId, f => PropertiesData().Generate().Id)
            .RuleFor(a => a.SellerId, f => UserData().Generate().Id);

        return appointments;
    }

    internal static Faker<Comment> CommentsData()
    {
        var comments = new Faker<Comment>()
            .RuleFor(c => c.Id, f => f.IndexFaker)
            .RuleFor(c => c.Admin, f => UserData().Generate())
            .RuleFor(c => c.Buyer, f => UserData().Generate())
            .RuleFor(c => c.Content, f => f.Rant.Review())
            .RuleFor(c => c.Property, f => PropertiesData())
            .RuleFor(c => c.Seller, f => UserData())
            .RuleFor(c => c.AdminId, f => UserData().Generate().Id)
            .RuleFor(c => c.BuyerId, f => UserData().Generate().Id)
            .RuleFor(c => c.CommentDate, f => f.Date.Recent())
            .RuleFor(c => c.CommentStatus, f => CommentStatus.Approved)
            .RuleFor(c => c.PropertyId, f => PropertiesData().Generate().Id)
            .RuleFor(c => c.SellerId, f => UserData().Generate().Id);

        return comments;
    }

    internal static Faker<Notification> NotificationsData()
    {
        var notifications = new Faker<Notification>()
            .RuleFor(n => n.Id, f => f.IndexFaker)
            .RuleFor(n => n.Admin, f => UserData())
            .RuleFor(n => n.Description, f => f.Rant.Review())
            .RuleFor(n => n.Name, f => f.Lorem.Text())
            .RuleFor(n => n.AdminId, f => UserData().Generate().Id);

        return notifications;
    }

    public static void Initialize(RedDbContext context)
    {
        if (context.Database.GetPendingMigrations().Any()) context.Database.Migrate();

        if (context.Users.Any()) return; // DB has been seeded

        context.Users.AddRange(UserData().Generate(200));
        context.SaveChanges();

        context.Addresses.AddRange(AddressesData().Generate(350));
        context.SaveChanges();

        context.Appointments.AddRange(AppointmentData().Generate(600));
        context.SaveChanges();

        context.Comments.AddRange(CommentsData().Generate(1200));
        context.SaveChanges();

        context.Notifications.AddRange(NotificationsData().Generate(600));
        context.SaveChanges();

        context.Properties.AddRange(PropertiesData().Generate(1000));
        context.SaveChanges();

        context.PropertyImages.AddRange(PropertyImagesData().Generate(2000));
        context.SaveChanges();

        context.PropertyTypes.AddRange(PropertyTypesData().Generate(10));
        context.SaveChanges();
    }
}*/