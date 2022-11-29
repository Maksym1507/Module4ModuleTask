using Module4ModuleTask.Models;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask
{
    public class App
    {
        private readonly ICategoryService _categoryService;
        private readonly ICustomerService _customerService;
        private readonly IPaymentService _paymentService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IShipperService _shipperService;
        private readonly ISupplierService _supplierService;

        public App(
            ICategoryService categoryService,
            ICustomerService customerService,
            IPaymentService paymentService,
            IProductService productService,
            IOrderService orderService,
            IShipperService shipperService,
            ISupplierService supplierService)
        {
            _categoryService = categoryService;
            _customerService = customerService;
            _paymentService = paymentService;
            _productService = productService;
            _orderService = orderService;
            _shipperService = shipperService;
            _supplierService = supplierService;
        }

        public async Task Start()
        {
            var category1Id = await _categoryService.AddCategoryAsync(new Category()
            {
                Name = "pizza",
                Description = "pizza with mushrooms",
                Active = true
            });

            var category2Id = await _categoryService.AddCategoryAsync(new Category()
            {
                Name = "roll",
                Description = "baked rolls",
                Active = true
            });

            var category1 = await _categoryService.GetCategoryAsync(category1Id);
            var category2 = await _categoryService.GetCategoryAsync(category2Id);

            var customer1Id = await _customerService.AddCustomerAsync(new Customer()
            {
                FirstName = "Oleg",
                LastName = "Vlasov",
                Class = 1,
                Room = 12,
                Building = "Dafi",
                Address = "Mira street, 21",
                City = "Kharkov",
                PostalCode = 28300,
                Country = "Ukraine",
                Phone = "380969275733",
                Email = "customer1@gmail.com",
                Password = "qwerty1234"
            });

            var customer2Id = await _customerService.AddCustomerAsync(new Customer()
            {
                FirstName = "Anton",
                LastName = "Petrov",
                Class = 2,
                Room = 10,
                Building = "Building2",
                Address = "Street 5",
                City = "London",
                PostalCode = 28300,
                Country = "Great Britain",
                Phone = "380969203480",
                Email = "customer2@gmail.com",
                Password = "asdfg2368"
            });

            var customer1 = await _customerService.GetCustomerAsync(customer1Id);
            var customer2 = await _customerService.GetCustomerAsync(customer2Id);

            var payment1Id = await _paymentService.AddPaymentAsync(new Payment()
            {
                PaymentType = PaymentType.Cash,
                Allowed = true,
            });

            var payment2Id = await _paymentService.AddPaymentAsync(new Payment()
            {
                PaymentType = PaymentType.CreditCard,
                Allowed = true,
            });

            var payment1 = await _paymentService.GetPaymentAsync(payment1Id);
            var payment2 = await _paymentService.GetPaymentAsync(payment2Id);

            var shipper1Id = await _shipperService.AddShippertAsync(new Shipper()
            {
                Name = "Vlad",
                Phone = "0634237015"
            });

            var shipper2Id = await _shipperService.AddShippertAsync(new Shipper()
            {
                Name = "Dima",
                Phone = "0893694622"
            });

            var shipper1 = await _shipperService.GetShipperAsync(shipper1Id);
            var shipper2 = await _shipperService.GetShipperAsync(shipper2Id);

            var supplier1Id = await _supplierService.AddSupplierAsync(new Supplier()
            {
                CompanyName = "ALevel",
                Address = "Street 1, 20",
                City = "Kharkov",
                PostalCode = 61034,
                Country = "Ukraine",
                Phone = "380956218312",
                Email = "alevel@gmail.com",
                DiscountType = DiscountType.FivePersent,
                DiscountAvailable = true
            });

            var supplier2Id = await _supplierService.AddSupplierAsync(new Supplier()
            {
                CompanyName = "Company2",
                Address = "Street 2",
                City = "Kyiv",
                PostalCode = 61034,
                Country = "Ukraine",
                Phone = "380347246493",
                Email = "company2gmail.com",
                DiscountType = DiscountType.TwentyPercent,
                DiscountAvailable = true
            });

            var supplier1 = await _supplierService.GetSupplierAsync(supplier1Id);
            var supplier2 = await _supplierService.GetSupplierAsync(supplier2Id);

            var product1Id = await _productService.AddProductAsync(new Product()
            {
                SKU = 91478234,
                Name = "Pizza Boom",
                Description = "spicy pizza with mushrooms",
                SupplierId = supplier2.Id,
                CategoryId = category1.Id,
                QuantityPerUnit = 10,
                UnitPrice = 137,
                Size = 30,
                Color = "yellow",
                Discount = DiscountType.None,
                Picture = "https://roll-club.kh.ua/wp-content/uploads/2022/09/134-vilnii-165.jpg.webp",
                Ranking = 7,
                Note = "spicy",
            });

            var product2Id = await _productService.AddProductAsync(new Product()
            {
                SKU = 54302174,
                Name = "Pizza Musch",
                Description = "salty pizza with cheese",
                SupplierId = supplier1.Id,
                CategoryId = category2.Id,
                QuantityPerUnit = 12,
                UnitPrice = 123,
                Size = 25,
                Color = "yellow",
                Discount = DiscountType.None,
                Picture = "https://roll-club.kh.ua/wp-content/uploads/2022/09/134-vilnii-165.jpg.webp",
                Ranking = 9,
                Note = "salty",
            });

            var product3Id = await _productService.AddProductAsync(new Product()
            {
                SKU = 34598278,
                Name = "Roll1",
                Description = "Roll with sauce",
                SupplierId = supplier2.Id,
                CategoryId = category2.Id,
                QuantityPerUnit = 7,
                UnitPrice = 197,
                Size = 35,
                Color = "black",
                Discount = DiscountType.TenPercent,
                Picture = "https://roll-club.kh.ua/wp-content/uploads/2022/09/134-vilnii-165.jpg.webp",
                Ranking = 10,
                Note = "tasty",
            });

            var product1 = await _productService.GetProductAsync(product1Id);
            var product2 = await _productService.GetProductAsync(product2Id);
            var product3 = await _productService.GetProductAsync(product3Id);

            int order1Id = await _orderService.AddOrderAsync(
                new Order()
                {
                    CustomerId = customer1.Id,
                    PaymentId = payment1!.Id,
                    RequiredDate = DateTime.UtcNow.AddDays(12),
                    ShipperId = shipper2.Id,
                    SalesTax = 12,
                    TransactStatus = TransactStatus.Declined
                },
                new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        ProductId = product1.Id,
                        Quantity = 2,
                    },
                    new OrderItem()
                    {
                        ProductId = product3.Id,
                        Quantity = 1,
                    }
                });

            int order2Id = await _orderService.AddOrderAsync(
                new Order()
                {
                    CustomerId = customer2.Id,
                    PaymentId = payment2!.Id,
                    RequiredDate = DateTime.UtcNow.AddDays(5),
                    ShipperId = shipper1.Id,
                    SalesTax = 32,
                    TransactStatus = TransactStatus.Declined
                },
                new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        ProductId = product3.Id,
                        Quantity = 5,
                    },
                    new OrderItem()
                    {
                        ProductId = product1.Id,
                        Quantity = 2,
                    },
                    new OrderItem()
                    {
                        ProductId = product2.Id,
                        Quantity = 7,
                    }
                });

            var order1 = await _orderService.GetOrderAsync(order1Id);
            var order2 = await _orderService.GetOrderAsync(order2Id);

            var productsByCategory = await _productService.GetProductsByCategoryIdAsync(category2.Id);
            var productsByCategoryAndSupplierId = await _productService.GetProductsByCategoryIdAndSupplierIdAsync(category1.Id, supplier2.Id);
            var customerWithOrders = await _customerService.GetCustomerWithOrdersAsync(customer2.Id);
            var supplierWithProducts = await _supplierService.GetSupplierWithProductsAsync(supplier2.Id);
            var ordersByShipperAndPaymentId = await _orderService.GetOrdersByShipperIdAndPaymentIdAsync(shipper2.Id, payment1.Id);
            var shipperWithOrders = await _shipperService.GetShipperWithOrdersAsync(shipper2.Id);

            category1.Description = "pizza with meat";
            var isUpdatedCategoryById = await _categoryService.UpdateCategoryAsync(category1.Id, category1);

            shipper1!.Name = "Antonio";
            shipper1.Phone = "0634237016";
            var isUpdatedShipperById = await _shipperService.UpdateShipperAsync(shipper1.Id, shipper1);

            payment1!.PaymentType = PaymentType.CreditCard;
            payment1.Allowed = false;
            var isUpdatedPaymentById = await _paymentService.UpdatePaymentAsync(payment1.Id, payment1);

            customer1.City = "Dnepr";
            var isUpdatedCustomerById = await _customerService.UpdateCustomerAsync(customer1.Id, customer1);

            supplier1!.PostalCode = 61834;
            supplier1.Email = "alevel1@gmail.com";
            var isUpdatedSupplierById = await _supplierService.UpdateSupplierAsync(supplier1.Id, supplier1);

            product2.Discount = DiscountType.None;
            product1.Color = "Green";
            var isUpdatedProduct1ById = await _productService.UpdateProductAsync(product1.Id, product1);
            var isUpdatedProduct2ById = await _productService.UpdateProductAsync(product2.Id, product2);

            order1.ShipDate = DateTime.UtcNow.AddDays(4);
            order1.TransactStatus = TransactStatus.Approved;
            order1.Paid = true;
            order1.PaymentDate = DateTime.UtcNow;
            var isUpdatedOrderById = await _orderService.UpdateOrderAsync(order1.Id, order1);

            var isDeletedCategoryById = await _categoryService.DeleteCategoryAsync(category1.Id);
            var isDeletedCustomerById = await _customerService.DeleteCustomerAsync(customer2.Id);
            var isDeletedPaymentById = await _paymentService.DeletePaymentAsync(payment1.Id);
            var isDeletedShipperById = await _shipperService.DeleteShipperAsync(shipper2.Id);
            var isDeletedSupplierById = await _supplierService.DeleteSupplierAsync(supplier1.Id);
            var isDeletedProductById = await _productService.DeleteProductAsync(product1.Id);
            var isDeletedOrderById = await _orderService.DeleteOrderAsync(order1.Id);

            var isDeletedProductByCategoryId = await _productService.DeleteProductsByCategoryIdAsync(category2.Id);
        }
    }
}
