using AutoMapper;
using Contact_Manager_DataAccess.Models;
using Contact_Manager_Domain.DTOs;
using User_Contact_Manager.Models;

public class MappingProfiles : Profile
{
    public MappingProfiles() 
    {
        CreateMap<Contact, ContactDTO>();
        CreateMap<ContactDTO, Contact>();
        CreateMap<ContactDTO, ContactViewModel>();
        CreateMap<ContactViewModel, ContactDTO>();
        //CreateMap<List<ContactViewModel>, List<ContactDTO>>();


    }
}