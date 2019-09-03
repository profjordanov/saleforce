using AutoMapper;
using Newtonsoft.Json.Linq;
using Saleforce.Permissions.Api.Entities;
using Saleforce.Permissions.Api.Views;

namespace Saleforce.Permissions.Api.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRoles, RoleView>(MemberList.None)
                .ForMember(dest => dest.DataScope,
                    opts => opts.MapFrom(
                        src => JObject.Parse(src.DataScope)))
                .ForMember(dest => dest.ExpiryDate,
                    opts => opts.MapFrom(
                        src => src.ExpireDate.HasValue ? src.ExpireDate.Value.UtcDateTime.ToString("o") : string.Empty))
                .ForMember(dest => dest.RoleType, opts => opts.MapFrom(
                    src => src.Role.RoleType));

            CreateMap<UserPermissions, UserPermissionView>(MemberList.None)
                .ForMember(dest => dest.ExpiryDate,
                    opts => opts.MapFrom(
                        src => src.ExpireDate.HasValue ? src.ExpireDate.Value.UtcDateTime.ToString("o") : string.Empty))
                .ForMember(dest => dest.PermissionType,
                    opts => opts.MapFrom(src => src.Permission.PermissionType));
        }
    }
}
