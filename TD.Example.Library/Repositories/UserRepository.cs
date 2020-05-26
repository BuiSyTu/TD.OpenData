using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Example.Library.Data;
using TD.Example.Library.Models;
using TD.Example.Library.ViewModels;

namespace TD.Example.Library.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

        User GetByName(string NameUser);
        User Get(User entity);
        object CustomServerSideSearchAction(DataTableAjaxPostModel model);

    }

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private UserContext _dbContext;
        public UserRepository(UserContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override IEnumerable<string> FreetextFields
        {
            get
            {
                return new string[] {
                   nameof(User.Name)
               };
            }
        }
        public override User GetById(int id)
        {
            var item = _dbContext.Users.FirstOrDefault(a => a.ID == id);
            return item;
        }

        public User GetByName(string NameUser)
        {
            var item = _dbContext.Users.FirstOrDefault(a => a.Name == NameUser);
            return item;
        }

        public User Get(User entity)
        {
            var item = _dbContext.Users.FirstOrDefault(i => i.ID == entity.ID);
            return item;
        }

        public object CustomServerSideSearchAction(DataTableAjaxPostModel model)
        {
            try
            {
                IQueryable<User> UsersQuery = _dbContext.Users.AsQueryable();
                var searchValue = model.search.value;
                if (!string.IsNullOrEmpty(searchValue))
                {
                    UsersQuery = UsersQuery.Where(t => t.Name.Contains(searchValue));
                }
                #region Sắp xếp
                IOrderedQueryable<User> UsersOrder = null;
                var order = model.order.Count;
                if (order > 0)
                {
                    for (int i = 0; i < order; i++)
                    {
                        var indexCol = model.order[i].column;
                        var dir = model.order[i].dir;
                        var colName = model.columns[indexCol].data;
                        colName = Common.UppercaseFirst(colName); //Viết hoa lại chữ cái đầu
                        if (!string.IsNullOrEmpty(colName))
                        {
                            if (i == 0)
                            {
                                if (dir == "asc")
                                    UsersOrder = UsersQuery.OrderBy(x => x.GetType().GetProperty(colName).GetValue(x, null));
                                else
                                    UsersOrder = UsersQuery.OrderByDescending(x => x.GetType().GetProperty(colName).GetValue(x, null));
                            }
                            else
                            {
                                if (dir == "asc")
                                    UsersOrder = UsersOrder.ThenBy(x => x.GetType().GetProperty(colName).GetValue(x, null));
                                else
                                    UsersOrder = UsersOrder.ThenByDescending(x => x.GetType().GetProperty(colName).GetValue(x, null));
                            }
                        }
                        else
                            UsersQuery = UsersQuery.OrderByDescending(x => x.ID);
                    }
                    if (UsersOrder != null)
                        UsersQuery = UsersOrder;
                }
                else
                    UsersQuery = UsersQuery.OrderByDescending(x => x.ID);
                #endregion

                var lstUser = UsersQuery
                    .Skip(model.start) //Bỏ qua số dữ liệu
                    .Take(model.length) //Số dữ liệu lấy ra
                    .ToList();
                // Tổng số dữ liệu lấy ra khi đã truy vấn (tìm kiếm) và chưa phân trang
                int filteredResultsCount = UsersQuery.Count();
                // Tổng số dữ liệu trong database
                int totalResultsCount = _dbContext.Users.Count();
                return new ObjectReturn
                {
                    draw = model.draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = lstUser
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public override void Update(int id, User change)
        {
            var item = GetById(id);
            if (item != null)
            {
                item.Name = change.Name;
                item.Code = change.Code;
                item.Address = change.Address;
                item.Phone = change.Phone;

                item.ModifiedBy = change.ModifiedBy;
                item.Modified = DateTime.Now;
                base.Update(id, item);
            }
        }

    }

}


