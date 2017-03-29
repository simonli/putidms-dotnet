using Putidms.Common;
using Putidms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Putidms.Domain.Provider
{
    public class CounselorProvider : BaseProvider<Counselor>
    {
        public bool HasUserName(string username)
        {
            return base.Repos.IsContains(u => u.UserName.ToUpper() == username.ToUpper());
        }

        public bool HasEmail(string email)
        {
            return base.Repos.IsContains(u => u.Email.ToUpper() == email.ToUpper());
        }
    }

    public class KlassRecordProvider : BaseProvider<KlassRecord>
    {
        public bool HasRecord(int counselorId, int klassId)
        {
            return base.Repos.IsContains(x => x.KlassId == klassId && x.CounselorId == counselorId);
        }
    }

    public class TrainingRecordProvider : BaseProvider<TrainingRecord>
    {
        public bool HasRecord(int counselorId, string name)
        {
            return base.Repos.IsContains(x => x.Name.ToUpper() == name.ToUpper() && x.CounselorId == counselorId);
        }
    }

    public class EvaluationRecordProvider : BaseProvider<EvaluationRecord>
    {
        public bool HasRecord(int counselorId, string item)
        {
            return base.Repos.IsContains(x => x.Item.ToUpper() == item.ToUpper() && x.CounselorId == counselorId);
        }
    }
}
