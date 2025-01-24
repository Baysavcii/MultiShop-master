using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;

        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto createCouponDto)
        {
            string query = "insert into Coupons (Code,Rate,IsActive,ValidDate) values(@code,@rate,@isActive,@validDate)";
            var parameters = new DynamicParameters() ;
            parameters.Add("@code",createCouponDto.Code) ;
            parameters.Add("@rate",createCouponDto.Rate) ;
            parameters.Add("@isActive", createCouponDto.IsActive) ;
            parameters.Add("@validDate", createCouponDto.ValidDate) ;
            using (var connection = _context.CreateConnection()) 
            {
                await connection.ExecuteAsync(query,parameters);
            }
        }

        public async Task DeleteDiscountCouponAsync(int id)
        {
            string query = "Delete From Coupons where CouponId=@couponId";
            var parameters = new DynamicParameters() ;
            parameters.Add("couponId",id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);
            }
        }

        public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponsAsync()
        {
            string query = "Select * From Coupons";
            using (var connection = _context.CreateConnection()) 
            {
                var values = await connection.QueryAsync<ResultDiscountCouponDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int id)
        {
            string query = "Select * From Coupons Where CouponId=@couponId";
            var parameters = new DynamicParameters() ;
            parameters.Add("couponId",id) ;
            using (var connection = _context.CreateConnection()) 
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query,parameters);
                return values;

            }
        }

        public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateouponDto)
        {
            string query = "Update Coupons Set Code=@code,Rate=@rate,IsActive=@isActive,ValidDate=@validDate where CouponId=@couponId";
            var parameters = new DynamicParameters();
            parameters.Add("@code", updateouponDto.Code);
            parameters.Add("@rate", updateouponDto.Rate);
            parameters.Add("@isActive", updateouponDto.IsActive);
            parameters.Add("@validDate", updateouponDto.ValidDate);
            parameters.Add("@couponId", updateouponDto.CouponId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
