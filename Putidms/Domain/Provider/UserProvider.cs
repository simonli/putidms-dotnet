using LinqKit;
using Putidms.Common;
using Putidms.Domain.Models;
using Putidms.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Putidms.Domain.Provider
{
    public class UserProvider :BaseProvider<User>
    {
        public bool HasUserName(string username)
        {
            return base.Repos.IsContains(u => u.UserName.ToUpper() == username.ToUpper());
        }

        public bool HasEmail(string email)
        {
            return base.Repos.IsContains(e => e.Email.ToUpper() == email.ToUpper());
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pagingUser">分页数据</param>
        /// <param name="roleID">角色ID</param>
        /// <param name="username">用户名</param>
        /// <param name="name">名称</param>
        /// <param name="sex">性别</param>
        /// <param name="email">Email</param>
        /// <param name="order">排序【null（默认）-ID降序，0-ID升序，1-ID降序，2-注册时间降序，3-注册时间升序，4-最后登录时间升序，5-最后登录时间降序】</param>
        /// <returns></returns>
        public Paging<User> FindPageList(Paging<User> pagingUser, int? roleID, string username, string name, int? sex, string email, int? order)
        {
            //查询表达式
            var _where = PredicateBuilder.New<User>().DefaultExpression;
            //var _where1 = PredicateBuilder.True<User>();
            if (!string.IsNullOrEmpty(username)) _where = _where.And(u => u.UserName.Contains(username));
            if (!string.IsNullOrEmpty(name)) _where = _where.And(u => u.Name.Contains(name));
            if (sex != null && sex >= 0 && sex <= 2) _where = _where.And(u => u.Sex == sex);
            if (!string.IsNullOrEmpty(email)) _where = _where.And(u => u.Email.Contains(email));
            //排序
            OrderParam _orderParam;
            switch (order)
            {
                case 0://ID升序
                    _orderParam = new OrderParam() { PropertyName = "Id", Method = OrderMethod.ASC };
                    break;
                case 1://ID降序
                    _orderParam = new OrderParam() { PropertyName = "Id", Method = OrderMethod.DESC };
                    break;
                case 2://注册时间降序
                    _orderParam = new OrderParam() { PropertyName = "CreateTime", Method = OrderMethod.ASC };
                    break;
                case 3://注册时间升序
                    _orderParam = new OrderParam() { PropertyName = "CreateTime", Method = OrderMethod.DESC };
                    break;
                case 4://最后登录时间升序
                    _orderParam = new OrderParam() { PropertyName = "LastLoginTime", Method = OrderMethod.ASC };
                    break;
                case 5://最后登录时间降序
                    _orderParam = new OrderParam() { PropertyName = "LastLoginTime", Method = OrderMethod.DESC };
                    break;
                default://ID降序
                    _orderParam = new OrderParam() { PropertyName = "Id", Method = OrderMethod.DESC };
                    break;
            }
            pagingUser.Items = Repos.FindPageList(pagingUser.PageSize, pagingUser.PageIndex, out pagingUser.TotalNumber, _where.Expand(), _orderParam).ToList();
            return pagingUser;
        }


        public Respond VerifyLogin(string username, string password)
        {
            Respond _respond = new Respond();
            _respond.Code = 0;
            User user = FindList().Where(x => x.UserName == username).FirstOrDefault();
            if (CommonUtil.MD5Encrypt64(password) == user.Password)
            {
                _respond.Code = 1;
                _respond.Data = user;
            }
            return _respond;
            

        }
    }
}
