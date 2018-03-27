using AutoMapper;
using ScimMicroservice.DLL.Models;
using ScimMicroservice.Models;

namespace ScimMicroservice
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<DLL.Models.User, ScimUser>();
            CreateMap<ScimUser, User>();
            CreateMap<ScimEmail, Email>();
            CreateMap<Email, ScimEmail>();
            CreateMap<ScimPhoneNumber, PhoneNumber>();
            CreateMap<PhoneNumber, ScimPhoneNumber>();
            CreateMap<ScimMailingAddress, MailingAddress>();
            CreateMap<MailingAddress, ScimMailingAddress>();
            CreateMap<ScimName, Name>();
            CreateMap<Name, ScimName>();
            CreateMap<Meta, ScimMeta>();
            CreateMap<ScimMeta, Meta>();
        }
    }
}
