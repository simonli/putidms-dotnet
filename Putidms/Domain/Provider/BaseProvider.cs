using Putidms.Common;
using Putidms.Domain.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Putidms.Domain.Provider
{
    public abstract class BaseProvider<T> where T : class
    {
        protected Repository<T> Repos;

        public BaseProvider() : this(ContextFactory.CurrentContext()) { }

        public BaseProvider(DbContext dbContext)
        {
            Repos = new Repository<T>(dbContext);
        }


        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>实体</returns>
        public virtual T Find(int ID)
        {
            return Repos.Find(ID);
        }

        /// <summary>
        /// 查找数据列表-【所有数据】
        /// </summary>
        /// <returns>所有数据</returns>
        public IQueryable<T> FindList()
        {
            return Repos.FindList();
        }

        /// <summary>
        /// 查找分页数据
        /// </summary>
        /// <param name="paging">分页数据</param>
        /// <returns>分页数据</returns>
        public Paging<T> FindPageList(Paging<T> paging)
        {
            paging.Items = Repos.FindPageList(paging.PageSize, paging.PageIndex, out paging.TotalNumber).ToList();
            return paging;
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        /// <returns>总记录数</returns>
        public virtual int Count()
        {
            return Repos.Count();
        }
        

        public virtual Respond Save(T entity)
        {
            Respond _respond = new Respond();
            bool hasError = false;
            int Id = (int)entity.GetType().GetProperty("Id").GetValue(entity);
            if (Id == 0)
            {
                entity.GetType().GetProperty("CreateTime").SetValue(entity, DateTime.Now);
                if (Repos.Add(entity) > 0)
                {
                    _respond.Code = 1;
                    _respond.Message = "新增成功！";
                    _respond.Data = entity;
                }
                else { hasError = true; }
            }
            else
            {
                T currentEntity = Find(Id);
                T dbEntity = EntityUtil.GetEntity(entity, currentEntity);
                dbEntity.GetType().GetProperty("UpdateTime").SetValue(dbEntity, DateTime.Now);
                dbEntity.GetType().GetProperty("UpdateUser").SetValue(dbEntity, 999);

                if (Repos.Update(dbEntity) > 0)
                {
                    _respond.Code = 1;
                    _respond.Message = "更新成功！";
                    _respond.Data = dbEntity;
                }
                else { hasError = true; }
            }
            if (hasError)
            {
                _respond.Code = 0;
                _respond.Message = "操作失败！";
            }

            return _respond;
        }

        public virtual Respond Delete(T entity)
        {
            Respond _respond = new Respond();

            if (Repos.Delete(entity) > 0)
            {
                _respond.Code = 1;
                _respond.Message = "删除数据成功！";
            }
            else
            {
                _respond.Code = 0;
                _respond.Message = "删除数据失败！";
            }
            return _respond;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>Code：0-删除失败；1-删除陈功；10-记录不存在</returns>
        public virtual Respond Delete(int ID)
        {
            Respond _respond = new Respond();
            var _entity = Find(ID);
            if (_entity == null)
            {
                _respond.Code = 10;
                _respond.Message = "ID为【" + ID + "】的记录不存在！";
            }
            else
            {
                if (Repos.Delete(_entity) > 0)
                {
                    _respond.Code = 1;
                    _respond.Message = "删除数据成功！";
                }
                else
                {
                    _respond.Code = 0;
                    _respond.Message = "删除数据失败！";
                }
            }
            return _respond;
        }
    }
}
