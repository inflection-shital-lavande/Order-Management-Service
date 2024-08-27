using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.interfaces;
using Order_Management.src.database.dto.payment_transaction;
using Order_Management.src.services.interfaces;

namespace Order_Management.src.api.payment_transaction;


    public class Payment_Transaction_Controller
    {
        public Payment_Transaction_Controller()
        {

        }

        [ProducesResponseType(200, Type = typeof(IEnumerable<PaymentTransaction>))]

        public async Task<IResult> GetAll(HttpContext httpContext, IPaymentTransactionService _paymentTransactionService)
        {
            try
            {
                var paymentTransactions = await _paymentTransactionService.GetAll();
                return ApiResponse.Success("Success", "paymentTransactions retrieved successfully", paymentTransactions);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving paymentTransactions");
            }
        }
        public async Task<IResult> GetById(Guid id, HttpContext httpContext, IPaymentTransactionService _paymentTransactionService)
        {
            try
            {
                var paymentTransactions = await _paymentTransactionService.GetById(id);
                return paymentTransactions == null ? ApiResponse.NotFound("Failure", "paymentTransactions not found")
                                       : ApiResponse.Success("Success", "paymentTransactions retrieved successfully", paymentTransactions);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the paymentTransactions");
            }
        }
        public async Task<IResult> Create(PaymentTransactionCreateModel paymentTransactions, HttpContext httpContext, IPaymentTransactionService _paymentTransactionService, IValidator<PaymentTransactionCreateModel> _createValidator)
        {
            try
            {
                if (paymentTransactions == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid paymentTransactions data");
                }

                var validationResult = _createValidator.Validate(paymentTransactions);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var createdPaymentTransaction = await _paymentTransactionService.Create(paymentTransactions);
                return ApiResponse.Success("Success", "paymentTransactions created successfully", createdPaymentTransaction);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the paymentTransactions");
            }
        }
       
        public async Task<IResult> Delete(Guid id, HttpContext httpContext, IPaymentTransactionService _paymentTransactionService)
        {
            try
            {
                var success = await _paymentTransactionService.Delete(id);
                return success ? ApiResponse.Success("Success", "paymentTransactions deleted successfully")
                               : ApiResponse.NotFound("Failure", "paymentTransactions not found");
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the paymentTransactions");
            }
        }

        public async Task<IResult> Search(string? InvoiceNumber,
                                                 
                                                  HttpContext httpContext, IPaymentTransactionService _paymentTransactionService)
        {
            try
            {
                var filter = new PaymentTransactionSearchFilter
                {
                    InvoiceNumber = InvoiceNumber,
                    
                };

                var paymentTransactions = await _paymentTransactionService.Search(filter);
                return paymentTransactions.Items.Any()
                    ? ApiResponse.Success("Success", "paymentTransactions retrieved successfully with filters", paymentTransactions)
                    : ApiResponse.NotFound("Failure", "No paymentTransactions found matching the filters");
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for paymentTransactions");
            }
        }
    }