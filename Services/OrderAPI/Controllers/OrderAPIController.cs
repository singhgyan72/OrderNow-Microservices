using AutoMapper;
using OrderNow.Services.OrderAPI.Data;
using OrderNow.Services.OrderAPI.Models;
using OrderNow.Services.OrderAPI.Models.DTO;
using OrderNow.Services.OrderAPI.Utility;
using OrderNow.Services.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Stripe.Checkout;
//using Stripe;
//using OrderNow.MessageBus;
using Microsoft.EntityFrameworkCore;

namespace OrderNow.Services.OrderAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        protected ResponseDTO _response;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private IProductService _productService;
        //private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;
        public OrderAPIController(AppDbContext db,
            IProductService productService, IMapper mapper, IConfiguration configuration)
           // ,IMessageBus messageBus)
        {
            _db = db;
           // _messageBus = messageBus;
            this._response = new ResponseDTO();
            _productService = productService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("GetOrders")]
        public ResponseDTO? Get(string? userId = "")
        {
            try
            {
                IEnumerable<OrderHeader> objList;
                if (User.IsInRole(Helpers.RoleAdmin))
                {
                    objList = _db.OrderHeaders.Include(u => u.OrderDetails).OrderByDescending(u => u.OrderHeaderId).ToList();
                }
                else
                {
                    objList = _db.OrderHeaders.Include(u => u.OrderDetails).Where(u=>u.UserId==userId).OrderByDescending(u => u.OrderHeaderId).ToList();
                }
                _response.Result = _mapper.Map<IEnumerable<OrderHeaderDTO>>(objList);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Authorize]
        [HttpGet("GetOrder/{id:int}")]
        public ResponseDTO? Get(int id)
        {
            try
            {
                OrderHeader orderHeader = _db.OrderHeaders.Include(u => u.OrderDetails).First(u => u.OrderHeaderId == id);
                _response.Result = _mapper.Map<OrderHeaderDTO>(orderHeader);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }



        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ResponseDTO> CreateOrder([FromBody] CartDTO cartDTO)
        {
            try
            {
                OrderHeaderDTO orderHeaderDTO = _mapper.Map<OrderHeaderDTO>(cartDTO.CartHeader);
                orderHeaderDTO.OrderTime = DateTime.Now;
                orderHeaderDTO.Status = Helpers.Status_Pending;
                orderHeaderDTO.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDTO>>(cartDTO.CartDetails);
                orderHeaderDTO.OrderTotal = Math.Round(orderHeaderDTO.OrderTotal, 2);
                OrderHeader orderCreated = _db.OrderHeaders.Add(_mapper.Map<OrderHeader>(orderHeaderDTO)).Entity;
                await _db.SaveChangesAsync();

                orderHeaderDTO.OrderHeaderId = orderCreated.OrderHeaderId;
                _response.Result = orderHeaderDTO;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message=ex.Message;
            }
            return _response;
        }


        //[Authorize]
        //[HttpPost("CreateStripeSession")]
        //public async Task<ResponseDTO> CreateStripeSession([FromBody] StripeRequestDTO stripeRequestDTO)
        //{
        //    try
        //    {
                
        //        var options = new SessionCreateOptions
        //        {
        //            SuccessUrl = stripeRequestDTO.ApprovedUrl,
        //            CancelUrl = stripeRequestDTO.CancelUrl,
        //            LineItems = new List<SessionLineItemOptions>(),                     
        //            Mode = "payment",
                    
        //        };

        //        var DiscountsObj = new List<SessionDiscountOptions>()
        //        {
        //            new SessionDiscountOptions
        //            {
        //                Coupon=stripeRequestDTO.OrderHeader.CouponCode
        //            }
        //        };

        //        foreach (var item in stripeRequestDTO.OrderHeader.OrderDetails)
        //        {
        //            var sessionLineItem = new SessionLineItemOptions
        //            {
        //                PriceData = new SessionLineItemPriceDataOptions
        //                {
        //                    UnitAmount = (long)(item.Price * 100), // $20.99 -> 2099
        //                    Currency = "usd",
        //                    ProductData = new SessionLineItemPriceDataProductDataOptions
        //                    {
        //                        Name = item.Product.Name
        //                    }
        //                },
        //                Quantity = item.Count
        //            };

        //            options.LineItems.Add(sessionLineItem);
        //        }

        //        if (stripeRequestDTO.OrderHeader.Discount > 0)
        //        {
        //            options.Discounts = DiscountsObj;
        //        }
        //        var service = new SessionService();
        //        Session session = service.Create(options);
        //        stripeRequestDTO.StripeSessionUrl = session.Url;
        //        OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == stripeRequestDTO.OrderHeader.OrderHeaderId);
        //        orderHeader.StripeSessionId = session.Id;
        //        _db.SaveChanges();
        //        _response.Result = stripeRequestDTO;

        //    }
        //    catch(Exception ex)
        //    {
        //        _response.Message= ex.Message;
        //        _response.IsSuccess = false;
        //    }
        //    return _response;
        //}


        //[Authorize]
        //[HttpPost("ValidateStripeSession")]
        //public async Task<ResponseDTO> ValidateStripeSession([FromBody] int orderHeaderId)
        //{
        //    try
        //    {

        //        OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == orderHeaderId);

        //        var service = new SessionService();
        //        Session session = service.Get(orderHeader.StripeSessionId);

        //        var paymentIntentService = new PaymentIntentService();
        //        PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

        //        if(paymentIntent.Status== "succeeded")
        //        {
        //            //then payment was successful
        //            orderHeader.PaymentIntentId = paymentIntent.Id;
        //            orderHeader.Status = Helpers.Status_Approved;
        //            _db.SaveChanges();
        //            RewardsDTO rewardsDTO = new()
        //            {
        //                OrderId = orderHeader.OrderHeaderId,
        //                RewardsActivity = Convert.ToInt32(orderHeader.OrderTotal),
        //                UserId = orderHeader.UserId
        //            };
        //            string topicName = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreateDTOpic");
        //            await _messageBus.PublishMessage(rewardsDTO,topicName);
        //            _response.Result = _mapper.Map<OrderHeaderDTO>(orderHeader);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _response.Message = ex.Message;
        //        _response.IsSuccess = false;
        //    }
        //    return _response;
        //}


        //[Authorize]
        //[HttpPost("UpdateOrderStatus/{orderId:int}")]
        //public async Task<ResponseDTO> UpdateOrderStatus(int orderId, [FromBody] string newStatus)
        //{
        //    try
        //    {
        //        OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == orderId);
        //        if (orderHeader != null)
        //        {
        //            if(newStatus == Helpers.Status_Cancelled)
        //            {
        //                //we will give refund
        //                var options = new RefundCreateOptions
        //                {
        //                    Reason = RefundReasons.RequestedByCustomer,
        //                    PaymentIntent = orderHeader.PaymentIntentId
        //                };

        //                var service = new RefundService();
        //                Refund refund = service.Create(options);
        //            }
        //            orderHeader.Status = newStatus;
        //            _db.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //    }
        //    return _response;
        //}
    }
}
