using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderNow.Services.CouponAPI.Data;
using OrderNow.Services.CouponAPI.Models;
using OrderNow.Services.CouponAPI.Models.DTO;

namespace OrderNow.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private readonly ResponseDTO _response;

        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDTO();
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(coupons);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.FirstOrDefault(i => i.CouponId == id);
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{couponCode}")]
        public object GetByCode(string couponCode)
        {
            try
            {
                Coupon coupon = _db.Coupons.FirstOrDefault(i => i.CouponCode.ToLower() == couponCode.ToLower());
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public object Post([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDTO);

                _db.Coupons.Add(coupon);

                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public object Put([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDTO);

                _db.Coupons.FirstOrDefault(i => i.CouponId == coupon.CouponId);
                if (coupon != null)
                {
                    _db.Coupons.Update(coupon);
                    _db.SaveChanges();
                }

                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("{id:int}")]
        public object Delete(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.FirstOrDefault(i => i.CouponId == id);
                if (coupon != null)
                {
                    _db.Coupons.Remove(coupon);
                    _db.SaveChanges();
                }

                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
