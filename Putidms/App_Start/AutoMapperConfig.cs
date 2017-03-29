using AutoMapper;
using Putidms.Domain.Models;
using Putidms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Putidms
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>().ReverseMap();
                cfg.CreateMap<Division, DivisionViewModel>().ReverseMap();
                cfg.CreateMap<Department, DepartmentViewModel>().ReverseMap();
                cfg.CreateMap<Klass, KlassViewModel>().ReverseMap();
                cfg.CreateMap<Counselor, CounselorViewModel>().ReverseMap();
                cfg.CreateMap<Duty, DutyViewModel>().ReverseMap();
                cfg.CreateMap<KlassRecord, KlassRecordViewModel>().ReverseMap();
                cfg.CreateMap<TrainingRecord, TrainingRecordViewModel>().ReverseMap();
                cfg.CreateMap<EvaluationRecord, EvaluationRecordViewModel>().ReverseMap();
                cfg.CreateMap<User, UserViewModel>().ReverseMap();
            });
        }
    }
}